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
    public class RTSServerCertGetter : ICertGetter
    {
        private X509Certificate2Collection _certCollection;

        private void QueryExecuted(object sender, DataReaderEventArgs e)
        {
            while (e.DataReader.Read())
            {
                byte[] abCert = e.DataReader.GetValue<byte[]>("CERT_BLOB");
                if ((abCert != null) && (abCert.Length > 0))
                {
                    X509Certificate2 x5c = new X509Certificate2(abCert);
                    x5c.FriendlyName = e.DataReader.GetValue<string>("DISTRIBUTED_TO");
                    
                    byte[] abModulus = e.DataReader.GetValue<byte[]>("MODULUS");
                    byte[] abExponent = e.DataReader.GetValue<byte[]>("EXPONENT");
                    byte[] abP = e.DataReader.GetValue<byte[]>("P");
                    byte[] abQ = e.DataReader.GetValue<byte[]>("Q");
                    byte[] abDP = e.DataReader.GetValue<byte[]>("DP");
                    byte[] abDQ = e.DataReader.GetValue<byte[]>("DQ");
                    byte[] abInverseQ = e.DataReader.GetValue<byte[]>("INVERSEQ");
                    byte[] abD = e.DataReader.GetValue<byte[]>("D");

                    StringBuilder sbRsaXml = new StringBuilder();
                    sbRsaXml.AppendLine("<RSAKeyValue>");
                    sbRsaXml.AppendLine(String.Format("<Modulus>{0}</Modulus>", Convert.ToBase64String(abModulus)));
                    sbRsaXml.AppendLine(String.Format("<Exponent>{0}</Exponent>", Convert.ToBase64String(abExponent)));
                    sbRsaXml.AppendLine(String.Format("<P>{0}</P>", Convert.ToBase64String(abP)));
                    sbRsaXml.AppendLine(String.Format("<Q>{0}</Q>", Convert.ToBase64String(abQ)));
                    sbRsaXml.AppendLine(String.Format("<DP>{0}</DP>", Convert.ToBase64String(abDP)));
                    sbRsaXml.AppendLine(String.Format("<DQ>{0}</DQ>", Convert.ToBase64String(abDQ)));
                    sbRsaXml.AppendLine(String.Format("<InverseQ>{0}</InverseQ>", Convert.ToBase64String(abInverseQ)));
                    sbRsaXml.AppendLine(String.Format("<D>{0}</D>", Convert.ToBase64String(abD)));
                    sbRsaXml.AppendLine("</RSAKeyValue>");
                    AsymmetricAlgorithm aa = AsymmetricAlgorithm.Create();
                    aa.FromXmlString(sbRsaXml.ToString());
                    x5c.PrivateKey = aa;

                    _certCollection.Add(x5c);
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
                dq.GetAllIRUEncryptionsKeys(true, QueryExecuted);
            }
        }

        #endregion
    }
}
