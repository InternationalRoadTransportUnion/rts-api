using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace IRU.RTS.Common.WCF
{
	public class BasicHttpClient<T> : ClientBase<T>, IDisposable where T : class
	{
		public BasicHttpClient(string remoteAddress) :
			base(new BasicHttpBinding(), new EndpointAddress(remoteAddress))
		{
			BasicHttpBinding binding = new BasicHttpBinding();
			if (String.Equals(this.Endpoint.Address.Uri.Scheme, "https", StringComparison.InvariantCultureIgnoreCase))
			{
				binding.Security.Mode = BasicHttpSecurityMode.Transport;
			}
			binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
			binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
			binding.ReaderQuotas.MaxDepth = int.MaxValue;
			binding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
			binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
			binding.MaxBufferSize = int.MaxValue;
			binding.MaxReceivedMessageSize = int.MaxValue;
			binding.OpenTimeout = TimeSpan.FromMinutes(3.0);
			binding.SendTimeout = TimeSpan.FromMinutes(15.0);
			binding.ReceiveTimeout = TimeSpan.FromMinutes(15.0);
			binding.CloseTimeout = TimeSpan.FromMinutes(3.0);

			Endpoint.Binding = binding;
		}

		public T GetProxy()
		{
			return (T)base.Channel;
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (State == CommunicationState.Faulted)
			{
				Abort();
			}
			else if (State != CommunicationState.Closed)
			{
				Close();
			}
		}

		#endregion
	}
}
