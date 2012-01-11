using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RTSDotNETClient
{
    /// <summary>
    /// Object returned by the X509EncryptString function
    /// </summary>
    public class EncryptionResult
    {
        /// <summary>
        /// The encrypted message
        /// </summary>
        public byte[] Encrypted { get; set; }

        /// <summary>
        /// The encrypted session key
        /// </summary>
        public byte[] SessionKey { get; set; }

        /// <summary>
        /// X.509 Certificate "thumbprint"
        /// </summary>
        public string Thumbprint { get; set; }
    }

    /// <summary>
    /// Encryption and decryption utility functions
    /// </summary>
    public class EncryptionHelper
    {
        /// <summary>
        /// Generate the SHA-1 hash of the string passed as a parameter
        /// </summary>
        /// <param name="str">The string to be hashed</param>
        /// <returns>Returns the SHA-1 hash in base 64 string format</returns>
        public static string GenerateHash(string str)
        {
            SHA1 oSha = new SHA1CryptoServiceProvider();
            byte[] abStr = Encoding.Unicode.GetBytes(str);
            return Convert.ToBase64String(oSha.ComputeHash(abStr, 0, abStr.Length));
        }

        /// <summary>
        /// Returns the X509Certificate2 object from a certificate file (.cer or .pfx)
        /// </summary>
        /// <param name="file">The certificate file</param>
        /// <returns>Returns a X509Certificate2 object</returns>
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

        /// <summary>
        /// Encrypt a string using the X509Certificate2 given as a parameter
        /// </summary>
        /// <param name="str">The string to be encrypted</param>
        /// <param name="cert">The X509Certificate2 object</param>
        /// <returns>Returns the message encrypted</returns>
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

        /// <summary>
        /// Decrypt a message
        /// </summary>
        /// <param name="sessionKey">The encrypted sesssion key</param>
        /// <param name="encrypted">The encrypted message</param>
        /// <param name="thumbprint">The X.509 certificate "thumbprint"</param>
        /// <param name="cert">The X509Certificate2 object to be used to decrypt the message</param>
        /// <returns>Returns the message decrypted</returns>
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
