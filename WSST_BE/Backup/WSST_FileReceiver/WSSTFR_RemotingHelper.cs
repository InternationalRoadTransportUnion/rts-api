using System;
using System.Xml;
using IRU.CommonInterfaces;
using IRU.RTS.CommonComponents;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;



namespace IRU.RTS.WSST
{
	/// <summary>
	/// Was WSSTFileReceiverPlugin in the design documents - mandar
	/// </summary>
	public class WSSTFR_RemotingHelper:IPlugIn, IRunnable
	{
		internal static IDBHelperFactory  m_dbHelperFactoryPlugin ;//="DBHelperFactory" 
		private string  m_EXTDBName;// 
		
		
		
		internal static string m_DoubleAgentDropPath; 
		internal static string m_TemporaryFolderPath;

		
		private int m_RemotingPort;//
		
		private string m_RemotingEndPoint;//

		private IPlugInManager m_PluginManager;

		private string m_PluginName;

		public WSSTFR_RemotingHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IPlugIn Members

		public void Configure(IPlugInManager pluginManager)
		{
////			<FileReceiverSettings DBHelperFactoryPlugin="DBHelperFactory" 
//				DBName="WSSTExternalDB" DoubleAgentDropPath="g:\DoubleAgent\Safetir"
////			TemporaryFolderPath="g:\temp\SAFETir" RemotingPort="4000" 
//				RemotingEndPoint="WSSTFileReceive.Rem"></FileReceiverSettings>

			m_PluginManager = pluginManager;
			try
			{
				XmlNode sectionNode = m_PluginManager.GetConfigurationSection(
					m_PluginName);
            
				XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,
					"./FileReceiverSettings");

				string sDBFactoryName = XMLHelper.GetAttributeNode(parameterNode,
					"DBHelperFactoryPlugin").InnerText;
				m_dbHelperFactoryPlugin = (IDBHelperFactory) m_PluginManager.GetPluginByName(sDBFactoryName);

				
				m_DoubleAgentDropPath = XMLHelper.GetAttributeNode(parameterNode,
					"DoubleAgentDropPath").InnerText;

				if (m_DoubleAgentDropPath.LastIndexOf("\\")!= m_DoubleAgentDropPath.Length-1)
					m_DoubleAgentDropPath+="\\";

				m_TemporaryFolderPath =XMLHelper.GetAttributeNode(parameterNode,
					"TemporaryFolderPath").InnerText;

				if (m_TemporaryFolderPath.LastIndexOf("\\")!= m_TemporaryFolderPath.Length-1)
					m_TemporaryFolderPath+="\\";

				
				string sRemotingPort = XMLHelper.GetAttributeNode(parameterNode,
					"RemotingPort").InnerText;
				m_RemotingPort= int.Parse(sRemotingPort);

				m_RemotingEndPoint = XMLHelper.GetAttributeNode(parameterNode,
					"RemotingEndPoint").InnerText;
			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of WSSTFR_RemotingHelper"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of WSSTFR_RemotingHelper"
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure of WSSTFR_RemotingHelper"
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
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning," WSSTFR_Remotinghelper has not registered channels, expecting other plugin to register a remoting tcpchannel");

			}


		}

		public void Unload()
		{
			// TODO:  Add WSSTFR_RemotingHelper.Unload implementation
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
			//Register the processor class with Remoting system.
			 
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(WSST_FileReceiver),m_RemotingEndPoint,  WellKnownObjectMode.SingleCall);				
		    
			
		}

		public void Stop()
		{
			// TODO:  Add WSSTFR_RemotingHelper.Stop implementation
		}

		#endregion
	}
}
