using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace RTSDotNETClient
{
    /// <summary>
    /// The base class for web service clients
    /// </summary>
    public class BaseWSClient
    {
        /// <summary>
        /// The web service url
        /// </summary>
        public string WebServiceUrl { get; set; }

        /// <summary>
        /// The public certificate used for encryption
        /// </summary>
        public X509Certificate2 PublicCertificate { get; set; }        

        /// <summary>
        /// Performs some sanity checks
        /// </summary>
        protected void SanityChecks()
        {
            if (string.IsNullOrEmpty(this.WebServiceUrl))
                throw new Exception("The WebServiceUrl is missing.");
            if (this.PublicCertificate == null)
                throw new Exception("The public certificate is missing.");            
        }
    }
}
