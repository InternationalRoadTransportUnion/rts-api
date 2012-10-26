using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.WS.Common.Security.RTSPlus.X509
{
    public class RTSPlusServiceAuthorizationManager : ServiceAuthorizationManager
    {
        RTSPlusSubscribersCertificateStore subscribersCertificateStore = new RTSPlusSubscribersCertificateStore();

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            if (!base.CheckAccessCore(operationContext))
            {
                return false;
            }
            string thumbprint = GetCertificateThumbprint(operationContext);

            if (thumbprint != null)
            {
                string sRTSSubscriberId = subscribersCertificateStore.GetClientSubscriberId(thumbprint);
                if (!String.IsNullOrEmpty(sRTSSubscriberId))
                {
                    OperationContext.Current.IncomingMessageProperties.Add("RTS_SUBSCRIBER_ID", sRTSSubscriberId);
                }

                return subscribersCertificateStore.IsValidClientCertificate(thumbprint);
            }

            return true;
        }

        public override bool CheckAccess(OperationContext operationContext, ref Message message)
        {
            return base.CheckAccess(operationContext, ref message);
        }

        protected override ReadOnlyCollection<IAuthorizationPolicy> GetAuthorizationPolicies(OperationContext operationContext)
        {
            return base.GetAuthorizationPolicies(operationContext);
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

            if (operationContext.ServiceSecurityContext.AuthorizationPolicies.Count > 0)
                throw new System.Security.SecurityException("No client certificate found");
            
            return null;
        }
    }
}

