using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace RTSDotNETClient
{
    /// <summary>
    /// A factory class to deserialize query and response objects from a xml string
    /// </summary>
    public class QueryResponseFactory
    {
        /// <summary>
        /// Deserialization of a query or response object
        /// </summary>
        /// <typeparam name="T">The query or response type</typeparam>
        /// <param name="xml">The xml to be deserialized</param>
        /// <param name="xsd">The xml schema definition file used to validate the xml</param>
        /// <returns>Returns a query or response object</returns>
        public static T Deserialize<T>(string xml, string xsd)
        {
            try
            {
                XmlReaderSettings config = new XmlReaderSettings();
                if (!Global.XsdValidationDisabled)
                {
                    config.ValidationType = ValidationType.Schema;
                    config.ValidationEventHandler += new ValidationEventHandler(delegate(object sender, ValidationEventArgs vea)
                    {
                        if (vea.Severity == XmlSeverityType.Error)
                            throw new Exception(vea.Message);
                    });
                    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetName().Name + ".resources." + xsd);
                    config.Schemas.Add(null, XmlReader.Create(stream));
                }
                StringReader sr = new StringReader(xml);
                XmlSerializer ser = new XmlSerializer(typeof(T));
                XmlReader reader = XmlReader.Create(sr, config);
                return (T)ser.Deserialize(reader);
            }
            catch (InvalidOperationException ex)
            {
                string err = ex.Message;
                if (ex.InnerException != null)
                    err += " " + ex.InnerException.Message;
                throw new XsdValidationException(err, ex);
            }
        }
    }
}
