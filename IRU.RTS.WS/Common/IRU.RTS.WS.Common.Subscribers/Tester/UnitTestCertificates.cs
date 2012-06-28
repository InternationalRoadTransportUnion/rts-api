using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using IRU.RTS.WS.Common.Subscribers;

namespace IRU.RTS.WS.Common.Subscribers.Tester
{
    [TestFixture]
    public class UnitTestCertificates
    {
        [Test]
        public void RTSClientCertGetter()
        {
            X509Certificate2Collection certs = new X509Certificate2Collection();
            ICertGetter cg = new RTSClientCertGetter();
            cg.GetCertificates(ref certs);
        }
    }
}
