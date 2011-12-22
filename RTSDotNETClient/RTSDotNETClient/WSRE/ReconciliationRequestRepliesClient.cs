using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace RTSDotNETClient.WSRE
{
    public class ReconciliationRequestRepliesClient : BaseWSClient
    {
        private const String InformationExchangeVersion = "2.0.0";

        public void Send(Query query)
        {
            SanityChecks();

            query.CalculateHash();
            string queryStr = query.Serialize();

            Global.Trace("WSRE QUERY:\r\n" + queryStr + "\r\n");

            // Encryption
            EncryptionResult encrypted = EncryptionHelper.X509EncryptString(queryStr, this.PublicCertificate);

            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            EndpointAddress remoteAddress = new EndpointAddress(this.WebServiceUrl);

            // Call the Web Service
            SafeTIRUploadWS.SafeTirUploadSoapClient ws = new SafeTIRUploadWS.SafeTirUploadSoapClient(binding, remoteAddress);
            SafeTIRUploadWS.SafeTIRReconParams request = new SafeTIRUploadWS.SafeTIRReconParams();
            request.SubscriberID = "RTSJAVA";
            request.Sender_MessageID = DateTime.UtcNow.ToString("'XXX'yyMMddHHmmssfff");
            request.MessageTag = encrypted.Thumbprint;
            request.ESessionKey = encrypted.SessionKey;
            request.SafeTIRReconData = encrypted.Encrypted;
            request.Information_Exchange_Version = InformationExchangeVersion;
            SafeTIRUploadWS.SafeTIRUploadAck ack = ws.WSRE(request);

            // Verify the Return Code => it should be 2 (OK)
            ReturnCode returnCode = (ReturnCode)ack.ReturnCode;
            if (returnCode != ReturnCode.SUCCESS)
                throw new RTSWebServiceException(String.Format("{0} ({1})", returnCode.ToString(), (int)returnCode), (int)returnCode);

            Global.Trace(string.Format("WSRE RESPONSE: RETURN_CODE={0} ({1})\r\n", (int)returnCode, returnCode));
        }
    }
}
