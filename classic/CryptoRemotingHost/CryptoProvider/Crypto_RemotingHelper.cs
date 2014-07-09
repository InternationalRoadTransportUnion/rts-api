using System;
using IRU.CommonInterfaces;
using System.Xml;
using IRU.RTS.CommonComponents;

using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;




namespace IRU.RTS.CryptoProvider
{
	/// <summary>
	/// Summary description for Crypto_RemotingHelper.
	/// </summary>
	public class Crypto_RemotingHelper:IPlugIn,IRunnable
	{
		
		//if port is 0 dont register a channel as some other plugin is expected to register the channel
		private int m_RemotingPort;//="4000"



		private string m_RemotingEndPoint;//="Crypto_Provider.rem"

		private IPlugInManager m_PluginManager;

		private string m_PluginName;
		public Crypto_RemotingHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IPlugIn Members
		public void Configure(IRU.CommonInterfaces.IPlugInManager PluginManager)
		{
			// TODO:  Add TCHQ_RemotingHelper.Configure implementation

			m_PluginManager = PluginManager;
			try
			{
				XmlNode sectionNode = m_PluginManager.GetConfigurationSection(
					m_PluginName);
            
				XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,
					"./Crypto_RemotingHelperSettings");

				
				string sRemotingPort = XMLHelper.GetAttributeNode(parameterNode,
					"RemotingPort").InnerText;
				m_RemotingPort= int.Parse(sRemotingPort);
				m_RemotingEndPoint = XMLHelper.GetAttributeNode(parameterNode,
					"RemotingEndPoint").InnerText;
			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of TCHQ_RemotingHelper"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of TCHQ_RemotingHelper"
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure of TCHQ_RemotingHelper"
					+ e.Message);
				throw e;
			}

			//register channels here so that more channels can be registered in config in other plugins
			if ( m_RemotingPort != 0)
			{
				TcpChannel chan = new TcpChannel(m_RemotingPort);
				ChannelServices.RegisterChannel(chan);
			}
			else
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning," TCHQ_Remoting helper has not registered channels, expecting other plugin to register a remoting tcpchannel");

			}

		}

		public void Unload()
		{
			// TODO:  Add TCHQ_RemotingHelper.Unload implementation
		}

		public string PluginName
		{
			get
			{
				
				return m_PluginName;
			}
			set
			{
				m_PluginName=value;
				
			}
		}

		#endregion

		#region IRunnable Members

		public void Start()
		{
			
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(CryptoOperations),m_RemotingEndPoint,  WellKnownObjectMode.Singleton);				
		
		}

		public void Stop()
		{
			// TODO:  Add Crypto_RemotingHelper.Stop implementation
		}

		#endregion
	}
}
