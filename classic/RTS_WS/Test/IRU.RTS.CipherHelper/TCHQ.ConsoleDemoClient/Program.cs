using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace IRU.RTS.CipherHelper.TCHQ.ConsoleDemoClient
{
    class Program
    {
        public static Assembly ResAssembly
        {
            get
            {
                return Assembly.GetExecutingAssembly();
            }
        }

        public static Stream GetResStream(string testfile)
        {
            return ResAssembly.GetManifestResourceStream(ResAssembly.GetName().Name + "." + testfile);
        }

        public static String GetResString(string testfile)
        {
            using (Stream stmOut = GetResStream(testfile))
            using (TextReader trOut = new StreamReader(stmOut, true))
            {
                return trOut.ReadToEnd();
            }
        }

        static void Main(string[] args)
        {
            XmlDocument xdPayload = new XmlDocument();
            using (Stream smPayload = GetResStream("resources.TCHQ_sample.xml"))
            {
                xdPayload.Load(smPayload);
            }

            // Complete the Payload message
            XmlNodeList xnlTemp = xdPayload.GetElementsByTagName("Sender");
            xnlTemp[0].InnerText = "RTSJAVA";
            xnlTemp = xdPayload.GetElementsByTagName("OriginTime");
            DateTime dtTemp = DateTime.Now;
            TimeSpan ts = TimeZoneInfo.Local.GetUtcOffset(dtTemp);
            string sOffset = "";
            if (ts.TotalSeconds >= 0)
                sOffset += "+";
            sOffset += (ts.Hours).ToString("00") + ':' + ts.Minutes.ToString("00");
            xnlTemp[0].InnerText = dtTemp.ToString("yyyy-MM-dd'T'HH':'mm':'ss") + sOffset;
            xnlTemp = xdPayload.GetElementsByTagName("SentTime");
            xnlTemp[0].InnerText = dtTemp.ToString("yyyy-MM-dd'T'HH':'mm':'ss") + sOffset;

            // Hash the Payload message
            Regex rWhitespace = new Regex(@"\s+");
            string sMsg = rWhitespace.Replace(xdPayload.OuterXml, " ");

            Regex rBody = new Regex("body>(?<body>.*)</.*body", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
            Match mBody = rBody.Match(sMsg);
            if ((!mBody.Success) || (mBody.Groups.Count < 2))
                throw new Exception("Body not found!");
            string sBodyToSign = mBody.Groups[1].Value;
            sBodyToSign = rWhitespace.Replace(sBodyToSign.Trim(), " ");
            byte[] abBodyToSign = Encoding.Unicode.GetBytes(sBodyToSign);

            SHA1 oSha = new SHA1CryptoServiceProvider();
            string sHash = Convert.ToBase64String(oSha.ComputeHash(abBodyToSign, 0, abBodyToSign.Length));

            xdPayload.LoadXml(sMsg);
            xnlTemp = xdPayload.GetElementsByTagName("Hash");
            xnlTemp[0].InnerText = sHash;

            // Encrypt the Payload message
            X509Certificate2 xcSend = new X509Certificate2();
            using (Stream smCertSend = GetResStream("resources.RTSJAVA_send.cer"))
            {
                byte[] abCertSend = new byte[smCertSend.Length];
                smCertSend.Read(abCertSend, 0, abCertSend.Length);
                xcSend.Import(abCertSend);
            }

            byte[] abIn = Encoding.Unicode.GetBytes(xdPayload.OuterXml);

            RSACryptoServiceProvider rAlg = (RSACryptoServiceProvider)xcSend.PublicKey.Key;
            TripleDESCryptoServiceProvider tdAlg = new TripleDESCryptoServiceProvider();

            tdAlg.IV = new byte[] { 0x03, 0x01, 0x04, 0x01, 0x05, 0x09, 0x02, 0x06 };

            byte[] abEncrypted = tdAlg.CreateEncryptor().TransformFinalBlock(abIn, 0, abIn.Length);
            byte[] abSessKey = rAlg.Encrypt(tdAlg.Key, false);

            // Call the Web Service
            TCHQ.SafeTIRHolderQueryServiceClassSoap ws = new TCHQ.SafeTIRHolderQueryServiceClassSoapClient();

            TCHQ.WSTCHQRequest request = new TCHQ.WSTCHQRequest();
            request.Body = new TCHQ.WSTCHQRequestBody();
            request.Body.su = new TCHQ.TIRHolderQuery();
            request.Body.su.SubscriberID = "RTSJAVA";
            request.Body.su.Query_ID = DateTime.UtcNow.ToString("'XXX'yyMMddHHmmssfff");
            request.Body.su.MessageTag = xcSend.Thumbprint.Replace(" ", "").ToUpper();
            request.Body.su.ESessionKey = abSessKey;
            request.Body.su.TIRCarnetHolderQueryParams = abEncrypted;

            TCHQ.WSTCHQResponse response = ws.WSTCHQ(request);

            // Verify the Return Code => it should be 2 (OK)
            if (response.Body.WSTCHQResult.ReturnCode != 2)
                throw new Exception(String.Format("Bad Return Code {0}, it should be 2.", response.Body.WSTCHQResult.ReturnCode));

            // Verify that the right Certificate is used to decrypt the answer
            X509Certificate2 xcRecv = new X509Certificate2();
            using (Stream smCertRecv = GetResStream("resources.RTSJAVA_recv.pfx"))
            {
                byte[] abCertRecv = new byte[smCertRecv.Length];
                smCertRecv.Read(abCertRecv, 0, abCertRecv.Length);
                xcRecv.Import(abCertRecv);
            }

            bool bThumprintsMatch = String.Equals(xcRecv.Thumbprint.Replace(" ", ""), response.Body.WSTCHQResult.MessageTag.Replace(" ", ""), StringComparison.InvariantCultureIgnoreCase);
            if (!bThumprintsMatch)
                throw new Exception("The wrong Certificate is used to decrypt => operation aborted");

            // Decrypt the answer
            rAlg = (RSACryptoServiceProvider)xcRecv.PrivateKey;
            tdAlg = new TripleDESCryptoServiceProvider();

            tdAlg.IV = new byte[] { 0x03, 0x01, 0x04, 0x01, 0x05, 0x09, 0x02, 0x06 };
            tdAlg.Key = rAlg.Decrypt(response.Body.WSTCHQResult.ESessionKey, false);
            abIn = tdAlg.CreateDecryptor().TransformFinalBlock(response.Body.WSTCHQResult.TIRCarnetHolderResponseParams, 0, response.Body.WSTCHQResult.TIRCarnetHolderResponseParams.Length);
            sMsg = Encoding.Unicode.GetString(abIn);
            

            // Write the answer to the Console
            Console.WriteLine(sMsg);
            Console.ReadLine();
        }
    }
}
