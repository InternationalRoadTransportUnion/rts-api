﻿using System;
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
        MySubscribersCertificateStore mySubscribersCertificateStore = new MySubscribersCertificateStore();

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            if (!base.CheckAccessCore(operationContext))
            {
                return false;
            }
            string thumbprint = GetCertificateThumbprint(operationContext);

            return mySubscribersCertificateStore.IsValidClientCertificate(thumbprint);
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

