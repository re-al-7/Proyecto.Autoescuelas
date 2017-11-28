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

namespace Autoescuelas.Web
{
    public partial class Consulta : Page
    {
        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                var strTitulo = "Entidades";
                var miSesion = new CSessionHandler();
                var rn = new RnVista();
                var dtVista = rn.ObtenerDatos("VW" + EntSucUrsal.StrAliasTabla.ToUpper() + "REP");
                var dtReporte = CControlHelper.DtAplicarRestriccion(dtVista);

                var strNombreReporte = CParametrosWeb.StrNombreSistema + "-" +
                                          EntSucUrsal.StrAliasTabla.ToUpper() + "-" +
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
                    dv.RowFilter = EntExaMen.Fields.id_examen + " = " + strId;

                    var detailTable = dv.ToTable();

                    dtgDetalles.DataSource = detailTable;
                    dtgDetalles.DataBind();
                    dtgDetalles.HeaderRow.TableSection = TableRowSection.TableHeader;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal",
                        "$('#currentdetail').appendTo('body').modal('show');", true);
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
                var rnListadoDatos = new RnVista();
                ArrayList arrColWhere = new ArrayList();
                arrColWhere.Add("ci");
                ArrayList arrValWhere = new ArrayList();
                arrValWhere.Add("'" + txt_NroCi.Text + "'");
                var dtDatos = rnListadoDatos.ObtenerDatos("vw_examen_lis", arrColWhere, arrValWhere);

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

        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            CargarListado();
            CControlHelper.CrearEstilosGrid(ref dtgListado);
        }
    }
}