using System;
using System.Data;
using System.Data.SqlClient ;

using IRU;
using IRU.CommonInterfaces; 
using IRU.RTS.CommonComponents ;
using System.Text ;

namespace IRU.RTS.WSRE
{
	/// <summary>
	/// This class is used by the WSRE_Processor to update the WSRE_INTERNAL_LOG and WSRE_SEQUENCE table.
	/// It uses the Struct_to_Table helper to update the tables using the WSRE_INTERNAL_LOG_STRUCT, WSRE_SEQUENCE_STRUCT.
	/// This class is also responsible for assigning the copyto job which is done using the WSRE_COPY_TO_JOB_STRUCT structure.
	/// 
	/// 
	/// </summary>
	public class InternalDBHelper
	{
		IDBHelper m_IntIDBHelper;
		
		WSRE_INTERNAL_LOG_STRUCT m_WSRE_INTERNAL_LOG_STRUCT;
		WSRE_SEQUENCE_STRUCT m_WSRE_SEQUENCE_STRUCT;

		private static string[] m_aKeyFields = new string[]{"SAFETIR_MESSAGE_IN_ID"};
		private static string[] m_aSeqFldList = new string[]{"SAFETIR_MESSAGE_IN_ID", "WSRE_STEP", "WSRE_STEP_RESULT", "WSRE_STEP_ERROR_DESCRIPTION", "LAST_UPDATE_TIME"};

		/// <summary>
		/// Stores references to the Internal DB Helper, WSRE_INTERNAL_LOG_STRUCT and WSRE_SEQUENCE_STRUCT used by then WSRE_Processor class
		/// </summary>
		/// <param name="iIntDBHelper">Internal DB IDBHelper</param>
		/// <param name="IntLogStruct">WSRE_INTERNAL_LOG_STRUCT used by WSRE_Processor</param>
		/// <param name="IntSequenceStruct">WSRE_SEQUENCE_STRUCT used by WSRE_Processor</param>
		public InternalDBHelper(IDBHelper iIntDBHelper, WSRE_INTERNAL_LOG_STRUCT  IntLogStruct, WSRE_SEQUENCE_STRUCT IntSequenceStruct)
		{
			m_IntIDBHelper  = iIntDBHelper;
			m_WSRE_INTERNAL_LOG_STRUCT = IntLogStruct;
			m_WSRE_SEQUENCE_STRUCT = IntSequenceStruct; 
		}

		/// <summary>
		/// Called by WSRE_Processor in the clean up stage of processing deletes rows in WSRE_INTERNAL_LOG and WSRE_SEQUENCE table
		/// for the SAFETIR_MESSAGE_IN_ID value stored in the structure
		/// </summary>
		/// <param name="RowsAffected">Out parameter, returns count of rows affected</param>
		public void CleanInternalDBForFailure(out int RowsAffected /*, WSRE_INTERNAL_LOG_STRUCT m_WSRE_INTERNAL_LOG_STRUCT*/)
		{
			StringBuilder sqlDelSequenceTbl=new StringBuilder();
			StringBuilder sqlDelInternalLogTbl=new StringBuilder();

			string TblToBeExecuted = "";
			RowsAffected = -1;
			try
			{
				m_IntIDBHelper.ConnectToDB();
				m_IntIDBHelper.BeginTransaction();
				
				sqlDelSequenceTbl.Append(" IF EXISTS (SELECT SAFETIR_MESSAGE_IN_ID FROM dbo.WSRE_SEQUENCE WITH (NOLOCK) WHERE ");
				sqlDelSequenceTbl.Append(" SAFETIR_MESSAGE_IN_ID=@SAFETIR_MESSAGE_IN_ID ) BEGIN ");
                sqlDelSequenceTbl.Append(" DELETE FROM dbo.[WSRE_SEQUENCE] WHERE ");
				sqlDelSequenceTbl.Append(" SAFETIR_MESSAGE_IN_ID = @SAFETIR_MESSAGE_IN_ID OPTION (FAST 1) ");
				sqlDelSequenceTbl.Append(" END ");

                sqlDelInternalLogTbl.Append(" IF EXISTS (SELECT SAFETIR_MESSAGE_IN_ID FROM dbo.WSRE_INTERNAL_LOG WITH (NOLOCK) WHERE ");
				sqlDelInternalLogTbl.Append(" SAFETIR_MESSAGE_IN_ID=@SAFETIR_MESSAGE_IN_ID ) BEGIN ");
                sqlDelInternalLogTbl.Append(" DELETE FROM dbo.[WSRE_INTERNAL_LOG] WHERE ");
				sqlDelInternalLogTbl.Append(" SAFETIR_MESSAGE_IN_ID = @SAFETIR_MESSAGE_IN_ID  OPTION (FAST 1) ");
				sqlDelInternalLogTbl.Append(" END ");

                TblToBeExecuted = "DELETE FROM dbo.[WSRE_SEQUENCE]"; // Just info
				SqlCommand sCmd = new SqlCommand(sqlDelSequenceTbl.ToString()) ;
				sCmd.Parameters.Add("@SAFETIR_MESSAGE_IN_ID", SqlDbType.Int).Value = m_WSRE_INTERNAL_LOG_STRUCT.SAFETIR_MESSAGE_IN_ID ;
				
				RowsAffected = m_IntIDBHelper.ExecuteNonQuery(sCmd);

                TblToBeExecuted = "DELETE FROM dbo.[WSRE_INTERNAL_LOG]"; // Just info
				SqlCommand sCmd2 = new SqlCommand(sqlDelInternalLogTbl.ToString()) ;
				sCmd2.Parameters.Add("@SAFETIR_MESSAGE_IN_ID", SqlDbType.Int).Value = m_WSRE_INTERNAL_LOG_STRUCT.SAFETIR_MESSAGE_IN_ID ;
				
				RowsAffected += m_IntIDBHelper.ExecuteNonQuery(sCmd2);

				m_IntIDBHelper.CommitTransaction();
			}
			catch(SqlException sqlEx)
			{
				m_IntIDBHelper.RollbackTransaction();
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning , sqlEx.Message + " - " + TblToBeExecuted + " 1 - " + sqlEx.StackTrace );  
				throw sqlEx;
			}
			catch(Exception ex)
			{
				m_IntIDBHelper.RollbackTransaction();
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceWarning , ex.Message + " - " + TblToBeExecuted + " 2 - " + ex.StackTrace );  
				throw ex;
			}
			finally
			{
				m_IntIDBHelper.Close();
			}
		}
		
		/// <summary>
		/// Uses the StrucToTable class to update the WSRE_INTERNAL_LOG and WSRE_SEQUENCE table
		/// </summary>
		/// <param name="afldList">String Array contianing the names of the fields to update in the WSRE_INTERNAL_LOG.
		/// All columns in WSRE_SEQUENCE table are populated hence the field list is stored as private class member and not passed to this function</param>
		public void LogSafeTIRfileContentsinInDB(/*WSRE_INTERNAL_LOG_STRUCT m_WSRE_INTERNAL_LOG_STRUCT, */string [] afldList)
		{
			StructToTable stLog = new StructToTable();
            SqlCommand cmdInsertLog = stLog.GetTableInsertCommand(m_WSRE_INTERNAL_LOG_STRUCT, "dbo.WSRE_INTERNAL_LOG", afldList);
			m_IntIDBHelper.ExecuteNonQuery(cmdInsertLog);

			StructToTable stSeq = new StructToTable();
            SqlCommand cmdInsertSeq = stSeq.GetTableInsertCommand(m_WSRE_SEQUENCE_STRUCT, "dbo.WSRE_SEQUENCE", m_aSeqFldList);
			m_IntIDBHelper.ExecuteNonQuery(cmdInsertSeq);

		}

		/// <summary>
		/// Used at end of each step by WSRE_Processor. Both the WSRE_INTERNAL_LOG and WSRE_SEQUENCE tableare updated.
		/// In case the parameter afldList is null only the WSRE_SEQUENCE table is updated.
		/// </summary>
		/// <param name="afldList">Array containing the fields to update</param>
		public void UpdateInternalLogReturnCode(string [] afldList)
		{
			if(afldList != null) //used where Only Sequence table needs to be updated
			{
				StructToTable st = new StructToTable();
                SqlCommand cmdUpdate = st.GetTableUpdateCommand(m_WSRE_INTERNAL_LOG_STRUCT, "dbo.WSRE_INTERNAL_LOG", afldList, m_aKeyFields);
				m_IntIDBHelper.ExecuteNonQuery(cmdUpdate);
			}

			StructToTable stSeq = new StructToTable();
            SqlCommand cmdInsertSeq = stSeq.GetTableInsertCommand(m_WSRE_SEQUENCE_STRUCT, "dbo.WSRE_SEQUENCE", m_aSeqFldList);
			m_IntIDBHelper.ExecuteNonQuery(cmdInsertSeq);

		}

		/// <summary>
		/// Insert row into WSRE_COPY_TO_JOB table using the StructToTable class
		/// </summary>
		/// <param name="afldList">String array containing the fields to update </param>
		/// <param name="CopyToDBHelper">IDBHelper to CopyToDB</param>
		/// <param name="WSRE_CopyToJob_data">Reference to the WSRE_COPY_TO_JOB_STRUCT</param>
		public void AssignCopyToJob(string [] afldList, IDBHelper CopyToDBHelper, WSRE_COPY_TO_JOB_STRUCT WSRE_CopyToJob_data)
		{
			StructToTable stLog = new StructToTable();
            SqlCommand cmdInsertLog = stLog.GetTableInsertCommand(WSRE_CopyToJob_data, "dbo.WSRE_COPY_TO_JOB", afldList);
			CopyToDBHelper.ExecuteNonQuery(cmdInsertLog);

		}

		/// <summary>
		/// Used while assigning the copyto job to see if the copy_to_id is avalid subscriber in the system.
		/// 
		/// </summary>
		/// <param name="SubscriberDBHelper">IDBHelper for the subscriberDB</param>
		/// <param name="WSRE_CopyToJob_data">Reference to the WSRE_COPY_TO_JOB_STRUCT</param>
		/// <returns>true if row exists false otherwise</returns>
		public bool CheckIfCopyToIDExists(IDBHelper SubscriberDBHelper, WSRE_COPY_TO_JOB_STRUCT WSRE_CopyToJob_data)
		{
			StringBuilder SqlCountQuery = new StringBuilder();
            SqlCountQuery.Append("SELECT COUNT(*) AS Expr1 FROM dbo.WS_SUBSCRIBER WHERE (SUBSCRIBER_ID = N'");
			SqlCountQuery.Append(WSRE_CopyToJob_data.COPY_TO_ID);
			SqlCountQuery.Append("')");
 
			int iCount = (int)SubscriberDBHelper.ExecuteScaler(SqlCountQuery.ToString());

			if(iCount >0)
				return true;
			else
				return false;
		}



	}

	/// <summary>
	/// Structure for WSRE_INTERNAL_LOG table
	/// </summary>
	public class WSRE_INTERNAL_LOG_STRUCT
	{
		//public  varName                                      nullableComment lengthComment                   
		//------- -------- ------------------------------ ---- --------------- ------------------------------- 
		//public  double   SAFETIR_MESSAGE_IN_ID          ;    // NotNullable  -9
		public  Int32   SAFETIR_MESSAGE_IN_ID          ;    // NotNullable  -9
		public  string   SENDER_MESSAGE_ID              ;    // NotNullable  -510
		public  string   SENDER_TCP_IP_ADDRESS          ;    // NotNullable  -128
        public byte[] SafeTIRReconData;    // NotNullable  -16
		public  string   DECRYPTION_KEY_ID              ;    // NotNullable  -50
		public  NullableInt			DECRYPTION_RESULT              ;    //Nullable      -4
		public  string   DECRYPTION_RESULT_DESCRIPTION  ;    //Nullable      -1600
		public  byte[]   SESSION_KEY_USED_ENCRYPTED_IN  ;    //Nullable      -16
		public  byte[]   SESSION_KEY_USED_DECRYPTED_IN  ;    //Nullable      -16
		public  string   SAFETIR_XML                    ;    //Nullable      -16
		public  NullableInt			SAFETIR_XML_VALID              ;    //Nullable      -4
		public  string   SAFETIR_XML_INVALID_REASON     ;    //Nullable      -1600
		public  string   SUBSCRIBER_ID                  ;    //Nullable      -510
		public  NullableInt			SUBSCRIBER_AUTHENTICATED       ;    //Nullable      -4
		public  string   CIF_FILENAME                   ;    //Nullable      -510
		public  NullableInt		LAST_STEP                      ;    //Nullable      -4
		public  NullableDateTime	COMPLETION_TIME                ;    //Nullable      -8
		public  DateTime ROW_CREATED_TIME               ;    // NotNullable  -8
		public	string	 copy_to_id						;	//Not mapped to Internal_log_table
	}

	/// <summary>
	/// Structure for WSRE_SEQUENCE table
	/// </summary>
	public class WSRE_SEQUENCE_STRUCT
	{
		//public  varName                                      nullableComment lengthComment                   
		//------- -------- ------------------------------ ---- --------------- ------------------------------- 
		//public  double   SAFETIR_MESSAGE_IN_ID          ;    // NotNullable  -9
		public  Int32   SAFETIR_MESSAGE_IN_ID          ;    // NotNullable  -9
		public  int      WSRE_STEP                      ;    // NotNullable  -4
		public  int      WSRE_STEP_RESULT               ;    // NotNullable  -4
		public  string   WSRE_STEP_ERROR_DESCRIPTION    ;    //Nullable      -1600
		public  DateTime LAST_UPDATE_TIME               ;    // NotNullable  -8

		/// <summary>
		/// Set Step, Result, description and LastUpdateTime
		/// </summary>
		/// <param name="iStep"></param>
		/// <param name="iResult"></param>
		/// <param name="sDesc"></param>
		/// <param name="dtLastTime"></param>
		public void SetMembers(int iStep, int iResult, string sDesc, DateTime dtLastTime)
		{

			WSRE_STEP = iStep;
			WSRE_STEP_RESULT = iResult;
			WSRE_STEP_ERROR_DESCRIPTION = sDesc ;
			LAST_UPDATE_TIME  = dtLastTime;         
		}

	}

	/// <summary>
	/// Structure for the COPY_TO_JOB table
	/// </summary>
	public class WSRE_COPY_TO_JOB_STRUCT
	{
		//public  varName                                      nullableComment lengthComment                   
		//------- -------- ------------------------------ ---- --------------- ------------------------------- 
		//public  double			SAFETIR_MESSAGE_IN_ID          ;    // NotNullable  -9
		public  Int32				SAFETIR_MESSAGE_IN_ID          ;    // NotNullable  -9
		public  string				COPY_TO_ID                     ;    // NotNullable  -510
		public  string				SAFETIR_XML                    ;    // NotNullable  -20
		public  string				SENDER_TCP_IP_ADDRESS          ;    // NotNullable  -128
		public  DateTime			JOB_REQUEST_TIME               ;    // NotNullable  -8
		public  int					JOB_STATUS                     ;    // NotNullable  -4
		public  NullableDateTime	JOB_PICKUP_TIME                ;    //Nullable      -8
		public  string				SUBSCRIBER_ID								;    //Nullable      -510
	}



    

}
