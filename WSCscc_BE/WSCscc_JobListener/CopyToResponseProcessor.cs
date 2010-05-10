using System;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;
using System.IO;
using System.Xml;
using System.Data;
using System.Collections;
//using System.Data.SqlClient;

namespace IRU.RTS.CopyToProcessor
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class CopyToResponseProcessor:IPlugIn, IActiveObject
	{


		#region DB Insert/Update string arrays

		//Step 1 - 30 - Copy Result
		private static string[] m_aStep1 = 
			new string[]{"COPY_RESULT","COPY_RESULT_DESCRIPTION", "RESULT_TIME" ,
							"LAST_STEP", "COMPLETION_TIME"};

		
		private static string[] m_aStepErr = 
			new string[]{"LAST_STEP", "COMPLETION_TIME"};
		#endregion


		#region Declare Variables

		internal static IDBHelperFactory  m_dbHelperFactoryPlugin ;//="DBHelperFactory" 
		private IPlugInManager m_PluginManager;
		private string m_PluginName;

		

		
		#endregion

		public CopyToResponseProcessor()
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
					"./CopyToResponseProcessorSettings");

				string sDBFactoryName = XMLHelper.GetAttributeNode(parameterNode,
					"DBHelperFactoryPlugin").InnerText;

				m_dbHelperFactoryPlugin = (IDBHelperFactory) m_PluginManager.GetPluginByName(sDBFactoryName);

								
			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of CopyToResponseProcessor"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of CopyToResponseProcessor"
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure of CopyToResponseProcessor"
					+ e.Message);
				throw e;
			}

		}

		public void Unload()
		{
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

		#region IActiveObject Members

		public void Enqueue(object objToEnqueue, string queueName)
		{
			throw new NotImplementedException("");
		}

		void IRU.CommonInterfaces.IActiveObject.Enqueue(object objToEnqueue)
		{
			ProcessCopyToResponse((Hashtable)objToEnqueue);			
		}

		#endregion

		#region Private Methods

		private void ProcessCopyToResponse(Hashtable ht)
		{

			DateTime filePickUpTime = DateTime.Now; //used to update time stamp for step 40
			#region Get file data
			byte[] baFileContents = (byte[])ht["FileContents"];
			#endregion
		
			
			#region Read Strings

			string sFileContents = System.Text.Encoding.Unicode.GetString(baFileContents);
	
			long safetir_message_in_id =-1;
			structStepResult[] aStepResult = new structStepResult[3]; 
			StringReader sr = new StringReader(sFileContents);
			bool bAllStepsread = false;

				string sSafetir_message_in_id = sr.ReadLine();
				safetir_message_in_id = long.Parse(sSafetir_message_in_id.Trim());
				
				string sLine;
				for (int stepCntr=0 ;stepCntr<3 ;stepCntr++)
				{
					aStepResult[stepCntr] = new structStepResult();
					#region Step
					sLine= sr.ReadLine();
					if (sLine==null)
						break;

					sLine= sLine.Trim();
                    int nStep = int.Parse(sLine);
					#endregion 

					#region StepResult
					sLine= sr.ReadLine();
					if (sLine==null)
						break;

					sLine= sLine.Trim();
					int nStepResult = int.Parse(sLine);
					#endregion 

					#region StepTimeStamp
					sLine= sr.ReadLine();
					if (sLine==null)
						break;

					sLine= sLine.Trim();
					DateTime nStepTimeStamp = DateTime.Parse(sLine);

					#endregion 

					#region StepDescription
					sLine= sr.ReadLine();
					if (sLine==null)
						break;

					sLine= sLine.Trim();
					string sResultDescription=null;
					if (sLine!="")
					{
						byte[] aBytes = System.Convert.FromBase64String(sLine);
						sResultDescription= System.Text.Encoding.Unicode.GetString(aBytes);
					
					}

					#endregion 

					aStepResult[stepCntr].SetMembers(nStep,nStepResult,nStepTimeStamp,sResultDescription);
					
					if(stepCntr==2)//all steps read successfully
					{
						bAllStepsread	=true;
					}
				}

			if (bAllStepsread==false)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"Unable to read CopyToResponse File"	);
			
					return;
			}

			#region create struct for log and sequence
			WSST_COPY_TO_LOG_STRUCT  Wsst_Copy_To_Log_Data = new WSST_COPY_TO_LOG_STRUCT  ();
			WSST_COPY_TO_SEQUENCE_STRUCT Wsst_Sequence_Data = new WSST_COPY_TO_SEQUENCE_STRUCT();

			#endregion

			
			#region Create DB helper Instances
			IDBHelper dbHelperWSSTCOPYT = CopyToJobListener.m_dbHelperFactoryPlugin.GetDBHelper("WSST_COPYTODB") ;//  null; //dbhelper for tchqdb
			

			 WSCscc_CopyToDbHelper CopytoDB_helper = new WSCscc_CopyToDbHelper(dbHelperWSSTCOPYT, Wsst_Copy_To_Log_Data, Wsst_Sequence_Data); 
			#endregion

			#region Update Step 25
				Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID=safetir_message_in_id;

				Wsst_Sequence_Data.SAFETIR_MESSAGE_IN_ID=Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID;

				Wsst_Sequence_Data.SetMembers(aStepResult[0].Step,aStepResult[0].StepResult,aStepResult[0].ResultDescription,aStepResult[0].StepTimeStamp);

			if (Wsst_Sequence_Data.COPY_STEP_RESULT!=1)
			{
				Wsst_Copy_To_Log_Data.LAST_STEP= new NullableInt( Wsst_Sequence_Data.COPY_STEP);
				Wsst_Copy_To_Log_Data.COMPLETION_TIME =new NullableDateTime (  Wsst_Sequence_Data.LAST_UPDATE_TIME);
			}

			try
			{
				dbHelperWSSTCOPYT.ConnectToDB();
				
				dbHelperWSSTCOPYT.BeginTransaction();  
				CopytoDB_helper.UpdateCopyToLogResultCode(m_aStepErr);
				dbHelperWSSTCOPYT.CommitTransaction();  

			}
			catch(Exception sqlEx)
			{
				dbHelperWSSTCOPYT.RollbackTransaction();

				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
				throw sqlEx ; //Back to FileListener
			}
			finally
			{
				dbHelperWSSTCOPYT.Close();
			}

			#endregion

			
			if(Wsst_Copy_To_Log_Data.LAST_STEP==null)
			{
				#region Update Step 30
			
			
				Wsst_Sequence_Data.SetMembers(aStepResult[1].Step,aStepResult[1].StepResult,aStepResult[1].ResultDescription,aStepResult[1].StepTimeStamp);

				Wsst_Copy_To_Log_Data.COPY_RESULT = new NullableInt( Wsst_Sequence_Data.COPY_STEP_RESULT);
				Wsst_Copy_To_Log_Data.COPY_RESULT_DESCRIPTION = Wsst_Sequence_Data.COPY_STEP_ERROR_DESCRIPTION;
				Wsst_Copy_To_Log_Data.RESULT_TIME= new NullableDateTime(Wsst_Sequence_Data.LAST_UPDATE_TIME);


				if (Wsst_Sequence_Data.COPY_STEP_RESULT!=1)
				{
					Wsst_Copy_To_Log_Data.LAST_STEP= new NullableInt( Wsst_Sequence_Data.COPY_STEP);
					Wsst_Copy_To_Log_Data.COMPLETION_TIME =new NullableDateTime (  Wsst_Sequence_Data.LAST_UPDATE_TIME);
					
				}


				try
				{
					dbHelperWSSTCOPYT.ConnectToDB();
				
					dbHelperWSSTCOPYT.BeginTransaction();  
					CopytoDB_helper.UpdateCopyToLogResultCode(m_aStep1);
					dbHelperWSSTCOPYT.CommitTransaction();  

				}
				catch(Exception sqlEx)
				{
					dbHelperWSSTCOPYT.RollbackTransaction();

					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTCOPYT.Close();
				}

				#endregion
			}

			
			if(Wsst_Copy_To_Log_Data.LAST_STEP==null)
			{
				#region Update Step 35
			
			
				Wsst_Sequence_Data.SetMembers(aStepResult[2].Step,aStepResult[2].StepResult,aStepResult[2].ResultDescription,aStepResult[2].StepTimeStamp);

				
				//this step indicates that the file has been written for intranet since we already
				//read this file we know this step is true hence the error condition below will never happen
				if (Wsst_Sequence_Data.COPY_STEP_RESULT!=1) 
				{
					Wsst_Copy_To_Log_Data.LAST_STEP= new NullableInt( Wsst_Sequence_Data.COPY_STEP);
					Wsst_Copy_To_Log_Data.COMPLETION_TIME =new NullableDateTime (  Wsst_Sequence_Data.LAST_UPDATE_TIME);
				}


				try
				{
					dbHelperWSSTCOPYT.ConnectToDB();
				
					dbHelperWSSTCOPYT.BeginTransaction();  
					CopytoDB_helper.UpdateCopyToLogResultCode(null);
					dbHelperWSSTCOPYT.CommitTransaction();  

				}
				catch(Exception sqlEx)
				{
					dbHelperWSSTCOPYT.RollbackTransaction();

					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTCOPYT.Close();
				}

				#endregion
			}
			
			if(Wsst_Copy_To_Log_Data.LAST_STEP==null)
			{
				#region Update Step 40
			
				
			
				Wsst_Sequence_Data.SetMembers(40,1,null,filePickUpTime);

				try
				{
					dbHelperWSSTCOPYT.ConnectToDB();
				
					dbHelperWSSTCOPYT.BeginTransaction();  
					CopytoDB_helper.UpdateCopyToLogResultCode(null);
					dbHelperWSSTCOPYT.CommitTransaction();  

				}
				catch(Exception sqlEx)
				{
					dbHelperWSSTCOPYT.RollbackTransaction();

					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTCOPYT.Close();
				}

				#endregion
			}

			if(Wsst_Copy_To_Log_Data.LAST_STEP==null)
			{
				#region Update Step 99 // final complete
			
				
				Wsst_Copy_To_Log_Data.LAST_STEP= new NullableInt( 99);
				Wsst_Copy_To_Log_Data.COMPLETION_TIME =new NullableDateTime (DateTime.Now);
			
				Wsst_Sequence_Data.SetMembers(99,1,null,Wsst_Copy_To_Log_Data.COMPLETION_TIME.Value);

				try
				{
					dbHelperWSSTCOPYT.ConnectToDB();
				
					dbHelperWSSTCOPYT.BeginTransaction();  
					CopytoDB_helper.UpdateCopyToLogResultCode(m_aStepErr);
					dbHelperWSSTCOPYT.CommitTransaction();  

				}
				catch(Exception sqlEx)
				{
					dbHelperWSSTCOPYT.RollbackTransaction();

					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "SAFETIR_MESSAGE_IN_ID :" +Wsst_Copy_To_Log_Data.SAFETIR_MESSAGE_IN_ID.ToString() + " - " + sqlEx.Message + " - " + sqlEx.StackTrace );  
					throw sqlEx ; //Back to FileListener
				}
				finally
				{
					dbHelperWSSTCOPYT.Close();
				}

				#endregion
			}
		}
		#endregion
	}
	#endregion
	struct structStepResult
	{
		public int Step;
		public int StepResult;
		public DateTime StepTimeStamp;
		public string ResultDescription;
	
		public void SetMembers(int StepNumber, int StepResultValue, DateTime TimeStamp, string Description)
		{
		
			Step= StepNumber;
			StepResult = StepResultValue;
			StepTimeStamp= TimeStamp;
			ResultDescription=Description;
		
		}
	}
}
