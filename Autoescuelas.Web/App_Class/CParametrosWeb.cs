#region usings

#endregion

namespace Autoescuelas.Web.App_Class
{
    /// <summary>
    /// Parametros para determinar el comportamiento
    /// </summary>
    public static class CParametrosWeb
    {
        #region Nombre del Sistema
        /// <summary>
        /// Nombre de la institución
        /// </summary>
        //public static string StrNombreInstitucion = "SEGIP";
        public static string StrNombreInstitucion = "SEGIP";

        /// <summary>
        /// Nombre del sistema SIPPEC
        /// </summary>
        //public static string StrNombreSistema = "SIPPEC";
        public static string StrNombreSistema = "Autoescuelas";

        /// <summary>
        /// Nombre de la pagina
        /// </summary>
        public static string StrNombrePagina = StrNombreSistema + " - " + (StrNombreInstitucion == "" ? "INTEGRATE" : StrNombreInstitucion);

        /// <summary>
        /// Nombre del sistema en la PAgina de Ingreso al sistema
        /// </summary>
        public static string StrPantallaInicio = StrNombreSistema + " - " + (StrNombreInstitucion == "" ? "INTEGRATE" : StrNombreInstitucion);

        #endregion

        #region Parametros de comportamiento del sistema
        /// <summary>
        /// Parametro para determinar si se captura la pantalla en caso de error
        /// </summary>
        public static bool BCapturaPantallaError = true;

        /// <summary>
        /// Parametro para determinar si se crea el Issue en BitBucket
        /// </summary>
        public static bool BIssuesOnBitBucket = true;

        /// <summary>
        /// Parametro para determinar si se crea el Issue en GitLab
        /// </summary>
        public static bool BIssuesOnGitLab = false;

        /// <summary>
        /// Parametro que determina los roles que pueden acceder a la pantalla de detalles en el sistema
        /// </summary>
        public static int[] RolesDetalles = {};
        #endregion

        #region KB: Parametros SharpBucket: https://github.com/MitjaBezensek/SharpBucket
        /// <summary>
        /// Cuenta del repositorio en BitBucket
        /// </summary>
        public static string StrBitBucketAccount = "integrate_bo";

        /// <summary>
        /// ConsumerKey para acceder al servicio de notificacion de BotBucket
        /// </summary>
        public static string StrBitBucketConsumerKey = "qQfjKGmxhJgRbJHkpb";

        /// <summary>
        /// ConsumerSecretKey para acceder al servicio de notificacion de BotBucket
        /// </summary>
        public static string StrBitBucketconsumerSecretKey = "yeN99q4DtUWKgnWznz8mQ6bD4VK7dhGw";

        /// <summary>
        /// Repositorio en BitBucket
        /// </summary>
        public static string StrBitBucketRepository = "integrate.sisplan";
        #endregion

        #region Parametros Imgur
        /// <summary>
        /// ClientId para subir las imagenes a Imgur 
        /// </summary>
        public static string StrImgurClientId = "4f4d43e26c9ddd0";
        #endregion

        #region  Parametros Mision, Vision
        /// <summary>
        /// Texto que contiene la Mision institucional
        /// </summary>
        public static string StrMision =
            "Otorgar a través de su registro, identificación a los bolivianos y bolivianas  que residen dentro y fuera del Estado Plurinacional de Bolivia y a personas con permanencia legal en el país, para el ejercicio pleno de sus derechos, desarrollando soluciones integrales, mediante la prestación de servicios con calidad y calidez empleando tecnología de última generación.";

        /// <summary>
        /// Texto que contiene la Vision institucional
        /// </summary>
        public static string StrVision =
            "Lograr una Bolivia libre de indocumentados que accede a sus derechos consagrados en la Constitución Política del Estado a través de su identidad y posicionar el Registró Único de Identificación Biométrico como la base  primaria referencial de información, para el conjunto de las entidades del país.";
        #endregion

        #region  Parametros PDES
        /// <summary>
        /// Identificador de la Agenda patriotica
        /// </summary>
        public static string StrPdesIdAgePat = "11";

        /// <summary>
        /// Identificador de la Accion del PDES
        /// </summary>
        public static string StrPdesCodAccion = "19";

        /// <summary>
        /// Identificador de la Meta del PDES
        /// </summary>
        public static string StrPdesCodMeta = "M1";

        /// <summary>
        /// Identificador de la Resultado del PDES
        /// </summary>
        public static string StrPdesCodResultado = "299";
    #endregion

        
    }
}