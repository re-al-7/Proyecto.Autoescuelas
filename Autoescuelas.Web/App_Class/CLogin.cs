#region usings

using System;
using System.Collections;
using System.Web.UI.WebControls;
using Autoescuelas.Dal;
using Autoescuelas.Dal.Entidades;
using Autoescuelas.Dal.Modelo;

#endregion

namespace Autoescuelas.Web.App_Class
{
    /// <summary>
    /// Clase utilizada para la verificación del usuario en la pantalla de ingreso al sistema
    /// </summary>
    public static class CLogin
    {
        /// <summary>
        /// FUncion para autenticar al usuario
        /// </summary>
        /// <param name="strValidacion">Texto de validación</param>
        /// <param name="strUsuario">Nombre de Usuario</param>
        /// <param name="strPass">Contraseña del usuario</param>
        /// <param name="miUsuario">Objeto del Tipo EntSegUsuarios que contiene toda la información del Usuario</param>
        /// <param name="miUsuarioRestriccion">Objeto del Tipo EntSegUsuariosrestriccion que contiene la informacion de la restriccion del usuario</param>
        /// <param name="miRol">Objeto del Tipo EntSegRoles que contiene la información del Rol activo</param>
        /// <param name="miPersona">Objeto del Tipo EntPerFichapersona que contiene la información personal del usuario</param>
        /// <returns>Retorna si el usuario ha sido autenticado o no</returns>
        public static bool AutenticarUsuario(ref string strValidacion, string strUsuario, string strPass, ref EntUsuArio miUsuario)
        {

            try
            {
                var rnUsuarioVerificacion = new RnUsuArio();
                var eUsuario = rnUsuarioVerificacion.ObtenerObjeto(EntUsuArio.Fields.login,
                     "'" + strUsuario + "'");
                if (eUsuario != null)
                {
                    if (eUsuario.password.ToUpper() == cFuncionesEncriptacion.generarMD5(strPass).ToUpper())
                    {

                        miUsuario = eUsuario;
                        return true;

                    }
                    else
                        strValidacion = "La contraseña ingresada no es correcta.";
                }
                else
                    strValidacion = "El Usuario no existe";
            }
            catch (Exception exp)
            {
                strValidacion = exp.Message;
                //errores_control1.cargar("Error", UtilErrores.Tipos.critico, exp);
            }

            return false;
        }

        /// <summary>
        /// Validacion de la información introducida al Sistema Web
        /// </summary>
        /// <param name="strValidacion">Texto que contiene el mensaje de error</param>
        /// <param name="txtUsuario">Objeto del tipo TextBox que representa al control de nombre de usuario del formulario de ingreso al sistema</param>
        /// <param name="txtPass">Objeto del tipo TextBox que representa al control de contraseña del formulario de ingreso al sistema</param>
        /// <returns></returns>
        public static bool WebValidarUsuario(ref string strValidacion, ref TextBox txtUsuario,
            ref TextBox txtPass)
        {
            var bProcede = true;
            
            //Validamos el Login
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                strValidacion = Environment.NewLine + "Debe ingresar el Nombre de Usuario";
                bProcede = false;
                txtUsuario.Focus();
            }
            //Validamos el Password
            if (string.IsNullOrEmpty(txtPass.Text))
            {
                strValidacion = Environment.NewLine + "Debe ingresar su Contraseña";
                bProcede = false;
                txtPass.Focus();
            }

            return bProcede;
        }
    }
}