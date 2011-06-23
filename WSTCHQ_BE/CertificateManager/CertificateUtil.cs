using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.CertificateManager
{
    public class CertificateUtil
    {
        public static X509Certificate GetX509CertificateFromLocalMachine(string thumbprint)
        {
            if (thumbprint == null)
                return null;

            thumbprint = thumbprint.ToUpper().Trim().Replace(" ", "").Replace("\t", "");

            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);

            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509CertificateCollection certificateCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
            if (certificateCollection.Count > 0)
            {
                return certificateCollection[0];
            }

            return null;
        }
    }
}
