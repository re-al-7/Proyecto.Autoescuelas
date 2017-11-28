#region

using System;
using System.Data;
using Npgsql;

#endregion


namespace Autoescuelas.PgConn
{


    public class CTrans
    {
        public NpgsqlTransaction MyTrans;
        public NpgsqlConnection MyConn;

        /// <summary>
        ///     Constructor, que además abre la conexion y la transaccion
        /// </summary>
        public CTrans()
        {
            CConn tempConnWebService = new CConn();
            this.MyConn = tempConnWebService.conexionBD;
            this.MyConn.Open();
            this.MyTrans = this.MyConn.BeginTransaction();
        }

        /// <summary>
        ///     Commit transaccion y cerrar conexion
        /// </summary>
        public void ConfirmarTransaccion()
        {
            try
            {
                this.MyTrans.Commit();
            }
            catch (Exception exp)
            {
                this.MyTrans.Rollback();
                if (this.MyConn.State == ConnectionState.Open)
                {
                    this.MyConn.Close();
                }
                throw exp;
            }
        }

        /// <summary>
        ///     RollBack transaccion y cerrar conexion
        /// </summary>
        public void AnularTransaccion()
        {
            try
            {
                this.MyTrans.Rollback();
                this.MyConn.Close();
            }
            catch (Exception exp)
            {
                this.MyTrans.Rollback();
                if (this.MyConn.State == ConnectionState.Open)
                {
                    this.MyConn.Close();
                }
                throw exp;
            }
        }
    }
}