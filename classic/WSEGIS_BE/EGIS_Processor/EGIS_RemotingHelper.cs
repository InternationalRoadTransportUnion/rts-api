using System;
using System.Runtime.Remoting;
using IRU.RTS;
using System.Xml;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using IRU.CommonInterfaces;
using IRU.RTS.CommonComponents;
using IRU.RTS.Common.WCF;

namespace IRU.RTS.WSEGIS
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class EGIS_RemotingHelper:   IRU.CommonInterfaces.IRunnable, IRU.CommonInterfaces.IPlugIn
    {
        internal static IDBHelperFactory  m_dbHelperFactoryPlugin ;//="DBHelperFactory" 
        
        internal static IRU.CommonInterfaces.ICache  m_InMemoryCachePlugin;//="InMemoryCache"
 
        private NetTcpServiceHost<EGIS_QueryProcessor, IEGISProcessor> _serviceHost = null;
        
        //if port is 0 dont register a channel as some other plugin is expected to register the channel
        private int m_RemotingPort;//="4000"


        internal static string m_CryptoProviderEndpoint;//="tcp://server:Port/CryptoProvider.rem" 
        private string m_RemotingEndPoint;//="WSEGIS_Processor.rem"

        private IPlugInManager m_PluginManager;

        private string m_PluginName;

        private string m_SchemaFilesPath;

        internal static string m_CarnetingServiceEndpoint;

        internal static string m_TransitMessagingServiceEndPoint;

        public EGIS_RemotingHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region IRunnable Members

        public void Start()
        {
            //Register the processor class with Remoting system.

            try
            {
                _serviceHost = new NetTcpServiceHost<EGIS_QueryProcessor,IEGISProcessor>(m_RemotingPort, m_RemotingEndPoint);
                _serviceHost.Open();
            }
            catch (Exception ex)
            {
                Stop();

                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError,
                     "Can't start ServiceHost: "
                     + ex.Message);
                throw ex;
            }           
            
            //Mandar 28-Sep-05

            //Read the schema files into XMLHelper
            string QuerySchemaPath = m_SchemaFilesPath + "\\EGIS.xsd";
            XMLValidationHelper.PopulateSchemas("http://rts.iru.org/egis", QuerySchemaPath);
        }

        public void Stop()
        {
            // TODO:  Add EGIS_RemotingHelper.Stop implementation
            if (_serviceHost != null)
            {
                _serviceHost.Dispose();
                _serviceHost = null;
            }
        }

        #endregion

        #region IPlugIn Members

        public void Configure(IRU.CommonInterfaces.IPlugInManager PluginManager)
        {
            // TODO:  Add EGIS_RemotingHelper.Configure implementation

            m_PluginManager = PluginManager;
            try
            {
                XmlNode sectionNode = m_PluginManager.GetConfigurationSection(
                    m_PluginName);
            
                XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,
                    "./EGIS_RemotingHelperSettings");

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

                m_CarnetingServiceEndpoint = XMLHelper.GetAttributeNode(parameterNode,
                    "CarnetingServiceEndpoint").InnerText;

                m_TransitMessagingServiceEndPoint = XMLHelper.GetAttributeNode(parameterNode,
                    "TransitMessagingServiceEndPoint").InnerText;
            }
            catch(ApplicationException e)
            {
                Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
                    "XMLNode not found in .Configure of EGIS_RemotingHelper"
                    + e.Message);
                throw e;
            }
            catch(ArgumentNullException e)
            {
                Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
                    "XMLNode not found in .Configure of EGIS_RemotingHelper"
                    + e.Message);
                throw e;
            }
            catch(FormatException e)
            {
                Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
                    "Invalid value while formating an XMLNode in .Configure of EGIS_RemotingHelper"
                    + e.Message);
                throw e;
            }

            //register channels here so that more channels can be registered in config in other plugins
            if (m_RemotingPort == 0)
            {
                Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning," EGIS_Remoting helper has not registered channels, expecting other plugin to register a remoting tcpchannel");

            }

        }

        public void Unload()
        {
            // TODO:  Add EGIS_RemotingHelper.Unload implementation
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
