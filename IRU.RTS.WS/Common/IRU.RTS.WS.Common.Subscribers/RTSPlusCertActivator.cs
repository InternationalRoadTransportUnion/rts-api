using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using IRU.Common.EnterpriseLibrary.Data;
using IRU.RTS.WS.Common.Data.WsSubscriber;

namespace IRU.RTS.WS.Common.Subscribers
{
    public class RTSPlusCertActivator: ICertActivator
    {
        private CertUsage _certUsage;
        private string _subscriberId;

        public RTSPlusCertActivator(CertUsage certUsage, string subscriberId)
        {
            _certUsage = certUsage;
            _subscriberId = subscriberId;

            UserId = null;
        }

        #region ICertActivator Members

        public string UserId { get; set; }

        public void ActivateCertificate(X509Certificate2 certificate)
        {
            if (certificate == null)
                throw new ArgumentNullException("certificate");

            using (DbQueries dq = new DbQueries())
            {
                try
                {
                    bool serverCert = false;

                    switch (_certUsage)
                    {
                        case CertUsage.Client:
                            serverCert = false;
                            break;
                        case CertUsage.Server:
                            if (certificate.PrivateKey == null)
                                throw new NullReferenceException(String.Format("A private key must be set for Certificate [{0}].", certificate.Thumbprint));
                            serverCert = true;
                            break;
                    }

                    dq.ActivateRtsplusSignatureKeyForSubscriber(UserId, _subscriberId, certificate.Thumbprint, serverCert);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Can not activate RTS+ {0} Certificate [{1}].", _certUsage, certificate.Thumbprint), ex);
                }
            }
        }

        #endregion
    }
}
