using System;
using System.Data;
using System.Data.SqlClient ;

using IRU;
using IRU.CommonInterfaces; 
using IRU.RTS.CommonComponents ;
using System.Text ;


namespace IRU.RTS.CopyToProcessor
{
	/// <summary>
	/// Summary description for WSCscc_CopyToDbHelper.
	/// </summary>
	public class WSCscc_CopyToDbHelper
	{
		IDBHelper m_CopyToDBIDBHelper;
		WSST_COPY_TO_LOG_STRUCT m_WSST_COPY_TO_LOG_DATA;
		WSST_COPY_TO_SEQUENCE_STRUCT m_WSST_COPY_TO_SEQUENCE_DATA;

		private static string[] m_aKeyFields = new string[]{"SAFETIR_MESSAGE_IN_ID"};
		private static string[] m_aSeqFldList = new string[]{"SAFETIR_MESSAGE_IN_ID", "COPY_STEP", "COPY_STEP_RESULT", "COPY_STEP_ERROR_DESCRIPTION", "LAST_UPDATE_TIME"};

		public WSCscc_CopyToDbHelper(IDBHelper iDbHelperCopyToDB, 
			WSST_COPY_TO_LOG_STRUCT Wsst_Copy_To_Log_Data, 
			WSST_COPY_TO_SEQUENCE_STRUCT Wsst_Copy_To_Sequence_Data)
		{
			m_CopyToDBIDBHelper = iDbHelperCopyToDB;
			m_WSST_COPY_TO_LOG_DATA = Wsst_Copy_To_Log_Data ;
			m_WSST_COPY_TO_SEQUENCE_DATA = Wsst_Copy_To_Sequence_Data ;
		}

		public void LogSafeTIRfileContentsinInDB(string [] afldList)
		{
			StructToTable stLog = new StructToTable();
			SqlCommand cmdInsertLog = stLog.GetTableInsertCommand(m_WSST_COPY_TO_LOG_DATA,"WSST_COPY_TO_LOG", afldList);
			m_CopyToDBIDBHelper.ExecuteNonQuery(cmdInsertLog);

			/*
			StructToTable stSeq = new StructToTable();
			SqlCommand cmdInsertSeq = stSeq.GetTableInsertCommand(m_WSST_COPY_TO_SEQUENCE_DATA,"WSST_COPY_TO_SEQUENCE", m_aSeqFldList);
			m_CopyToDBIDBHelper.ExecuteNonQuery(cmdInsertSeq);
			*/
		}

		public void UpdateCopyToLogResultCode(string [] afldList)
		{
			if(afldList != null) //used where Only Sequence table needs to be updated
			{
				StructToTable st = new StructToTable();
				SqlCommand cmdUpdate = st.GetTableUpdateCommand (m_WSST_COPY_TO_LOG_DATA,"WSST_COPY_TO_LOG",afldList ,m_aKeyFields );
				m_CopyToDBIDBHelper.ExecuteNonQuery(cmdUpdate);
			}

			StructToTable stSeq = new StructToTable();
			SqlCommand cmdInsertSeq = stSeq.GetTableInsertCommand(m_WSST_COPY_TO_SEQUENCE_DATA,"WSST_COPY_TO_SEQUENCE", m_aSeqFldList);
			m_CopyToDBIDBHelper.ExecuteNonQuery(cmdInsertSeq);

		}



	}
	public class WSST_COPY_TO_LOG_STRUCT
	{
		//public  varName                                      nullableComment lengthComment                   
		//------- -------- ------------------------------ ---- --------------- ------------------------------- 
		public  double   SAFETIR_MESSAGE_IN_ID          ;    // NotNullable  -9
		public  string   COPY_TO_ID                     ;    // NotNullable  -510
		public  string   SUBSCRIBER_ID                     ;    // NotNullable  -510
		public  string   ORIGINAL_SENDER_TCP_IP_ADDRESS ;    // NotNullable  -128
		public  NullableInt      COPY_RESULT                    ;    //Nullable      -4
		public  string   COPY_RESULT_DESCRIPTION        ;    //Nullable      -16
		public  string   COPY_TO_ADDRESS                ;    //Nullable      -1000
		public  NullableInt    ENCRYPTION_RESULT              ;    //Nullable      -4
		public  string   ENCRYPTION_RESULT_DESCRIPTION  ;    //Nullable      -16
		public  string   ENCRYPTION_KEY_ID_USED         ;    //Nullable      -50
		public  byte[]   SESSION_KEY_USED_ENCRYPTED_OUT ;    //Nullable      -16
		public  byte[]   SESSION_KEY_USED_DECRYPTED_OUT ;    //Nullable      -16
		public  DateTime CREATION_TIME                  ;    // NotNullable  -8
		public  NullableDateTime  RESULT_TIME                    ;    //Nullable      -8
		public  NullableInt     LAST_STEP                      ;    //Nullable      -4
		public  NullableDateTime COMPLETION_TIME                ;    //Nullable      -8
		public  byte[]	SafeTIRUploadData		;
	}
/*
	public class WSST_COPY_TO_JOB_STRUCT
	{
		//public  varName                                      nullableComment lengthComment                   
		//------- -------- ------------------------------ ---- --------------- ------------------------------- 
		public  double				SAFETIR_MESSAGE_IN_ID          ;    // NotNullable  -9
		public  string				COPY_TO_ID                     ;    // NotNullable  -510
		public  string				SAFETIR_XML                    ;    // NotNullable  -20
		public  string				SENDER_TCP_IP_ADDRESS          ;    // NotNullable  -128
		public  DateTime			JOB_REQUEST_TIME               ;    // NotNullable  -8
		public  int					JOB_STATUS                     ;    // NotNullable  -4
		public  NullableDateTime	JOB_PICKUP_TIME                ;    //Nullable      -8
	}
*/
	public class WSST_COPY_TO_SEQUENCE_STRUCT
	{
		//public  varName                                      nullableComment lengthComment                   
		//------- -------- ------------------------------ ---- --------------- ------------------------------- 
		public  double   SAFETIR_MESSAGE_IN_ID          ;    // NotNullable  -4
		public  int      COPY_STEP                      ;    // NotNullable  -4
		public  int      COPY_STEP_RESULT               ;    // NotNullable  -4
		public  string   COPY_STEP_ERROR_DESCRIPTION    ;    //Nullable      -1600
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
			COPY_STEP = iStep;
			COPY_STEP_RESULT = iResult;
			COPY_STEP_ERROR_DESCRIPTION = sDesc ;
			LAST_UPDATE_TIME  = dtLastTime;         
		}


	}
}
