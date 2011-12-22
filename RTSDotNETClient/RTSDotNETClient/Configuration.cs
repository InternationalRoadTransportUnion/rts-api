using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RTSDotNETClient
{
    public class Configuration : ConfigurationSection
    {
        [ConfigurationProperty("TraceEnabled", DefaultValue = "false", IsRequired = false)]
        public Boolean TraceEnabled
        {
            get { return (Boolean)this["TraceEnabled"]; }
            set { this["TraceEnabled"] = value; }
        }
        [ConfigurationProperty("XsdValidationEnabled", DefaultValue = "true", IsRequired = false)]
        public Boolean XsdValidationEnabled
        {
            get { return (Boolean)this["XsdValidationEnabled"]; }
            set { this["XsdValidationEnabled"] = value; }
        }
        [ConfigurationProperty("TirCarnetQueryWSUrl", DefaultValue = "", IsRequired = false)]
        public string TirCarnetQueryWSUrl
        {
            get { return (string)this["TirCarnetQueryWSUrl"]; }
            set { this["TirCarnetQueryWSUrl"] = value; }
        }
        [ConfigurationProperty("ReconciliationWSUrl", DefaultValue = "", IsRequired = false)]
        public string ReconciliationWSUrl
        {
            get { return (string)this["ReconciliationWSUrl"]; }
            set { this["ReconciliationWSUrl"] = value; }
        }
        [ConfigurationProperty("SafeTirUploadWSUrl", DefaultValue = "", IsRequired = false)]
        public string SafeTirUploadWSUrl
        {
            get { return (string)this["SafeTirUploadWSUrl"]; }
            set { this["SafeTirUploadWSUrl"] = value; }
        }
    }
}
