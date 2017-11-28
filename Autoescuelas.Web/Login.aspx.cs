#region usings

using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Autoescuelas.Dal.Entidades;
using Autoescuelas.PgConn;
using Autoescuelas.Web.App_Class;

#endregion

namespace Autoescuelas.Web
{
    public partial class Login : Page
    {
        public string GetBrowser()
        {
            try
            {
                var browser = Request.Browser;
                return "Tipo = " + browser.Type + "\n"
                       + "Nombre = " + browser.Browser + "\n"
                       + "Version = " + browser.Version + "\n"
                       + "Major Version = " + browser.MajorVersion + "\n"
                       + "Minor Version = " + browser.MinorVersion + "\n"
                       + "Platform = " + browser.Platform + "\n"
                       + "Is Beta = " + browser.Beta + "\n"
                       + "Is Crawler = " + browser.Crawler + "\n"
                       + "Is AOL = " + browser.AOL + "\n"
                       + "Is Win16 = " + browser.Win16 + "\n"
                       + "Is Win32 = " + browser.Win32 + "\n"
                       + "Supports Frames = " + browser.Frames + "\n"
                       + "Supports Tables = " + browser.Tables + "\n"
                       + "Supports Cookies = " + browser.Cookies + "\n"
                       + "Supports VBScript = " + browser.VBScript + "\n"
                       + "Supports JavaScript = " + browser.EcmaScriptVersion + "\n"
                       + "Supports Java Applets = " + browser.JavaApplets + "\n"
                       + "Supports ActiveX Controls = " + browser.ActiveXControls;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetIp()
        {
            //KB: http://stackoverflow.com/questions/735350/how-to-get-a-users-client-ip-address-in-asp-net
            var context = HttpContext.Current;
            var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                var addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                    return addresses[0];
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                HttpContext.Current.Session["USERBD"] = null;
                HttpContext.Current.Session["PASSBD"] = null;

                var bProcede = false;
                var strTextoValidacion = "";

                var miUsuario = new EntUsuArio();
                
                //Validacion INTEGRATE
                //Cambiamos el esquema de la session
                

                if (CLogin.WebValidarUsuario(ref strTextoValidacion, ref txtUsuario, ref txtPass) &&
                    CLogin.AutenticarUsuario(ref strTextoValidacion, txtUsuario.Text, txtPass.Text, ref miUsuario))
                {

                    var miSesion = new CSessionHandler();
                    miSesion.AppUsuario = miUsuario;
                    
                    var minutosSession = 120000;
                    var sKey = txtUsuario.Text;
                    
                    HttpContext.Current.Cache.Insert(sKey, sKey);

                    // Se crea el ticket de autenticación
                    var ticket = new FormsAuthenticationTicket(2, //Version del Ticket
                        txtUsuario.Text, //ID de usuario asociado al ticket
                        DateTime.Now, //fecha de creacion del ticket
                        DateTime.Now.AddMinutes(minutosSession), //expiracion de la cookie
                        false, // Si el usuario cliquó en "Recuérdame" la cookie no expira.
                        miUsuario.id_rol.ToString(), // Almacena datos del usuario, en este caso los roles
                        FormsAuthentication.FormsCookiePath); // El path de la cookie especificado en el Web.Config

                    // Se encripta el ticket para añadir más seguridad
                    var cticket = FormsAuthentication.Encrypt(ticket);

                    //Creamos una cookie con el ticket encriptado
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cticket);

                    cookie.Path = FormsAuthentication.FormsCookiePath;
                    //Adicionamos la cookie creada al cliente
                    Response.Cookies.Add(cookie);

                    //Culture es-BO
                    Session.LCID = 16394;

                    //La url de Redireccion

                    //Cargamos el Menu
                    var menu = new CSegRolPagina();
                    var dtMenu = menu.ObtenerMenu();                    
                    miSesion.DtMenu = dtMenu;


                    //Limpiamos el error
                    pnlError.Visible = false;
                    lblError.Text = "";

                    bProcede = true;
                }
                else
                {
                    pnlError.Visible = true;
                    lblError.Text = strTextoValidacion;
                }

                if (bProcede)
                    Response.Redirect("~/Template/Dashboard.aspx");
            }
            catch (Exception exp)
            {
                pnlError.Visible = true;
                lblError.Text = exp.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                //Titulo de Pagina
                Title = CParametrosWeb.StrPantallaInicio;
                lblTitulo.Text = CParametrosWeb.StrPantallaInicio;
                txtUsuario.Focus();

                //Version del Sistema
                lblVersion.Text = String.Format("Version: {0} - De fecha: {1}",
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
                        .ToString(CParametros.ParFormatoFechaHora).Replace("/",".").Replace(":",".").Replace(" ","-"));
            }
        }
    }
}