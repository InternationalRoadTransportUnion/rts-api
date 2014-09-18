using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace IRU.RTS.Common.WCF
{
	public class NetTcpServiceHost<T, I> : ServiceHost, IDisposable where T : class where I : class
	{
		public NetTcpServiceHost(int tcpPort, string endPoint) :
			base(typeof(T))
		{
			if (!typeof(I).IsInterface)
			{
				throw new Exception(String.Format("{0} must be an interface", typeof(I).ToString()));
			}

			if (!typeof(I).IsAssignableFrom(typeof(T)))
			{
				throw new Exception(String.Format("{0} must implements {1}", typeof(T).ToString(), typeof(I).ToString()));
			}

			string remotingAddress = String.Format("net.tcp://localhost:{0}/{1}", tcpPort, endPoint);

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
			AddServiceEndpoint(typeof(I), binding, remotingAddress);

			// Check to see if the service host already has a ServiceMetadataBehavior
			ServiceMetadataBehavior smb = Description.Behaviors.Find<ServiceMetadataBehavior>();
			// If not, add one
			if (smb == null)
				smb = new ServiceMetadataBehavior();
			smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
			Description.Behaviors.Add(smb);
			AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), remotingAddress + "/mex");

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
