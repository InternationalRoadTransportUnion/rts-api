using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace IRU.RTS.WS.Common.Security.RTSPlus.X509
{
    public class RTSPlusSecurityBehaviorExtension : ServiceCredentialsElement
    {
        ConfigurationPropertyCollection properties;
        private const string KDefaultServiceCertificateThumbprintPropertyName = "defaultServiceCertificateThumbprint";

        public override Type BehaviorType
        {
            get { return typeof(RTSPlusServiceCredentials); }
        }
        
        public string DefaultServiceCertificateThumbprint
        {
            get { return (string)base[KDefaultServiceCertificateThumbprintPropertyName]; }
            set
            {
                base[KDefaultServiceCertificateThumbprintPropertyName] = value;
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                if (this.properties == null)
                {
                    ConfigurationPropertyCollection properties = base.Properties;
                    properties.Add(new ConfigurationProperty(
                        KDefaultServiceCertificateThumbprintPropertyName,
                        typeof(String),
                        null,
                        null,
                        null,
                        ConfigurationPropertyOptions.None));
                    this.properties = properties;
                }
                return this.properties;
            }
        }

        protected override object CreateBehavior()
        {
            RTSPlusServiceCredentials creds = new RTSPlusServiceCredentials(DefaultServiceCertificateThumbprint);
            base.ApplyConfiguration(creds);
            return creds;
        }
    }
}
