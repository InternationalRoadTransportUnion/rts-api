﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using IRU.Common.EnterpriseLibrary.Data;
using IRU.RTS.WS.Common.Data.WsSubscriber;

namespace IRU.RTS.WS.Common.Subscribers
{
    public class RTSClientCertGetter: ICertGetter
    {
        private X509Certificate2Collection _certCollection;

        private void QueryExecuted(object sender, DataReaderEventArgs e)
        {
            while (e.DataReader.Read())
            {
                byte[] abCert = e.DataReader.GetValue<byte[]>("CERT_BLOB");
                if ((abCert != null) && (abCert.Length > 0))
                {
                    X509Certificate2WithRtsSubscriber x5c = new X509Certificate2WithRtsSubscriber(e.DataReader.GetValue<string>("SUBSCRIBER_ID"), abCert);
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
                dq.GetAllSubscriberEncryptionKeys(true, QueryExecuted);
            }
        }

        #endregion
    }
}
