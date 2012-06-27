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
            string serviceCertSubjectName = null;

            if (subscriberIdentityName.Contains("IRU.Common.Messaging from@somewhere.com (test)"))
            {
                serviceCertSubjectName = "IRU.Common.Messaging to@somewhere.com (test)";
            }

            if (serviceCertSubjectName == null)
            {
                return null;
            }

            // Get the certificate store for the current user.
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;
                // If using a certificate with a trusted root you do not need to FindByTimeValid, instead:
                // currentCerts.Find(X509FindType.FindBySubjectDistinguishedName, certName, true);
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySubjectName, serviceCertSubjectName, false);
                if (signingCert.Count == 0)
                    return null;
                // Return the first certificate in the collection, has the right name and is current.
                return signingCert[0];
            }
            finally
            {
                store.Close();
            }
        }

    }
}
