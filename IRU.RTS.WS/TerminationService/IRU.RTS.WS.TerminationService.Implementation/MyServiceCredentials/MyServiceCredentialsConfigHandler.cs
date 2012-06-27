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

        public override Type BehaviorType
        {
            get { return typeof(MyServiceCredentials); }
        }

        public bool UseSubscriberCertificateDatabase
        {
            get { return (bool)base["useSubscriberCertificateDatabase"]; }
            set
            {
                base["useSubscriberCertificateDatabase"] = value;
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
                        "useSubscriberCertificateDatabase",
                        typeof(Boolean),
                        false,
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
            MyServiceCredentials creds = new MyServiceCredentials();
            creds.UseSubscriberCertificateDatabase = UseSubscriberCertificateDatabase;
            base.ApplyConfiguration(creds);
            return creds;
        }
    }
}
