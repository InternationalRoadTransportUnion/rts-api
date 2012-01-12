using System;

namespace RTSDotNETClient
{

    /// <summary>
    /// Global static class to store global variables
    /// </summary>
    public class Global
    {
        /// <summary>
        /// Trace enabled or not (default is false)
        /// </summary>
        public static bool TraceEnabled { get; set; }

        /// <summary>
        /// Performs or not the xsd validation of the queries and responses (default is false) 
        /// </summary>
        public static bool XsdValidationDisabled { get; set; }

        /// <summary>
        /// The url of the web service Tir Carnet Holder Query 
        /// </summary>
        public static string TirCarnetQueryWSUrl { get; set; }

        /// <summary>
        /// The url of the reconciliation web service 
        /// </summary>
        public static string ReconciliationWSUrl { get; set; }

        /// <summary>
        /// The url of the SafeTir Upload web service 
        /// </summary>
        public static string SafeTirUploadWSUrl { get; set; }

        /// <summary>
        /// Loads the RTSDotNETClient configuration from the configuration file (.config file)
        /// </summary>
        public static void LoadConfiguration()
        {
            LoadConfiguration("RTSDotNETClient");
        }

        /// <summary>
        /// Loads the RTSDotNETClient configuration from the configuration file (.config file)
        /// </summary>
        /// <param name="sectionName">The name of the RTSDotNETClient section in the .config file</param>
        public static void LoadConfiguration(string sectionName)
        {
            Configuration config = (Configuration)System.Configuration.ConfigurationManager.GetSection(sectionName);
            if (config == null)            
                return;
            Global.TraceEnabled = config.TraceEnabled;
            Global.TirCarnetQueryWSUrl = config.TirCarnetQueryWSUrl;
            Global.ReconciliationWSUrl = config.ReconciliationWSUrl;
            Global.SafeTirUploadWSUrl = config.SafeTirUploadWSUrl;
            Global.XsdValidationDisabled = config.XsdValidationDisabled;
        }

        /// <summary>
        /// The class static constructor
        /// </summary>
        static Global()
        {
            TraceEnabled = false;
            XsdValidationDisabled = false;
            TirCarnetQueryWSUrl = "";
            ReconciliationWSUrl = "";
            SafeTirUploadWSUrl = "";
        }

        /// <summary>
        /// Writes a trace
        /// </summary>
        /// <param name="str">The message to write</param>
        internal static void Trace(string str)
        {
            System.Diagnostics.Trace.WriteLineIf(TraceEnabled,"["+DateTime.Now.ToString("dd/MM/yyy HH:mm:ss") + "] " + str);
        }

 
    }
}
