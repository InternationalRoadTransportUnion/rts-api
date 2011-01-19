using System;
using IRU.CommonInterfaces;
using IRU.RTS.CommonComponents;
using System.Xml;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;


namespace IRU.RTS.AlertProcessor
{
	/// <summary>
	/// Summary description for LogListener.
	/// </summary>
	public class LogListener:IPlugIn, IRunnable 
	{
		#region Declare Variables

		internal  IDBHelperFactory  m_dbHelperFactoryPlugin ;//="DBHelperFactory" 

		private System.Threading.ManualResetEvent m_ShutDownEvent;

		Thread m_DBListenThread;	
		

		private IPlugInManager m_PluginManager;

		private string m_PluginName;


		private INotificationHandler m_INotificationHandler;

		private string m_DBToMonitor;

		//internal DBHelper m_IDBToMonitor;

		private int m_PollDelay;


		#endregion

		public LogListener()
		{
			//
			// TODO: Add constructor logic here
			//
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
					"./ListenerSettings");

				string sDBFactoryName = XMLHelper.GetAttributeNode(parameterNode,
					"IDBHelperFactory").InnerText;
				m_dbHelperFactoryPlugin = (IDBHelperFactory) m_PluginManager.GetPluginByName(sDBFactoryName);

				
				m_PollDelay = int.Parse( XMLHelper.GetAttributeNode(parameterNode,
					"PollDelay").InnerText);

				m_DBToMonitor=XMLHelper.GetAttributeNode(parameterNode,
					"DBToMonitor").InnerText;


				string sNotificationHandler = XMLHelper.GetAttributeNode(parameterNode,
					"INotificationHandler").InnerText;
				m_INotificationHandler = (INotificationHandler) m_PluginManager.GetPluginByName(sNotificationHandler);

				
			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of LogListener"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of LogListener"
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure of LogListener"
					+ e.Message);
				throw e;
			}

						


		}


		public void Unload()
		{
			// TODO:  Add LogListener.Unload implementation
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
			// TODO:  Add LogListener.Stop implementation
			m_ShutDownEvent.Set();
			m_DBListenThread.Join();
		}

		#endregion


		#region Mainthread Function

		private void ListenerThreadFunction()
		{
		
		
			while (true)
			{
				//wait on event convert into milliseconds
				bool bDidFire;
				bDidFire= m_ShutDownEvent.WaitOne( m_PollDelay * 1000,false);

				if (bDidFire)
					break; //get out of the loop

				#region Main Processing
				try
				{
					
					
						PollAlertTrack();
						
					
				}
				catch (Exception ex)
				{
					//do nothing
					Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError,"Alert LogListenerthread exception swallowed ." + ex.Message + "\r\n" +  ex.StackTrace);
				}
				

				#endregion
			
			}
		
		}

		private bool PollAlertTrack()
		{
			bool bRecordProcessed=false;
			
			IDBHelper dbHelper = m_dbHelperFactoryPlugin.GetDBHelper(m_DBToMonitor ) ;
			try
			{			
				
				string sCommand ="SELECT [SERVICE_ID], [FAULT_TYPE] ,[THRESHOLD_QUANTITY],[SQL_QUERY],[LAST_COUNT],       [LAST_RUN_TIME]      ,[NO_FAULT_DELAY]      ,[FAULTS_NO_ALERT_DELAY]      ,[ALERT_DELAY]      ,[ALERT_DESCRIPTION] FROM [ALERT_TRACK] with (readpast) WHERE ([NEXT_TIME_TO_RUN] < @NOW OR [NEXT_TIME_TO_RUN] IS NULL) ORDER BY FAULT_TYPE";
		
				SqlCommand sPollCommand = new SqlCommand();
				sPollCommand.CommandText=sCommand;
				sPollCommand.CommandTimeout = 500;

				SqlParameter sParam = new SqlParameter("@NOW",SqlDbType.DateTime);
				sParam.Value=DateTime.Now;
				sPollCommand.Parameters.Add(sParam);


				DataSet dsAlerts = new DataSet();

				dbHelper.ConnectToDB();
				dbHelper.FillDataSetTable(sPollCommand ,dsAlerts,"AlertTrack");
				dbHelper.Close();
				

				DataTable dtAlerts= dsAlerts.Tables[0];
		
				foreach (DataRow drAlert in dtAlerts.Rows)
				{

					//check for service shutdown
					bool bDidFire;
					bDidFire= m_ShutDownEvent.WaitOne( 100,false);//wait 100 ms to check if shutdown has started

					if (bDidFire)
						break; //get out of the loop

					string sServiceID = (string)drAlert[0];
					int nFaultType= (int)drAlert[1];
					int nThreshHoldQty= (int)drAlert[2];

					string sSQL=(string)drAlert[3];

					int nLastCount= drAlert[4]==System.DBNull.Value?0:(int)drAlert[4];
					DateTime dLastRunTime= drAlert[5]==System.DBNull.Value?DateTime.MinValue :(DateTime)drAlert[5];
					int nNoFaultDelay= (int)drAlert[6];
					int nFaultNoAlertDelay= (int)drAlert[7];
					int nAlertDelay= (int)drAlert[8];
					string sAlertDescription =(string)drAlert[9];

					int nReturnCount= AlertRunner(sSQL);

					DateTime dNextTimeToRun, dLastTimeRun;
					dLastTimeRun= DateTime.Now;

					if (nReturnCount>nThreshHoldQty) //generate alert
					{
						dNextTimeToRun= dLastTimeRun.AddMinutes(nAlertDelay);

						StringBuilder sMessage = new StringBuilder();

						sMessage.Append("RTS Alert delivery:");
						
						sMessage.Append("\r\n");
						sMessage.Append("A alert has been generated as parameter value has crossed threshold limit:");

						sMessage.Append("\r\n");

						sMessage.Append("Generation Time :");
						sMessage.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss" ));
						sMessage.Append("\r\n");


						sMessage.Append("Service ID :");
						sMessage.Append(sServiceID);
						sMessage.Append("\r\n");


						sMessage.Append("Fault :");
						sMessage.Append(nFaultType.ToString());
						sMessage.Append("\r\n");

						sMessage.Append("Desc :");
						sMessage.Append(sAlertDescription);
						sMessage.Append("\r\n");


						sMessage.Append("Alert Count:");
						sMessage.Append(nReturnCount.ToString());
						sMessage.Append("\r\n");

						sMessage.Append("Threshold Quantity :");
						sMessage.Append(nThreshHoldQty.ToString());
						sMessage.Append("\r\n");


						sMessage.Append("\r\n This is an automated Alert Delivery. Do not reply.");
						sMessage.Append("\r\n");

						Hashtable htMessageParams = new Hashtable();
						htMessageParams.Add("subject", sServiceID + " Alert");

						m_INotificationHandler.Notify(sMessage.ToString(),htMessageParams);
						Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceVerbose ,
							"ALert generated for notification" + (string)htMessageParams["subject"]);
					} 
					else if (nReturnCount > 0) //faults no alert delay
					{
						dNextTimeToRun= dLastTimeRun.AddMinutes(nFaultNoAlertDelay);
					}
					else
					{
						dNextTimeToRun= dLastTimeRun.AddMinutes(nNoFaultDelay);
					}
					
					UpdateAlertStatus(nFaultType,dLastTimeRun,dNextTimeToRun,nReturnCount);
					
					
				}


			}
			finally
			{
				
				dbHelper.Close();
			
			}

			return bRecordProcessed;

		
		
		
		
		}

		private void UpdateAlertStatus(int FaultType, DateTime LastRun, DateTime NextTimeToRun, int LastCount)
		{
			SqlCommand sUpdateCommand = new SqlCommand();

			#region UpdateStatement
		string sUpdate ="UPDATE [ALERT_TRACK]   SET [NEXT_TIME_TO_RUN] = @NEXT_TIME_TO_RUN,  [LAST_COUNT] = @LAST_COUNT, [LAST_RUN_TIME] = @LAST_RUN_TIME       WHERE FAULT_TYPE=@FAULT_TYPE";

			sUpdateCommand.CommandTimeout = 500; 
			sUpdateCommand.CommandText=sUpdate;
			#endregion

			#region set params
			SqlParameter sParam;

			sParam = new SqlParameter("@FAULT_TYPE",SqlDbType.Int);
			sParam.Value=FaultType;
			sUpdateCommand.Parameters.Add(sParam);

			
			sParam = new SqlParameter("@NEXT_TIME_TO_RUN",SqlDbType.DateTime);
			sParam.Value=NextTimeToRun;
			sUpdateCommand.Parameters.Add(sParam);

			
			sParam = new SqlParameter("@LAST_COUNT",SqlDbType.Int);
			sParam.Value=LastCount;
			sUpdateCommand.Parameters.Add(sParam);

			sParam = new SqlParameter("@LAST_RUN_TIME",SqlDbType.DateTime);
			sParam.Value=LastRun;
			sUpdateCommand.Parameters.Add(sParam);



			#endregion
		
		
			#region execute
			IDBHelper ldbHelper = m_dbHelperFactoryPlugin.GetDBHelper(m_DBToMonitor ) ;

			try

			{
				ldbHelper.ConnectToDB();
				ldbHelper.ExecuteNonQuery(sUpdateCommand);

			}
		
			finally
			{
				ldbHelper.Close();
			}


			#endregion
		
		}

		private int AlertRunner(string SQLStatement)
		{
			int nCount=0;
			#region connect to DB and run the SQL
			IDBHelper dbHelper = m_dbHelperFactoryPlugin.GetDBHelper(m_DBToMonitor ) ;

			try

			{
				dbHelper.ConnectToDB();
				nCount = (int)dbHelper.ExecuteScaler(SQLStatement);
			}
			finally
			{
				dbHelper.Close();
			}
			#endregion

			return nCount;
		}
		#endregion

	}
}
