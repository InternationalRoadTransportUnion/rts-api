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

namespace IRU.RTS.WS.TerminationService.Implementation.MyServiceCredentials
{
    internal class MyX509SecurityTokenProvider : SecurityTokenProvider
    {
        X509Certificate2 defaultCertificate;

        public MyX509SecurityTokenProvider(X509Certificate2 certificate)
        {
            this.defaultCertificate = certificate;
        }

        protected override SecurityToken GetTokenCore(TimeSpan timeout)
        {
            X509SecurityToken result = null;
            if (ServiceSecurityContext.Current != null)
            {
                IIdentity subscriberIdentiy = ServiceSecurityContext.Current.PrimaryIdentity;
                if (subscriberIdentiy.IsAuthenticated && subscriberIdentiy.AuthenticationType == "X509")
                {
                    X509Certificate2 mySubscriberServiceCertificate = GetServiceCertificateFromSubscriberStore(subscriberIdentiy.Name);
                    if (mySubscriberServiceCertificate != null)
                    {
                        result = new X509SecurityToken(mySubscriberServiceCertificate);
                    }
                }
            }

            if (result == null)
            {
                result = new X509SecurityToken(defaultCertificate);
            }
            return result;
        }

        private static X509Certificate2 GetServiceCertificateFromSubscriberStore(string subscriberIdentityName)
        {
            //TODO: implement using SUBSCRIBER database instead of Windows Certificate Store
            MySubscribersCertificateStore mySubscribersCertificateStore = new MySubscribersCertificateStore();

            // MSDN Note: http://msdn.microsoft.com/en-us/library/system.servicemodel.servicesecuritycontext.primaryidentity(v=vs.90).aspx 
            // The primary identity is obtained from the credentials used to authenticate the current user. 
            // If the credential is an X.509 certificate, the identity is a concatenation of the subject name 
            // and the thumbprint (in that order). The subject name is separated from the thumbprint 
            // with a semicolon and a space. If the subject field of the certificate is null, 
            // the primary identity includes just a semicolon, a space, and the thumbprint.
            string clientCertThumbprint = subscriberIdentityName.Split(new string[] {"; "}, StringSplitOptions.None)[1];

            string serviceCertThumbprint = mySubscribersCertificateStore.GetServiceCertificateThumbprintFromClientOne(clientCertThumbprint);

            if (serviceCertThumbprint == null)
            {
                return null;
            }

            return mySubscribersCertificateStore.GetCertificateFromStore(serviceCertThumbprint);
        }

    }
}
