using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using IRU.RTS.WS.Common.Subscribers;

namespace IRU.RTS.WS.Common.Subscribers.Tester
{
    [TestFixture]
    public class UnitTestCertificates: IDisposable
    {
        private TransactionScope _transactionScope;

        public UnitTestCertificates()
        {
            _transactionScope = new TransactionScope();
        }

        [Test]
        public void T001_GetRTSClientCertificates()
        {
            X509Certificate2Collection certs = CertificatesStore.GetCertificates(CertStore.RTS, CertUsage.Client);
            Assert.Greater(certs.Count, 0);
        }

        [Test]
        public void T002_GetRTSServerCertificates()
        {
            X509Certificate2Collection certs = CertificatesStore.GetCertificates(CertStore.RTS, CertUsage.Server);
            Assert.Greater(certs.Count, 0);
        }

        [Test]
        public void T003_AddAndActivateRTSPlusClientCertificates()
        {
            byte[] abCert = UnitTestHelper.GetTestBytes("certificates.RSA_2048_test_#1.crt");
            X509Certificate2 x2c = new X509Certificate2(abCert);
            CertificatesStore.AddCertificate(CertStore.RTS_PLUS, CertUsage.Client, "RSA_2048_test", x2c);            

            abCert = UnitTestHelper.GetTestBytes("certificates.RSA_2048_test_#2.crt");
            x2c = new X509Certificate2(abCert);
            CertificatesStore.AddCertificate(CertStore.RTS_PLUS, CertUsage.Client, "RSA_2048_test", x2c);

            CertificatesStore.ActivateCertificate(CertStore.RTS_PLUS, CertUsage.Client, "RSA_2048_test", x2c);
        }

        [Test]
        public void T004_GetRTSPlusClientCertificates()
        {
            X509Certificate2Collection certs = CertificatesStore.GetCertificates(CertStore.RTS_PLUS, CertUsage.Client);
            Assert.Greater(certs.Count, 0);
        }

        [Test]
        public void T005_AddAndActivateRTSPlusServerCertificates()
        {
            byte[] abCert = UnitTestHelper.GetTestBytes("certificates.RSA_2048_test_#2.pfx");
            X509Certificate2 x2c = new X509Certificate2(abCert, (string)null, X509KeyStorageFlags.Exportable);
            CertificatesStore.AddCertificate(CertStore.RTS_PLUS, CertUsage.Server, "RSA_2048_test", x2c);

            abCert = UnitTestHelper.GetTestBytes("certificates.RSA_2048_test_#1.pfx");
            x2c = new X509Certificate2(abCert, (string)null, X509KeyStorageFlags.Exportable);
            CertificatesStore.AddCertificate(CertStore.RTS_PLUS, CertUsage.Server, "RSA_2048_test", x2c);

            CertificatesStore.ActivateCertificate(CertStore.RTS_PLUS, CertUsage.Server, "RSA_2048_test", x2c);
        }

        [Test]
        public void T006_GetRTSPlusServerCertificates()
        {
            X509Certificate2Collection certs = CertificatesStore.GetCertificates(CertStore.RTS_PLUS, CertUsage.Server);
            Assert.Greater(certs.Count, 0);
        }

        #region IDisposable Members

        public void Dispose()
        {
            _transactionScope.Dispose(); // Rollback
        }

        #endregion
    }
}
