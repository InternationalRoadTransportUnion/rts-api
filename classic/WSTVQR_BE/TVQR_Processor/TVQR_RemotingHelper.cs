using System;
using System.Runtime.Remoting;
using IRU.RTS;
using System.Xml;
using IRU.CommonInterfaces;
using IRU.RTS.CommonComponents;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;


namespace IRU.RTS.WSTVQR
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class TVQR_RemotingHelper:   IRU.CommonInterfaces.IRunnable, IRU.CommonInterfaces.IPlugIn
	{
		internal static IDBHelperFactory  m_dbHelperFactoryPlugin ;//="DBHelperFactory" 
		
		internal static IRU.CommonInterfaces.ICache  m_InMemoryCachePlugin;//="InMemoryCache" 
		
		//if port is 0 dont register a channel as some other plugin is expected to register the channel
		private int m_RemotingPort;//="4000"


		internal static string m_CryptoProviderEndpoint;//="tcp://server:Port/CryptoProvider.rem" 
		private string m_RemotingEndPoint;//="WSTVQR_Processor.rem"

		private IPlugInManager m_PluginManager;

		private string m_PluginName;

		private string m_SchemaFilesPath;

        public TVQR_RemotingHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IRunnable Members

		public void Start()
		{
			//Register the processor class with Remoting system.
			 
			 RemotingConfiguration.RegisterWellKnownServiceType(typeof(TVQR_QueryProcessor),m_RemotingEndPoint,  WellKnownObjectMode.SingleCall);				
		    

			
			//Mandar 28-Sep-05

			//Read the schema files into XMLHelper
			string QuerySchemaPath = m_SchemaFilesPath + "\\TVQR.xsd";
			XMLValidationHelper.PopulateSchemas("http://rts.iru.org/tvqr",QuerySchemaPath);
			

		}

		public void Stop()
		{
			// TODO:  Add TVQR_RemotingHelper.Stop implementation
		}

		#endregion

		#region IPlugIn Members

		public void Configure(IRU.CommonInterfaces.IPlugInManager PluginManager)
		{
			// TODO:  Add TVQR_RemotingHelper.Configure implementation

			m_PluginManager = PluginManager;
			try
			{
				XmlNode sectionNode = m_PluginManager.GetConfigurationSection(
					m_PluginName);
            
				XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,
					"./TVQR_RemotingHelperSettings");

				string sDBFactoryName = XMLHelper.GetAttributeNode(parameterNode,
					"DBHelperFactoryPlugin").InnerText;
				m_dbHelperFactoryPlugin = (IDBHelperFactory) m_PluginManager.GetPluginByName(sDBFactoryName);

				string sCacheName = XMLHelper.GetAttributeNode(parameterNode,
					"InMemoryCachePlugin").InnerText;

				m_InMemoryCachePlugin =(ICache ) m_PluginManager.GetPluginByName(sCacheName);
				
				string sRemotingPort = XMLHelper.GetAttributeNode(parameterNode,
					"RemotingPort").InnerText;
				m_RemotingPort= int.Parse(sRemotingPort);

				m_CryptoProviderEndpoint = XMLHelper.GetAttributeNode(parameterNode,
					"CryptoProviderEndpoint").InnerText;
				m_RemotingEndPoint = XMLHelper.GetAttributeNode(parameterNode,
					"RemotingEndPoint").InnerText;

				m_SchemaFilesPath = XMLHelper.GetAttributeNode(parameterNode,
					"SchemaFilesPath").InnerText;
			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of TVQR_RemotingHelper"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of TVQR_RemotingHelper"
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure of TVQR_RemotingHelper"
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
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning," TVQR_Remoting helper has not registered channels, expecting other plugin to register a remoting tcpchannel");

			}

		}

		public void Unload()
		{
			// TODO:  Add TVQR_RemotingHelper.Unload implementation
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
	}
}
