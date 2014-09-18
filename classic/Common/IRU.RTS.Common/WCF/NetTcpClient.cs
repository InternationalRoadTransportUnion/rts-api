using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace IRU.RTS.Common.WCF
{
	public class NetTcpClient<T> : ClientBase<T>, IDisposable where T : class
	{
		public NetTcpClient(string remoteAddress) :
			base(new NetTcpBinding(), new EndpointAddress("net.tcp://127.0.0.1"))
		{			
			NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
			binding.CloseTimeout = new TimeSpan(0, 1, 0);
			binding.MaxBufferPoolSize = int.MaxValue;
			binding.MaxBufferSize = int.MaxValue;
			binding.MaxReceivedMessageSize = int.MaxValue;
			binding.OpenTimeout = new TimeSpan(0, 1, 0);
			binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
			binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
			binding.ReaderQuotas.MaxDepth = int.MaxValue;
			binding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
			binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
			binding.ReceiveTimeout = new TimeSpan(0, 15, 0);
			binding.SendTimeout = new TimeSpan(0, 15, 0);

			Endpoint.Binding = binding;

			if (remoteAddress.StartsWith("tcp://"))
			{
				remoteAddress = String.Format("net.{0}", remoteAddress);
			}

			Endpoint.Address = new EndpointAddress(remoteAddress);
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
