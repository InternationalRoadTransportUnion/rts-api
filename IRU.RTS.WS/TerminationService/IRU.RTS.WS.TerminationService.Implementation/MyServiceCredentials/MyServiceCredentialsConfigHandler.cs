using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace IRU.RTS.WS.TerminationService.Implementation.MyServiceCredentials
{
    public class MyServiceCredentialsConfigHandler : ServiceCredentialsElement
    {
        ConfigurationPropertyCollection properties;
        private const string KDefaultServiceCertificateThumbprintPropertyName = "defaultServiceCertificateThumbprint";

        public override Type BehaviorType
        {
            get { return typeof(MyServiceCredentials); }
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
                        ConfigurationPropertyOptions.IsRequired));
                    this.properties = properties;
                }
                return this.properties;
            }
        }

        protected override object CreateBehavior()
        {
            MyServiceCredentials creds = new MyServiceCredentials(DefaultServiceCertificateThumbprint);
            base.ApplyConfiguration(creds);
            return creds;
        }
    }
}
