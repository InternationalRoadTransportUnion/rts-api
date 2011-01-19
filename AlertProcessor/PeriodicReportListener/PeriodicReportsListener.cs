using System;
using IRU.CommonInterfaces;
using IRU.RTS.CommonComponents;
using System.Xml;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.IO;


namespace IRU.RTS.AlertProcessor
{
	/// <summary>
	/// Summary description for ReportListener.
	/// </summary>
	public class PeriodicReportsListener:IPlugIn, IRunnable 
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

		public PeriodicReportsListener()
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
					"XMLNode not found in .Configure of ReportListener"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of ReportListener"
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure of ReportListener"
					+ e.Message);
				throw e;
			}

						


		}


		public void Unload()
		{
			// TODO:  Add ReportListener.Unload implementation
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

			ThreadStart ts = new ThreadStart(PRListenerThreadFunction);

			m_DBListenThread = new Thread(ts);

			m_DBListenThread.Name = m_PluginName + " thread";

			m_DBListenThread.Start();	

			
		}

		public void Stop()
		{
			// TODO:  Add ReportListener.Stop implementation
			m_ShutDownEvent.Set();
			m_DBListenThread.Join();
		}

		#endregion


		#region Mainthread Function

		private void PRListenerThreadFunction()
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
					
					
					PollReport();
						
					
				}
				catch (Exception ex)
				{
					//do nothing
					Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError,"Alert ReportListenerthread exception swallowed ." + ex.Message + "\r\n" +  ex.StackTrace);
				}
				

				#endregion
			
			}
		
		}
		#endregion

		private bool PollReport()
		{
			bool bRecordProcessed=false;
			
			IDBHelper dbHelper = m_dbHelperFactoryPlugin.GetDBHelper(m_DBToMonitor ) ;
			try
			{			
				
				string sCommand ="SELECT     PERIODIC_REPORT.REPORT_ID, PERIODIC_REPORT.SERVICE_ID, PERIODIC_REPORT_SCHEDULE.NEXT_SCHEDULE_ID,  PERIODIC_REPORT_SQL.QUERY_SQL,PERIODIC_REPORT_SQL.QUERY_DESCRIPTION, PERIODIC_REPORT_SCHEDULE.SCHEDULE_DAY, PERIODIC_REPORT_SCHEDULE.SCHEDULE_TIME FROM         PERIODIC_REPORT with (nolock) INNER JOIN  PERIODIC_REPORT_SCHEDULE with (nolock)  ON PERIODIC_REPORT.REPORT_ID = PERIODIC_REPORT_SCHEDULE.REPORT_ID AND  PERIODIC_REPORT.NEXT_SCHEDULE_ID = PERIODIC_REPORT_SCHEDULE.SCHEDULE_ID INNER JOIN                       PERIODIC_REPORT_SQL with (nolock) ON PERIODIC_REPORT.REPORT_ID = PERIODIC_REPORT_SQL.REPORT_ID WHERE     (PERIODIC_REPORT.NEXT_EXECUTION_TIME <= @NOW)  OR                       (PERIODIC_REPORT.NEXT_EXECUTION_TIME IS NULL) ORDER BY PERIODIC_REPORT.REPORT_ID";
				

				SqlCommand sPollCommand = new SqlCommand();
				sPollCommand.CommandText=sCommand;

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

					int nReportID= (int)drAlert[0];
					string sServiceID = (string)drAlert[1];
					int nNextSchedID= (int)drAlert[2];
					string sSQL=(string)drAlert[3];
					string sDescription=(string)drAlert[4];
					int nNextDay= (int)drAlert[5];
					string nNextTime= (string)drAlert[6];


					#region get NExtScheduleID and Time
					DateTime dNextTimeToRun, dLastTimeRun;
					dLastTimeRun= DateTime.Now;
					dNextTimeToRun = CalculateNextTime(dLastTimeRun, nNextDay, nNextTime);

					

					#endregion
					

						StringBuilder sMessage = new StringBuilder();

					    sMessage.Append("Report Delivery System:");
						
						sMessage.Append("\r\n");
						sMessage.Append("Please find attached periodic report:");

					    sMessage.Append("\r\n");

						sMessage.Append("Generation Time :");
						sMessage.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss" ));
						sMessage.Append("\r\n");


						sMessage.Append("Service ID :");
						sMessage.Append(sServiceID);
						sMessage.Append("\r\n");


						sMessage.Append("ReportID :");
						sMessage.Append(nReportID.ToString());
						sMessage.Append("\r\n");

						sMessage.Append("Description:");
						sMessage.Append(sDescription.ToString());
						sMessage.Append("\r\n");

					
					    
						sMessage.Append("\r\n This is an automated Report Delivery. Do not reply.");
						sMessage.Append("\r\n");

					#region Dump Report
						string sReportString= RunReport(sSQL);
					#endregion


						Hashtable htMessageParams = new Hashtable();
						htMessageParams.Add("subject",  sServiceID + " Report:" +  sDescription);

					    htMessageParams.Add("attachmentContents",sReportString.ToString());
					    htMessageParams.Add("attachmentName", sServiceID + "_" + nReportID.ToString()+"_" + dLastTimeRun.ToString("yyyyMMddHHss")+".csv"  );

						UpdateReportStatus(nReportID,dLastTimeRun,dNextTimeToRun,nNextSchedID);

						m_INotificationHandler.Notify(sMessage.ToString()  ,htMessageParams);
					
					
					
					//2006-11-06 - moved to above sending the email
					//UpdateReportStatus(nReportID,dLastTimeRun,dNextTimeToRun,nNextSchedID);
					
					
				}


			}
			finally
			{
				
				dbHelper.Close();
			
			}

			return bRecordProcessed;

		
		
		
		
		}

		private DateTime CalculateNextTime(DateTime LastTimeRun,  int NextDay, string NextTime)
		{
		
			DateTime dtNextRunTime;
			
				int loopDOW=-1;
				DateTime dtTomorrow = LastTimeRun;
			
			    loopDOW= (int)dtTomorrow.DayOfWeek;

				while (loopDOW+1!=NextDay) //loop till we have our day could be the same day
				{
					dtTomorrow= dtTomorrow.AddDays(1); //jump to the next date and check DOW
					loopDOW= (int)dtTomorrow.DayOfWeek;
				}
			
				dtNextRunTime=DateTime.Parse(dtTomorrow.ToString("yyyy-MM-dd") + " " + NextTime) ;
			//In case the dtnextruntime is less than lastrun time then the user has probably selected only one day and time in the week to run this report so just add 7 days to dtNextRunTime and return
			if (dtNextRunTime <LastTimeRun) 
			{
				dtNextRunTime= dtNextRunTime.AddDays(7);
			}

			
			return dtNextRunTime;


			}

		private void UpdateReportStatus(int ReportID, DateTime LastRun,DateTime NextTimeToRun, int NextScheduleID  )
		{
			SqlCommand sUpdateCommand = new SqlCommand();

			#region UpdateStatement
			string sUpdate ="UPDATE [PERIODIC_REPORT]   SET [NEXT_EXECUTION_TIME] = @NEXT_EXECUTION_TIME,  [LAST_EXECUTION_TIME] = @LAST_EXECUTION_TIME, [NEXT_SCHEDULE_ID] = @NEXT_SCHEDULE_ID       WHERE REPORT_ID=@REPORT_ID";


			sUpdateCommand.CommandText=sUpdate;
			#endregion

			#region set params
			SqlParameter sParam;

			sParam = new SqlParameter("@REPORT_ID",SqlDbType.Int);
			sParam.Value=ReportID;
			sUpdateCommand.Parameters.Add(sParam);

			
			sParam = new SqlParameter("@NEXT_EXECUTION_TIME",SqlDbType.DateTime);
			sParam.Value=NextTimeToRun;
			sUpdateCommand.Parameters.Add(sParam);

			
		

			sParam = new SqlParameter("@LAST_EXECUTION_TIME",SqlDbType.DateTime);
			sParam.Value=LastRun;
			sUpdateCommand.Parameters.Add(sParam);

			sParam = new SqlParameter("@NEXT_SCHEDULE_ID",SqlDbType.Int);
			sParam.Value=NextScheduleID;
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


		private string RunReport(string SQLStatement)
		{
		
			#region connect to DB and run the SQL
			IDBHelper ldbHelper = m_dbHelperFactoryPlugin.GetDBHelper(m_DBToMonitor ) ;

			StringBuilder sReportBuilder = new StringBuilder();
				
			
			StringWriter sRepWriter = new StringWriter(sReportBuilder);
		
		
			#region write out Contents


		
		
			IDataReader repReader=null; 
			try
			{
				ldbHelper.ConnectToDB();
				repReader =  ldbHelper.GetDataReader(SQLStatement, CommandBehavior.SingleResult );
				bool bReadResult= repReader.Read();
				int nRowCounter = 0;
				#region DumpHeader

				for (int nFieldCounter=0; nFieldCounter< repReader.FieldCount; nFieldCounter++)
				{
					sRepWriter.Write(repReader.GetName(nFieldCounter));
					if (nFieldCounter<repReader.FieldCount-1)
					{
						sRepWriter.Write(",");
					}
					
				}

				sRepWriter.Write("\r\n");
				#endregion
				
				if (bReadResult==true)
				{
					do
					{
						#region DumpRows

						for (int nFieldCounter=0; nFieldCounter< repReader.FieldCount; nFieldCounter++)
						{
							sRepWriter.Write(repReader[nFieldCounter]);
							if (nFieldCounter<repReader.FieldCount-1)
							{
								sRepWriter.Write(",");

							}
					
						}
						sRepWriter.Write("\r\n");

						nRowCounter++;
						#endregion
					}while ( repReader.Read());
				}
				else
				{
					sRepWriter.Write( "No Data matching your criteria.");
					
				}
				
				

			}
			catch (SqlException exSQL)
			{
					Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError, "Error occured in ReportRunner \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number + "\r\n" + exSQL.StackTrace );
				return "Error Processing Report. Check Log file on the server.";
			
			}
			finally
			{
				sRepWriter.Flush();
				sRepWriter.Close();
				if (repReader!=null)
				{
					repReader.Close();
				}
				ldbHelper.Close();
			}


			#endregion

		
			
			
			#endregion

			return sReportBuilder.ToString();
		}
		

	}
}
