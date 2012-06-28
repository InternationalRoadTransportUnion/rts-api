using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.WS.Common.Subscribers
{
    public enum CertStore
    {
        RTS,
        RTS_PLUS
    }

    public enum CertUsage
    {
        Client,
        Server
    }

    public class CertificatesStore
    {
        static public X509Certificate2Collection GetCertificates(CertStore certStore, CertUsage certUsage)
        {
            X509Certificate2Collection res = new X509Certificate2Collection();

            ICertGetter cg = null;

            switch (certStore)
            {
                case CertStore.RTS:
                    switch (certUsage)
                    {
                        case CertUsage.Client:
                            cg = new RTSClientCertGetter();
                            break;
                        case CertUsage.Server:
                            cg = new RTSServerCertGetter();
                            break;
                    }
                    break;
                case CertStore.RTS_PLUS:
                    switch (certUsage)
                    {
                        case CertUsage.Client:
                            cg = new RTSPlusClientCertGetter();
                            break;
                        case CertUsage.Server:
                            cg = new RTSPlusServerCertGetter();
                            break;
                    }
                    break;
            }

            if (cg != null)
                cg.GetCertificates(ref res);

            return res;
        }
    }
}
