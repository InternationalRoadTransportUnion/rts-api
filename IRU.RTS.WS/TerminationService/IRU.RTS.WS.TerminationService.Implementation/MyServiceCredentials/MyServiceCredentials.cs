using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace IRU.RTS.WS.TerminationService.Implementation.MyServiceCredentials
{
    public class MyServiceCredentials : ServiceCredentials
    {
        string _defaultServiceCertificateThumbprint;

        public MyServiceCredentials(string defaultServiceCertificateThumbprint)
        {
            // set default Service certificate

            if (defaultServiceCertificateThumbprint == null)
            {
                throw new ArgumentNullException("defaultServiceCertificateThumbprint");
            }
            // get it from SUBSCRIBER database
            MySubscribersCertificateStore mySubscribersCertificateStore = new MySubscribersCertificateStore();
            this.ServiceCertificate.Certificate = mySubscribersCertificateStore.GetServiceCertificateFromStore(defaultServiceCertificateThumbprint);
        }

        protected MyServiceCredentials(MyServiceCredentials other)
            : base(other)
        {
            this._defaultServiceCertificateThumbprint = other._defaultServiceCertificateThumbprint;
        }

        public string DefaultServiceCertificateThumbprint
        {
            get
            {
                return this._defaultServiceCertificateThumbprint;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this._defaultServiceCertificateThumbprint = value;
            }
        }

        public override SecurityTokenManager CreateSecurityTokenManager()
        {
            return new MyServiceCredentialsSecurityTokenManager(this);
        }

        protected override ServiceCredentials CloneCore()
        {
            return new MyServiceCredentials(this);
        }
        
    }
}
