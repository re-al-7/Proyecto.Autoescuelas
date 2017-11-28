#region
using System;
using System.Collections;
using System.Data.Common;
using System.Text;
using System.Data;
using Npgsql;
using NpgsqlTypes;
#endregion

namespace Autoescuelas.PgConn
{
    public class CConn
    {
        public NpgsqlConnection conexionBD = new NpgsqlConnection();

        private enumTipoConexion _TipoConexion = enumTipoConexion.useDataReader;
        private enum enumTipoConexion
        {
            useDataAdapter,
            useDataReader
        }

        /// <summary>
        ///   Constructor de la Clase que se encarga de configurar la Cadena de Conexion
        /// </summary>
        public CConn()
        {
            conexionBD.ConnectionString = "Server=" + CParametros.server +
                                                ";Database=" + CParametros.bd +
                                                ";User ID=" + CParametros.user +
                                                ";Port=" + CParametros.puerto +
                                                ";Password=" + CParametros.pass +
                                                ";Pooling=false";
        }



        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="query" type="string">
        ///   <para>
        ///     Consulta a la Base de Datos
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        private DataTable cargarDataTable(string query)
        {
            var dt = new DataTable();

            dt.Clear();
            try
            {
                var command = new NpgsqlCommand(query, this.conexionBD);

                if (_TipoConexion == enumTipoConexion.useDataReader)
                {
                    if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();
                    DbDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(dr);
                    dr.Close();
                    if (command.Connection.State != ConnectionState.Closed) command.Connection.Close();
                }
                else
                {
                    var da = new NpgsqlDataAdapter();
                    da = new NpgsqlDataAdapter(command);
                    da.Fill(dt);
                }
                command.Dispose();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query: ", query);
                throw ex;
            }
            return dt;
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="query" type="string">
        ///   <para>
        ///     Consulta a la Base de Datos
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        private DataTable cargarDataTable(string query, ref CTrans Trans)
        {

            var dt = new DataTable();
            dt.Clear();
            try
            {
                var command = new NpgsqlCommand(query, Trans.MyConn);
                command.Transaction = Trans.MyTrans;

                if (_TipoConexion == enumTipoConexion.useDataReader)
                {
                    if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();
                    NpgsqlDataReader dr = (NpgsqlDataReader)command.ExecuteReader(CommandBehavior.Default);
                    dt.Load(dr);
                    dr.Close();
                }
                else
                {
                    var da = new NpgsqlDataAdapter();
                    da = new NpgsqlDataAdapter(command);
                    da.Fill(dt);
                }
                command.Dispose();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTable", query);
                throw ex;
            }
            return dt;
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="query" type="string">
        ///   <para>
        ///     Consulta a la Base de Datos
        ///   </para>
        /// </param>
        /// <returns>
        ///   DbDataReader con el resultado de la ejecución del Query
        /// </returns>
        private DbDataReader cargarDataReader(string query)
        {
            DbDataReader dr = null;
            try
            {
                var command = new NpgsqlCommand(query, this.conexionBD);
                command.Connection.Open();

                dr = (DbDataReader)command.ExecuteReader(CommandBehavior.CloseConnection);
                dr.Close();
                if (command.Connection.State != ConnectionState.Closed) command.Connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                ex.Data.Add("cargarDataReader: ", query);
                throw ex;
            }
            return dr;
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="query" type="string">
        ///   <para>
        ///     Consulta a la Base de Datos
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        private DbDataReader cargarDataReader(string query, ref CTrans Trans)
        {
            DbDataReader dr = null;
            try
            {
                var command = new NpgsqlCommand(query, Trans.MyConn);
                command.Transaction = Trans.MyTrans;

                if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();
                dr = (NpgsqlDataReader)command.ExecuteReader(CommandBehavior.Default);
                dr.Close();

                command.Dispose();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataReader", query);
                throw ex;
            }
            return dr;
        }

        /// <summary>
        ///   Función que ejecuta un Query
        /// </summary>
        /// <param name="query" type="string">
        ///   <para>
        ///     Query para ejecutarse
        ///   </para>
        /// </param>
        /// <returns>
        ///   Registros afectados por la ejecución del query [SQL]
        /// </returns>
        private int Ejecutar(string query)
        {
            try
            {
                if (this.conexionBD.State == ConnectionState.Closed)
                {
                    this.conexionBD.Open();
                }
                var command = new NpgsqlCommand(query, this.conexionBD);
                int numReg = command.ExecuteNonQuery();
                this.conexionBD.Close();
                command.Connection.Close();
                command.Dispose();
                return numReg;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en Ejecutar", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que ejecuta un Query
        /// </summary>
        /// <param name="query" type="string">
        ///   <para>
        ///     Query para ejecutarse
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Registros afectados por la ejecución del query [SQL]
        /// </returns>
        private int Ejecutar(string query, ref CTrans Trans)
        {
            try
            {
                var command = new NpgsqlCommand(query, Trans.MyConn);
                command.Transaction = Trans.MyTrans;
                int numReg = command.ExecuteNonQuery();
                command.Dispose();
                this.conexionBD.Close();
                return numReg;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en Ejecutar", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>        
        /// <returns>
        ///   Devuelve TRUE o FALSE dependiendo del éxito de la Operacion
        /// </returns>
        public bool execStoreProcedure(string nombreSP)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                command.Connection = this.conexionBD;
                command.ExecuteNonQuery();
                command.Connection.Close();
                command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="myTrans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve TRUE o FALSE dependiendo del éxito de la Operacion
        /// </returns>
        public bool execStoreProcedure(string nombreSP, ref CTrans myTrans)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                command.Connection = myTrans.MyConn;
                command.ExecuteNonQuery();
                command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }


        #region CargarDataTables

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Consulta a la Base de Datos
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DataTable cargarDataTable(string tabla, ArrayList arrColumnas)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla;

            try
            {
                return this.cargarDataTable(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTable", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DataTable cargarDataTable(string tabla, ArrayList arrColumnas, ref CTrans Trans)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla;

            try
            {
                return this.cargarDataTable(query, ref Trans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTable", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DataTable cargarDataTableAnd(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere,
                                            ArrayList arrValoresWhere)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " and " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }

            try
            {
                return this.cargarDataTable(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTableAnd", query);
                throw ex;
            }
        }

        public DataTable cargarDataTableAnd(string tabla, ArrayList arrColumnas)
        {


            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla;

            try
            {
                return this.cargarDataTable(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTableAnd", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DataTable cargarDataTableAnd(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere,
                                            ArrayList arrValoresWhere, ref CTrans Trans)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " and " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            try
            {
                return this.cargarDataTable(query, ref Trans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTableAnd", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="sParametrosAdicionales" type="string">
        ///   <para>
        ///     Parametros adicionales de la Consulta
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DataTable cargarDataTableAnd(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere,
                                            ArrayList arrValoresWhere, string sParametrosAdicionales)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " AND " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            query = query + sParametrosAdicionales;

            try
            {
                return this.cargarDataTable(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTableAnd", query);
                throw ex;
            }
        }

        public DataTable cargarDataTableLike(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere,
                                             ArrayList arrValoresWhere, string sParametrosAdicionales)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " AND " + arrColumnasWhere[intContador] + " LIKE " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            query = query + sParametrosAdicionales;

            try
            {
                return this.cargarDataTable(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTableAnd", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="sParametrosAdicionales" type="string">
        ///   <para>
        ///     Parametros Adicionales
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DataTable cargarDataTableAnd(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere,
                                            ArrayList arrValoresWhere, string sParametrosAdicionales, ref CTrans Trans)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " AND " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            query = query + sParametrosAdicionales;

            try
            {
                return this.cargarDataTable(query, ref Trans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTableAnd", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DataTable cargarDataTableOr(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere,
                                           ArrayList arrValoresWhere)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " OR " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }

            try
            {
                return this.cargarDataTable(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTableOr", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DataTable cargarDataTableOr(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere,
                                           ArrayList arrValoresWhere, ref CTrans Trans)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " OR " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            try
            {
                return this.cargarDataTable(query, ref Trans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTableOr", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="sParametrosAdicionales" type="string">
        ///   <para>
        ///     Parametros adicionales de la Consulta
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DataTable cargarDataTableOr(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere,
                                           ArrayList arrValoresWhere, string sParametrosAdicionales)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " OR " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            query = query + sParametrosAdicionales;

            try
            {
                return this.cargarDataTable(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTableOr", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="sParametrosAdicionales" type="string">
        ///   <para>
        ///     Parametros Adicionales
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DataTable cargarDataTableOr(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere,
                                           ArrayList arrValoresWhere, string sParametrosAdicionales, ref CTrans Trans)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " OR " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            query = query + sParametrosAdicionales;

            try
            {
                return this.cargarDataTable(query, ref Trans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTableOr", query);
                throw ex;
            }
        }

        #endregion

        #region Cargar DataReaders

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Consulta a la Base de Datos
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DbDataReader cargarDataReader(string tabla, ArrayList arrColumnas)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla;

            try
            {
                return this.cargarDataReader(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTable", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DbDataReader cargarDataReader(string tabla, ArrayList arrColumnas, ref CTrans Trans)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla;

            try
            {
                return this.cargarDataReader(query, ref Trans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTable", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DbDataReader cargarDataReader(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " and " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }

            try
            {
                return this.cargarDataReader(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataTableAnd", query);
                throw ex;
            }
        }

        public DbDataReader cargarDataReaderAnd(string tabla, ArrayList arrColumnas)
        {


            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla;

            try
            {
                return this.cargarDataReader(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataRewaderAnd", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DbDataReader cargarDataReaderAnd(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans Trans)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " and " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            try
            {
                return this.cargarDataReader(query, ref Trans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataReaderAnd", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DbDataReader cargarDataReaderAnd(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " AND " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            try
            {
                return this.cargarDataReader(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataReaderAnd", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="sParametrosAdicionales" type="string">
        ///   <para>
        ///     Parametros adicionales de la Consulta
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DbDataReader cargarDataReaderAnd(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string sParametrosAdicionales)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " AND " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            query = query + sParametrosAdicionales;

            try
            {
                return this.cargarDataReader(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataReaderAnd", query);
                throw ex;
            }
        }

        public DbDataReader cargarDataReaderLike(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string sParametrosAdicionales)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " AND " + arrColumnasWhere[intContador] + " LIKE " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            query = query + sParametrosAdicionales;

            try
            {
                return this.cargarDataReader(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataReaderLike", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="sParametrosAdicionales" type="string">
        ///   <para>
        ///     Parametros Adicionales
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DbDataReader cargarDataReaderAnd(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string sParametrosAdicionales, ref CTrans Trans)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " AND " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            query = query + sParametrosAdicionales;

            try
            {
                return this.cargarDataReader(query, ref Trans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataReaderAnd", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DbDataReader cargarDataReaderOr(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " OR " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }

            try
            {
                return this.cargarDataReader(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataReaderOr", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DbDataReader cargarDataReaderOr(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans Trans)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " OR " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            try
            {
                return this.cargarDataReader(query, ref Trans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataReaderOr", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="sParametrosAdicionales" type="string">
        ///   <para>
        ///     Parametros adicionales de la Consulta
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DbDataReader cargarDataReaderOr(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string sParametrosAdicionales)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " OR " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            query = query + sParametrosAdicionales;

            try
            {
                return this.cargarDataReader(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataReaderOr", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Función que devuelve un DataTable a partir de la ejecución de una consulta [SQL]
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="sParametrosAdicionales" type="string">
        ///   <para>
        ///     Parametros Adicionales
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   DataTable con el resultado de la ejecución del Query
        /// </returns>
        public DbDataReader cargarDataReaderOr(string tabla, ArrayList arrColumnas, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string sParametrosAdicionales, ref CTrans Trans)
        {
            string query = "SELECT ";
            bool primerReg = true;
            foreach (string columna in arrColumnas)
            {
                if (primerReg)
                {
                    query = query + columna;
                    primerReg = false;
                }
                else
                    query = query + ", " + columna;
            }
            query = query + " FROM " + tabla + " WHERE ";

            bool boolBandera = false;
            for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
            {
                if (boolBandera)
                {
                    query = query + " OR " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                }
                else
                {
                    query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    boolBandera = true;
                }
            }
            query = query + sParametrosAdicionales;

            try
            {
                return this.cargarDataReader(query, ref Trans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en cargarDataReaderOr", query);
                throw ex;
            }
        }

        #endregion

        #region Funcion: Ejecutar

        /// <summary>
        ///   Metodo que realiza el borrado de registros en una tabla de una Base de Datos
        /// </summary>
        /// <param name="nomTabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla a realizar el delete
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <returns>
        ///   El numero de filas afectadas al realizar la consulta
        /// </returns>
        public int deleteBD(string nomTabla, ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
        {
            string query = "";
            try
            {
                query = "DELETE FROM " + nomTabla + " WHERE ";
                bool boolBandera = false;
                for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
                {
                    if (boolBandera)
                    {
                        query = query + " and " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    }
                    else
                    {
                        query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                        boolBandera = true;
                    }
                }

                return this.Ejecutar(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en deleteBD", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que realiza el borrado de registros en una tabla de una Base de Datos
        /// </summary>
        /// <param name="nomTabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla a realizar el delete
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL] 
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="Trans" type="cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Cantidad de registros afectados
        /// </returns>
        public int deleteBD(string nomTabla, ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans Trans)
        {
            string query = "";
            try
            {
                query = "DELETE FROM " + nomTabla + " WHERE ";
                bool boolBandera = false;
                for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
                {
                    if (boolBandera)
                    {
                        query = query + " and " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    }
                    else
                    {
                        query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                        boolBandera = true;
                    }
                }

                return this.Ejecutar(query, ref Trans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en deleteBD", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que realiza el borrado de registros en una tabla de una Base de Datos
        /// </summary>
        /// <param name="nomTabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla a realizar el delete
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL] 
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="strParametrosAdicionales" type="string">
        ///   <para>
        ///     Parametros adicionales
        ///   </para>
        /// </param>
        /// <returns>
        ///   Cantidad de registros afectados
        /// </returns>
        public int deleteBD(string nomTabla, ArrayList arrColumnasWhere, ArrayList arrValoresWhere,
                            string strParametrosAdicionales)
        {
            string query = "";
            try
            {
                query = "DELETE FROM " + nomTabla + " WHERE ";
                bool boolBandera = false;
                for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
                {
                    if (boolBandera)
                    {
                        query = query + " and " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    }
                    else
                    {
                        query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                        boolBandera = true;
                    }
                }
                query = query + strParametrosAdicionales;
                return this.Ejecutar(query);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en deleteBD", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que realiza el borrado de registros en una tabla de una Base de Datos
        /// </summary>
        /// <param name="nomTabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla a realizar el delete
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas where [SQL] 
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas where [SQL] 
        ///   </para>
        /// </param>
        /// <param name="strParametrosAdicionales" type="string">
        ///   <para>
        ///     Parametros adicionales
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Cantidad de registros afectados
        /// </returns>
        public int deleteBD(string nomTabla, ArrayList arrColumnasWhere, ArrayList arrValoresWhere,
                            string strParametrosAdicionales, ref CTrans Trans)
        {
            string query = "";
            try
            {
                query = "DELETE FROM " + nomTabla + " WHERE ";
                bool boolBandera = false;
                for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
                {
                    if (boolBandera)
                    {
                        query = query + " and " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                    }
                    else
                    {
                        query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador];
                        boolBandera = true;
                    }
                }

                query = query + strParametrosAdicionales;

                return this.Ejecutar(query, ref Trans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en deleteBD", query);
                throw ex;
            }
        }

        #endregion

        #region Funcion Ejecutar con Imagenes

        /// <summary>
        ///   Metodo que realiza la inserción de un registro en una tabla de una Base de Datos
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla donde va a realizarse la inserción
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValores" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas where [SQL]
        ///   </para>
        /// </param>
        /// <returns>
        ///   A bool value...
        /// </returns>
        public bool insertBD(string tabla, ArrayList arrColumnas, ArrayList arrValores)
        {
            //Para soporte de imagenes---------------------------
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] arrParam = encoding.GetBytes("images");
            ArrayList arrPosicionByte = new ArrayList();
            //---------------------------------------------------

            string query = "";
            try
            {
                query = "INSERT INTO " + tabla + "(";
                bool primerReg = true;

                for (int intContador = 0; intContador < arrColumnas.Count; intContador++)
                {
                    if (arrValores[intContador] == null)
                    {
                        Console.WriteLine(intContador);
                    }
                    else
                    {
                        if (primerReg)
                        {
                            query = query + arrColumnas[intContador].ToString();
                            primerReg = false;
                        }
                        else
                            query = query + ", " + arrColumnas[intContador].ToString();
                    }
                }

                query = query + ") VALUES (";

                primerReg = true;
                for (int intContador = 0; intContador < arrValores.Count; intContador++)
                {
                    if (arrValores[intContador] == null)
                    {
                        Console.WriteLine(intContador);
                    }
                    else
                    {
                        string strValorSet = "";
                        if (arrValores[intContador].GetType().Equals(arrParam.GetType()))
                        {
                            strValorSet = "?";
                            arrPosicionByte.Add(intContador);
                        }
                        else
                        {
                            strValorSet = arrValores[intContador].ToString();
                        }

                        if (primerReg)
                        {
                            query = query + strValorSet;
                            primerReg = false;
                        }
                        else
                        {
                            query = query + " , " + strValorSet;
                        }
                    }
                }

                query = query + ")";


                if (this.conexionBD.State == ConnectionState.Closed)
                    this.conexionBD.Open();

                var command = new NpgsqlCommand(query, this.conexionBD);

                //Para soporte de imagenes---------------------------
                foreach (int posicion in arrPosicionByte)
                {
                    command.Parameters.Add(new NpgsqlParameter("@parametro" + posicion.ToString(), NpgsqlDbType.Bytea));
                    command.Parameters["@parametro" + posicion.ToString()].Value = (byte[])arrValores[posicion];
                }
                //---------------------------------------------------

                int numReg = command.ExecuteNonQuery();
                command.Dispose();
                this.conexionBD.Close();
                return (numReg > 0);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en insertBD", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que realiza la inserción de un registro en una tabla de una Base de Datos
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla donde va a realizarse la inserción
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValores" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// /// <param name="intIdentity" type="System.Int32">
        ///   <para>
        ///     Valor regresado al momento de insertar la identidad
        ///   </para>
        /// </param>
        /// <returns>
        ///   Exito de la operacion
        /// </returns>
        public bool insertBD(string tabla, ArrayList arrColumnas, ArrayList arrValores, ref int intIdentity)
        {

            //Para soporte de imagenes---------------------------
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] arrParam = encoding.GetBytes("images");
            ArrayList arrPosicionByte = new ArrayList();
            //---------------------------------------------------

            string query = "";
            try
            {
                query = "INSERT INTO " + tabla + "(";
                bool primerReg = true;
                for (int intContador = 0; intContador < arrColumnas.Count; intContador++)
                {
                    if (arrValores[intContador] == null)
                    {
                        Console.WriteLine(intContador);
                    }
                    else
                    {
                        if (primerReg)
                        {
                            query = query + arrColumnas[intContador];
                            primerReg = false;
                        }
                        else
                            query = query + ", " + arrColumnas[intContador];
                    }
                }

                query = query + ") VALUES (";

                primerReg = true;
                for (int intContador = 0; intContador < arrValores.Count; intContador++)
                {
                    if (arrValores[intContador] == null)
                    {
                        Console.WriteLine(intContador);
                    }
                    else
                    {
                        string strValorSet = "";
                        if (arrValores[intContador].GetType().Equals(arrParam.GetType()))
                        {
                            strValorSet = "?";
                            arrPosicionByte.Add(intContador);
                        }
                        else
                        {
                            strValorSet = arrValores[intContador].ToString();
                        }

                        if (primerReg)
                        {
                            query = query + strValorSet;
                            primerReg = false;
                        }
                        else
                        {
                            query = query + " , " + strValorSet;
                        }
                    }
                }

                query = query + "); SELECT ? = @@IDENTITY";

                var command = new NpgsqlCommand(query, this.conexionBD);
                //Para soporte de imagenes---------------------------
                foreach (int posicion in arrPosicionByte)
                {
                    command.Parameters.Add(new NpgsqlParameter("@parametro" + posicion, NpgsqlDbType.Bytea));
                    command.Parameters["@parametro" + posicion].Value = arrValores[posicion];
                }
                //---------------------------------------------------

                NpgsqlParameter parIdentity = command.Parameters.Add("identity", NpgsqlDbType.Integer, 0, "key");
                parIdentity.Direction = ParameterDirection.Output;

                int numReg = command.ExecuteNonQuery();
                intIdentity = int.Parse(command.Parameters[command.Parameters.Count - 1].Value.ToString());
                command.Dispose();
                this.conexionBD.Close();
                return (numReg > 0);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en insertBD", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que realiza la inserción de un registro en una tabla de una Base de Datos
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla donde va a realizarse la inserción
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValores" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Exito de la operación
        /// </returns>
        public bool insertBD(string tabla, ArrayList arrColumnas, ArrayList arrValores, ref CTrans Trans)
        {
            //Para soporte de imagenes---------------------------
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] arrParam = encoding.GetBytes("images");
            ArrayList arrPosicionByte = new ArrayList();
            //---------------------------------------------------

            string query = "";
            try
            {
                query = "INSERT INTO " + tabla + "(";
                bool primerReg = true;
                for (int intContador = 0; intContador < arrColumnas.Count; intContador++)
                {
                    if (arrValores[intContador] == null)
                    {
                        Console.WriteLine(intContador);
                    }
                    else
                    {
                        if (primerReg)
                        {
                            query = query + arrColumnas[intContador].ToString();
                            primerReg = false;
                        }
                        else
                            query = query + ", " + arrColumnas[intContador].ToString();
                    }
                }

                query = query + ") VALUES (";

                primerReg = true;
                for (int intContador = 0; intContador < arrValores.Count; intContador++)
                {
                    if (arrValores[intContador] == null)
                    {
                        Console.WriteLine(intContador);
                    }
                    else
                    {
                        string strValorSet = "";
                        if (arrValores[intContador].GetType().Equals(arrParam.GetType()))
                        {
                            strValorSet = "?";
                            arrPosicionByte.Add(intContador);
                        }
                        else
                        {
                            strValorSet = arrValores[intContador].ToString();
                        }

                        if (primerReg)
                        {
                            query = query + strValorSet;
                            primerReg = false;
                        }
                        else
                        {
                            query = query + " , " + strValorSet;
                        }
                    }
                }

                query = query + ")";

                var command = new NpgsqlCommand(query, Trans.MyConn);
                //Para soporte de imagenes---------------------------
                foreach (int posicion in arrPosicionByte)
                {
                    command.Parameters.Add(new NpgsqlParameter("@parametro" + posicion.ToString(), NpgsqlDbType.Bytea));
                    command.Parameters["@parametro" + posicion.ToString()].Value = (byte[])arrValores[posicion];
                }
                //---------------------------------------------------

                command.Transaction = Trans.MyTrans;
                int numReg = command.ExecuteNonQuery();
                command.Dispose();
                this.conexionBD.Close();
                return (numReg > 0);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en insertBD", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que realiza la inserción de un registro en una tabla de una Base de Datos
        /// </summary>
        /// <param name="tabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla donde va a realizarse la inserción
        ///   </para>
        /// </param>
        /// <param name="arrColumnas" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValores" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los valores de las columnas en la consulta [SQL]
        ///   </para>
        /// </param>
        /// /// <param name="intIdentity" type="System.Int32">
        ///   <para>
        ///     Valor regresado al momento de insertar la identidad
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   A bool value...
        /// </returns>
        public bool insertBD(string tabla, ArrayList arrColumnas, ArrayList arrValores, ref int intIdentity,
                             ref CTrans Trans)
        {

            //Para soporte de imagenes---------------------------
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] arrParam = encoding.GetBytes("images");
            ArrayList arrPosicionByte = new ArrayList();
            //---------------------------------------------------

            string query = "";
            try
            {
                query = "INSERT INTO " + tabla + "(";
                bool primerReg = true;
                for (int intContador = 0; intContador < arrColumnas.Count; intContador++)
                {
                    if (arrValores[intContador] == null)
                    {
                        Console.WriteLine(intContador);
                    }
                    else
                    {
                        if (primerReg)
                        {
                            query = query + arrColumnas[intContador];
                            primerReg = false;
                        }
                        else
                            query = query + ", " + arrColumnas[intContador];
                    }
                }

                query = query + ") VALUES (";

                primerReg = true;
                for (int intContador = 0; intContador < arrValores.Count; intContador++)
                {
                    if (arrValores[intContador] == null)
                    {
                        Console.WriteLine(intContador);
                    }
                    else
                    {
                        string strValorSet = "";
                        if (arrValores[intContador].GetType().Equals(arrParam.GetType()))
                        {
                            strValorSet = "?";
                            arrPosicionByte.Add(intContador);
                        }
                        else
                        {
                            strValorSet = arrValores[intContador].ToString();
                        }

                        if (primerReg)
                        {
                            query = query + strValorSet;
                            primerReg = false;
                        }
                        else
                        {
                            query = query + " , " + strValorSet;
                        }
                    }
                }

                query = query + "); SELECT ? = @@IDENTITY";

                var command = new NpgsqlCommand(query, Trans.MyConn);
                //Para soporte de imagenes---------------------------
                foreach (int posicion in arrPosicionByte)
                {
                    command.Parameters.Add(new NpgsqlParameter("@parametro" + posicion, NpgsqlDbType.Bytea));
                    command.Parameters["@parametro" + posicion].Value = arrValores[posicion];
                }
                //---------------------------------------------------

                NpgsqlParameter parIdentity = command.Parameters.Add("identity", NpgsqlDbType.Integer, 0, "key");
                parIdentity.Direction = ParameterDirection.Output;

                command.Transaction = Trans.MyTrans;
                int numReg = command.ExecuteNonQuery();
                intIdentity = int.Parse(command.Parameters[command.Parameters.Count - 1].Value.ToString());
                command.Dispose();
                this.conexionBD.Close();
                return (numReg > 0);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en insertBD", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que realiza la actualizacion de un registro en una tabla de una Base de Datos
        /// </summary>
        /// <param name="nomTabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnasSet" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas del SET [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresSet" type="System.Collections.ArrayList">
        ///   <para>
        ///    Coleccion de objetos referidas a los valores de las columnas del SET [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas del WHERE [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas del WHERE [SQL]
        ///   </para>
        /// </param>
        /// <returns>
        ///   Número de registros afectados por la ejecución del query
        /// </returns>
        public int updateBD(string nomTabla, ArrayList arrColumnasSet, ArrayList arrValoresSet,
                            ArrayList arrColumnasWhere, ArrayList arrValoresWhere)
        {
            //Para soporte de imagenes---------------------------
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] arrParam = encoding.GetBytes("images");
            ArrayList arrPosicionByte = new ArrayList();
            //---------------------------------------------------

            string query = "UPDATE " + nomTabla.ToUpper() + " SET ";
            try
            {
                bool boolBandera = false;

                for (int intContador = 0; intContador < arrValoresSet.Count; intContador++)
                {
                    if (arrValoresSet[intContador] == null)
                    {
                        if (boolBandera)
                        {
                            query = query + " , " + arrColumnasSet[intContador].ToString().ToUpper() + " = null ";
                        }
                        else
                        {
                            query = query + arrColumnasSet[intContador].ToString().ToUpper() + " = null ";
                            boolBandera = true;
                        }
                    }
                    else
                    {
                        string strValorSet = "";
                        if (arrValoresSet[intContador].GetType().Equals(arrParam.GetType()))
                        {
                            strValorSet = "?";
                            arrPosicionByte.Add(intContador);
                        }
                        else
                        {
                            strValorSet = arrValoresSet[intContador].ToString();
                        }
                        if (boolBandera)
                        {
                            query = query + " , " + arrColumnasSet[intContador].ToString().ToUpper() + " = " +
                                    strValorSet;
                        }
                        else
                        {
                            query = query + arrColumnasSet[intContador].ToString().ToUpper() + " = " + strValorSet;
                            boolBandera = true;
                        }
                    }
                }
                query += " WHERE ";

                boolBandera = false;
                for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
                {
                    if (boolBandera)
                    {
                        query = query + " and " + arrColumnasWhere[intContador].ToString().ToUpper() + " = " +
                                arrValoresWhere[intContador] + "";
                    }
                    else
                    {
                        query = query + arrColumnasWhere[intContador].ToString().ToUpper() + " = " +
                                arrValoresWhere[intContador] + "";
                        boolBandera = true;
                    }
                }

                if (this.conexionBD.State == ConnectionState.Closed)
                    this.conexionBD.Open();

                var command = new NpgsqlCommand(query, this.conexionBD);

                //Para soporte de imagenes---------------------------
                foreach (int posicion in arrPosicionByte)
                {
                    command.Parameters.Add(new NpgsqlParameter("@parametro" + posicion.ToString(), NpgsqlDbType.Bytea));
                    command.Parameters["@parametro" + posicion.ToString()].Value = (byte[])arrValoresSet[posicion];
                }
                //---------------------------------------------------

                int numReg = command.ExecuteNonQuery();
                command.Dispose();
                this.conexionBD.Close();
                return numReg;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en updateBD", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que realiza la actualizacion de un registro en una tabla de una Base de Datos
        /// </summary>
        /// <param name="nomTabla" type="string">
        ///   <para>
        ///     Nombre de la tabla a realizar la actualizacion
        ///   </para>
        /// </param>
        /// <param name="arrColumnasSet" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas del SET [SQL] 
        ///   </para>
        /// </param>
        /// <param name="arrValoresSet" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas del SET [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas del WHERE [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas del WHERE [SQL]
        ///   </para>
        /// </param>
        /// <param name="Trans" type="cTrans">
        ///   <para>
        ///     Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Cantidad de registros afectados por la consulta
        /// </returns>
        public int updateBD(string nomTabla, ArrayList arrColumnasSet, ArrayList arrValoresSet,
                            ArrayList arrColumnasWhere, ArrayList arrValoresWhere, ref CTrans Trans)
        {
            //Para soporte de imagenes---------------------------
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] arrParam = encoding.GetBytes("images");
            ArrayList arrPosicionByte = new ArrayList();
            //---------------------------------------------------

            string query = "";
            try
            {
                query = "update " + nomTabla + " set ";
                bool boolBandera = false;
                for (int intContador = 0; intContador < arrValoresSet.Count; intContador++)
                {
                    if (arrValoresSet[intContador] == null)
                    {
                        if (boolBandera)
                        {
                            query = query + " , " + arrColumnasSet[intContador] + " = null ";
                        }
                        else
                        {
                            query = query + arrColumnasSet[intContador] + " = null ";
                            boolBandera = true;
                        }
                    }
                    else
                    {
                        string strValorSet = "";
                        if (arrValoresSet[intContador].GetType().Equals(arrParam.GetType()))
                        {
                            strValorSet = "?";
                            arrPosicionByte.Add(intContador);
                        }
                        else
                        {
                            strValorSet = arrValoresSet[intContador].ToString();
                        }
                        if (boolBandera)
                        {
                            query = query + " , " + arrColumnasSet[intContador] + " = " + strValorSet;
                        }
                        else
                        {
                            query = query + arrColumnasSet[intContador] + " = " + strValorSet;
                            boolBandera = true;
                        }
                    }
                }
                query += " where ";

                boolBandera = false;
                for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
                {
                    if (boolBandera)
                    {
                        query = query + " and " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador] +
                                "";
                    }
                    else
                    {
                        query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador] + "";
                        boolBandera = true;
                    }
                }

                var command = new NpgsqlCommand(query, Trans.MyConn);

                //Para soporte de imagenes---------------------------
                foreach (int posicion in arrPosicionByte)
                {
                    command.Parameters.Add(new NpgsqlParameter("@parametro" + posicion.ToString(), NpgsqlDbType.Bytea));
                    command.Parameters["@parametro" + posicion.ToString()].Value = (byte[])arrValoresSet[posicion];
                }
                //---------------------------------------------------

                command.Transaction = Trans.MyTrans;
                int numReg = command.ExecuteNonQuery();
                command.Dispose();
                this.conexionBD.Close();
                return numReg;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en updateBD", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que realiza la actualizacion de un registro en una tabla de una Base de Datos
        /// </summary>
        /// <param name="nomTabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnasSet" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas del SET [SQL] 
        ///   </para>
        /// </param>
        /// <param name="arrValoresSet" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas del SET [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas del WHERE [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas del WHERE [SQL]
        ///   </para>
        /// </param>
        /// <param name="strParametrosAdicionales" type="string">
        ///   <para>
        ///     Parametros adicionales
        ///   </para>
        /// </param>
        /// <returns>
        ///   A int value...
        /// </returns>
        public int updateBD(string nomTabla, ArrayList arrColumnasSet, ArrayList arrValoresSet,
                            ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales)
        {
            //Para soporte de imagenes---------------------------
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] arrParam = encoding.GetBytes("images");
            ArrayList arrPosicionByte = new ArrayList();
            //---------------------------------------------------

            string query = "";
            try
            {
                query = "update " + nomTabla + " set ";
                bool boolBandera = false;
                for (int intContador = 0; intContador < arrValoresSet.Count; intContador++)
                {
                    if (arrValoresSet[intContador] == null)
                    {
                        if (boolBandera)
                        {
                            query = query + " , " + arrColumnasSet[intContador] + " = null ";
                        }
                        else
                        {
                            query = query + arrColumnasSet[intContador] + " = null ";
                            boolBandera = true;
                        }
                    }
                    else
                    {
                        string strValorSet = "";
                        if (arrValoresSet[intContador].GetType().Equals(arrParam.GetType()))
                        {
                            strValorSet = "?";
                            arrPosicionByte.Add(intContador);
                        }
                        else
                        {
                            strValorSet = arrValoresSet[intContador].ToString();
                        }
                        if (boolBandera)
                        {
                            query = query + " , " + arrColumnasSet[intContador] + " = " + strValorSet;
                        }
                        else
                        {
                            query = query + arrColumnasSet[intContador] + " = " + strValorSet;
                            boolBandera = true;
                        }
                    }

                }
                query += " WHERE ";

                boolBandera = false;
                for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
                {
                    if (boolBandera)
                    {
                        query = query + " and " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador] +
                                "";
                    }
                    else
                    {
                        query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador] + "";
                        boolBandera = true;
                    }
                }

                query = query + strParametrosAdicionales;

                if (this.conexionBD.State == ConnectionState.Closed)
                    this.conexionBD.Open();

                var command = new NpgsqlCommand(query, this.conexionBD);

                //Para soporte de imagenes---------------------------
                foreach (int posicion in arrPosicionByte)
                {
                    command.Parameters.Add(new NpgsqlParameter("@parametro" + posicion.ToString(), NpgsqlDbType.Bytea));
                    command.Parameters["@parametro" + posicion.ToString()].Value = (byte[])arrValoresSet[posicion];
                }
                //---------------------------------------------------

                int numReg = command.ExecuteNonQuery();
                command.Dispose();
                this.conexionBD.Close();
                return numReg;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en updateBD", query);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que realiza la actualizacion de un registro en una tabla de una Base de Datos
        /// </summary>
        /// <param name="nomTabla" type="string">
        ///   <para>
        ///     Nombre de la Tabla
        ///   </para>
        /// </param>
        /// <param name="arrColumnasSet" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas del SET [SQL] 
        ///   </para>
        /// </param>
        /// <param name="arrValoresSet" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas del SET [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrColumnasWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a las columnas del WHERE [SQL]
        ///   </para>
        /// </param>
        /// <param name="arrValoresWhere" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidas a los valores de las columnas del WHERE [SQL]
        ///   </para>
        /// </param>
        /// <param name="strParametrosAdicionales" type="string">
        ///   <para>
        ///     
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   A int value...
        /// </returns>
        public int updateBD(string nomTabla, ArrayList arrColumnasSet, ArrayList arrValoresSet,
                            ArrayList arrColumnasWhere, ArrayList arrValoresWhere, string strParametrosAdicionales,
                            ref CTrans Trans)
        {
            //Para soporte de imagenes---------------------------
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] arrParam = encoding.GetBytes("images");
            ArrayList arrPosicionByte = new ArrayList();
            //---------------------------------------------------

            string query = "";
            try
            {
                query = "update " + nomTabla + " set ";
                bool boolBandera = false;
                for (int intContador = 0; intContador < arrValoresSet.Count; intContador++)
                {
                    if (arrValoresSet[intContador] == null)
                    {
                        if (boolBandera)
                        {
                            query = query + " , " + arrColumnasSet[intContador] + " = null ";
                        }
                        else
                        {
                            query = query + arrColumnasSet[intContador] + " = null ";
                            boolBandera = true;
                        }
                    }
                    else
                    {
                        string strValorSet = "";
                        if (arrValoresSet[intContador].GetType().Equals(arrParam.GetType()))
                        {
                            strValorSet = "?";
                            arrPosicionByte.Add(intContador);
                        }
                        else
                        {
                            strValorSet = arrValoresSet[intContador].ToString();
                        }
                        if (boolBandera)
                        {
                            query = query + " , " + arrColumnasSet[intContador] + " = " + strValorSet;
                        }
                        else
                        {
                            query = query + arrColumnasSet[intContador] + " = " + strValorSet;
                            boolBandera = true;
                        }
                    }
                }
                query += " where ";

                boolBandera = false;
                for (int intContador = 0; intContador < arrValoresWhere.Count; intContador++)
                {
                    if (boolBandera)
                    {
                        query = query + " and " + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador] +
                                "";
                    }
                    else
                    {
                        query = query + arrColumnasWhere[intContador] + " = " + arrValoresWhere[intContador] + "";
                        boolBandera = true;
                    }
                }

                query = query + strParametrosAdicionales;

                var command = new NpgsqlCommand(query, Trans.MyConn);

                //Para soporte de imagenes---------------------------
                foreach (int posicion in arrPosicionByte)
                {
                    command.Parameters.Add(new NpgsqlParameter("@parametro" + posicion.ToString(), NpgsqlDbType.Bytea));
                    command.Parameters["@parametro" + posicion.ToString()].Value = (byte[])arrValoresSet[posicion];
                }
                //---------------------------------------------------

                command.Transaction = Trans.MyTrans;
                int numReg = command.ExecuteNonQuery();
                command.Dispose();
                this.conexionBD.Close();
                return numReg;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Query en updateBD", query);
                throw ex;
            }
        }

        #endregion

        #region Funciones: SpSel to DataTable

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSel(string strNombreTabla)
        {
            DataTable dtTemp = new DataTable();

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "Sel");
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSel(string strNombreTabla, ref CTrans myTrans)
        {
            DataTable dtTemp = new DataTable();

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "Sel", ref myTrans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSel(string strNombreTabla, ArrayList arrParametrosSelect)
        {
            DataTable dtTemp = new DataTable();

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "SelPick", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSel(string strNombreTabla, ArrayList arrParametrosSelect, ref CTrans myTrans)
        {
            DataTable dtTemp = new DataTable();

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "SelPick", arrNombreParametros,
                                                       arrParametros, ref myTrans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSel(string strNombreTabla, ArrayList arrParametrosWhere,
                                               ArrayList arrParametrosValores)
        {
            DataTable dtTemp = new DataTable();

            string strParametrosWhere = "";

            bool bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " AND " + strParametrosWhere;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("ColumnasWhere");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosWhere);

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "SelCnd", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSel(string strNombreTabla, ArrayList arrParametrosWhere,
                                               ArrayList arrParametrosValores, ref CTrans myTrans)
        {
            DataTable dtTemp = new DataTable();

            string strParametrosWhere = "";

            bool bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " AND " + strParametrosWhere;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("ColumnasWhere");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosWhere);

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "SelCnd", arrNombreParametros,
                                                       arrParametros, ref myTrans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSel(string strNombreTabla, ArrayList arrParametrosSelect,
                                               ArrayList arrParametrosWhere, ArrayList arrParametrosValores)
        {
            DataTable dtTemp = new DataTable();

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " AND " + strParametrosWhere;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "SelPickCnd", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSel(string strNombreTabla, ArrayList arrParametrosSelect,
                                               ArrayList arrParametrosWhere, ArrayList arrParametrosValores,
                                               ref CTrans myTrans)
        {
            DataTable dtTemp = new DataTable();

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " AND " + strParametrosWhere;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "SelPickCnd", arrNombreParametros,
                                                       arrParametros, ref myTrans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="strParamAdicionales"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSel(string strNombreTabla, ArrayList arrParametrosSelect,
                                               ArrayList arrParametrosWhere, ArrayList arrParametrosValores,
                                               string strParamAdicionales)
        {
            DataTable dtTemp = new DataTable();

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " AND " + strParametrosWhere;
            }


            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            arrNombreParametros.Add("Parametros");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);
            arrParametros.Add(strParamAdicionales);

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "SelPickCndExt", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="strParamAdicionales"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSel(string strNombreTabla, ArrayList arrParametrosSelect,
                                               ArrayList arrParametrosWhere, ArrayList arrParametrosValores,
                                               string strParamAdicionales, ref CTrans myTrans)
        {
            DataTable dtTemp = new DataTable();

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " AND " + strParametrosWhere;
            }


            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            arrNombreParametros.Add("Parametros");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);
            arrParametros.Add(strParamAdicionales);

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "SelPickCndExt", arrNombreParametros,
                                                       arrParametros, ref myTrans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSelOr(string strNombreTabla, ArrayList arrParametrosSelect,
                                                 ArrayList arrParametrosWhere, ArrayList arrParametrosValores)
        {
            DataTable dtTemp = new DataTable();

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " OR " + strParametrosWhere;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "SelPickCnd", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSelOr(string strNombreTabla, ArrayList arrParametrosSelect,
                                                 ArrayList arrParametrosWhere, ArrayList arrParametrosValores,
                                                 ref CTrans myTrans)
        {
            DataTable dtTemp = new DataTable();

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " OR " + strParametrosWhere;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "SelPickCnd", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="strParamAdicionales"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSelOr(string strNombreTabla, ArrayList arrParametrosSelect,
                                                 ArrayList arrParametrosWhere, ArrayList arrParametrosValores,
                                                 string strParamAdicionales)
        {
            DataTable dtTemp = new DataTable();

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " OR " + strParametrosWhere;
            }


            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            arrNombreParametros.Add("Parametros");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);
            arrParametros.Add(strParamAdicionales);

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "SelPickCndExt", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="strParamAdicionales"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DataTable execStoreProcedureSelOr(string strNombreTabla, ArrayList arrParametrosSelect,
                                                 ArrayList arrParametrosWhere, ArrayList arrParametrosValores,
                                                 string strParamAdicionales, ref CTrans myTrans)
        {
            DataTable dtTemp = new DataTable();

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " OR " + strParametrosWhere;
            }


            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            arrNombreParametros.Add("Parametros");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);
            arrParametros.Add(strParamAdicionales);

            try
            {
                dtTemp = this.execStoreProcedureToDataTable("Sp" + strNombreTabla + "SelPickCndExt", arrNombreParametros,
                                                       arrParametros, ref myTrans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return dtTemp;
        }

        #endregion


        #region Funciones: SpSel to DataReader

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSel(string strNombreTabla)
        {
            try
            {
                return this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "Sel");
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSel(string strNombreTabla, ref CTrans myTrans)
        {
            try
            {
                return this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "Sel", ref myTrans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSel(string strNombreTabla, ArrayList arrParametrosSelect)
        {
            DbDataReader drReader = null;
            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);

            try
            {
                drReader = this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "SelPick", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return drReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSel(string strNombreTabla, ArrayList arrParametrosSelect, ref CTrans myTrans)
        {
            DbDataReader drReader = null;

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);

            try
            {
                drReader = this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "SelPick", arrNombreParametros,
                                                       arrParametros, ref myTrans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return drReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSel(string strNombreTabla, ArrayList arrParametrosWhere,
                                               ArrayList arrParametrosValores)
        {
            DbDataReader drReader = null;

            string strParametrosWhere = "";

            bool bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " AND " + strParametrosWhere;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("ColumnasWhere");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosWhere);

            try
            {
                drReader = this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "SelCnd", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return drReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSel(string strNombreTabla, ArrayList arrParametrosWhere,
                                               ArrayList arrParametrosValores, ref CTrans myTrans)
        {
            DbDataReader drReader = null;

            string strParametrosWhere = "";

            bool bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " AND " + strParametrosWhere;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("ColumnasWhere");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosWhere);

            try
            {
                drReader = this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "SelCnd", arrNombreParametros,
                                                       arrParametros, ref myTrans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return drReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSel(string strNombreTabla, ArrayList arrParametrosSelect,
                                               ArrayList arrParametrosWhere, ArrayList arrParametrosValores)
        {
            DbDataReader drReader = null;

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " AND " + strParametrosWhere;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);

            try
            {
                drReader = this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "SelPickCnd", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return drReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSel(string strNombreTabla, ArrayList arrParametrosSelect,
                                               ArrayList arrParametrosWhere, ArrayList arrParametrosValores,
                                               ref CTrans myTrans)
        {
            DbDataReader drReader = null;

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " AND " + strParametrosWhere;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);

            try
            {
                drReader = this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "SelPickCnd", arrNombreParametros,
                                                       arrParametros, ref myTrans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return drReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="strParamAdicionales"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSel(string strNombreTabla, ArrayList arrParametrosSelect,
                                               ArrayList arrParametrosWhere, ArrayList arrParametrosValores,
                                               string strParamAdicionales)
        {
            DbDataReader drReader = null;

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " AND " + strParametrosWhere;
            }


            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            arrNombreParametros.Add("Parametros");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);
            arrParametros.Add(strParamAdicionales);

            try
            {
                drReader = this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "SelPickCndExt", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return drReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="strParamAdicionales"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSel(string strNombreTabla, ArrayList arrParametrosSelect,
                                               ArrayList arrParametrosWhere, ArrayList arrParametrosValores,
                                               string strParamAdicionales, ref CTrans myTrans)
        {
            DbDataReader drReader = null;

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " AND " + strParametrosWhere;
            }


            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            arrNombreParametros.Add("Parametros");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);
            arrParametros.Add(strParamAdicionales);

            try
            {
                drReader = this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "SelPickCndExt", arrNombreParametros,
                                                       arrParametros, ref myTrans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return drReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSelOr(string strNombreTabla, ArrayList arrParametrosSelect,
                                                 ArrayList arrParametrosWhere, ArrayList arrParametrosValores)
        {
            DbDataReader drReader = null;

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " OR " + strParametrosWhere;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);

            try
            {
                drReader = this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "SelPickCnd", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return drReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSelOr(string strNombreTabla, ArrayList arrParametrosSelect,
                                                 ArrayList arrParametrosWhere, ArrayList arrParametrosValores,
                                                 ref CTrans myTrans)
        {
            DbDataReader drReader = null;

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " OR " + strParametrosWhere;
            }

            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);

            try
            {
                drReader = this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "SelPickCnd", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return drReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="strParamAdicionales"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSelOr(string strNombreTabla, ArrayList arrParametrosSelect,
                                                 ArrayList arrParametrosWhere, ArrayList arrParametrosValores,
                                                 string strParamAdicionales)
        {
            DbDataReader drReader = null;

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " OR " + strParametrosWhere;
            }


            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            arrNombreParametros.Add("Parametros");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);
            arrParametros.Add(strParamAdicionales);

            try
            {
                drReader = this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "SelPickCndExt", arrNombreParametros,
                                                       arrParametros);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return drReader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNombreTabla"></param>
        /// <param name="arrParametrosSelect"></param>
        /// <param name="arrParametrosWhere"></param>
        /// <param name="arrParametrosValores"></param>
        /// <param name="strParamAdicionales"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public DbDataReader execStoreProcedureDataReaderSelOr(string strNombreTabla, ArrayList arrParametrosSelect,
                                                 ArrayList arrParametrosWhere, ArrayList arrParametrosValores,
                                                 string strParamAdicionales, ref CTrans myTrans)
        {
            DbDataReader drReader = null;

            string strParametrosSelect = "";

            bool bPrimerElemento = true;
            foreach (string strSelect in arrParametrosSelect)
            {
                if (bPrimerElemento)
                {
                    strParametrosSelect = strSelect;
                    bPrimerElemento = false;
                }
                else
                    strParametrosSelect = strParametrosSelect + ", " + strSelect;
            }


            string strParametrosWhere = "";
            bPrimerElemento = true;
            for (int i = 0; i < arrParametrosWhere.Count - 1; i++)
            {
                if (bPrimerElemento)
                {
                    strParametrosWhere = arrParametrosWhere[i].ToString() + " = " + arrParametrosValores[i].ToString();
                    bPrimerElemento = false;
                }
                else
                    strParametrosWhere = strParametrosWhere + " OR " + strParametrosWhere;
            }


            ArrayList arrNombreParametros = new ArrayList();
            arrNombreParametros.Add("Columnas");
            arrNombreParametros.Add("ColumnasWhere");
            arrNombreParametros.Add("Parametros");
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(strParametrosSelect);
            arrParametros.Add(strParametrosWhere);
            arrParametros.Add(strParamAdicionales);

            try
            {
                drReader = this.execStoreProcedureToDbDataReader("Sp" + strNombreTabla + "SelPickCndExt", arrNombreParametros,
                                                       arrParametros, ref myTrans);
            }
            catch (Exception ex)
            {
                ex.Data.Add("execStoreProcedureSel", strNombreTabla);
                throw ex;
            }

            return drReader;
        }

        #endregion

        #region Funciones: Ejecutar SP

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve TRUE o FALSE dependiendo del éxito de la Operacion
        /// </returns>
        public bool execStoreProcedure(string nombreSP, ArrayList arrParametros)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                for (int intContador = 0; intContador < arrParametros.Count; intContador++)
                {
                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(), DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Integer, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Integer, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            case "System.Numerics.BigInteger":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Bigint;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               arrParametros[intContador]));
                                }
                                break;
                        }
                    }
                }
                command.Connection = this.conexionBD;
                command.ExecuteNonQuery();
                command.Connection.Close();
                command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <param name="myTrans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve TRUE o FALSE dependiendo del éxito de la Operacion
        /// </returns>
        public bool execStoreProcedure(string nombreSP, ArrayList arrParametros, ref CTrans myTrans)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                for (int intContador = 0; intContador < arrParametros.Count; intContador++)
                {
                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(), DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Integer, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Integer, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            case "System.Numerics.BigInteger":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Bigint;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               arrParametros[intContador]));
                                }
                                break;
                        }
                    }
                }
                command.Connection = myTrans.MyConn;
                command.ExecuteNonQuery();
                command.Connection.Close();
                command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los nombres de los parametros
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <returns>
        ///   Número de registros afectados
        /// </returns>
        public int execStoreProcedure(string nombreSP, ArrayList arrNombreParametros, ArrayList arrParametros)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                for (int intContador = 0; intContador < arrParametros.Count; intContador++)
                {
                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(),
                                                                   DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Byte[]":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Bytea;
                                }
                                break;
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Integer, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Integer, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            case "System.Numerics.BigInteger":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Bigint;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(), arrParametros[intContador]));
                                }
                                break;
                        }
                    }
                }
                command.Connection = this.conexionBD;

                command.Connection.Open();
                int intRes = command.ExecuteNonQuery();

                command.Connection.Close();
                command.Dispose();
                return intRes;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los nombres de los parametros
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Número de registros afectados
        /// </returns>
        public int execStoreProcedure(string nombreSP, ArrayList arrNombreParametros, ArrayList arrParametros,
                                      ref CTrans Trans)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                for (int intContador = 0; intContador < arrParametros.Count; intContador++)
                {
                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(),
                                                                   DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Byte[]":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Bytea;
                                }
                                break;
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Integer, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Integer, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            case "System.Numerics.BigInteger":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Bigint;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               arrParametros[intContador]));
                                }
                                break;
                        }
                    }
                }
                command.Connection = Trans.MyConn;
                command.Transaction = Trans.MyTrans;

                int intRes = command.ExecuteNonQuery();

                return intRes;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado y devuelve la Identidad Recuperada
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los nombres de los parametros
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <returns>
        ///   Identidad insertada
        /// </returns>
        public object execStoreProcedureIdentity(string nombreSP, ArrayList arrNombreParametros, ArrayList arrParametros)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                //Primero la identidad 
                command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[0].ToString(), arrParametros[0]));
                command.Parameters["@" + arrNombreParametros[0].ToString()].Direction = ParameterDirection.Output;
                command.Parameters["@" + arrNombreParametros[0].ToString()].Size = 30;

                for (int intContador = 1; intContador < arrParametros.Count; intContador++)
                {
                    //Verificamos si el parametro es ID o ID_PERS

                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(),
                                                                   DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Byte[]":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Bytea;
                                }
                                break;
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Integer, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Integer, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            case "System.Numerics.BigInteger":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Bigint;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               arrParametros[intContador]));
                                }
                                break;
                        }
                    }

                }

                command.Connection = this.conexionBD;

                command.Connection.Open();
                int intRes = command.ExecuteNonQuery();

                object valorDevuelto = command.Parameters["@" + arrNombreParametros[0].ToString()].Value;

                command.Connection.Close();
                command.Dispose();

                return valorDevuelto;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado y devuelve la Identidad Recuperada
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los nombres de los parametros
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Identidad insertada
        /// </returns>
        public object execStoreProcedureIdentity(string nombreSP, ArrayList arrNombreParametros, ArrayList arrParametros,
                                                 ref CTrans Trans)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                //Primero la identidad 
                command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[0].ToString(), arrParametros[0]));
                command.Parameters["@" + arrNombreParametros[0].ToString()].Direction = ParameterDirection.Output;
                command.Parameters["@" + arrNombreParametros[0].ToString()].Size = 30;

                for (int intContador = 1; intContador < arrParametros.Count; intContador++)
                {
                    //Verificamos si el parametro es ID o ID_PERS

                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(),
                                                                   DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Byte[]":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Bytea;
                                }
                                break;
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Integer,
                                                                               4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Integer,
                                                                               4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric,
                                                                               4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric,
                                                                               4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(
                                        new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(),
                                                            DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(
                                        new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(),
                                                            arrParametros[intContador]));
                                    command.Parameters["@" + arrNombreParametros[intContador].ToString()].NpgsqlDbType =
                                        NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(
                                        new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(),
                                                            arrParametros[intContador]));
                                    command.Parameters["@" + arrNombreParametros[intContador].ToString()].NpgsqlDbType =
                                        NpgsqlDbType.Timestamp;
                                }
                                break;
                            case "System.Numerics.BigInteger":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(
                                        new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(),
                                                            arrParametros[intContador]));
                                    command.Parameters["@" + arrNombreParametros[intContador].ToString()].NpgsqlDbType =
                                        NpgsqlDbType.Bigint;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(
                                        new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(),
                                                            DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(
                                        new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(),
                                                            arrParametros[intContador]));
                                    command.Parameters["@" + arrNombreParametros[intContador].ToString()].NpgsqlDbType =
                                        NpgsqlDbType.Varchar;
                                }
                                break;
                        }
                    }

                }
                command.Connection = Trans.MyConn;
                command.Transaction = Trans.MyTrans;

                int intRes = command.ExecuteNonQuery();

                object valorDevuelto = command.Parameters["@" + arrNombreParametros[0].ToString()].Value;

                return valorDevuelto;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        #endregion

        #region Funciones: Ejecutar SPs a un DataTable

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve un DataTable con el resultado de la consulta
        /// </returns>
        public DataTable execStoreProcedureToDataTable(string nombreSP)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                command.Connection = this.conexionBD;

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);

                DataTable dtTemp = new DataTable();
                da.Fill(dtTemp);

                command.Connection.Close();
                command.Dispose();
                return dtTemp;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="myTrans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve un DataTable con el resultado de la consulta
        /// </returns>
        public DataTable execStoreProcedureToDataTable(string nombreSP, ref CTrans myTrans)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                command.Connection = myTrans.MyConn;
                command.Transaction = myTrans.MyTrans;


                NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);

                DataTable dtTemp = new DataTable();
                da.Fill(dtTemp);

                command.Connection.Close();
                command.Dispose();
                return dtTemp;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve un DataTable con el resultado de la consulta
        /// </returns>
        public DataTable execStoreProcedureToDataTable(string nombreSP, ArrayList arrParametros)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                for (int intContador = 0; intContador < arrParametros.Count; intContador++)
                {
                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter(arrParametros[intContador].ToString(), DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               arrParametros[intContador]));
                                }
                                break;
                        }
                    }
                }

                command.Connection = this.conexionBD;

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);

                DataTable dtTemp = new DataTable();
                da.Fill(dtTemp);

                command.Connection.Close();
                command.Dispose();
                return dtTemp;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve un DataTable con el resultado de la consulta
        /// </returns>
        public DataTable execStoreProcedureToDataTable(string nombreSP, ArrayList arrParametros, ref CTrans Trans)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                for (int intContador = 0; intContador < arrParametros.Count; intContador++)
                {
                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter(arrParametros[intContador].ToString(), DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               arrParametros[intContador]));
                                }
                                break;
                        }
                    }
                }


                command.Connection = Trans.MyConn;
                command.Transaction = Trans.MyTrans;

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);

                DataTable dtTemp = new DataTable();
                da.Fill(dtTemp);

                command.Connection.Close();
                command.Dispose();
                return dtTemp;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los nombres de los parametros
        ///   </para>
        /// </param>
        /// /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve un DataTable con el resultado de la consulta
        /// </returns>
        public DataTable execStoreProcedureToDataTable(string nombreSP, ArrayList arrNombreParametros,
                                                       ArrayList arrParametros)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                for (int intContador = 0; intContador < arrParametros.Count; intContador++)
                {
                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter(arrNombreParametros[intContador].ToString(),
                                                                   DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               arrParametros[intContador]));
                                }
                                break;
                        }
                    }
                }

                command.Connection = this.conexionBD;

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);

                DataTable dtTemp = new DataTable();
                da.Fill(dtTemp);

                command.Connection.Close();
                command.Dispose();
                return dtTemp;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los nombres de los parametros
        ///   </para>
        /// </param>
        /// /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <param name="myTrans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve un DataTable con el resultado de la consulta
        /// </returns>
        public DataTable execStoreProcedureToDataTable(string nombreSP, ArrayList arrNombreParametros,
                                                       ArrayList arrParametros, ref CTrans Trans)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                if (arrNombreParametros.Count != arrParametros.Count)
                    throw new Exception(
                        "Error al ejecutar procedimiento almacenado. El numero de parametros debe ser igual al número de nombres de parametros.");


                for (int intContador = 0; intContador < arrParametros.Count; intContador++)
                {
                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter(arrNombreParametros[intContador].ToString(),
                                                                   DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               arrParametros[intContador]));
                                }
                                break;
                        }
                    }
                }
                command.Connection = Trans.MyConn;
                command.Transaction = Trans.MyTrans;

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);

                DataTable dtTemp = new DataTable();
                da.Fill(dtTemp);

                command.Dispose();
                return dtTemp;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        #endregion

        #region Funciones: Ejecutar SPs a un DataReader

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve un DbDataReader con el resultado de la consulta
        /// </returns>
        public DbDataReader execStoreProcedureToDbDataReader(string nombreSP)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                command.Connection = this.conexionBD;
                if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();
                DbDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (command.Connection.State != ConnectionState.Closed) command.Connection.Close();
                command.Dispose();
                return dr;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="myTrans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve un DbDataReader con el resultado de la consulta
        /// </returns>
        public DbDataReader execStoreProcedureToDbDataReader(string nombreSP, ref CTrans myTrans)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                command.Connection = myTrans.MyConn;
                command.Transaction = myTrans.MyTrans;

                if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();
                DbDataReader dr = command.ExecuteReader(CommandBehavior.Default);
                command.Dispose();
                return dr;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve un DbDataReader con el resultado de la consulta
        /// </returns>
        public DbDataReader execStoreProcedureToDbDataReader(string nombreSP, ArrayList arrParametros)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                for (int intContador = 0; intContador < arrParametros.Count; intContador++)
                {
                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter(arrParametros[intContador].ToString(), DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               arrParametros[intContador]));
                                }
                                break;
                        }
                    }
                }

                command.Connection = this.conexionBD;

                if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();
                DbDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (command.Connection.State != ConnectionState.Closed) command.Connection.Close();
                command.Dispose();
                return dr;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <param name="Trans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve un DbDataReader con el resultado de la consulta
        /// </returns>
        public DbDataReader execStoreProcedureToDbDataReader(string nombreSP, ArrayList arrParametros, ref CTrans Trans)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                for (int intContador = 0; intContador < arrParametros.Count; intContador++)
                {
                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter(arrParametros[intContador].ToString(), DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               arrParametros[intContador]));
                                }
                                break;
                        }
                    }
                }


                command.Connection = Trans.MyConn;
                command.Transaction = Trans.MyTrans;

                if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();
                DbDataReader dr = command.ExecuteReader(CommandBehavior.Default);
                command.Dispose();
                return dr;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los nombres de los parametros
        ///   </para>
        /// </param>
        /// /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve un DbDataReader con el resultado de la consulta
        /// </returns>
        public DbDataReader execStoreProcedureToDbDataReader(string nombreSP, ArrayList arrNombreParametros,
                                                       ArrayList arrParametros)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                for (int intContador = 0; intContador < arrParametros.Count; intContador++)
                {
                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter(arrNombreParametros[intContador].ToString(),
                                                                   DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               arrParametros[intContador]));
                                }
                                break;
                        }
                    }
                }

                command.Connection = this.conexionBD;

                if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();
                DbDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (command.Connection.State != ConnectionState.Closed) command.Connection.Close();
                command.Dispose();
                return dr;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        /// <summary>
        ///   Metodo que ejecuta un Procedimiento Almacenado
        /// </summary>
        /// <param name="nombreSP" type="string">
        ///   <para>
        ///     Nombre del Stored procedure
        ///   </para>
        /// </param>
        /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los nombres de los parametros
        ///   </para>
        /// </param>
        /// /// <param name="arrParametros" type="System.Collections.ArrayList">
        ///   <para>
        ///     Coleccion de objetos referidad a los parametros
        ///   </para>
        /// </param>
        /// <param name="myTrans" type="Conexion.cTrans">
        ///   <para>
        ///     Objeto que contiene la Transaccion activa utilizada para realizar la operacion
        ///   </para>
        /// </param>
        /// <returns>
        ///   Devuelve un DbDataReader con el resultado de la consulta
        /// </returns>
        public DbDataReader execStoreProcedureToDbDataReader(string nombreSP, ArrayList arrNombreParametros,
                                                       ArrayList arrParametros, ref CTrans Trans)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.CommandText = nombreSP;
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                if (arrNombreParametros.Count != arrParametros.Count)
                    throw new Exception(
                        "Error al ejecutar procedimiento almacenado. El numero de parametros debe ser igual al número de nombres de parametros.");


                for (int intContador = 0; intContador < arrParametros.Count; intContador++)
                {
                    if (arrParametros[intContador] == null)
                    {
                        command.Parameters.Add(new NpgsqlParameter(arrNombreParametros[intContador].ToString(),
                                                                   DBNull.Value));
                    }
                    else
                    {
                        switch (arrParametros[intContador].GetType().ToString())
                        {
                            case "System.Int32":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.Decimal":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, NpgsqlDbType.Numeric, 4, "",
                                                                               ParameterDirection.Input, false, 0, 0,
                                                                               DataRowVersion.Proposed,
                                                                               arrParametros[intContador]));
                                }
                                break;
                            case "System.String":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Varchar;
                                }
                                break;
                            case "System.DateTime":
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador, DBNull.Value));
                                }
                                else
                                {
                                    //command.Parameters.Add(new NpgsqlParameter("@" + arrNombreParametros[intContador].ToString(), arrParametros[intContador]));
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador,
                                                                               arrParametros[intContador]));
                                    command.Parameters["@" + intContador].NpgsqlDbType = NpgsqlDbType.Timestamp;
                                }
                                break;
                            default:
                                if (arrParametros[intContador] == null)
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               DBNull.Value));
                                }
                                else
                                {
                                    command.Parameters.Add(new NpgsqlParameter("@" + intContador.ToString(),
                                                                               arrParametros[intContador]));
                                }
                                break;
                        }
                    }
                }
                command.Connection = Trans.MyConn;
                command.Transaction = Trans.MyTrans;

                if (command.Connection.State == ConnectionState.Closed) command.Connection.Open();
                DbDataReader dr = command.ExecuteReader(CommandBehavior.Default);
                command.Dispose();
                return dr;
            }
            catch (Exception ex)
            {
                ex.Data.Add("StoredProcedure", nombreSP);
                throw ex;
            }
        }

        #endregion

    }
}
