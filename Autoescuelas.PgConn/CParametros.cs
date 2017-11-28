using System.Configuration;

namespace Autoescuelas.PgConn
{
    static public class CParametros
    {
        // DESARROLLO
        //public static bool bUseIntegratedSecurity = false;
        //public static string server = "10.0.0.5";
        //public static string user = "SIAPS";
        //public static string pass = "siaps2011";
        //public static string bd = "SIAPS";
        //public static string schema = "SIAPS.";

        //PRODUCCION SIAPS
        public static bool bChangeUserOnLogon ;
        public static bool bUseIntegratedSecurity ;
        public static string server ;
        public static string puerto ;
        public static string user ;
        public static string pass ;
        public static string schema ;
        public static string bd ;


        static CParametros()
        {
            bChangeUserOnLogon = false;
            bUseIntegratedSecurity = false;
            server = WcServer;
            user = WcUser;
            pass = WcPass;
            schema = "";
            bd = WcBd;
            puerto = WcPort;
        }

        //Valores desde el WebConfig
        public static string WcServer
        {
            get
            {
                return ConfigurationManager.AppSettings["serverPg"];
            }
        }
        public static string WcUser
        {
            get
            {
                return ConfigurationManager.AppSettings["userPg"];
            }
        }
        public static string WcPass
        {
            get
            {
                return ConfigurationManager.AppSettings["passPg"];
            }
        }
        public static string WcBd
        {
            get
            {
                return ConfigurationManager.AppSettings["bdPg"];
            }
        }
        public static string WcPort
        {
            get
            {
                return ConfigurationManager.AppSettings["puertoPg"];
            }
        }


        //Otros parametros
        public static string ParFormatoFechaHora = "dd/MM/yyyy HH:mm:ss.ffffff";
        public static string ParFormatoFecha = "dd/MM/yyyy";
    }
}
