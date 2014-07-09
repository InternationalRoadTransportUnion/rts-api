using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.IdentityModel.Tokens;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.WS.Common.Security.RTSPlus.X509
{
    internal class RTSPlusSecurityTokenProvider : SecurityTokenProvider
    {
        X509Certificate2 _defaultCertificate;

        public RTSPlusSecurityTokenProvider(X509Certificate2 defaultCertificate)
        {
            if (defaultCertificate == null)
            {
                throw new ArgumentNullException("defaultCertificate");
            }
            this._defaultCertificate = defaultCertificate;
        }

        protected override SecurityToken GetTokenCore(TimeSpan timeout)
        {
            X509SecurityToken result = null;
            if (ServiceSecurityContext.Current != null)
            {
                IIdentity subscriberIdentiy = ServiceSecurityContext.Current.PrimaryIdentity;
                if (subscriberIdentiy.IsAuthenticated && subscriberIdentiy.AuthenticationType == "X509")
                {
                    X509Certificate2 subscriberServerCertificate = GetServiceCertificateFromSubscriberStore(subscriberIdentiy.Name);
                    if (subscriberServerCertificate != null)
                    {
                        result = new X509SecurityToken(subscriberServerCertificate);
                    }
                }
            }

            if (result == null)
            {
                result = new X509SecurityToken(_defaultCertificate);
            }
            return result;
        }

        private static X509Certificate2 GetServiceCertificateFromSubscriberStore(string subscriberIdentityName)
        {
            RTSPlusSubscribersCertificateStore subscribersCertificateStore = new RTSPlusSubscribersCertificateStore();

            // MSDN Note: http://msdn.microsoft.com/en-us/library/system.servicemodel.servicesecuritycontext.primaryidentity(v=vs.90).aspx 
            // The primary identity is obtained from the credentials used to authenticate the current user. 
            // If the credential is an X.509 certificate, the identity is a concatenation of the subject name 
            // and the thumbprint (in that order). The subject name is separated from the thumbprint 
            // with a semicolon and a space. If the subject field of the certificate is null, 
            // the primary identity includes just a semicolon, a space, and the thumbprint.
            string clientCertThumbprint = subscriberIdentityName.Split(new string[] {"; "}, StringSplitOptions.None)[1];

            string serviceCertThumbprint = subscribersCertificateStore.GetServiceCertificateThumbprintFromClientOne(clientCertThumbprint);

            if (serviceCertThumbprint == null)
            {
                return null;
            }

            return subscribersCertificateStore.GetServiceCertificateFromStore(serviceCertThumbprint);
        }

    }
}
