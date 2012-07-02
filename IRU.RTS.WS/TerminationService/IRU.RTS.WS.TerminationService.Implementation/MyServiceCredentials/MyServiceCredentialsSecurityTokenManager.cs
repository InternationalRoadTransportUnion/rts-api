using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.IdentityModel.Selectors;
using System.ServiceModel.Security;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;

namespace IRU.RTS.WS.TerminationService.Implementation.MyServiceCredentials
{
    internal class MyServiceCredentialsSecurityTokenManager : ServiceCredentialsSecurityTokenManager
    {
        MyServiceCredentials credentials;

        public MyServiceCredentialsSecurityTokenManager(MyServiceCredentials credentials)
            : base(credentials)
        {
            this.credentials = credentials;
        }

        public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement tokenRequirement)
        {
            SecurityTokenProvider result = null;

            // Return your implementation of the SecurityTokenProvider based on the 
            // tokenRequirement argument.
            if (tokenRequirement.TokenType == SecurityTokenTypes.X509Certificate)
            {
                MessageDirection direction = tokenRequirement.GetProperty<MessageDirection>(
                    ServiceModelSecurityTokenRequirement.MessageDirectionProperty);

                if (direction == MessageDirection.Input)
                {
                    if (tokenRequirement.KeyUsage == SecurityKeyUsage.Exchange)
                    {
                        result = new MyX509SecurityTokenProvider(credentials.ServiceCertificate.Certificate);
                    }
                }
            }

            // in case of no customization: return the base default SecurityTokenProvider
            if (result == null)
            {
                result = base.CreateSecurityTokenProvider(tokenRequirement);
            }

            return result;
        }

        public override SecurityTokenAuthenticator CreateSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver)
        {
            // Return your implementation of SecurityTokenProvider, if required.
            // This implementation delegates to the base class.
            return base.CreateSecurityTokenAuthenticator(tokenRequirement, out outOfBandTokenResolver);
        }

        public override SecurityTokenSerializer CreateSecurityTokenSerializer(SecurityTokenVersion version)
        {
            // Return your implementation of SecurityTokenProvider, if required.
            // This implementation delegates to the base class.
            return base.CreateSecurityTokenSerializer(version);
        }
    }
}
