#region usings

using System.Text;

#endregion

namespace Autoescuelas.Web.App_Class
{
    /// <summary>
    /// Helpers para establecer comportamiento Bootstrap en los formularios del sistema
    /// </summary>
    public static class CBootstrapModal
    {
        /// <summary>
        /// Muestra un modal con una imagen de exito de la operacion y refresca la pagina
        /// KB: https://nakupanda.github.io/bootstrap3-dialog/
        /// </summary>
        /// <returns>Bloque en javascrip</returns>
        public static string GetSuccessModalAndRefresh()
        {
            var strMensaje = new StringBuilder();
            strMensaje.AppendLine("var $textAndPic = $('<div></div>');");
            strMensaje.AppendLine(
                "$textAndPic.append('<img style=\"display: block; margin: 0 auto;\" src=\"../images/check.png\" />');");
            strMensaje.AppendLine("");
            strMensaje.AppendLine("BootstrapDialog.show({");
            strMensaje.AppendLine("    title: 'Accion realizada satisfactoriamente',");
            strMensaje.AppendLine("    message: $textAndPic,");
            strMensaje.AppendLine("    closable: false,");
            strMensaje.AppendLine("    draggable: true,");
            strMensaje.AppendLine("    cssClass: 'dialog-vertical-center',");
            strMensaje.AppendLine("    size: BootstrapDialog.SIZE_SMALL,");
            strMensaje.AppendLine("    type: BootstrapDialog.TYPE_SUCCESS,");
            strMensaje.AppendLine("    buttons: [{");
            strMensaje.AppendLine("        label: 'Aceptar',");
            strMensaje.AppendLine("        cssClass: 'btn-success',");
            strMensaje.AppendLine("        action: function(dialogItself){");
            strMensaje.AppendLine("            location.reload();");
            strMensaje.AppendLine("        }");
            strMensaje.AppendLine("    }]");
            strMensaje.AppendLine("});");

            return strMensaje.ToString();
        }
    }
}