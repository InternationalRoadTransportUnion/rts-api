using System;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;
using IRU.RTS.CryptoInterfaces;
using System.Xml;
using System.Threading;
using System.Data.SqlClient;
using System.Data;

namespace IRU.RTS.CopyToProcessor
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class CopyToJobListener:IPlugIn, IRunnable
	{


		#region Declare Variables

		internal static IDBHelperFactory  m_dbHelperFactoryPlugin ;//="DBHelperFactory" 

		internal static string m_DoubleAgentDropPath; 
		internal static string m_TemporaryFolderPath;

		private int m_DelayBetweenPollsInMilliSeconds;

		private System.Threading.ManualResetEvent m_ShutDownEvent;

		Thread m_DBListenThread;

		internal static string m_CryptoProviderEndpoint;

		private IPlugInManager m_PluginManager;

		private string m_PluginName;

		

		
		#endregion



		public CopyToJobListener()
		{
		}
		#region IPlugIn Members

		public void Configure(IPlugInManager pluginManager)
		{
			m_PluginManager = pluginManager;
			try
			{
				XmlNode sectionNode = m_PluginManager.GetConfigurationSection(
					m_PluginName);
            
				XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,
					"./CopyToJobListenerSettings");

				string sDBFactoryName = XMLHelper.GetAttributeNode(parameterNode,
					"DBHelperFactoryPlugin").InnerText;

				m_dbHelperFactoryPlugin = (IDBHelperFactory) m_PluginManager.GetPluginByName(sDBFactoryName);

				m_TemporaryFolderPath =XMLHelper.GetAttributeNode(parameterNode,
					"TemporaryFolderPath").InnerText;

				if (m_TemporaryFolderPath.LastIndexOf("\\")!= m_TemporaryFolderPath.Length-1)
					m_TemporaryFolderPath+="\\";

				m_DoubleAgentDropPath = XMLHelper.GetAttributeNode(parameterNode,
					"DoubleAgentDropPath").InnerText;

				if (m_DoubleAgentDropPath.LastIndexOf("\\")!= m_DoubleAgentDropPath.Length-1)
					m_DoubleAgentDropPath+="\\";

				m_CryptoProviderEndpoint = XMLHelper.GetAttributeNode(parameterNode,
					"CryptoProviderEndpoint").InnerText;

				m_DelayBetweenPollsInMilliSeconds = int.Parse(XMLHelper.GetAttributeNode(parameterNode,
					"DelayBetweenPollsInMilliSeconds").InnerText);

				
				
				
			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of WSCscc_JobListener"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of WSCscc_JobListener"
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure of WSCscc_JobListener"
					+ e.Message);
				throw e;
			}

		}

		public void Unload()
		{
			Stop();
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
			m_ShutDownEvent = new ManualResetEvent(false);
			
			//moved this out to the main thread, if there is any exception it used to prevent the PM from loading

			//SetupFileSystemListener();

			ThreadStart ts = new ThreadStart(ListenerThreadFunction);

			m_DBListenThread = new Thread(ts);

			m_DBListenThread.Name = m_PluginName + " thread";

			m_DBListenThread.Start();	
		}

		public void Stop()
		{
			m_ShutDownEvent.Set();
		}

		#endregion

		#region Mainthread Function

		private void ListenerThreadFunction()
		{
		
		
			while (true)
			{
				//wait on event convert into milliseconds
				bool bDidFire;
				bDidFire= m_ShutDownEvent.WaitOne( m_DelayBetweenPollsInMilliSeconds ,false);

				if (bDidFire)
					break; //get out of the loop

				bool bRecordProcessed = false;
				#region Main Processing
				try
				{
					
					do
					{
					
						bRecordProcessed=PollRecord();
					}
					while (bRecordProcessed==true);
					
				}
				catch (Exception ex)
				{
					//do nothing
					Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceWarning,"File Watcher thread exception swallowed ." + ex.Message + "\r\n" +  ex.StackTrace);
				}
				

				#endregion
			
			}
		
		}

		private bool PollRecord()
		{
			bool bRecordProcessed=false;
			
			IDBHelper dbHelperWSSTCOPYTO = m_dbHelperFactoryPlugin.GetDBHelper("WSST_COPYTODB") ;

			try
			{
				dbHelperWSSTCOPYTO.ConnectToDB();
				//string sCommand ="Update  WSST_Copy_to_Job  set @outID=SAFETIR_Message_IN_ID, Job_status=1, Job_pickuptime=getdate() where  SAFETIR_Message_IN_ID = (Select top 1 SAFETIR_Message_IN_ID from WSST_Copy_to_Job with (READPAST) where Job_status=0 order by SAFETIR_Message_IN_ID) " + ;


                string sCommand = "Update  dbo.WSST_Copy_to_Job  set @outID=SAFETIR_Message_IN_ID, JOB_STATUS=1, JOB_PICKUP_TIME=getdate() where  SAFETIR_Message_IN_ID = (Select top 1 SAFETIR_Message_IN_ID from dbo.WSST_Copy_to_Job with (READPAST) where Job_status=0 order by SAFETIR_Message_IN_ID) " +
                    " SELECT [SAFETIR_MESSAGE_IN_ID]   ,[COPY_TO_ID] , [SAFETIR_XML] , [SENDER_TCP_IP_ADDRESS], [SUBSCRIBER_ID] FROM dbo.[WSST_COPY_TO_JOB] with (readpast) WHERE  	[SAFETIR_MESSAGE_IN_ID] = @outID";

				SqlCommand sPollCommand = new SqlCommand(sCommand);

				SqlParameter sParam;

				sParam = new SqlParameter("@outID",SqlDbType.Int);
				sParam.Direction = ParameterDirection.Output;
				sPollCommand.Parameters.Add(sParam);

				/*
				sParam = new SqlParameter("@outXML",SqlDbType.NText);
				sParam.Size=8*1024;
				sParam.Direction = ParameterDirection.Output;
				sPollCommand.Parameters.Add(sParam);

				sParam = new SqlParameter("@outCopyToID",SqlDbType.NVarChar);
				sParam.Direction = ParameterDirection.Output;
				sPollCommand.Parameters.Add(sParam);
*/

				IDataReader sqlReader = dbHelperWSSTCOPYTO.GetDataReader( sPollCommand, CommandBehavior.SingleRow);

				if (sqlReader.Read())
				{


					//if (sPollCommand.Parameters["@outID"].Value != System.DBNull.Value) // a row was found
					//{
						/*
						int nSafeTIRInID = (int)sPollCommand.Parameters["@outID"].Value;
						string sXml = (string)sPollCommand.Parameters["@outXML"].Value;
						string sCopyToID=(string)sPollCommand.Parameters["@outCopyToID"].Value;
						*/

					int nSafeTIRInID = int.Parse(sqlReader[0].ToString());
					string sCopyToID=sqlReader[1].ToString();
					string sXml = sqlReader[2].ToString();
					string sIP = sqlReader[3].ToString();
					string sSubscriberID = sqlReader[4].ToString();
					CopyToJobProcessor cJobProcessor = new CopyToJobProcessor();
					cJobProcessor.ProcessCopyToJob(sSubscriberID,sCopyToID,nSafeTIRInID,sIP,sXml);

//////
//////						int nSafeTIRInID = sqlReader.GetInt32(0);
//////						string sCopyToID=sqlReader.GetString(1);
//////						string sXml = sqlReader.GetString(2);

						
						

				
						bRecordProcessed=true;
					//}
					sqlReader.Close();
				}


			}
			finally
			{
				dbHelperWSSTCOPYTO.Close();
			
			}

			return bRecordProcessed;

		
		
		
		
		}
		#endregion
	}
}
