using System;

namespace RTSDotNETClient
{

    public class Global
    {
        public static bool TraceEnabled { get; set; }
        public static bool XsdValidationEnabled { get; set; }
        public static string TirCarnetQueryWSUrl { get; set; }
        public static string ReconciliationWSUrl { get; set; }
        public static string SafeTirUploadWSUrl { get; set; }

        public static void LoadConfiguration()
        {
            Configuration config = (Configuration)System.Configuration.ConfigurationManager.GetSection("RTSDotNETClient");
            if (config == null)            
                return;
            Global.TraceEnabled = config.TraceEnabled;
            Global.TirCarnetQueryWSUrl = config.TirCarnetQueryWSUrl;
            Global.ReconciliationWSUrl = config.ReconciliationWSUrl;
            Global.SafeTirUploadWSUrl = config.SafeTirUploadWSUrl;
            Global.XsdValidationEnabled = config.XsdValidationEnabled;
        }

        static Global()
        {
            TraceEnabled = false;
            XsdValidationEnabled = true;
            TirCarnetQueryWSUrl = "";
            ReconciliationWSUrl = "";
            SafeTirUploadWSUrl = "";
        }

        internal static void Trace(string str)
        {
            System.Diagnostics.Trace.WriteLineIf(TraceEnabled,"["+DateTime.Now.ToString("dd/MM/yyy HH:mm:ss") + "] " + str);
        }

 
    }
}
