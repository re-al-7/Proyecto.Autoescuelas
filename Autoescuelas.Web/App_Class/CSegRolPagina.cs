#region usings

using System.Collections;
using System.Data;
using System.Web.UI;
using Autoescuelas.Dal.Entidades;
using Autoescuelas.Dal.Modelo;
using Integrate.SisPlan.Dal.Entidades;

#endregion

namespace Autoescuelas.Web.App_Class
{
    /// <summary>
    /// Funciones para administracion de Roles y Paginas
    /// </summary>
    public class CSegRolPagina : Page
    {
        /// <summary>
        /// Funcion para obtener los Menus (menu vertical) que un usuario puede acceder
        /// </summary>
        /// <returns>DataTable con los elementos del menu</returns>
        public DataTable ObtenerMenu()
        {
            var miSesion = new CSessionHandler();

            var dtMenu = new DataTable();
            dtMenu.Columns.Add(EntSegPaginas.Fields.Paginaspg.ToString(), typeof(int));
            dtMenu.Columns.Add(EntSegPaginas.Fields.Aplicacionsap.ToString(), typeof(string));
            dtMenu.Columns.Add(EntSegPaginas.Fields.Nombrespg.ToString(), typeof(string));
            dtMenu.Columns.Add(EntSegPaginas.Fields.Nombremenuspg.ToString(), typeof(string));
            dtMenu.Columns.Add(EntSegPaginas.Fields.Descripcionspg.ToString(), typeof(string));

            DataRow drNew = dtMenu.NewRow();
            //drNew[EntSegPaginas.Fields.Paginaspg.ToString()] = 1;
            //drNew[EntSegPaginas.Fields.Aplicacionsap.ToString()] = "AUT";
            //drNew[EntSegPaginas.Fields.Nombrespg.ToString()] = "lstSegUsuariosRestriccion.aspx";
            //drNew[EntSegPaginas.Fields.Nombremenuspg.ToString()] = "Cambiar Rol Activo";
            //drNew[EntSegPaginas.Fields.Descripcionspg.ToString()] = "Cambiar Rol Activo";
            //dtMenu.Rows.Add(drNew);

            //drNew = dtMenu.NewRow();
            //drNew[EntSegPaginas.Fields.Paginaspg.ToString()] = 2;
            //drNew[EntSegPaginas.Fields.Aplicacionsap.ToString()] = "AUT";
            //drNew[EntSegPaginas.Fields.Nombrespg.ToString()] = "pagSegCambiarPassword.aspx";
            //drNew[EntSegPaginas.Fields.Nombremenuspg.ToString()] = "Cambiar Password";
            //drNew[EntSegPaginas.Fields.Descripcionspg.ToString()] = "Cambiar Password";
            //dtMenu.Rows.Add(drNew);

            switch (miSesion.AppUsuario.id_rol)
            {
                case 1: //Administrador
                    drNew = dtMenu.NewRow();
                    drNew[EntSegPaginas.Fields.Paginaspg.ToString()] = 3;
                    drNew[EntSegPaginas.Fields.Aplicacionsap.ToString()] = "AUT";
                    drNew[EntSegPaginas.Fields.Nombrespg.ToString()] = "abmUsuarios.aspx";
                    drNew[EntSegPaginas.Fields.Nombremenuspg.ToString()] = "Usuarios";
                    drNew[EntSegPaginas.Fields.Descripcionspg.ToString()] = "Usuarios";
                    dtMenu.Rows.Add(drNew);

                    //drNew = dtMenu.NewRow();
                    //drNew[EntSegPaginas.Fields.Paginaspg.ToString()] = 4;
                    //drNew[EntSegPaginas.Fields.Aplicacionsap.ToString()] = "AUT";
                    //drNew[EntSegPaginas.Fields.Nombrespg.ToString()] = "abmSegUsuariosRestriccion.aspx";
                    //drNew[EntSegPaginas.Fields.Nombremenuspg.ToString()] = "Roles de Usuario";
                    //drNew[EntSegPaginas.Fields.Descripcionspg.ToString()] = "Roles de Usuario";
                    //dtMenu.Rows.Add(drNew);

                    drNew = dtMenu.NewRow();
                    drNew[EntSegPaginas.Fields.Paginaspg.ToString()] = 11;
                    drNew[EntSegPaginas.Fields.Aplicacionsap.ToString()] = "AUT";
                    drNew[EntSegPaginas.Fields.Nombrespg.ToString()] = "abmAutoescuelas.aspx";
                    drNew[EntSegPaginas.Fields.Nombremenuspg.ToString()] = "AutoEscuelas";
                    drNew[EntSegPaginas.Fields.Descripcionspg.ToString()] = "AutoEscuelas";
                    dtMenu.Rows.Add(drNew);
                    
                    drNew = dtMenu.NewRow();
                    drNew[EntSegPaginas.Fields.Paginaspg.ToString()] = 12;
                    drNew[EntSegPaginas.Fields.Aplicacionsap.ToString()] = "AUT";
                    drNew[EntSegPaginas.Fields.Nombrespg.ToString()] = "abmSucursal.aspx";
                    drNew[EntSegPaginas.Fields.Nombremenuspg.ToString()] = "Sucursales";
                    drNew[EntSegPaginas.Fields.Descripcionspg.ToString()] = "Sucursales";
                    dtMenu.Rows.Add(drNew);

                    drNew = dtMenu.NewRow();
                    drNew[EntSegPaginas.Fields.Paginaspg.ToString()] = 13;
                    drNew[EntSegPaginas.Fields.Aplicacionsap.ToString()] = "AUT";
                    drNew[EntSegPaginas.Fields.Nombrespg.ToString()] = "abmVehiculo.aspx";
                    drNew[EntSegPaginas.Fields.Nombremenuspg.ToString()] = "Vehiculos";
                    drNew[EntSegPaginas.Fields.Descripcionspg.ToString()] = "Vehiculos";
                    dtMenu.Rows.Add(drNew);

                    drNew = dtMenu.NewRow();
                    drNew[EntSegPaginas.Fields.Paginaspg.ToString()] = 14;
                    drNew[EntSegPaginas.Fields.Aplicacionsap.ToString()] = "AUT";
                    drNew[EntSegPaginas.Fields.Nombrespg.ToString()] = "abmExamen.aspx";
                    drNew[EntSegPaginas.Fields.Nombremenuspg.ToString()] = "Examenes";
                    drNew[EntSegPaginas.Fields.Descripcionspg.ToString()] = "Examenes";
                    dtMenu.Rows.Add(drNew);
                    break;
                case 2: //Instructor
                    drNew = dtMenu.NewRow();
                    drNew[EntSegPaginas.Fields.Paginaspg.ToString()] = 13;
                    drNew[EntSegPaginas.Fields.Aplicacionsap.ToString()] = "AUT";
                    drNew[EntSegPaginas.Fields.Nombrespg.ToString()] = "abmVehiculo.aspx";
                    drNew[EntSegPaginas.Fields.Nombremenuspg.ToString()] = "Vehiculos";
                    drNew[EntSegPaginas.Fields.Descripcionspg.ToString()] = "Vehiculos";
                    dtMenu.Rows.Add(drNew);

                    drNew = dtMenu.NewRow();
                    drNew[EntSegPaginas.Fields.Paginaspg.ToString()] = 14;
                    drNew[EntSegPaginas.Fields.Aplicacionsap.ToString()] = "AUT";
                    drNew[EntSegPaginas.Fields.Nombrespg.ToString()] = "abmExamen.aspx";
                    drNew[EntSegPaginas.Fields.Nombremenuspg.ToString()] = "Examenes";
                    drNew[EntSegPaginas.Fields.Descripcionspg.ToString()] = "Examenes";
                    dtMenu.Rows.Add(drNew);
                    break;
            }


            //Creamos el diccionario con las reglas
            var arrPags = new ArrayList();
            foreach (DataRow dr in dtMenu.Rows)
                arrPags.Add(dr[EntSegPaginas.Fields.Nombrespg.ToString()].ToString());

            miSesion.DtMenu = dtMenu;
            miSesion.ArrMenu = arrPags;
            return dtMenu;
        }
    }
}