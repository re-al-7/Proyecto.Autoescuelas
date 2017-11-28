#region usings

using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Autoescuelas.Dal.Modelo;
using Autoescuelas.Web.App_Class;
using DocumentFormat.OpenXml.Bibliography;
using Integrate.SisPlan.Dal.Entidades;
using Npgsql;

#endregion

namespace Autoescuelas.Web
{
    /// <summary>
    /// MasterPage Main
    /// </summary>
    public partial class Main : MasterPage
    {
        protected string NavColor = "";
        protected string NavColorFont = "#FFFFFF";
        private string _strImageLog = "";
        private string _errorCaptureImage = "";

        /// <summary>
        /// Variable que almacena el mensaje de ERROR que se muestra en la pagina maestra
        /// </summary>
        public string Localerror
        {
            get
            {
                if (Request["error"] == null)
                    return "";
                return Request["error"];
            }
        }

        /// <summary>
        /// Modulo actual
        /// </summary>
        public string LocalModulo
        {
            get
            {
                if (Request["mod"] == null)
                    return "";
                return Request["mod"];
            }
        }

        /// <summary>
        /// Variable que almacena el mensaje que se muestran en la pagina maestra
        /// </summary>
        public string Localmsg
        {
            get
            {
                if (Request["msg"] == null)
                    return "";
                return Request["msg"];
            }
        }

        /// <summary>
        /// Funcion para mostrar el error en una ventana modal a partir de una excepcion
        /// </summary>
        /// <param name="anfitrion">Objeto padre de la ventana modal</param>
        /// <param name="exp">Excepcion a ser mostrada</param>
        /// <param name="tipo">Tipo de la excepcion</param>
        public void MostrarPopUp(Page anfitrion, Exception exp)
        {
            try
            {
                CSessionHandler miSession = new CSessionHandler();
                miSession.CurrentException = exp;
                divErrorDetalle.Visible = true;
                lblErrorMensaje.Text = "";
                lblErrorDescripcion.Text = "";
                lblErrorTitulo.Text = "Error";

                if (exp is CSimpleException)
                {
                    lblErrorMensaje.Text = exp.Message;
                    lblErrorDescripcion.Text = "";
                    divError.Attributes.Add("class", "modal-header modal-header-warning");
                    divErrorDetalle.Visible = false;
                }
                else if (exp is NpgsqlException)
                {
                    var sqlex = (NpgsqlException) exp;
                    lblErrorMensaje.Text = sqlex.Message;
                    lblErrorDescripcion.Text = sqlex.Detail;
                }
                else
                {
                    if (exp.Message == "El reporte no ha devuelto resultados")
                    {
                        lblErrorTitulo.Text = "Alerta";
                        lblErrorMensaje.Text = exp.Message;
                        lblErrorDescripcion.Text =
                            "<br/><strong>Descripción:</strong> Revise los parametros del reporte para obtener resultados";
                    }
                    else if (exp.Message == "Referencia a objeto no establecida como instancia de un objeto.")
                    {
                        lblErrorTitulo.Text = "Alerta";
                        lblErrorMensaje.Text = exp.Message;
                        lblErrorDescripcion.Text =
                            "<br/><strong>Descripción:</strong> Es posible que se intente acceder a un objeto no inicializado.";                        
                    }
                    else
                    {                            
                        lblErrorMensaje.Text = "No se pudo realizar la transacción. Revise el detalle adjunto";
                        lblErrorDescripcion.Text = exp.Message;
                            
                    }
                }
                

                ScriptManager.RegisterStartupScript(anfitrion, anfitrion.GetType(), "myModalError",
                    "$('#myModalError').appendTo('body').modal('show');", true);
            }
            catch (Exception e2)
            {
                Debug.Print(e2.Message);
            }
        }

        protected void btnRecargar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void lnkCambiarPass_Click(object sender, EventArgs e)
        {
            var miSesion = new CSessionHandler();
            miSesion.StrModuloActivo = "SEG";
            Response.Redirect("~/SEG/pagSegCambiarPassword.aspx");
        }

        protected void lnkCerrar_Click(object sender, EventArgs e)
        {
            var miSesion = new CSessionHandler();
            miSesion.SessionClear();
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void lnkUserProfile_Click(object sender, EventArgs e)
        {
            var miSesion = new CSessionHandler();
            miSesion.StrModuloActivo = "SEG";
            var strUrl = "~/SEG/lstSegUsuariosRestriccion.aspx";
            Response.Redirect(strUrl);
        }

        protected void LoginStatus_LoggedOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Tratamos de Obtener el Proyecto del Usuario
            try
            {
                var miSesion = new CSessionHandler();
                lblUsuario.Text = "<b>Bienvenido </b>" + miSesion.AppUsuario.nombres + " " +
                                  miSesion.AppUsuario.apellidos;
                switch (miSesion.AppUsuario.id_rol)
                {
                    case 1:
                        lblRolActivo.Text = "<b>Rol: </b> Administrador";
                        break;

                    case 2:
                        lblRolActivo.Text = "<b>Rol: </b> Instructor";
                        break;
                }                

                if (!LocalModulo.IsNullOrEmpty())
                    miSesion.StrModuloActivo = LocalModulo;

                Page.Title = CParametrosWeb.StrNombrePagina;
                //lblTituloPagina.Text = CParametrosWeb.StrNombrePagina;

                CargarMenuVertical(miSesion.StrModuloActivo);

                if (!Page.IsPostBack)
                {
                    //Los mensajes de llegada
                    if (!Localmsg.IsNullOrEmpty())
                    {
                        pnlMsg.Visible = true;
                        lblmsg.Text = Localmsg;
                    }
                    if (!Localerror.IsNullOrEmpty())
                    {
                        pnlError.Visible = true;
                        lblError.Text = Localerror;
                    }
                }
            }
            catch (Exception expIgnored)
            {
                Debug.WriteLine(expIgnored.Message);
            }
        }

        /// <summary>
        /// Funcion que obtiene y arma el menu vertical del sistema en base al modulo activo
        /// </summary>
        /// <param name="strModulo">Modulo activo</param>
        private void CargarMenuVertical(string strModulo)
        {
            try
            {
                //Primero el PADRE ul
                var ulControlPadre = new HtmlGenericControl();
                ulControlPadre.Attributes["class"] = "nav";
                ulControlPadre.Attributes["id"] = "side-menu";
                ulControlPadre.TagName = "ul";
                divMenu.Controls.Add(ulControlPadre);
                
                //Ahora los Hijos filtrados por Modulo
                var dtTodoMenu = ObtenerMenu();
                var dvMenuSeccion = dtTodoMenu.DefaultView;
                var dtMenu = dvMenuSeccion.ToTable();

                var dtMenuNivel1 = ObtenerMenuPrimerNivel(dtMenu);
                foreach (DataRow drMenu in dtMenuNivel1.Rows)
                {
                    var idNivel1 = "menu_" + drMenu[EntSegPaginas.Fields.Paginaspg.ToString()];
                    var liControl = new HtmlGenericControl
                    {
                        TagName = "li",
                        ID = idNivel1
                    };

                    ulControlPadre.Controls.Add(liControl);

                    if (!drMenu[EntSegPaginas.Fields.Nombrespg.ToString()].ToString().Contains(".aspx"))
                    {
                        //Tiene hijos
                        var aControl = new HtmlGenericControl();
                        aControl.Attributes["href"] = "#";
                        aControl.Attributes["id_padre"] = idNivel1;
                        aControl.InnerHtml = "<i class='fa'></i> " +
                                             drMenu[EntSegPaginas.Fields.Nombremenuspg.ToString()] +
                                             " <span class='fa arrow'></span>";
                        aControl.TagName = "a";
                        liControl.Controls.Add(aControl);                        
                    }
                    else
                    {
                        //No tiene Hijos
                        var aControl = new HtmlGenericControl();
                        aControl.Attributes["href"] =
                            ResolveClientUrl(drMenu[EntSegPaginas.Fields.Aplicacionsap.ToString()] + "\\" +
                                             drMenu[EntSegPaginas.Fields.Nombrespg.ToString()]);
                        aControl.Attributes["id_padre"] = idNivel1;
                        aControl.Attributes["class"] = "rq_menu";
                        aControl.InnerHtml = "<i class='fa'></i> " +
                                             drMenu[EntSegPaginas.Fields.Nombremenuspg.ToString()] + "</a>";
                        aControl.TagName = "a";
                        liControl.Controls.Add(aControl);
                    }
                }
            }
            catch (Exception exp)
            {
                MostrarPopUp(Page, exp);
            }
        }
        
        
        /// <summary>
        /// Obtiene los elementos del menu vertical desde la base de datos
        /// </summary>
        /// <returns>DataTable con elementos del menu a partir del rol del usuario</returns>
        private DataTable ObtenerMenu()
        {
            var miSesion = new CSessionHandler();

            if (miSesion.DtMenu == null)
            {
                var cMenu = new CSegRolPagina();
                var dtMenu = cMenu.ObtenerMenu();

                return dtMenu;
            }
            return miSesion.DtMenu;
        }

        /// <summary>
        /// Realiza el filtro de los elementos del menu para devolver solo los elementos de primer nivel o elementos padre
        /// </summary>
        /// <param name="dtMenu">DataTable con los elementos del menu</param>
        /// <returns>DataTable con los elementos del primer nivel del menu</returns>
        private DataTable ObtenerMenuPrimerNivel(DataTable dtMenu)
        {
            var dvMenu = dtMenu.DefaultView;
            return dvMenu.ToTable();
        }
    }
}