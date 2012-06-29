using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.WS.TerminationService.Implementation.MyServiceCredentials
{
    public class MySubscribersCertificateStore
    {
        // CERT_SIGN_CLIENT_CERT Thumbprint:
        //  fd bd b9 e6 d2 81 b8 f4 96 f9 03 02 75 77 7f b6 eb 0b f9 1d
        // CERT_SIGN_SEVER_CERT Thumbprint:
        //  90 a9 fa 8b 31 18 da 54 0d bb b5 c3 cf 52 5f bb 5e 81 40 d5

        // IRU.Common.Messaging from@somewhere.com (test) Thumbprint:
        //  e0 ca ea 17 8d f2 1d b2 0a 48 29 6b db 3a 9e 74 de 7d b8 1e
        // IRU.Common.Messaging to@somewhere.com (test) Thumbprint:
        //  db 10 5d bd 75 42 00 af 76 91 0e e5 61 94 28 e4 c5 d3 6e 3b

        // Client & Service certificate pairs are matched by array index!
        //  i.e.: 1st cells from both arrays contain:
        //      CERT_SIGN_CLIENT_CERT Thumbprint and CERT_SIGN_SEVER_CERT Thumbprint
 
        // Clients certificates array:
        string[] myClientsCertificatesThumprints = new string[] { 
            "FDBDB9E6D281B8F496F9030275777FB6EB0BF91D", 
            "E0CAEA178DF21DB20A48296BDB3A9E74DE7DB81E" };

        // Services certificates array:
        string[] myServicesCertificatesThumprints = new string[] { 
            "90A9FA8B3118DA540DBBB5C3CF525FBB5E8140D5", 
            "DB105DBD754200AF76910EE5619428E4C5D36E3B" };

        public string[] ClientsCertificatesThumprints()
        {
            return myClientsCertificatesThumprints;
        }

        public string[] ServicesCertificatesThumprints()
        {
            return myServicesCertificatesThumprints;
        }

        public bool IsValidClientCertificate(string thumbprint)
        {
            return myClientsCertificatesThumprints.Contains(thumbprint);
        }

        public string GetServiceCertificateThumbprintFromClientOne(string clientThumbprint)
        {
            int i = myClientsCertificatesThumprints.ToList().IndexOf(clientThumbprint);
            return myServicesCertificatesThumprints[i];
        }

        public X509Certificate2 GetCertificateFromStore (string certificateThumbprint)
        {
            // Get the certificate from file:
            switch (certificateThumbprint)
            {
                case "90A9FA8B3118DA540DBBB5C3CF525FBB5E8140D5":
                    return new X509Certificate2(@"D:\TIR-EPD\Test\WCF-X509-WS-Security-Interop\SoapUI client\certificates\CERT_SIGN_SERVER_CERT.pfx", "1234");
                case "DB105DBD754200AF76910EE5619428E4C5D36E3B":
                    return new X509Certificate2(@"D:\TIR-EPD\Test\WCF-X509-WS-Security-Interop\SoapUI client\certificates\to@somewhere.com.pfx", "1234");
                default:
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
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);
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
