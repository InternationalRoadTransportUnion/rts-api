using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace RTSDotNETClient
{
    
    public class Envelope 
    {
        public string Hash { get; set; }
    }

    public class BaseQueryResponse
    {        
        protected string xsd;
        public Envelope Envelope { get; set; }

        public BaseQueryResponse()
        {
            this.Envelope = new Envelope();
        }

        public void CalculateHash()
        {
            if (this.Envelope.Hash == null)
                this.Envelope.Hash = "";
            string xml = this.Serialize();

            Match m = Regex.Match(xml, "<Body>(.*?)</Body>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            if (m.Success)
            {
                //string sBodyToSign = Regex.Replace(m.Groups[1].Value.Trim(), @"\s+", "");
                string sBodyToSign = m.Groups[1].Value.Trim();
                this.Envelope.Hash = EncryptionHelper.GenerateHash(sBodyToSign);
            }
            else
                throw new Exception("Body not found!");
        }

        public string Serialize()
        {
            XmlSerializer ser = new XmlSerializer(this.GetType());
            string xml;
            using (StringWriter sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    ser.Serialize(writer, this);
                    xml = sw.ToString();
                }
            }

            if (Global.XsdValidationEnabled)
            {
                try
                {
                    XmlReaderSettings config = new XmlReaderSettings();
                    config.ValidationType = ValidationType.Schema;
                    config.ValidationEventHandler += new ValidationEventHandler(delegate(object sender, ValidationEventArgs vea)
                    {
                        if (vea.Severity == XmlSeverityType.Error)
                            throw new Exception(vea.Message);
                    });
                    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetName().Name + ".resources." + xsd);
                    config.Schemas.Add(null, XmlReader.Create(stream));

                    StringReader sr = new StringReader(xml);
                    XmlReader reader = XmlReader.Create(sr, config);
                    while (reader.Read())
                    { }
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                    if (ex.InnerException != null)
                        err += " " + ex.InnerException.Message;
                    throw new XsdValidationException(err, ex);
                }
            }
            return xml;
        }
    }
}
