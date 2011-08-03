using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace IRU.RTS.TIREPD.B2G.CipherHelper
{
    public class MessageEventArgs : EventArgs
    {
        public string Message = null;
        
        public MessageEventArgs(string message)
        {
            this.Message = message;
        }
    }

    public delegate void MessageEventHandler(Object sender, MessageEventArgs e);

    public class Crypto
    {
        private string[] _b2gElems = 
            { 
                "SubscriberID",
                "CertificateID",
                "ESessionKey",
                "SubscriberMessageID",
                "InformationExchangeVersion",
                "MessageName",
                "TimeSent",
                "MessageContent"
            };

        private X509Certificate2 _certificate = null;

        public X509Certificate2 Certificate
        {
            get
            {
                return _certificate;
            }
        }

        private string _SubscriberID = "";
        private string _SubscriberMessageID = "";
        private string _InformationExchangeVersion = "";
        private string _MessageName = "";
        private string _TimeSent = "";

        public string SubscriberID
        {
            set { _SubscriberID = value ?? ""; }
        }

        public string SubscriberMessageID
        {
            set { _SubscriberMessageID = value ?? ""; }
        }

        public string InformationExchangeVersion
        {
            set { _InformationExchangeVersion = value ?? ""; }
        }

        public string MessageName
        {
            set { _MessageName = value ?? ""; }
        }

        public DateTime? TimeSent
        {
            set 
            {
                if (value.HasValue)
                {
                    _TimeSent = value.Value.ToString("s") + value.Value.ToString("zzz");
                }
                else
                    _TimeSent = ""; 
            }
        }

        public event MessageEventHandler OnMessage;

        public Crypto(string certificateFileName, string certificatePassword)
        {
            X509Certificate2 oCert = new X509Certificate2(certificateFileName, certificatePassword);
            RSACryptoServiceProvider rsaCsp = null;

            if (oCert.HasPrivateKey)
            {
                if (!(oCert.PrivateKey is RSACryptoServiceProvider))
                    throw new Exception("Expected a RSA private Key");
                rsaCsp = (RSACryptoServiceProvider)oCert.PrivateKey;
                if (rsaCsp.KeySize != 1024)
                    throw new Exception("Expected a RSA private Key in 1024 bits");
            }
                        
            if (!(oCert.PublicKey.Key is RSACryptoServiceProvider))
                throw new Exception("Expected a RSA public Key");
            rsaCsp = (RSACryptoServiceProvider)oCert.PublicKey.Key;
            if (rsaCsp.KeySize != 1024)
                throw new Exception("Expected a RSA public Key in 1024 bits");

            _certificate = oCert;
        }

        private XDocument CheckIsB2G(Stream input)
        {
            using (XmlReader xrIn = XmlReader.Create(input))
            {
                XDocument xdB2G = XDocument.Load(xrIn);

                XElement xeB2G = xdB2G.Root.Descendants().FirstOrDefault(n => n.Name.LocalName == "TIREPDB2G");
                if (xeB2G == null)
                    throw new Exception("Node <TIREPDB2G> not found");

                XElement xeSU = xeB2G.Descendants().FirstOrDefault(n => n.Name.LocalName == "su");
                if (xeSU == null)
                    throw new Exception("Node <su> not found");

                foreach (string sNode in _b2gElems)
                {
                    XElement xeTemp = xeSU.Descendants().FirstOrDefault(n => n.Name.LocalName == sNode);
                    if (xeSU == null)
                        throw new Exception(String.Format("Node <{0}> not found", sNode));
                }

                return xdB2G;
            }
        }

        public string Decrypt(Stream input)
        {
            XDocument message = CheckIsB2G(input);

            string sCertId = message.Root.Descendants().First(n => n.Name.LocalName == "CertificateID").Value;
            string sSessKey = message.Root.Descendants().First(n => n.Name.LocalName == "ESessionKey").Value;
            string sContent = message.Root.Descendants().First(n => n.Name.LocalName == "MessageContent").Value;

            if ((!_certificate.Thumbprint.Equals(sCertId)) && (OnMessage != null))
                OnMessage(this, new MessageEventArgs("The Certificate ID of the message doesn't match with your certificate. The decryption will probably fail."));

            byte[] aSessKey = Convert.FromBase64String(sSessKey);
            byte[] aContent = Convert.FromBase64String(sContent);

            RSACryptoServiceProvider rAlg = (RSACryptoServiceProvider)_certificate.PrivateKey;
            TripleDESCryptoServiceProvider tdAlg = new TripleDESCryptoServiceProvider();

            tdAlg.IV = new byte[] { 0x03, 0x01, 0x04, 0x01, 0x05, 0x09, 0x02, 0x06 };
            tdAlg.Key = rAlg.Decrypt(aSessKey, false);

            byte[] abDecrypted = tdAlg.CreateDecryptor().TransformFinalBlock(aContent, 0, aContent.Length);
            return Encoding.Unicode.GetString(abDecrypted);
        }

        public string Encrypt(Stream input)
        {
            StringBuilder sbOut = new StringBuilder();
            XmlDocument xmdSOAP = new XmlDocument();
            xmdSOAP.LoadXml(Properties.Settings.Default.Empty2BGRequest);            
            XmlWriterSettings xwsSOAP = new XmlWriterSettings();
            xwsSOAP.Indent = true;
            xwsSOAP.OmitXmlDeclaration = true;
            using (XmlNodeReader xnrSOAP = new XmlNodeReader(xmdSOAP))            
            using (XmlWriter xwSOAP = XmlWriter.Create(sbOut, xwsSOAP))            
            {
                xnrSOAP.MoveToContent();
                XDocument xdSOAP = XDocument.Load(xnrSOAP);

                StreamReader srIn = new StreamReader(input);                
                byte[] abIn = Encoding.Unicode.GetBytes(srIn.ReadToEnd());

                RSACryptoServiceProvider rAlg = (RSACryptoServiceProvider)_certificate.PublicKey.Key;
                TripleDESCryptoServiceProvider tdAlg = new TripleDESCryptoServiceProvider();

                tdAlg.IV = new byte[] { 0x03, 0x01, 0x04, 0x01, 0x05, 0x09, 0x02, 0x06 };

                byte[] abEncrypted = tdAlg.CreateEncryptor().TransformFinalBlock(abIn, 0, abIn.Length);
                string sEncrypted = Convert.ToBase64String(abEncrypted);

                byte[] abSessKey = rAlg.Encrypt(tdAlg.Key, false);
                string sSessKey = Convert.ToBase64String(abSessKey);

                xdSOAP.Root.Descendants().First(n => n.Name.LocalName == "MessageContent").Value = sEncrypted;
                xdSOAP.Root.Descendants().First(n => n.Name.LocalName == "ESessionKey").Value = sSessKey;
                xdSOAP.Root.Descendants().First(n => n.Name.LocalName == "CertificateID").Value = _certificate.Thumbprint;

                xdSOAP.Root.Descendants().First(n => n.Name.LocalName == "SubscriberID").Value = _SubscriberID;
                xdSOAP.Root.Descendants().First(n => n.Name.LocalName == "SubscriberMessageID").Value = _SubscriberMessageID;
                xdSOAP.Root.Descendants().First(n => n.Name.LocalName == "InformationExchangeVersion").Value = _InformationExchangeVersion;
                xdSOAP.Root.Descendants().First(n => n.Name.LocalName == "MessageName").Value = _MessageName;
                xdSOAP.Root.Descendants().First(n => n.Name.LocalName == "TimeSent").Value = _TimeSent;

                xdSOAP.Save(xwSOAP);
            }

            return sbOut.ToString();
        }
    }
}
