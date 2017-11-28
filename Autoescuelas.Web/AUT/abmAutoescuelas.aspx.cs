// // <copyright file="abmClaInstituciones.aspx.cs" company="INTEGRATE - Soluciones Informaticas">
// // Copyright (c) 2016 Todos los derechos reservados
// // </copyright>
// // <author>ReAl </author>
// // <date>2016-10-26 3:43 p. m.</date>

#region usings

using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autoescuelas.Dal.Entidades;
using Autoescuelas.Dal.Modelo;
using Autoescuelas.PgConn;
using Autoescuelas.Web.App_Class;
using ClosedXML.Excel;

#endregion

namespace Autoescuelas.Web.AUT
{
    public partial class abmAutoescuelas : Page
    {
        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Template/Dashboard.aspx");
        }

        protected void btnEditaGuardar_Click(object sender, EventArgs e)
        {
            var bProcede = false;
            try
            {
                var miSession = new CSessionHandler();
                var rn = new RnAutOescuela();
                var objPag = rn.ObtenerObjeto(int.Parse(txtEdit_Id.Text));

                if (objPag != null)
                {
                    objPag.dir_autoescuela = txtEdit_dir_autoescuela.Text;
                    objPag.depa_autoescuela = ddlEdit_depa.SelectedValue;
                    objPag.loc_autoescuela = txtEdit_loc_autoescuela.Text;
                    objPag.telefono_autoescuela = txtEdit_telefono_autoescuela.Text;
                    objPag.nombre_autoescuela = txtEdit_nombre_autoescuela.Text;
                    objPag.usumod = miSession.AppUsuario.login;
                    rn.Update(objPag);

                    bProcede = true;
                }
            }
            catch (Exception exp)
            {
                var master = (Main) Master; master?.MostrarPopUp(this, exp);
            }

            if (bProcede)
                ScriptManager.RegisterStartupScript(this, GetType(), "Exito",
                    CBootstrapModal.GetSuccessModalAndRefresh(), true);
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                var strTitulo = "AutoEscuelas";
                var miSesion = new CSessionHandler();
                var rn = new RnVista();
                var dtVista = rn.ObtenerDatos("VW_" + EntAutOescuela.StrAliasTabla.ToUpper() + "_REP");
                var dtReporte = CControlHelper.DtAplicarRestriccion(dtVista);

                var strNombreReporte = CParametrosWeb.StrNombreSistema + "-" +
                                          EntAutOescuela.StrAliasTabla.ToUpper() + "-" +
                                          DateTime.Now.ToString("yyyy-MM-dd HH-mm") + ".xlsx";
                var template = Server.MapPath("~/doc_templates/" + CParametrosWeb.StrNombreInstitucion + "_Reporte.xlsx");

                using (var wb = new XLWorkbook(template))
                {
                    wb.Worksheets.Worksheet(1).Cell(5, 1).Value = strTitulo;
                    wb.Worksheets.Worksheet(1).Cell(6, 1).Value = "Elaborado por: " + miSesion.AppUsuario.login;
                    wb.Worksheets.Worksheet(1).Cell(7, 1).Value = "Fecha: " +
                                                                  DateTime.Now.ToString(CParametros.ParFormatoFechaHora);

                    wb.Worksheets.Worksheet(1).Cell(9, 1).InsertTable(dtReporte);
                    wb.Worksheets.Worksheet(1).Table("Table1").ShowAutoFilter = true;
                    wb.Worksheets.Worksheet(1).Table("Table1").Style.Alignment.Vertical =
                        XLAlignmentVerticalValues.Center;
                    wb.Worksheets.Worksheet(1).Columns(2, 2 + dtReporte.Columns.Count).AdjustToContents();

                    foreach (var column in wb.Worksheets.Worksheet(1).Columns())
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

                    using (var myMemoryStream = new MemoryStream())
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
            var bProcede = false;
            try
            {
                var miSession = new CSessionHandler();
                var rn = new RnAutOescuela();
                var objPag = new EntAutOescuela();
                objPag.dir_autoescuela = txtNew_dir_autoescuela.Text;
                objPag.depa_autoescuela = ddlNew_depa.SelectedValue;
                objPag.loc_autoescuela = txtNew_loc_autoescuela.Text;
                objPag.telefono_autoescuela = txtNew_telefono_autoescuela.Text;
                objPag.nombre_autoescuela = txtNew_nombre_autoescuela.Text;
                objPag.usucre = miSession.AppUsuario.login;
                rn.Insert(objPag);

                bProcede = true;
            }
            catch (Exception exp)
            {
                var master = (Main) Master; master?.MostrarPopUp(this, exp);
            }

            if (bProcede)
                ScriptManager.RegisterStartupScript(this, GetType(), "Exito",
                    CBootstrapModal.GetSuccessModalAndRefresh(), true);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#newModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "NewModalScript", sb.ToString(), false);
        }

        protected void dtgListado_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var miSesion = new CSessionHandler();
                var imbDetalles =
                    (LinkButton)
                    e.Row.Cells[CControlHelper.GetColumnIndexByName(ref dtgListado, "Acciones")].FindControl("ImbVer");
            }
        }

        protected void dtgListado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var strRedireccion = "";
            try
            {
                if (e.CommandName.Equals("detalles"))
                {
                    var strId = e.CommandArgument.ToString();

                    //Filtramos el Dataset
                    var dv = ((DataTable) dtgListado.DataSource).DefaultView;
                    dv.RowFilter = EntAutOescuela.Fields.id_autoescuelas + " = " + strId;

                    var detailTable = dv.ToTable();

                    dtgDetalles.DataSource = detailTable;
                    dtgDetalles.DataBind();
                    dtgDetalles.HeaderRow.TableSection = TableRowSection.TableHeader;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal",
                        "$('#currentdetail').appendTo('body').modal('show');", true);
                }
                else if (e.CommandName.Equals("modificar"))
                {
                    var strId = e.CommandArgument.ToString();

                    var rn = new RnAutOescuela();
                    var objPag = rn.ObtenerObjeto(int.Parse(strId));

                    if (objPag != null)
                    {
                        txtEdit_Id.Text = strId;
                        txtEdit_dir_autoescuela.Text = objPag.dir_autoescuela;
                        ddlEdit_depa.SelectedValue = objPag.depa_autoescuela;
                        txtEdit_loc_autoescuela.Text = objPag.loc_autoescuela;
                        txtEdit_telefono_autoescuela.Text = objPag.telefono_autoescuela ;
                        txtEdit_nombre_autoescuela.Text = objPag.nombre_autoescuela ;

                        var sb = new StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#editModal').modal('show');");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "EditModalScript", sb.ToString(), false);
                    }
                }
                else if (e.CommandName.Equals("eliminar"))
                {
                    var strId = e.CommandArgument.ToString();

                    var rn = new RnAutOescuela();
                    var objPag = rn.ObtenerObjeto(int.Parse(strId));

                    if (objPag != null)
                    {
                        //objPag.apiestado = "ELIMINADO";
                        //objPag.usumod = miSesion.appUsuario.login;
                        //rn.Update(objPag);

                        rn.Delete(objPag);

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
                Title = CParametrosWeb.StrNombrePagina;

                if (!Page.IsPostBack)
                    CargarListado();
                else
                    CargarListado();

                CControlHelper.CrearEstilosGrid(ref dtgListado);
            }
            catch (Exception exp)
            {
                var master = (Main) Master; master?.MostrarPopUp(this, exp);
            }
        }

        /// <summary>
        /// Funcion que obtiene el DataTable del formulario a partir de la Tabla ClaInstituciones
        /// </summary>
        /// <returns>DataTable con los datos obtenidos</returns>
        private DataTable CargarDataTable()
        {
            try
            {
                var miSession = new CSessionHandler();
                var rnListadoDatos = new RnAutOescuela();
                var dtDatos = rnListadoDatos.ObtenerDataTable();

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
                //var dt = CargarDataTable();
                var dt = CargarDataTable();
                dtgListado.DataSource = dt;
                dtgListado.DataBind();

                CControlHelper.CrearEstilosGrid(ref dtgListado);
            }
            catch (Exception exp)
            {
                var master = (Main) Master; master?.MostrarPopUp(this, exp);
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
    }
}