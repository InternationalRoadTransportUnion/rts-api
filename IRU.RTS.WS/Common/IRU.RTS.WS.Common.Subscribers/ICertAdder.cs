using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.WS.Common.Subscribers
{
    public interface ICertAdder
    {
        string UserId { get; set; }

        void AddCertificate(X509Certificate2 certificate);
    }
}