using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.WS.TerminationService.Implementation.MyServiceCredentials
{
    public class MyX509CertificateValidator : X509CertificateValidator
    {
        MySubscribersCertificateStore _mySubscribersCertificateStore = new MySubscribersCertificateStore();

        public MyX509CertificateValidator()
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
            if (!_mySubscribersCertificateStore.IsValidClientCertificate(certificate.Thumbprint))
            {
                throw new SecurityTokenValidationException
                  ("Certificate was not issued by a trusted issuer");
            }
        }
    }
}
