using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RTSDotNETClient
{
    public class EncryptionResult
    {
        public byte[] Encrypted { get; set; }
        public byte[] SessionKey { get; set; }
        public string Thumbprint { get; set; }
    }

    public class EncryptionHelper
    {
        public static string GenerateHash(string str)
        {
            SHA1 oSha = new SHA1CryptoServiceProvider();
            byte[] abStr = Encoding.Unicode.GetBytes(str);
            return Convert.ToBase64String(oSha.ComputeHash(abStr, 0, abStr.Length));
        }

        public static X509Certificate2 GetCertificateFromFile(string file)
        {            
            if (string.IsNullOrEmpty(file) || !File.Exists(file))
                throw new Exception(string.Format("The certficate file \"{0}\" could not be found.",file));
            using (FileStream cerFile = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                X509Certificate2 cert = new X509Certificate2();
                byte[] abCert = new byte[cerFile.Length];
                cerFile.Read(abCert, 0, abCert.Length);
                cert.Import(abCert);
                return cert;
            }
        }

        public static EncryptionResult X509EncryptString(string str, X509Certificate2 cert)
        {
            byte[] abIn = Encoding.Unicode.GetBytes(str);

            if (cert.PublicKey.Key == null)
                throw new Exception("The public key is missing.");

            if (!(cert.PublicKey.Key is RSACryptoServiceProvider))
                throw new Exception(string.Format("The type \"{0}\" of the public key is incorrect. The expected type is \"RSACryptoServiceProvider\".",
                    cert.PublicKey.Key.GetType().Name));

            RSACryptoServiceProvider rAlg = (RSACryptoServiceProvider)cert.PublicKey.Key;
            TripleDESCryptoServiceProvider tdAlg = new TripleDESCryptoServiceProvider();

            tdAlg.IV = new byte[] { 0x03, 0x01, 0x04, 0x01, 0x05, 0x09, 0x02, 0x06 };

            EncryptionResult result = new EncryptionResult();
            result.Encrypted = tdAlg.CreateEncryptor().TransformFinalBlock(abIn, 0, abIn.Length);
            result.SessionKey = rAlg.Encrypt(tdAlg.Key, false);
            result.Thumbprint = cert.Thumbprint.Replace(" ", "").ToUpper();
            return result;
        }

        public static string X509DecryptString(byte[] sessionKey, byte[] encrypted, string thumbprint, X509Certificate2 cert)
        {
            // Verify that the right Certificate is used to decrypt the answer            

            bool bThumprintsMatch = String.Equals(cert.Thumbprint.Replace(" ", ""), thumbprint.Replace(" ", ""), StringComparison.InvariantCultureIgnoreCase);
            if (!bThumprintsMatch)
                throw new Exception("The wrong Certificate is used to decrypt => operation aborted");

            if (cert.PrivateKey == null)
                throw new Exception("The private key is missing.");

            if (!(cert.PrivateKey is RSACryptoServiceProvider))
                throw new Exception(string.Format("The type \"{0}\" of the private key is incorrect. The expected type is \"RSACryptoServiceProvider\".",
                    cert.PrivateKey.GetType().Name));

            // Decrypt the answer
            RSACryptoServiceProvider rAlg = (RSACryptoServiceProvider)cert.PrivateKey;
            TripleDESCryptoServiceProvider tdAlg = new TripleDESCryptoServiceProvider();

            tdAlg.IV = new byte[] { 0x03, 0x01, 0x04, 0x01, 0x05, 0x09, 0x02, 0x06 };
            tdAlg.Key = rAlg.Decrypt(sessionKey, false);
            byte[] abIn = tdAlg.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
            return Encoding.Unicode.GetString(abIn);
        }

    }
}
