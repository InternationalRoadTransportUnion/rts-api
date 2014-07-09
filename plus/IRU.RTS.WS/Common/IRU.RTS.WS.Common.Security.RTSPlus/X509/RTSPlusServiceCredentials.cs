using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using System.Reflection;

namespace IRU.RTS.WS.Common.Security.RTSPlus.X509
{
    public class RTSPlusServiceCredentials : ServiceCredentials
    {
        string _defaultServiceCertificateThumbprint;

        public RTSPlusServiceCredentials(string defaultServiceCertificateThumbprint)
        {
            // set default Service certificate

            if (defaultServiceCertificateThumbprint == null)
            {                
                X509Certificate2 x5c = new X509Certificate2();
                Assembly asm = Assembly.GetExecutingAssembly();
                byte[] abCert;
                using (Stream sm = asm.GetManifestResourceStream(asm.GetName().Name + "." + "X509.Certificates.rtsplus_default.pfx"))
                {
                    abCert = new byte[sm.Length];
                    sm.Read(abCert, 0, abCert.Length);
                }                 
                x5c.Import(abCert, "pass-1234", X509KeyStorageFlags.Exportable);
                this.ServiceCertificate.Certificate = x5c;
            }
            else
            {
                // get it from SUBSCRIBER database
                RTSPlusSubscribersCertificateStore subscribersCertificateStore = new RTSPlusSubscribersCertificateStore();
                this.ServiceCertificate.Certificate = subscribersCertificateStore.GetServiceCertificateFromStore(defaultServiceCertificateThumbprint);
            }
        }

        protected RTSPlusServiceCredentials(RTSPlusServiceCredentials other)
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
            return new RTSPlusServiceCredentialsSecurityTokenManager(this);
        }

        protected override ServiceCredentials CloneCore()
        {
            return new RTSPlusServiceCredentials(this);
        }
        
    }
}
