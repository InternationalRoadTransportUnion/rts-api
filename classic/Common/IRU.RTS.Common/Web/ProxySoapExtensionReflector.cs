using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services.Description;

namespace IRU.RTS.Common.Web
{
	public class ProxySoapExtensionReflector: SoapExtensionReflector
	{
		public override void ReflectMethod()
		{
			// nothing
		}

		public override void ReflectDescription()
		{
			ServiceDescription description = ReflectionContext.ServiceDescription;
			foreach (Service service in description.Services)
			{
				foreach (Port port in service.Ports)
				{
					foreach (ServiceDescriptionFormatExtension extension in port.Extensions)
					{
						SoapAddressBinding binding = extension as SoapAddressBinding;
						if (null != binding)
						{
							Uri uriLocation = new Uri(binding.Location);
							binding.Location = String.Format("http://replace.me.iru.org{0}", uriLocation.AbsolutePath);
						}
					}
				}
			}

			string webAppPath = HttpContext.Current.ApplicationInstance.Server.MapPath("~/");
			string wsdlPath = String.Format("{0}.wsdl", Path.GetFileNameWithoutExtension(new Uri(ReflectionContext.ServiceUrl).AbsolutePath));
			description.Write(Path.Combine(webAppPath, wsdlPath));
		}
	}
}
