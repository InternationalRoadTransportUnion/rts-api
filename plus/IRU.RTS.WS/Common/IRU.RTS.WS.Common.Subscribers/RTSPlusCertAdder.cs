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
    public class RTSPlusCertAdder: ICertAdder
    {
        private CertUsage _certUsage;
        private string _subscriberId;

        public RTSPlusCertAdder(CertUsage certUsage, string subscriberId)
        {
            _certUsage = certUsage;
            _subscriberId = subscriberId;

            UserId = null;
        }

        #region ICertAdder Members

        public string UserId { get; set; }

        public void AddCertificate(X509Certificate2 certificate)
        {
            if (certificate == null)
                throw new ArgumentNullException("certificate");

            using (DbQueries dq = new DbQueries())
            {
                try
                {
                    string sPrivKeyXml = null;

                    switch (_certUsage)
                    {
                        case CertUsage.Client:
                            break;
                        case CertUsage.Server:
                            if (certificate.PrivateKey == null)
                                throw new NullReferenceException(String.Format("A private key must be set for Certificate [{0}].", certificate.Thumbprint));
                            sPrivKeyXml = certificate.PrivateKey.ToXmlString(true);
                            break;
                    }

                    dq.InsertRtsplusSignatureKeyForSubscriber(UserId, _subscriberId, certificate.NotBefore, certificate.NotAfter, certificate.Thumbprint, certificate.Export(X509ContentType.Cert), sPrivKeyXml);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Can not insert RTS+ {0} Certificate [{1}].", _certUsage, certificate.Thumbprint), ex);
                }
            }
        }

        #endregion
    }
}
