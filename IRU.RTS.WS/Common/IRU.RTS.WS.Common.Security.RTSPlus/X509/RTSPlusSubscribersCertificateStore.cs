using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using IRU.RTS.WS.Common.Subscribers;

namespace IRU.RTS.WS.Common.Security.RTSPlus.X509
{
    public class RTSPlusSubscribersCertificateStore
    {
        public X509Certificate2 GetValidClientCertificate(string thumbprint)
        {
            // get certificate from SUBSCRIBER db Store:
            X509Certificate2Collection certsCli = CertificatesStore.GetCertificates(CertStore.RTS_PLUS, CertUsage.Client);
            X509Certificate2Collection currentCertsCli = certsCli.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection certsCliMatching = currentCertsCli.Find(X509FindType.FindByThumbprint, thumbprint, false);

            if (certsCliMatching.Count == 1)
                return certsCliMatching[0];
            else
                return null;
        }

        public string GetClientSubscriberId(string thumbprint)
        {
            X509Certificate2 x2c = GetValidClientCertificate(thumbprint);
            if (x2c != null)
                return x2c.SubscriberId();
            else
                return null;
        }

        public bool IsValidClientCertificate(string thumbprint)
        {
            // get certificate from SUBSCRIBER db Store:
            return GetValidClientCertificate(thumbprint) != null;
        }

        public string GetServiceCertificateThumbprintFromClientOne(string clientThumbprint)
        {
            // get certificate from SUBSCRIBER db Store:
            X509Certificate2 certCli = GetValidClientCertificate(clientThumbprint);
            if (certCli != null)
            {
                X509Certificate2Collection certsSrv = CertificatesStore.GetCertificates(CertStore.RTS_PLUS, CertUsage.Server);
                X509Certificate2Collection certsSrvPaired = certsSrv.FindBySubscriberId(certCli.SubscriberId());
                if (certsSrvPaired.Count == 1)
                {
                    return certsSrvPaired[0].Thumbprint;
                }
            }
            return null;
        }

        public X509Certificate2 GetServiceCertificateFromStore (string certificateThumbprint)
        {
            // get certificate from SUBSCRIBER db Store:
            X509Certificate2Collection certsSrv = CertificatesStore.GetCertificates(CertStore.RTS_PLUS, CertUsage.Server);
            X509Certificate2Collection currentCertsSrv = certsSrv.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection certsSrvMatching = currentCertsSrv.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);

            if (certsSrvMatching.Count == 1)
                return certsSrvMatching[0];
            else
                return null;
        }
    }
}
