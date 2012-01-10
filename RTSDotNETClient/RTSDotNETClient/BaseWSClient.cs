using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace RTSDotNETClient
{
    public class BaseWSClient
    {
        public string WebServiceUrl { get; set; }
        public X509Certificate2 PublicCertificate { get; set; }        

        protected void SanityChecks()
        {
            if (string.IsNullOrEmpty(this.WebServiceUrl))
                throw new Exception("The WebServiceUrl is missing.");
            if (this.PublicCertificate == null)
                throw new Exception("The public certificate is missing.");            
        }
    }
}
