using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.WS.Common.Subscribers
{
    public static class X509Certificate2RTSExtension
    {
        public static string SubscriberId(this X509Certificate2 certificate)
        {
            return certificate.FriendlyName;
        }

        public static X509Certificate2Collection FindBySubscriberId(this X509Certificate2Collection certificates, string subscriberId)
        {
            X509Certificate2Collection res = new X509Certificate2Collection();

            foreach (X509Certificate2 cert in certificates)
            {
                if (String.Equals(subscriberId, cert.SubscriberId(), StringComparison.InvariantCultureIgnoreCase))
                    res.Add(cert);
            }

            return res;
        }
    }
}
