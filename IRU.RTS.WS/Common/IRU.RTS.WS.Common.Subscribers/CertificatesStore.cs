using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

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
            using (CacheManagerFactory cmf = new CacheManagerFactory())
            {
                ICacheManager icm = cmf.Create("RtsCache");

                string sCacheKey = String.Format("Certificates#{0}#{1}", certStore, certUsage);
                X509Certificate2Collection res = (X509Certificate2Collection)icm.GetData(sCacheKey);

                if (res == null)
                {
                    res = new X509Certificate2Collection();
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
                            cg = new RTSPlusCertGetter(certUsage);
                            break;
                    }

                    if (cg != null)
                        cg.GetCertificates(ref res);

                    icm.Add(sCacheKey, res, CacheItemPriority.Normal, null, new SlidingTime(TimeSpan.FromMinutes(5)));
                }

                return res;
            }
        }

        static public void AddCertificate(CertStore certStore, CertUsage certUsage, string subscriberId, X509Certificate2 certificate)
        {
            using (CacheManagerFactory cmf = new CacheManagerFactory())
            {
                ICacheManager icm = cmf.Create("RtsCache");

                string sCacheKey = String.Format("Certificates#{0}#{1}", certStore, certUsage);

                if (certStore == CertStore.RTS_PLUS)
                {
                    ICertAdder ica = new RTSPlusCertAdder(certUsage, subscriberId);
                    ica.AddCertificate(certificate);
                    icm.Remove(sCacheKey);
                }
                else
                    throw new NotSupportedException("Only the RTS+ Store is supported.");
            }
        }

        static public void ActivateCertificate(CertStore certStore, CertUsage certUsage, string subscriberId, X509Certificate2 certificate)
        {
            using (CacheManagerFactory cmf = new CacheManagerFactory())
            {
                ICacheManager icm = cmf.Create("RtsCache");

                string sCacheKey = String.Format("Certificates#{0}#{1}", certStore, certUsage);

                if (certStore == CertStore.RTS_PLUS)
                {
                    ICertActivator ica = new RTSPlusCertActivator(certUsage, subscriberId);
                    ica.ActivateCertificate(certificate);
                    icm.Remove(sCacheKey);
                }
                else
                    throw new NotSupportedException("Only the RTS+ Store is supported.");
            }
        }

        static public void DeactivateCertificate(CertStore certStore, CertUsage certUsage, string subscriberId, X509Certificate2 certificate)
        {
            using (CacheManagerFactory cmf = new CacheManagerFactory())
            {
                ICacheManager icm = cmf.Create("RtsCache");

                string sCacheKey = String.Format("Certificates#{0}#{1}", certStore, certUsage);

                if (certStore == CertStore.RTS_PLUS)
                {
                    ICertDeactivator ica = new RTSPlusCertDeactivator(certUsage, subscriberId);
                    ica.DeactivateCertificate(certificate);
                    icm.Remove(sCacheKey);
                }
                else
                    throw new NotSupportedException("Only the RTS+ Store is supported.");
            }
        }
    }
}
