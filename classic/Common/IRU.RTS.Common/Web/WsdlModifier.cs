using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

using IRU.RTS.Common.Extension;

namespace IRU.RTS.Common.Web
{
	public class WsdlModifier
	{
		public static void Process(HttpRequest request, HttpResponse response)
		{
			if (request.Url.OriginalString.EndsWith("?wsdl", StringComparison.InvariantCultureIgnoreCase))
			{
				string webAppPath = HttpContext.Current.ApplicationInstance.Server.MapPath("~/");
				string wsdlPath = String.Format("{0}.wsdl", Path.GetFileNameWithoutExtension(request.Url.AbsolutePath));
				string wsdlFullPath = Path.Combine(webAppPath, wsdlPath);

				if (File.Exists(wsdlFullPath))
				{
					response.ContentType = "text/xml";
					response.Charset = "utf-8";
					response.ContentEncoding = Encoding.UTF8;
					
					XmlDocument xdWsdl = new XmlDocument();
					xdWsdl.Load(wsdlFullPath);
					
					XElement xeWsdl = XElement.Parse(xdWsdl.InnerXml);

					foreach (XElement xeSvc in xeWsdl.Descendants("{http://schemas.xmlsoap.org/wsdl/}service"))
					{
						// SOAP 1.1
						foreach (XElement xeAddr in xeSvc.Descendants("{http://schemas.xmlsoap.org/wsdl/soap/}address"))
						{
							Uri uPub = new Uri(xeAddr.Attribute("location").Value);
							Uri uNew = TranslateUri(request, uPub);
							xeAddr.SetAttributeValue("location", uNew.ToString());
						}

						// SOAP 1.2
						foreach (XElement xeAddr in xeSvc.Descendants("{http://schemas.xmlsoap.org/wsdl/soap12/}address"))
						{
							Uri uPub = new Uri(xeAddr.Attribute("location").Value);
							Uri uNew = TranslateUri(request, uPub);
							xeAddr.SetAttributeValue("location", uNew.ToString());
						}
					}

					XmlWriterSettings xws = new XmlWriterSettings();
					xws.Encoding = Encoding.UTF8;
					xws.Indent = true;
					using (XmlWriter xw = XmlWriter.Create(response.OutputStream, xws))
					{
						xeWsdl.WriteTo(xw);
					}

					response.End();
				}
			}
		}

		private static Uri TranslateUri(HttpRequest request, Uri publishedUri)
		{
			Uri uCalled = request.Url;
			string sAbsPath = publishedUri.AbsolutePath;

			Uri uUri = new Uri(new Uri(request.GetCallerProtocol() + "://" + uCalled.Authority), sAbsPath);

			return uUri;
		}
	}
}
