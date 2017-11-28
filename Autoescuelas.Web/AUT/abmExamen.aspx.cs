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
    public partial class abmExamen : Page
    {
        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Template/Dashboard.aspx");
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                var strTitulo = "Examenes";
                var miSesion = new CSessionHandler();
                var rn = new RnVista();
                var dtVista = rn.ObtenerDatos("VW_" + EntExaMen.StrAliasTabla.ToUpper() + "_REP");
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

        protected void btnNewGuardar_Click(object sender, EventArgs e)
        {
            var bProcede = false;
            try
            {
                var miSession = new CSessionHandler();
                var rn = new RnExaMen();
                var objPag = new EntExaMen();
                objPag.id_sucursal = int.Parse(ddlid_sucursal.SelectedValue);
                objPag.id_vehiculo = int.Parse(ddlNew_id_vehiculo.SelectedValue);
                objPag.id_tipoexamen = int.Parse(ddlNew_id_tipoexamen.SelectedValue);
                objPag.id_tipolicencia = int.Parse(ddlNew_id_tipolicencia.SelectedValue);
                objPag.id_usuario = int.Parse(ddlNew_id_usuario.SelectedValue);
                objPag.id_postulante = int.Parse(ddlNew_id_postulante.SelectedValue);
                objPag.nota_exa_prac = int.Parse(txtNew_nota_exa_prac.Text);
                objPag.nota_exa_teo = int.Parse(txtNew_nota_exa_prac.Text);
                objPag.usucre = miSession.AppUsuario.login;
                rn.Insert(objPag);

                bProcede = true;
            }
            catch (Exception exp)
            {
                var master = (Main) Master; master?.MostrarPopUp(this, exp);
            }

            if (bProcede)
                Response.Redirect("~/AUT/abmExamen.aspx?msg=Se ha REGISTRADO el EXAMEN satisfactoriamente.");
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
                    dv.RowFilter = EntExaMen.Fields.id_examen + " = " + strId;

                    var detailTable = dv.ToTable();

                    dtgDetalles.DataSource = detailTable;
                    dtgDetalles.DataBind();
                    dtgDetalles.HeaderRow.TableSection = TableRowSection.TableHeader;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal",
                        "$('#currentdetail').appendTo('body').modal('show');", true);
                }
                else if (e.CommandName.Equals("eliminar"))
                {
                    var strId = e.CommandArgument.ToString();

                    var rn = new RnExaMen();
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
                {
                    CargarDdlAutoEscuelas();
                    CargarDdlSucursales();
                    CargarDdlUsuarios();
                    CargarDdlPostulantes();
                    CargarDdlTipoExamen();
                    CargarDdlTipoLicencia();
                    CargarDdlVehiculos();                    
                    CargarListado();
                }
                

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

        /// <summary>
        /// Funcion que se encarga de llenar los datos de Usuarios Instructores
        /// </summary>
        private void CargarDdlUsuarios()
        {
            try
            {
                var rn = new RnUsuArio();
                var dt = rn.ObtenerDataTable(EntUsuArio.Fields.id_sucursal, ddlid_sucursal.SelectedValue);
                dt.Columns.Add(new DataColumn("Descr", typeof(string),
                    "nombres + ' ' + apellidos"));

                ddlNew_id_usuario.DataValueField = EntUsuArio.Fields.id_usuario.ToString();
                ddlNew_id_usuario.DataTextField = "Descr";
                ddlNew_id_usuario.DataSource = dt;
                ddlNew_id_usuario.DataBind();

                if (Localfiltro > 0 && ddlid_autoescuela.Items.Count > 0)
                    ddlid_autoescuela.SelectedValue = Localfiltro.ToString();
            }
            catch (Exception exp)
            {
                var master = (Main)Master; master?.MostrarPopUp(this, exp);
            }
        }

        /// <summary>
        /// Funcion que se encarga de llenar los datos de Postulantes
        /// </summary>
        private void CargarDdlPostulantes()
        {
            try
            {
                var rn = new RnPosTulante();
                var dt = rn.ObtenerDataTable();
                dt.Columns.Add(new DataColumn("Descr", typeof(string),
                    "ci_postulante + ' - ' + nombre_postulante + ' ' + ap_pat_postulante"));

                ddlNew_id_postulante.DataValueField = EntPosTulante.Fields.id_postulante.ToString();
                ddlNew_id_postulante.DataTextField = "Descr";
                ddlNew_id_postulante.DataSource = dt;
                ddlNew_id_postulante.DataBind();

                if (Localfiltro > 0 && ddlid_autoescuela.Items.Count > 0)
                    ddlid_autoescuela.SelectedValue = Localfiltro.ToString();
            }
            catch (Exception exp)
            {
                var master = (Main)Master; master?.MostrarPopUp(this, exp);
            }
        }

        /// <summary>
        /// Funcion que se encarga de llenar los datos de Vehiculos
        /// </summary>
        private void CargarDdlVehiculos()
        {
            try
            {
                var rn = new RnVehIculos();
                var dt = rn.ObtenerDataTable(EntVehIculos.Fields.id_autoescuelas, ddlid_autoescuela.SelectedValue);
                dt.Columns.Add(new DataColumn("Descr", typeof(string),
                    "placa_vehiculo + ' - ' + modelo_vehiculo + ' ' + modelo_vehiculo"));

                ddlNew_id_vehiculo.DataValueField = EntVehIculos.Fields.id_vehiculo.ToString();
                ddlNew_id_vehiculo.DataTextField = "Descr";
                ddlNew_id_vehiculo.DataSource = dt;
                ddlNew_id_vehiculo.DataBind();
            }
            catch (Exception exp)
            {
                var master = (Main)Master; master?.MostrarPopUp(this, exp);
            }
        }

        /// <summary>
        /// Funcion que se encarga de llenar los datos de Vehiculos
        /// </summary>
        private void CargarDdlTipoExamen()
        {
            try
            {
                var rn = new RnTipOexamen();
                var dt = rn.ObtenerDataTable();
                
                ddlNew_id_tipoexamen.DataValueField = EntTipOexamen.Fields.id_tipoexamen.ToString();
                ddlNew_id_tipoexamen.DataTextField = EntTipOexamen.Fields.des_tipoexamen.ToString();
                ddlNew_id_tipoexamen.DataSource = dt;
                ddlNew_id_tipoexamen.DataBind();
            }
            catch (Exception exp)
            {
                var master = (Main)Master; master?.MostrarPopUp(this, exp);
            }
        }

        /// <summary>
        /// Funcion que se encarga de llenar los datos de Vehiculos
        /// </summary>
        private void CargarDdlTipoLicencia()
        {
            try
            {
                var rn = new RnTipOlicencia();
                var dt = rn.ObtenerDataTable();
                
                ddlNew_id_tipolicencia.DataValueField = EntTipOlicencia.Fields.id_tipolicencia.ToString();
                ddlNew_id_tipolicencia.DataTextField = EntTipOlicencia.Fields.categoria.ToString();
                ddlNew_id_tipolicencia.DataSource = dt;
                ddlNew_id_tipolicencia.DataBind();
            }
            catch (Exception exp)
            {
                var master = (Main)Master; master?.MostrarPopUp(this, exp);
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
                arrColWhere.Add(EntSucUrsal.Fields.id_sucursal.ToString());
                ArrayList arrValWhere = new ArrayList();
                arrValWhere.Add(ddlid_sucursal.SelectedValue);
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
            CargarDdlUsuarios();
            CargarListado();
        }
    }
}