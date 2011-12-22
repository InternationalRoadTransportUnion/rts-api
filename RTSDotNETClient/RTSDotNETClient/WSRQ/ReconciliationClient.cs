using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace RTSDotNETClient.WSRQ
{
    public class ReconciliationClient : BaseWSClient
    {
        private const String InformationExchangeVersion = "1.0.0";

        public Response DownloadReconciliationRequests(string subscriberID, Query query)
        {
            SanityChecks();

            query.CalculateHash();
            string queryStr = query.Serialize();

            Global.Trace("WSRQ QUERY:\r\n" + queryStr + "\r\n");

            // Encryption
            EncryptionResult encrypted = EncryptionHelper.X509EncryptString(queryStr, this.PublicCertificate);

            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            EndpointAddress remoteAddress = new EndpointAddress(this.WebServiceUrl);

            // Call the Web Service
            ReconciliationWS.ReconciliationQueryServiceClassSoap ws = new ReconciliationWS.ReconciliationQueryServiceClassSoapClient(binding, remoteAddress);
            
            ReconciliationWS.WSRQRequest request = new ReconciliationWS.WSRQRequest();
            request.Body = new ReconciliationWS.WSRQRequestBody();
            request.Body.su = new ReconciliationWS.ReconciliationQuery();
            request.Body.su.SubscriberID = subscriberID;
            request.Body.su.Sender_MessageID = DateTime.UtcNow.ToString("'XXX'yyMMddHHmmssfff");
            request.Body.su.MessageTag = encrypted.Thumbprint;
            request.Body.su.ESessionKey = encrypted.SessionKey;
            request.Body.su.ReconciliationQueryData = encrypted.Encrypted;
            request.Body.su.Information_Exchange_Version = InformationExchangeVersion;

            ReconciliationWS.WSRQResponse response = ws.WSRQ(request);

            // Verify the Return Code => it should be 2 (OK)
            ReturnCode returnCode = (ReturnCode)response.Body.WSRQResult.ReturnCode;
            if (returnCode != ReturnCode.SUCCESS)
                throw new RTSWebServiceException(String.Format("{0} ({1})", returnCode.ToString(), (int)returnCode), (int)returnCode);

            string respStr = EncryptionHelper.X509DecryptString(response.Body.WSRQResult.ESessionKey,
                    response.Body.WSRQResult.ReconciliationRequestData, response.Body.WSRQResult.MessageTag, this.PrivateCertificate);

            Global.Trace(string.Format("WSRQ RESPONSE: RETURN_CODE={0} ({1})\r\n{2}\r\n", (int)returnCode, returnCode, respStr));

            return QueryResponseFactory.Deserialize<Response>(respStr, Response.Xsd);

        }
    }
}
