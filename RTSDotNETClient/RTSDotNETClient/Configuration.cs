using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RTSDotNETClient
{
    /// <summary>
    /// The definition of the RTSDotNETClient configuration section
    /// </summary>
    public class Configuration : ConfigurationSection
    {
        /// <summary>
        /// Trace enabled or not (default is false)
        /// </summary>
        [ConfigurationProperty("TraceEnabled", DefaultValue = "false", IsRequired = false)]
        public Boolean TraceEnabled
        {
            get { return (Boolean)this["TraceEnabled"]; }
            set { this["TraceEnabled"] = value; }
        }

        /// <summary>
        /// Performs or not the xsd validation of the queries and responses (default is false) 
        /// </summary>
        [ConfigurationProperty("XsdValidationDisabled", DefaultValue = "false", IsRequired = false)]
        public Boolean XsdValidationDisabled
        {
            get { return (Boolean)this["XsdValidationDisabled"]; }
            set { this["XsdValidationDisabled"] = value; }
        }

        /// <summary>
        /// The url of the web service Tir Carnet Holder Query 
        /// </summary>
        [ConfigurationProperty("TirCarnetQueryWSUrl", DefaultValue = "", IsRequired = false)]
        public string TirCarnetQueryWSUrl
        {
            get { return (string)this["TirCarnetQueryWSUrl"]; }
            set { this["TirCarnetQueryWSUrl"] = value; }
        }

        /// <summary>
        /// The url of the reconciliation web service
        /// </summary>
        [ConfigurationProperty("ReconciliationWSUrl", DefaultValue = "", IsRequired = false)]
        public string ReconciliationWSUrl
        {
            get { return (string)this["ReconciliationWSUrl"]; }
            set { this["ReconciliationWSUrl"] = value; }
        }

        /// <summary>
        /// The url of the SafeTir Upload web service
        /// </summary>
        [ConfigurationProperty("SafeTirUploadWSUrl", DefaultValue = "", IsRequired = false)]
        public string SafeTirUploadWSUrl
        {
            get { return (string)this["SafeTirUploadWSUrl"]; }
            set { this["SafeTirUploadWSUrl"] = value; }
        }
    }
}
