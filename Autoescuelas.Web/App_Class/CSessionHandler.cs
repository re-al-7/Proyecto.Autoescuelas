#region usings

using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using Autoescuelas.Dal.Entidades;

#endregion

namespace Autoescuelas.Web.App_Class
{
    /// <summary>
    /// Helper para administrar la sesión del usuario
    /// </summary>
    public class CSessionHandler : Page
    {
        
        /// <summary>
        /// Usuario logeado al sistema
        /// </summary>
        public EntUsuArio AppUsuario
        {
            get
            {
                try
                {
                    var sessionData = (EntUsuArio) Session["SegUsuario"];
                    return sessionData;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set { Session["SegUsuario"] = value; }
        }

        /// <summary>
        /// Arraylist para verificacion de accesos del usuario a los menus
        /// </summary>
        public ArrayList ArrMenu
        {
            get
            {
                try
                {
                    var sessionData = (ArrayList) Session["arrMenu"];
                    return sessionData;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set { Session["arrMenu"] = value; }
        }

        /// <summary>
        /// Excepcion vigente en el sistema
        /// </summary>
        public Exception CurrentException
        {
            get
            {
                try
                {
                    var sessionData = (Exception) Session["currentException"];
                    return sessionData;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set { Session["currentException"] = value; }
        }

        /// <summary>
        /// DataTable del Menu Vertical del Sistema
        /// </summary>
        public DataTable DtMenu
        {
            get
            {
                try
                {
                    var sessionData = (DataTable) Session["dtMenu"];
                    return sessionData;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set { Session["dtMenu"] = value; }
        }

        /// <summary>
        /// Modulo activo que define el menu vertical del sistema
        /// </summary>
        public string StrModuloActivo
        {
            get
            {
                try
                {
                    var strModulo = Session["strModuloActivo"]?.ToString();
                    return strModulo;
                }
                catch (Exception)
                {
                    return "";
                }
            }
            set { Session["strModuloActivo"] = value; }
        }

        /// <summary>
        /// Funcion para eliminar las variables de sesion
        /// </summary>
        public void SessionClear()
        {
            StrModuloActivo = null;
            DtMenu = null;
            ArrMenu = null;
            AppUsuario = null;
            CurrentException = null;
        }
    }
}