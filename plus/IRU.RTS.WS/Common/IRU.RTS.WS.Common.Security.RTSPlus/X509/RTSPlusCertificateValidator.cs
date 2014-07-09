using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.WS.Common.Security.RTSPlus.X509
{
    public class RTSPlusCertificateValidator : X509CertificateValidator
    {
        RTSPlusSubscribersCertificateStore _subscribersCertificateStore = new RTSPlusSubscribersCertificateStore();

        public RTSPlusCertificateValidator()
        {
        }

        public override void Validate(X509Certificate2 certificate)
        {
            // Check that there is a certificate.
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            // Check that the certificate issuer matches the configured issuer.
            if (!_subscribersCertificateStore.IsValidClientCertificate(certificate.Thumbprint))
            {
                throw new SecurityTokenValidationException
                  ("Certificate was not issued by a trusted issuer");
            }
        }
    }
}
