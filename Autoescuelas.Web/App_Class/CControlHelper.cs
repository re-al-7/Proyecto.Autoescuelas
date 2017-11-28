#region usings

using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace Autoescuelas.Web.App_Class
{
    /// <summary>
    /// 
    /// </summary>
    public static class CControlHelper
    {
        
        /// <summary>
        /// Funcion que se encarga de implementar los estilos del plugin js http://bootstrap-table.wenzhixin.net.cn
        /// </summary>
        /// <param name="dtgListado">GridView en el que se implementaran los estilos</param>
        /// <param name="bShowExport">Valor que indica si se mostrara el boton de exportar</param>
        /// <param name="bShowFilter">Valor quye indica si se muestran cabeceras de filtro</param>
        /// <param name="bAplyStyles">Valor quye indica si se aplican estilos</param>
        public static void CrearEstilosGrid(ref GridView dtgListado, bool bShowExport = true, bool bShowFilter = false, bool bAplyStyles = true, bool bStickyHeader = false)
        {
            if (bShowExport)
            {
                dtgListado.Attributes.Add("data-show-export", "true");
                dtgListado.Attributes.Add("data-export-types", "['csv','txt','doc','excel']");
            }
            if (bShowFilter == true)
            {
                dtgListado.Attributes.Add("data-filter-control", "true");
                dtgListado.Attributes.Add("data-filter-show-clear", "true");
                dtgListado.Attributes.Add("data-search", "false");
            }            
            if (bStickyHeader)
            {
                dtgListado.Attributes.Add("data-sticky-header", "true");
                dtgListado.Attributes.Add("data-sticky-header-offset-y", "51px");
            }
            
            if (bAplyStyles)
                if (dtgListado.Rows.Count > 0)
                {
                    dtgListado.HeaderRow.TableSection = TableRowSection.TableHeader;
                    dtgListado.HeaderRow.Attributes.Add("class", "th_background_1");
                    
                    if (dtgListado.Columns.Count == 0)
                    {
                        for (var i = 0; i < dtgListado.Rows[0].Cells.Count; i++)
                        {
                            string strCabecera = dtgListado.HeaderRow.Cells[i].Text;
                            if (strCabecera == "ACCIONES")
                            {
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-searchable", "false");
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-sortable", "false");
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-width", "150px");
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-align", "center");
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-valign", "middle");                                
                            }
                            else
                            {
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-valign", "middle");
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-sortable", "true");
                                if (bShowFilter == true)
                                {
                                    dtgListado.HeaderRow.Cells[i].Attributes.Add("data-filter-control", "input");
                                    dtgListado.HeaderRow.Cells[i].Attributes.Add("data-filter-control-placeholder",
                                        dtgListado.Columns[i].HeaderText);
                                }
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-field", strCabecera);
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-title-tooltip", strCabecera);                                
                                if (strCabecera.EndsWith("%") || strCabecera.StartsWith("%"))
                                {
                                    dtgListado.HeaderRow.Cells[i].Attributes.Add("data-cell-style", "cellStyle");
                                    dtgListado.HeaderRow.Cells[i].Attributes.Add("data-halign", "right");
                                    dtgListado.HeaderRow.Cells[i].Attributes.Add("data-align", "right");
                                }
                            }
                        }
                    }
                    else
                    {
                        for (var i = 0; i < dtgListado.Columns.Count; i++)
                        {
                            if (dtgListado.Columns[i].HeaderText.ToUpper() == "ACCIONES")
                            {
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-searchable", "false");
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-sortable", "false");
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-width", "150px");
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-align", "center");
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-valign", "middle");
                            }
                            else
                            {
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-valign", "middle");
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-sortable", "true");
                                if (bShowFilter == true)
                                {
                                    dtgListado.HeaderRow.Cells[i].Attributes.Add("data-filter-control", "input");
                                    dtgListado.HeaderRow.Cells[i].Attributes.Add("data-filter-control-placeholder",
                                        dtgListado.Columns[i].HeaderText);
                                }
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-field", dtgListado.Columns[i].HeaderText);
                                dtgListado.HeaderRow.Cells[i].Attributes.Add("data-title-tooltip", dtgListado.Columns[i].HeaderText);
                                
                                string strCabecera = dtgListado.HeaderRow.Cells[i].Text;
                                if (strCabecera.EndsWith("%") || strCabecera.StartsWith("%"))
                                    if (!dtgListado.HeaderRow.Cells[i].Text.IsNullOrEmpty())
                                    {
                                        dtgListado.HeaderRow.Cells[i].Attributes.Add("data-cell-style", "cellStyle");
                                        dtgListado.HeaderRow.Cells[i].Attributes.Add("data-halign", "right");
                                        dtgListado.HeaderRow.Cells[i].Attributes.Add("data-align", "right");
                                    }
                            }
                        }
                    }
                }
        }



        /// <summary>
        /// Funcion que aplica la restriccion del usuario sobre un DataTable
        /// </summary>
        /// <param name="dtOriginal">DataTable original con todos los datos</param>
        /// <returns>DatTable despues de aplicar las restricciones de usuario</returns>
        public static DataTable DtAplicarRestriccion(DataTable dtOriginal)
        {
            dtOriginal.CaseSensitive = false;
            var dv = dtOriginal.DefaultView;
            var miSesion = new CSessionHandler();

            var dtFiltrada = dv.ToTable();
            /*
            if (miSesion.AppRestriccion.restriccion == "F" &&
                dtOriginal.Columns.Contains(ent.Fields.Idcuf.ToString()))
                dv.RowFilter = EntClaUnidadesfuncionales.Fields.Idcuf + " = " + miSesion.AppRestriccion.Idcufu;

            var dtFiltrada = dv.ToTable();

            foreach (var dataTableCol in dtFiltrada.Columns.Cast<DataColumn>().ToList())
                if (dataTableCol.ColumnName.ToUpper().Contains(EntClaInstituciones.Fields.Idcin.ToString().ToUpper()) ||
                    dataTableCol.ColumnName.ToUpper()
                        .Contains(EntClaGerenciasadministrativas.Fields.Idcga.ToString().ToUpper()) ||
                    dataTableCol.ColumnName.ToUpper()
                        .Contains(EntClaUnidadesejecutoras.Fields.Idcue.ToString().ToUpper()) ||
                    dataTableCol.ColumnName.ToUpper()
                        .Contains(EntClaUnidadesfuncionales.Fields.Idcufd.ToString().ToUpper()) ||
                    dataTableCol.ColumnName.ToUpper()
                        .Contains(EntClaUnidadesfuncionales.Fields.Idcuf.ToString().ToUpper()))
                    dtFiltrada.Columns.Remove(dataTableCol.ColumnName);
*/
            return dtFiltrada;
        }

        /// <summary>
        /// Funcion que aplica la restriccion del usuario sobre un DataTable y además remueve columnas listadas en un array
        /// </summary>
        /// <param name="dtOriginal">DataTable original con todos los datos</param>
        /// <param name="arrRemoveCols">Arraylist de columnas a ser eliminadas del DataTable original</param>
        /// <returns>DatTable despues de aplicar las restricciones de usuario</returns>
        public static DataTable DtAplicarRestriccion(DataTable dtOriginal, ArrayList arrRemoveCols)
        {
            var dtFiltrada = DtAplicarRestriccion(dtOriginal);

            foreach (var dataTableCol in dtFiltrada.Columns.Cast<DataColumn>().ToList())
            foreach (string removeCol in arrRemoveCols)
                if (dataTableCol.ColumnName.ToUpper().Equals(removeCol.ToUpper()))
                    dtFiltrada.Columns.Remove(dataTableCol.ColumnName);
            return dtFiltrada;
        }

        /// <summary>
        /// Funcion que remueve columnas de un DataTable listadas en un array 
        /// </summary>
        /// <param name="dtOriginal">DataTable original con todos los datos</param>
        /// <param name="arrRemoveCols">Arraylist de columnas a ser eliminadas del DataTable original</param>
        /// <returns>DatTable despues de remover las columnas especificadas</returns>
        public static DataTable DtRemoveCols(DataTable dtOriginal, ArrayList arrRemoveCols)
        {
            foreach (var dataTableCol in dtOriginal.Columns.Cast<DataColumn>().ToList())
            foreach (string removeCol in arrRemoveCols)
                if (dataTableCol.ColumnName.ToUpper().Equals(removeCol.ToUpper()))
                    dtOriginal.Columns.Remove(dataTableCol.ColumnName);
            return dtOriginal;
        }

        /// <summary>
        /// Funcion que remueve columnas de un DataTable listadas en un array 
        /// </summary>
        /// <param name="dtOriginal">DataTable original con todos los datos</param>
        /// <param name="arrSelectedCols">Listado de columnas que deben conservarse en el DataTable</param>
        /// <returns>DataTable sólo con las columnas seleccionadas</returns>
        public static DataTable DtSeleccionarColumnas(DataTable dtOriginal, ArrayList arrSelectedCols)
        {
            var dtFiltrada = DtAplicarRestriccion(dtOriginal);

            foreach (var dataTableCol in dtFiltrada.Columns.Cast<DataColumn>().ToList())
            {
                var bRemove = true;
                foreach (string removeCol in arrSelectedCols)
                    if (dataTableCol.ColumnName.ToUpper().Equals(removeCol.ToUpper()))
                        bRemove = false;
                if (bRemove)
                    dtFiltrada.Columns.Remove(dataTableCol.ColumnName);
            }
            return dtFiltrada;
        }

        /// <summary>
        /// Funcion recursiva para encontrar un Control por ID
        /// </summary>
        /// <param name="page">Contenedor donde se iniciara la busqueda </param>
        /// <param name="id">ID del control a buscar</param>
        /// <returns></returns>
        public static Control FindAll(ControlCollection page, string id)
        {
            foreach (Control c in page)
            {
                if (c.ID == id)
                    return c;

                if (!c.HasControls()) continue;
                var res = FindAll(c.Controls, id);

                if (res != null)
                    return res;
            }
            return null;
        }


        /// <summary>
        /// funcion para obtener el INDEX de una columna en un gridview
        /// </summary>
        /// <param name="grid">GridView que contiene las columnas</param>
        /// <param name="name">Nombre de la columna a buscar</param>
        /// <returns></returns>
        public static int GetColumnIndexByName(ref GridView grid, string name)
        {
            foreach (DataControlField col in grid.Columns)
                if (col.HeaderText.ToLower().Trim() == name.ToLower().Trim())
                    return grid.Columns.IndexOf(col);
            return -1;
        }
    }
}