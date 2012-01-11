using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;

namespace RTSDotNETClient.WSST
{
    /// <summary>
    /// The SafeTIRTransmissionClient class allows to transmit SafeTir data to IRU
    /// </summary>
    public class SafeTIRTransmissionClient : BaseWSClient
    {

        /// <summary>
        /// Transmit SafeTir data to IRU
        /// </summary>
        /// <param name="query">The query object</param>
        public void Send(Query query)
        {
            SanityChecks();

            query.CalculateHash();
            string queryStr = query.Serialize();

            Global.Trace("WSST QUERY:\r\n" + queryStr + "\r\n");

            // Encryption
            EncryptionResult encrypted = EncryptionHelper.X509EncryptString(queryStr, this.PublicCertificate);

            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            EndpointAddress remoteAddress = new EndpointAddress(this.WebServiceUrl);

            // Call the Web Service
            SafeTIRUploadWS.SafeTirUploadSoapClient ws = new SafeTIRUploadWS.SafeTirUploadSoapClient(binding, remoteAddress);
            SafeTIRUploadWS.SafeTIRUploadParams request = new SafeTIRUploadWS.SafeTIRUploadParams();
            request.SubscriberID = query.Body.SubscriberID;
            request.Sender_MessageID = query.Body.SenderMessageID;
            request.MessageTag = encrypted.Thumbprint;
            request.ESessionKey = encrypted.SessionKey;
            request.safeTIRUploadData = encrypted.Encrypted;
            
            SafeTIRUploadWS.SafeTIRUploadAck ack = ws.WSST(request);

            // Verify the Return Code => it should be 2 (OK)
            ReturnCode returnCode = (ReturnCode)ack.ReturnCode;
            if (returnCode != ReturnCode.SUCCESS)
                throw new RTSWebServiceException(String.Format("{0} ({1})", returnCode.ToString(), (int)returnCode), (int)returnCode);

            Global.Trace(string.Format("WSST RESPONSE: RETURN_CODE={0} ({1})\r\n", (int)returnCode, returnCode));
        }
    }
}
