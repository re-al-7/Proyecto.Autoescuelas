// // <copyright file="Dashboard.aspx.cs" company="INTEGRATE - Soluciones Informaticas">
// // Copyright (c) 2016 Todos los derechos reservados
// // </copyright>
// // <author>ReAl </author>
// // <date>2016-09-24 8:40 p. m.</date>

#region usings

using System;
using System.Web.UI;
using Autoescuelas.Dal.Entidades;
using Autoescuelas.Dal.Modelo;
using Autoescuelas.Web.App_Class;

#endregion

namespace Autoescuelas.Web.Template
{
    public partial class Dashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Titulo de Pagina
                Title = CParametrosWeb.StrNombrePagina;

                var miSession = new CSessionHandler();
                var rnEx = new RnExaMen();
                litEvaluaciones.Text = rnEx
                    .FuncionesCount(EntExaMen.Fields.id_sucursal, EntExaMen.Fields.id_sucursal, miSession.AppUsuario.id_sucursal).ToString();

                var rnSuc = new RnSucUrsal();
                var objSuc = rnSuc.ObtenerObjeto(miSession.AppUsuario.id_sucursal);

                var rnVe = new RnVehIculos();
                LitVehiculos.Text = rnVe
                    .FuncionesCount(EntVehIculos.Fields.id_autoescuelas, EntVehIculos.Fields.id_autoescuelas, objSuc.id_autoescuelas).ToString();
            }
            catch (Exception exp)
            {
                ((Main)Master).MostrarPopUp(this, exp);
            }
        }
    }
}