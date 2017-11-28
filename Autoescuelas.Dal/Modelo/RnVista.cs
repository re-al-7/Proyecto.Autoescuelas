#region
using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

using Autoescuelas.PgConn;

#endregion

namespace Autoescuelas.Dal.Modelo
{
    public class RnVista 
    {
        /// <summary>
        /// Funcion que carga un DropDownList con los valores de un procedimiento Almacenado
        /// </summary>
        /// <param name="cmb">Control del tipo DropDownList en el que se van a cargar los datos de la tabla segusuariosrestriccionsegusuariosrestriccion</param>
        /// <param name="strNombreProc">Nombre del Procedimiento Almacenado necesario para cargar el SP</param>
        /// <param name="arrNomParam">Nombre de los Parametros del SP</param>
        /// <param name="arrValParam">Valores para los parametros del SP</param>
        public void CargarCombo(ref DropDownList cmb, string strNombreProc, ArrayList arrNomParam, ArrayList arrValParam)
        {
            try
            {
                DataTable dtOrigen = this.ObtenerDatosProcAlm(strNombreProc, arrNomParam, arrValParam);

                if (dtOrigen.Columns.Count > 0)
                {
                    cmb.DataValueField = dtOrigen.Columns[0].ColumnName;
                    cmb.DataTextField = dtOrigen.Columns[1].ColumnName;
                    cmb.DataSource = dtOrigen;
                    cmb.DataBind();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga un DropDownList con los valores de un procedimiento Almacenado
        /// </summary>
        /// <param name="cmb">Control del tipo DropDownList en el que se van a cargar los datos de la tabla segusuariosrestriccionsegusuariosrestriccion</param>
        /// <param name="strNombreProc">Nombre del Procedimiento Almacenado necesario para cargar el SP</param>
        public void CargarCombo(ref DropDownList cmb, string strNombreProc)
        {
            try
            {
                DataTable dtOrigen = this.ObtenerDatosProcAlm(strNombreProc);
                if (dtOrigen.Columns.Count > 0)
                {
                    cmb.DataValueField = dtOrigen.Columns[0].ColumnName;
                    cmb.DataTextField = dtOrigen.Columns[1].ColumnName;
                    cmb.DataSource = dtOrigen;
                    cmb.DataBind();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga un DropDownList con los valores de un DataTable
        /// </summary>
        /// <param name="cmb">Control del tipo DropDownList en el que se van a cargar los datos de la tabla segusuariosrestriccionsegusuariosrestriccion</param>
        /// <param name="dtOrigen">Origen de Datos</param>
        public void CargarCombo(ref DropDownList cmb, DataTable dtOrigen)
        {
            try
            {
                if (dtOrigen.Columns.Count > 0)
                {
                    cmb.DataValueField = dtOrigen.Columns[0].ColumnName;
                    cmb.DataTextField = dtOrigen.Columns[1].ColumnName;
                    cmb.DataSource = dtOrigen;
                    cmb.DataBind();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de una vista a un Grid
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="dtg">Nombre del Grid</param>
        public void CargarGridView(string vista, ref GridView dtg)
        {
            try
            {
                ArrayList arrColumnas = new ArrayList();
                arrColumnas.Add("*");

                ArrayList arrColumnasWhere = new ArrayList();
                arrColumnasWhere.Add("'1'");
                ArrayList arrValoresWhere = new ArrayList();
                arrValoresWhere.Add("'1'");

                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableAnd(vista, arrColumnas, arrColumnasWhere, arrValoresWhere);

                dtg.DataSource = table;
                dtg.DataBind();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de una vista a un Grid
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="dtg">Nombre del Grid</param>
        /// <param name="arrColumnas">Array de COlumnas seleccionadas en la Vista</param>
        public void CargarGridView(string vista, ref GridView dtg, ArrayList arrColumnas)
        {
            try
            {
                ArrayList arrColumnasWhere = new ArrayList();
                arrColumnasWhere.Add("'1'");
                ArrayList arrValoresWhere = new ArrayList();
                arrValoresWhere.Add("'1'");

                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableAnd(vista, arrColumnas, arrColumnasWhere, arrValoresWhere);

                dtg.DataSource = table;
                dtg.DataBind();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de una vista a un Grid
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="dtg">Nombre del Grid</param>
        /// <param name="arrColumnasWhere">Nombre de las columnas por las que se va a filtrar el resultado</param>
        /// <param name="arrValoresWhere">Valor para cada una de las columnas con las que se va a filtrar el resultado</param>
        public void CargarGridView(string vista, ref GridView dtg, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
        {
            try
            {
                ArrayList arrColumnas = new ArrayList();
                arrColumnas.Add("*");

                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableAnd(vista, arrColumnas, arrColumnasWhere, arrValoresWhere);

                dtg.DataSource = table;
                dtg.DataBind();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void CargarGridView(string vista, ref GridView dtg, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
        {
            try
            {
                ArrayList arrColumnas = new ArrayList();
                arrColumnas.Add("*");

                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableAnd(vista, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);

                dtg.DataSource = table;
                dtg.DataBind();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void CargarGridView(string vista, ref GridView dtg, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParamAdicionales)
        {
            try
            {
                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableAnd(vista, arrColumnas, arrColumnasWhere, arrValoresWhere, strParamAdicionales);

                dtg.DataSource = table;
                dtg.DataBind();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de una vista a un Grid (Condiciones OR)
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="dtg">Nombre del Grid</param>
        /// <param name="arrColumnasWhere">Nombre de las columnas por las que se va a filtrar el resultado</param>
        /// <param name="arrValoresWhere">Valor para cada una de las columnas con las que se va a filtrar el resultado</param>
        public void CargarGridViewOr(string vista, ref GridView dtg, ArrayList arrColumnasWhere,
                                     ArrayList arrValoresWhere)
        {
            try
            {
                ArrayList arrColumnas = new ArrayList();
                arrColumnas.Add("*");

                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableOr(vista, arrColumnas, arrColumnasWhere, arrValoresWhere);

                dtg.DataSource = table;
                dtg.DataBind();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de un DataTable
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <returns>DataTable con el resultado de las consultas</returns>
        public DataTable ObtenerDatos(string vista)
        {
            try
            {
                ArrayList arrColumnasWhere = new ArrayList();
                arrColumnasWhere.Add("'1'");
                ArrayList arrValoresWhere = new ArrayList();
                arrValoresWhere.Add("'1'");

                ArrayList arrColumnas = new ArrayList();
                arrColumnas.Add("*");

                CConn local = new CConn();
                DataTable table = new DataTable();
                table = local.cargarDataTableAnd(vista, arrColumnas, arrColumnasWhere, arrValoresWhere);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de una vista a un DataTable
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="arrColumnas">Array de Columnas  seleccionadas en la Vista</param>
        /// <returns>DataTable con el resultado de la consulta</returns>
        public DataTable ObtenerDatos(string vista, ArrayList arrColumnas)
        {
            try
            {
                ArrayList arrColumnasWhere = new ArrayList();
                arrColumnasWhere.Add("'1'");
                ArrayList arrValoresWhere = new ArrayList();
                arrValoresWhere.Add("'1'");

                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableAnd(vista, arrColumnas, arrColumnasWhere, arrValoresWhere);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de una vista a un DataTable
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="arrColumnas">Array de Columnas  seleccionadas en la Vista</param>
        /// <param name="strParametrosAdicionales">Parametros adicionales</param>
        /// <returns>DataTable con el resultado de la consulta</returns>
        public DataTable ObtenerDatos(string vista, ArrayList arrColumnas, string strParametrosAdicionales)
        {
            try
            {
                ArrayList arrColumnasWhere = new ArrayList();
                arrColumnasWhere.Add("'1'");
                ArrayList arrValoresWhere = new ArrayList();
                arrValoresWhere.Add("'1'");

                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableAnd(vista, arrColumnas, arrColumnasWhere, arrValoresWhere, strParametrosAdicionales);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de una vista a un DataTable a partir de columnas filtradas
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="arrColumnasWhere">Nombre de las columnas por las que se va a filtrar el resultado</param>
        /// <param name="arrValoresWhere">Valor para cada una de las columnas con las que se va a filtrar el resultado</param>
        /// <returns>DatatTable con el resultado de la consulta</returns>
        public DataTable ObtenerDatos(string vista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
        {
            try
            {
                ArrayList arrColumnas = new ArrayList();
                arrColumnas.Add("*");

                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableAnd(vista, arrColumnas, arrColumnasWhere, arrValoresWhere);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de determinadas columnas de una vista a un DataTable a partir de columnas filtradas
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="arrColumnas">Array de columnas seleccionadas de la Vista</param>
        /// <param name="arrColumnasWhere">Nombre de las columnas por las que se va a filtrar el resultado</param>
        /// <param name="arrValoresWhere">Valor para cada una de las columnas con las que se va a filtrar el resultado</param>
        /// <param name="strParametrosAdicionales">Condiciones adicionales concatenadas al final de la consulta</param>
        /// <returns>DataTable con el resultado de la consulta</returns>
        public DataTable ObtenerDatos(string vista, ArrayList arrColumnas, ArrayList arrColumnasWhere,
                                      ArrayList arrValoresWhere, string strParametrosAdicionales)
        {
            try
            {
                DataTable table = new DataTable();
                CConn local = new CConn();

                table = local.cargarDataTableAnd(vista, arrColumnas, arrColumnasWhere, arrValoresWhere,
                                                 strParametrosAdicionales);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public DataTable ObtenerDatosLike(string vista, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
        {
            try
            {
                DataTable table = new DataTable();
                CConn local = new CConn();

                table = local.cargarDataTableLike(vista, arrColumnas, arrColumnasWhere, arrValoresWhere,
                                                 strParametrosAdicionales);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de determinadas columnas de una vista a un DataTable a partir de columnas filtradas
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="arrColumnas">Array de columnas seleccionadas de la Vista</param>
        /// <param name="arrColumnasWhere">Nombre de las columnas por las que se va a filtrar el resultado</param>
        /// <param name="arrValoresWhere">Valor para cada una de las columnas con las que se va a filtrar el resultado</param>
        /// <returns>DataTable con el resultado de la consulta</returns>
        public DataTable ObtenerDatos(string vista, ArrayList arrColumnas, ArrayList arrColumnasWhere,
                                      ArrayList arrValoresWhere)
        {
            try
            {
                DataTable table = new DataTable();
                CConn local = new CConn();

                table = local.cargarDataTableAnd(vista, arrColumnas, arrColumnasWhere, arrValoresWhere);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de una vista a un DataTable a partir de columnas filtradas
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="arrColumnasWhere">Nombre de las columnas por las que se va a filtrar el resultado</param>
        /// <param name="arrValoresWhere">Valor para cada una de las columnas con las que se va a filtrar el resultado</param>
        /// <param name="strParametrosAdicionales">Condiciones adicionales concatenadas al final de la consulta</param>
        /// <returns>DataTable con el resultado de la Consulta</returns>
        public DataTable ObtenerDatos(string vista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere,
                                      string strParametrosAdicionales)
        {
            try
            {
                ArrayList arrColumnas = new ArrayList();
                arrColumnas.Add("*");

                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableAnd(vista, arrColumnas, arrColumnasWhere, arrValoresWhere,
                                                 strParametrosAdicionales);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de una vista a un DataTable a partir de un filtro escrito manualmente
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="condicionesWhere">Condiciones adicionales concatenadas al final de la consulta</param>
        /// <returns>DataTable con el resultado de la Consulta</returns>
        public DataTable ObtenerDatos(string vista, string condicionesWhere)
        {
            try
            {
                ArrayList arrColumnas = new ArrayList();
                arrColumnas.Add("*");

                ArrayList arrColumnasWhere = new ArrayList();
                arrColumnasWhere.Add("'1'");
                ArrayList arrValoresWhere = new ArrayList();
                arrValoresWhere.Add("'1'");

                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableOr(vista, arrColumnas, arrColumnasWhere, arrValoresWhere,
                                                " AND (" + condicionesWhere + ")");

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de una vista a un DataTable a partir de columnas filtradas (Condiciones OR)
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="arrColumnasWhere">Nombre de las columnas por las que se va a filtrar el resultado</param>
        /// <param name="arrValoresWhere">Valor para cada una de las columnas con las que se va a filtrar el resultado</param>
        /// <returns>DataTable con el resultado de la consulta</returns>
        public DataTable ObtenerDatosOr(string vista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
        {
            try
            {
                ArrayList arrColumnas = new ArrayList();
                arrColumnas.Add("*");

                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableOr(vista, arrColumnas, arrColumnasWhere, arrValoresWhere);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Funcion que carga el resultado de una consulta SELECT de determinadas columnas de una vista a un DataTable a partir de columnas filtradas (Condiciones OR)
        /// </summary>
        /// <param name="vista">Nombre de la Vista</param>
        /// <param name="arrColumnas">Nombre de las columnas seleccionadas</param>
        /// <param name="arrColumnasWhere">Nombre de las columnas por las que se va a filtrar el resultado</param>
        /// <param name="arrValoresWhere">Valor para cada una de las columnas con las que se va a filtrar el resultado</param>
        /// <param name="strParametrosAdicionales"></param>
        /// <returns>DataTable con el resultado de la consulta</returns>
        public DataTable ObtenerDatosOr(string vista, ArrayList arrColumnas, ArrayList arrColumnasWhere,
                                        ArrayList arrValoresWhere, string strParametrosAdicionales)
        {
            try
            {
                DataTable table = new DataTable();
                CConn local = new CConn();

                table = local.cargarDataTableOr(vista, arrColumnas, arrColumnasWhere, arrValoresWhere,
                                                strParametrosAdicionales);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vista"></param>
        /// <param name="arrColumnasWhere"></param>
        /// <param name="arrValoresWhere"></param>
        /// <param name="strParametrosAdicionales"></param>
        /// <returns></returns>
        public DataTable ObtenerDatosOr(string vista, ArrayList arrColumnasWhere, ArrayList arrValoresWhere,
                                        string strParametrosAdicionales)
        {
            try
            {
                ArrayList arrColumnas = new ArrayList();
                arrColumnas.Add("*");

                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.cargarDataTableOr(vista, arrColumnas, arrColumnasWhere, arrValoresWhere,
                                                strParametrosAdicionales);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreProcAlm"></param>
        /// <param name="arrParametros"></param>
        /// <returns></returns>
        public DataTable ObtenerDatosProcAlm(string nombreProcAlm, ArrayList arrParametros)
        {
            try
            {
                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.execStoreProcedureToDataTable(nombreProcAlm, arrParametros);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreProcAlm"></param>
        /// <returns></returns>
        public DataTable ObtenerDatosProcAlm(string nombreProcAlm)
        {
            try
            {
                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.execStoreProcedureToDataTable(nombreProcAlm);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreProcAlm"></param>
        /// <param name="arrNombreParametros"></param>
        /// <param name="arrParametros"></param>
        /// <returns></returns>
        public DataTable ObtenerDatosProcAlm(string nombreProcAlm, ArrayList arrNombreParametros, ArrayList arrParametros)
        {
            try
            {
                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.execStoreProcedureToDataTable(nombreProcAlm, arrNombreParametros, arrParametros);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreProcAlm"></param>
        /// <param name="arrNombreParametros"></param>
        /// <param name="arrParametros"></param>
        /// <returns></returns>
        public DataTable ObtenerDatosProcAlm(string nombreProcAlm, ArrayList arrNombreParametros, ArrayList arrParametros, ref CTrans myTrans)
        {
            try
            {
                DataTable table = new DataTable();
                CConn local = new CConn();
                table = local.execStoreProcedureToDataTable(nombreProcAlm, arrNombreParametros, arrParametros, ref myTrans);

                return table;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreProcAlm"></param>                
        /// <returns></returns>
        public int EjecutarProcAlm(string nombreProcAlm)
        {
            try
            {
                CConn local = new CConn();
                bool iTotal = local.execStoreProcedure(nombreProcAlm);
                return iTotal ? 1 : 0;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreProcAlm"></param>        
        /// <param name="arrParametros"></param>
        /// <returns></returns>
        public int EjecutarProcAlm(string nombreProcAlm, ArrayList arrParametros)
        {
            try
            {
                CConn local = new CConn();
                bool iTotal = local.execStoreProcedure(nombreProcAlm, arrParametros);
                return iTotal ? 1 : 0;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreProcAlm"></param>
        /// <param name="arrNombreParametros"></param>
        /// <param name="arrParametros"></param>
        /// <returns></returns>
        public int EjecutarProcAlm(string nombreProcAlm, ArrayList arrNombreParametros, ArrayList arrParametros)
        {
            try
            {
                CConn local = new CConn();
                int iTotal = local.execStoreProcedure(nombreProcAlm, arrNombreParametros, arrParametros);
                return iTotal;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreProcAlm"></param>
        /// <param name="arrNombreParametros"></param>
        /// <param name="arrParametros"></param>
        /// <returns></returns>
        public int EjecutarProcAlm(string nombreProcAlm, ArrayList arrNombreParametros, ArrayList arrParametros, ref CTrans myTrans)
        {
            try
            {
                CConn local = new CConn();
                int iTotal = local.execStoreProcedure(nombreProcAlm, arrNombreParametros, arrParametros, ref myTrans);
                return iTotal;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
