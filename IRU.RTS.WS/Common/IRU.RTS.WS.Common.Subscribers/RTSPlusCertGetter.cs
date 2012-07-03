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
    public class RTSPlusCertGetter : ICertGetter
    {
        private CertUsage _certUsage;

        public RTSPlusCertGetter(CertUsage certUsage)
        {
            _certUsage = certUsage;
        }

        private X509Certificate2Collection _certCollection;

        private void QueryExecuted(object sender, DataReaderEventArgs e)
        {
            while (e.DataReader.Read())
            {
                byte[] abCert = e.DataReader.GetValue<byte[]>("CERT_BLOB");
                string sPrivateKeyXml = e.DataReader.GetValue<string>("PRIVATE_KEY");

                if ((abCert != null) && (abCert.Length > 0))
                {
                    X509Certificate2 x5c = null;

                    if ((_certUsage == CertUsage.Client) && (sPrivateKeyXml == null))
                    {
                        x5c = new X509Certificate2(abCert);                        
                    }
                    else if ((_certUsage == CertUsage.Server) && (sPrivateKeyXml != null))
                    {
                        x5c = new X509Certificate2(abCert);
                        AsymmetricAlgorithm aa = AsymmetricAlgorithm.Create();
                        aa.FromXmlString(sPrivateKeyXml.ToString());
                        x5c.PrivateKey = aa;
                    }

                    if (x5c != null)
                    {
                        x5c.FriendlyName = e.DataReader.GetValue<string>("SUBSCRIBER_ID");
                        _certCollection.Add(x5c);
                    }
                }
            }
        }

        #region ICertGetter Members

        public void GetCertificates(ref X509Certificate2Collection certCollection)
        {
            if (certCollection == null)
                throw new ArgumentNullException("certCollection");
            _certCollection = certCollection;

            using (DbQueries dq = new DbQueries())
            {
                dq.GetAllRtsplusSignatureKeys(true, QueryExecuted);
            }
        }

        #endregion
    }
}
