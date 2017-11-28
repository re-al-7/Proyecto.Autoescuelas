// // <copyright file="abmSegUsuarios.aspx.cs" company="INTEGRATE - Soluciones Informaticas">
// // Copyright (c) 2016 Todos los derechos reservados
// // </copyright>
// // <author>ReAl </author>
// // <date>2016-10-05 11:59 a. m.</date>

#region usings

using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autoescuelas.Dal;
using Autoescuelas.Dal.Entidades;
using Autoescuelas.Dal.Modelo;
using Autoescuelas.PgConn;
using Autoescuelas.Web.App_Class;
using ClosedXML.Excel;
using static Autoescuelas.Web.App_Class.CParametrosWeb;

#endregion

namespace Autoescuelas.Web.AUT
{
    public partial class AbmUsuarios : Page
    {
        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Template/Dashboard.aspx");
        }

        protected void btnEditaGuardar_Click(object sender, EventArgs e)
        {
            Boolean bProcede = false;
            try
            {
                var miSession = new CSessionHandler();
                RnUsuArio rn = new RnUsuArio();
                EntUsuArio objPag = rn.ObtenerObjeto(int.Parse(txtEdit_Id.Text));

                if (objPag != null)
                {
                    objPag.id_rol = chkEdit_Administrador.Checked ? 1 : 2;
                    objPag.nombres = txtEdit_nombres.Text;
                    objPag.apellidos = txtEdit_apellidos.Text;
                    objPag.usumod = miSession.AppUsuario.login;
                    rn.Update(objPag);
                }

                bProcede = true;
            }
            catch (Exception exp)
            {
                var master = (Main) Master; master?.MostrarPopUp(this, exp);
            }

            if (bProcede)
                Response.Redirect(Request.AppRelativeCurrentExecutionFilePath);
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string strTitulo = "Usuarios del Sistema";
                CSessionHandler miSesion = new CSessionHandler();
                RnVista rn = new RnVista();
                DataTable dtVista = rn.ObtenerDatos("VW_" + EntUsuArio.StrAliasTabla.ToUpper() + "_REP");
                DataTable dtReporte = CControlHelper.DtAplicarRestriccion(dtVista);

                String strNombreReporte = StrNombreSistema + "-" + EntUsuArio.StrAliasTabla.ToUpper() +
                                          "-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm") + ".xlsx";
                string template = Server.MapPath("~/doc_templates/" + StrNombreInstitucion + "_Reporte.xlsx");

                using (XLWorkbook wb = new XLWorkbook(template))
                {
                    wb.Worksheets.Worksheet(1).Cell(5, 1).Value = strTitulo;
                    wb.Worksheets.Worksheet(1).Cell(6, 1).Value = "Elaborado por: " + miSesion.AppUsuario.nombres +
                                                                  " " + miSesion.AppUsuario.apellidos;
                    wb.Worksheets.Worksheet(1).Cell(7, 1).Value = "Fecha: " +
                                                                  DateTime.Now.ToString(CParametros.ParFormatoFechaHora);

                    wb.Worksheets.Worksheet(1).Cell(9, 1).InsertTable(dtReporte);
                    wb.Worksheets.Worksheet(1).Table("Table1").ShowAutoFilter = true;
                    wb.Worksheets.Worksheet(1).Table("Table1").Style.Alignment.Vertical =
                        XLAlignmentVerticalValues.Center;
                    wb.Worksheets.Worksheet(1).Columns(2, 2 + dtReporte.Columns.Count).AdjustToContents();

                    foreach (IXLColumn column in wb.Worksheets.Worksheet(1).Columns())
                        if (column.Width > 60)
                        {
                            column.Width = 60;
                            column.Style.Alignment.WrapText = true;
                        }

                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=\"" + strNombreReporte + "\"");

                    using (MemoryStream myMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(myMemoryStream);
                        myMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception exp)
            {
                var master = (Main) Master; master?.MostrarPopUp(this, exp);
            }
        }

        protected void btnNewGuardar_Click(object sender, EventArgs e)
        {
            Boolean bProcede = false;
            try
            {
                var miSession = new CSessionHandler();
                RnUsuArio rn = new RnUsuArio();
                EntUsuArio objPag = new EntUsuArio();
                objPag.login = txtNew_Login.Text;
                objPag.password = cFuncionesEncriptacion.generarMD5(txtNew_password.Text);
                objPag.id_rol = chkNew_Administrador.Checked ? 1 : 2;
                objPag.nombres = txtNew_nombres.Text;
                objPag.apellidos = txtNew_apellidos.Text;
                objPag.id_sucursal = int.Parse(ddlid_sucursal.SelectedValue);
                objPag.usucre = miSession.AppUsuario.login;

                rn.Insert(objPag);

                bProcede = true;
            }
            catch (Exception exp)
            {
                var master = (Main) Master; master?.MostrarPopUp(this, exp);
            }

            if (bProcede)
                Response.Redirect(Request.AppRelativeCurrentExecutionFilePath);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            chkNew_Administrador.Checked = false;            
            
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#newModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "NewModalScript", sb.ToString(), false);
        }

        protected void dtgListado_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CSessionHandler miSesion = new CSessionHandler();
                LinkButton imbDetalles =
                    (LinkButton)
                    e.Row.Cells[CControlHelper.GetColumnIndexByName(ref dtgListado, "Acciones")].FindControl("ImbVer");               
            }
        }

        protected void dtgListado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String strRedireccion = "";
            try
            {
                if (e.CommandName.Equals("detalles"))
                {
                    string strId = e.CommandArgument.ToString();

                    //Filtramos el Dataset
                    DataView dv = ((DataTable) dtgListado.DataSource).DefaultView;
                    dv.RowFilter =  EntUsuArio.Fields.id_usuario + " = " + strId;

                    DataTable detailTable = dv.ToTable();

                    dtgDetalles.DataSource = detailTable;
                    dtgDetalles.DataBind();
                    dtgDetalles.HeaderRow.TableSection = TableRowSection.TableHeader;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal",
                        "$('#currentdetail').appendTo('body').modal('show');", true);
                }
                else if (e.CommandName.Equals("restaurar"))
                {
                    var miSesion = new CSessionHandler();
                    string strId = e.CommandArgument.ToString();
                    RnUsuArio rn = new RnUsuArio();
                    EntUsuArio objUsuario = rn.ObtenerObjeto(int.Parse(strId));

                    if (objUsuario != null)
                    {
                        objUsuario.password = cFuncionesEncriptacion.generarMD5((objUsuario.login + "01").Trim()).ToUpper();
                        objUsuario.apiestado = CApi.Estado.ELABORADO.ToString();
                        objUsuario.usumod = miSesion.AppUsuario.login;

                        rn.Update(objUsuario);
                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "Exito",
                            "alert('Se ha reseteado su Password');window.location=\"../Template/Dashboard.aspx\";",
                            true);
                    }
                }
                else if (e.CommandName.Equals("modificar"))
                {
                    string strId = e.CommandArgument.ToString();

                    RnUsuArio rn = new RnUsuArio();
                    EntUsuArio objPag = rn.ObtenerObjeto(int.Parse(strId));

                    if (objPag != null)
                    {
                        txtEdit_Id.Text = strId;
                        chkEdit_Administrador.Checked = objPag.id_rol == 1;
                        txtEdit_nombres.Text = objPag.nombres ;
                        txtEdit_apellidos.Text = objPag.apellidos ;

                        StringBuilder sb = new StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#editModal').modal('show');");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "EditModalScript", sb.ToString(), false);
                    }
                }
                else if (e.CommandName.Equals("eliminar"))
                {
                    string strId = e.CommandArgument.ToString();
                    var miSession = new CSessionHandler();
                    RnUsuArio rn = new RnUsuArio();
                    EntUsuArio objPag = rn.ObtenerObjeto(int.Parse(strId));

                    if (objPag != null)
                    {
                        if (objPag.apiestado == CApi.Estado.ELABORADO.ToString())
                            objPag.apiestado = CApi.Estado.ELIMINADO.ToString();
                        else
                            objPag.apiestado = CApi.Estado.ELABORADO.ToString();

                        objPag.usumod = miSession.AppUsuario.login;
                        rn.Update(objPag);

                        strRedireccion = Request.AppRelativeCurrentExecutionFilePath;
                    }
                }
            }
            catch (Exception exp)
            {
                var master = (Main) Master; master?.MostrarPopUp(this, exp);
            }

            if (!strRedireccion.IsNullOrEmpty())
                Response.Redirect(strRedireccion);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Titulo de Pagina
                Title = StrNombrePagina;

                if (!Page.IsPostBack)
                {
                    CargarDdlAutoEscuelas();
                    CargarDdlSucursales();
                    CargarListado();
                }
                else
                {
                    CargarListado();
                    CControlHelper.CrearEstilosGrid(ref dtgListado);
                }
            }
            catch (Exception exp)
            {
                var master = (Main) Master; master?.MostrarPopUp(this, exp);
            }
        }

        /// <summary>
        /// Funcion que obtiene el DataTable del formulario
        /// </summary>
        /// <returns>DataTable con los datos obtenidos</returns>
        private DataTable CargarDataTable()
        {
            try
            {
                RnUsuArio rnListadoDatos = new RnUsuArio();
                ArrayList arrColumnas = new ArrayList();
                arrColumnas.Add("*");

                ArrayList arrColumnasWhere = new ArrayList();
                arrColumnasWhere.Add(EntUsuArio.Fields.id_sucursal.ToString());
                ArrayList arrValoresWhere = new ArrayList();
                arrValoresWhere.Add(ddlid_sucursal.SelectedValue);

                DataTable dtDatos = rnListadoDatos.ObtenerDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere,
                    " ORDER BY " + EntUsuArio.Fields.login);

                return dtDatos;
            }
            catch (Exception exp)
            {
                var master = (Main) Master; master?.MostrarPopUp(this, exp);
            }
            return null;
        }

        
        /// <summary>
        /// Funcion que se encarga de llenar los datos del Listado
        /// </summary>
        private void CargarListado()
        {
            try
            {
                DataTable dt = CargarDataTable();
                dtgListado.DataSource = dt;
                dtgListado.DataBind();

                CControlHelper.CrearEstilosGrid(ref dtgListado);
            }
            catch (Exception exp)
            {
                var master = (Main) Master; master?.MostrarPopUp(this, exp);
            }
        }

        /// <summary>
        /// Funcion que se encarga de llenar los datos de Autoescuelas
        /// </summary>
        private void CargarDdlAutoEscuelas()
        {
            try
            {
                var rn = new RnAutOescuela();
                var dt = rn.ObtenerDataTable();

                ddlid_autoescuela.DataValueField = EntAutOescuela.Fields.id_autoescuelas.ToString();
                ddlid_autoescuela.DataTextField = EntAutOescuela.Fields.nombre_autoescuela.ToString();
                ddlid_autoescuela.DataSource = dt;
                ddlid_autoescuela.DataBind();

                if (Localfiltro > 0 && ddlid_autoescuela.Items.Count > 0)
                    ddlid_autoescuela.SelectedValue = Localfiltro.ToString();
            }
            catch (Exception exp)
            {
                var master = (Main)Master; master?.MostrarPopUp(this, exp);
            }
        }

        /// <summary>
        /// Funcion que se encarga de llenar los datos de ...
        /// </summary>
        private void CargarDdlSucursales()
        {
            try
            {
                var rn = new RnSucUrsal();
                var dt = rn.ObtenerDataTable(EntSucUrsal.Fields.id_autoescuelas, ddlid_autoescuela.SelectedValue);

                ddlid_sucursal.DataValueField = EntSucUrsal.Fields.id_sucursal.ToString();
                ddlid_sucursal.DataTextField = EntSucUrsal.Fields.direccion_sucursal.ToString();
                ddlid_sucursal.DataSource = dt;
                ddlid_sucursal.DataBind();
            }
            catch (Exception exp)
            {
                var master = (Main)Master; master?.MostrarPopUp(this, exp);
            }
        }

        #region Parametros de llegada

        /// <summary>
        /// Propiedad que permite realizar el filtrado del contenido del grid
        /// </summary>
        public int Localfiltro
        {
            get
            {
                if (Request["filtro"] == null)
                    return 0;
                return int.Parse(Request["filtro"]);
            }
        }
        #endregion Parametros de llegada

        protected void ddlid_autoescuela_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDdlSucursales();
            CargarListado();
        }

        protected void ddlid_sucursal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CargarListado();
        }
    }
}