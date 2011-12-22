using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace RTSDotNETClient.TCHQ
{
    public class HolderQueryClient : BaseWSClient
    {

        public Response QueryCarnet(Query query)
        {
            SanityChecks();

            query.CalculateHash();
            string queryStr = query.Serialize();

            Global.Trace("TCHQ QUERY:\r\n" + queryStr + "\r\n");

            // Encryption
            EncryptionResult encrypted = EncryptionHelper.X509EncryptString(queryStr, this.PublicCertificate);            

            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            EndpointAddress remoteAddress = new EndpointAddress(this.WebServiceUrl);

            // Call the Web Service
            CarnetHolderQueryWS.SafeTIRHolderQueryServiceClassSoap ws = new CarnetHolderQueryWS.SafeTIRHolderQueryServiceClassSoapClient(binding, remoteAddress);

            CarnetHolderQueryWS.WSTCHQRequest request = new CarnetHolderQueryWS.WSTCHQRequest();
            request.Body = new CarnetHolderQueryWS.WSTCHQRequestBody();
            request.Body.su = new CarnetHolderQueryWS.TIRHolderQuery();
            request.Body.su.SubscriberID = query.Body.Sender;
            request.Body.su.Query_ID = DateTime.UtcNow.ToString("'XXX'yyMMddHHmmssfff");
            request.Body.su.MessageTag = encrypted.Thumbprint;
            request.Body.su.ESessionKey = encrypted.SessionKey;
            request.Body.su.TIRCarnetHolderQueryParams = encrypted.Encrypted;
            
            CarnetHolderQueryWS.WSTCHQResponse response = ws.WSTCHQ(request);

            // Verify the Return Code => it should be 2 (OK)
            ReturnCode returnCode = (ReturnCode)response.Body.WSTCHQResult.ReturnCode;
            if (returnCode != ReturnCode.SUCCESS)
                throw new RTSWebServiceException(String.Format("{0} ({1})", returnCode.ToString(), (int)returnCode),(int)returnCode);

            string respStr = EncryptionHelper.X509DecryptString(response.Body.WSTCHQResult.ESessionKey,
                    response.Body.WSTCHQResult.TIRCarnetHolderResponseParams, response.Body.WSTCHQResult.MessageTag, this.PrivateCertificate);

            Global.Trace(string.Format("TCHQ RESPONSE: RETURN_CODE={0} ({1})\r\n{2}\r\n", (int)returnCode, returnCode, respStr));

            return QueryResponseFactory.Deserialize<Response>(respStr, Response.Xsd);
        }
    }
}
