using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.IdentityModel.Claims;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.WS.TerminationService.Implementation.MyServiceCredentials
{
    public class MyCertificateAuthorizationManager : ServiceAuthorizationManager
    {
        string[] OurClientsCertificatesStore = new MyCertificatesStore().ClientsCertificatesThumprints();

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            if (!base.CheckAccessCore(operationContext))
            {
                return false;
            }
            string thumbprint = GetCertificateThumbprint(operationContext);
            // TODO: Check the thumbprint against your database, then return true if found, otherwise false  
            //  -> hard coded comparison to our list of possible client certificates thumbprints:
            //          - CERT_SIGN_CLIENT_CERT
            //          - IRU.Common.Messaging from@somewhere.com (test)

            // try change default ServiceCertificate according to Subscriber's service certificate 
            // define in Db for this operation context subscriber's client certificate...

            if (thumbprint=="E0CAEA178DF21DB20A48296BDB3A9E74DE7DB81E")
            {
                operationContext.Host.Credentials.ServiceCertificate.SetCertificate(
                                                            StoreLocation.CurrentUser,
                                                            StoreName.My,
                                                            X509FindType.FindBySubjectName,
                                                            "IRU.Common.Messaging to@somewhere.com (test)");
            }

            return OurClientsCertificatesStore.Contains(thumbprint);
        }

        private string GetCertificateThumbprint(OperationContext operationContext)
        {
            foreach (var claimSet in operationContext.ServiceSecurityContext.AuthorizationContext.ClaimSets)
            {
                foreach (Claim claim in claimSet.FindClaims(ClaimTypes.Thumbprint, Rights.Identity))
                {
                    string tb = BitConverter.ToString((byte[])claim.Resource);
                    tb = tb.Replace("-", "").ToUpperInvariant();
                    return tb;
                }
            }
            throw new System.Security.SecurityException("No client certificate found");
        }
    }
}

