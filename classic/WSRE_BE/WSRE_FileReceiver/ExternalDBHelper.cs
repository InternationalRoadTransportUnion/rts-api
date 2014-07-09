using System;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace IRU.RTS.WSRE
{
	/// <summary>
	/// This class is used by WSRE_FileReceiver to update the WSRE_Exernal_Log table in the WSRE_EXTERNAL_DB
	/// </summary>
	public class ExternalDBHelper
	{
//"SAFETIR_MESSAGE_IN_ID" ,"ENCRYPTION_KEY_ID","SUBSCRIBER_ID",
//"SafeTIRUploadData","COPY_TO_ID","SENDER_TCP_IP_ADDRESS","ROW_CREATED_TIME","SENDER_MESSAGEID","RETURN_CODE","RETURN_CODE_DESCRIPTION"
        private static string[] m_aLogRecord = new string[] { "SAFETIR_MESSAGE_IN_ID", "SESSION_KEY_USED_ENCRYPTED_IN", "ENCRYPTION_KEY_ID", "SUBSCRIBER_ID", "SafeTIRReconData", "SENDER_TCP_IP_ADDRESS", "ROW_CREATED_TIME", "SENDER_MESSAGEID" };
		private static string[] m_aUpdateRecord = new string[]{"RETURN_CODE","RETURN_CODE_DESCRIPTION"};
		private static string[] m_aKeyFields = new string[]{"SAFETIR_MESSAGE_IN_ID"};

		IDBHelper m_ExtIDBHelper;
		WSRE_EXTERNAL_LOG_STRUCT m_WSRE_EXTERNAL_LOG_STRUCT;

		/// <summary>
		/// Stores references to ExternalIDBHElper and the main WSRE_EXTERNAL_LOG_STRUCT. 
		/// These are used in the member variables.
		/// </summary>
		/// <param name="iExtDBHelper">Reference to External DB IDBHelper</param>
		/// <param name="ExtLogStruct">Reference to WSRE_EXTERNAL_LOG_STRUCT in WSRE_FileReceiver</param>
		public ExternalDBHelper(IDBHelper iExtDBHelper, WSRE_EXTERNAL_LOG_STRUCT ExtLogStruct)
		{
			m_ExtIDBHelper = iExtDBHelper;
			m_WSRE_EXTERNAL_LOG_STRUCT = ExtLogStruct;
		}

		/// <summary>
		/// Uses Struct_To_Table to generete an Insert SQLCommand and executes against External DB IDBHelper.
		/// </summary>
		/// <returns>Number of Rows affected</returns>
		public int LogSafeTIRfileContentsinInDB()
		{
			StructToTable st = new StructToTable();
			SqlCommand cmdInsert = st.GetTableInsertCommand(m_WSRE_EXTERNAL_LOG_STRUCT,"dbo.WSRE_EXTERNAL_LOG", m_aLogRecord );
			return m_ExtIDBHelper.ExecuteNonQuery(cmdInsert);

		}

		/// <summary>
		/// Uses Struct_To_Table to generete an Update SQLCommand and executes against External DB IDBHelper
		/// </summary>
		/// <returns>Number of Rows affected</returns>
		public int UpdateExternalLogReturnCode()
		{
			StructToTable st = new StructToTable();
            SqlCommand cmdInsert = st.GetTableUpdateCommand(m_WSRE_EXTERNAL_LOG_STRUCT, "dbo.WSRE_EXTERNAL_LOG", m_aUpdateRecord, m_aKeyFields);
			return m_ExtIDBHelper.ExecuteNonQuery(cmdInsert);
		}

	}
    public class WSRE_EXTERNAL_LOG_STRUCT
    {
        public double SAFETIR_MESSAGE_IN_ID;	// NotNullable	-9
        public string ENCRYPTION_KEY_ID;	//Nullable	-50
        public byte[] SESSION_KEY_USED_ENCRYPTED_IN; //Nullable image
        public string SUBSCRIBER_ID;	//Nullable	-510
        public byte[] SafeTIRReconData;	//Nullable	-16
        public string SENDER_TCP_IP_ADDRESS;	// NotNullable	-1000
        public DateTime ROW_CREATED_TIME;	// NotNullable	-8
        public string SENDER_MESSAGEID;	// Nullable	-20
        public NullableInt RETURN_CODE;	// Nullable	-4
        public string RETURN_CODE_DESCRIPTION;	//Nullable	-1600

    }
	/*
	public class Subscriber_DBHelper
	{
		private IDBHelper m_subscriberHelper;
		public Subscriber_DBHelper(IDBHelper subscriberHelper)
		{
			m_subscriberHelper = subscriberHelper;
		}

		public int AuthenticateQuerySender(String SubscriberID,out String Password, 
			String ServiceID, Int32 MethodID, out String SessionKeyAlgo, out String HashAlgo, 
			out String CopyToUrl)
		{
			int iErrorCode = 0;
			StringBuilder strBuilder = new StringBuilder("SELECT ");
			
			strBuilder.Append("WS_SUBSCRIBER.SUBSCRIBER_ID, WS_SUBSCRIBER.SUBSCRIBER_PASSWORD, ") ;
			strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD.ACTIVE, WS_SUBSCRIBER_SERVICES.ACTIVE AS Expr1, ") ;
			strBuilder.Append("WS_SUBSCRIBER_SERVICES.SESSIONKEY_ENC_ALGO, WS_SUBSCRIBER_SERVICES.HASH_ALGO, ");
			strBuilder.Append("WS_SUBSCRIBER_SERVICES.COPY_TO_TARGET_URL ");

			strBuilder.Append("FROM WS_SUBSCRIBER INNER JOIN ");
			strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD ON WS_SUBSCRIBER.SUBSCRIBER_ID = WS_SUBSCRIBER_SERVICE_METHOD.SUBSCRIBER_ID INNER JOIN ");
			strBuilder.Append("WS_SUBSCRIBER_SERVICES ON WS_SUBSCRIBER.SUBSCRIBER_ID = WS_SUBSCRIBER_SERVICES.SUBSCRIBER_ID AND ");
			strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD.SERVICE_ID = WS_SUBSCRIBER_SERVICES.SERVICE_ID ");

			strBuilder.Append("WHERE (WS_SUBSCRIBER.SUBSCRIBER_ID = N'");
			strBuilder.Append(SubscriberID);
			strBuilder.Append("') ");
			strBuilder.Append(" AND (WS_SUBSCRIBER_SERVICE_METHOD.SERVICE_ID = N'");
			strBuilder.Append(ServiceID); 
			strBuilder.Append("') ");
			strBuilder.Append(" AND (WS_SUBSCRIBER_SERVICE_METHOD.METHOD_ID = ");
			strBuilder.Append(MethodID);
			strBuilder.Append(" ) ");
			//			strBuilder.Append(" AND (WS_SUBSCRIBER_SERVICE_METHOD.ACTIVE = 1) ");
			//			strBuilder.Append(" AND (WS_SUBSCRIBER_SERVICES.ACTIVE = 1) ");

			string sSql = strBuilder.ToString();
			IDataReader rdr =  m_subscriberHelper.GetDataReader(sSql, CommandBehavior.SingleRow);  
			if(rdr.Read())
			{
				#region Get all the values related to Subscriber
				int iServiceMethodId = Convert.ToInt32(rdr.GetValue(2));
				if(iServiceMethodId == 1)
				{
					if(Convert.ToInt32(rdr.GetValue(3)) == 1) 
					{
						if(rdr.GetValue(1) == DBNull.Value)  
						{
							Password = null;
						}
						else
						{
							Password = rdr.GetValue(1).ToString();
						}
						SessionKeyAlgo = rdr.GetValue(4).ToString();
						HashAlgo = rdr.GetValue(5).ToString();
						CopyToUrl = rdr.GetValue(6).ToString();
					}
					else
					{
						iErrorCode = 1233; //Invalid Service of Service Not Active
						Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo, 
							"Service " + ServiceID  + " for Subcriber " + SubscriberID + " is Not Active ");

						Password = "";
						SessionKeyAlgo = "";
						HashAlgo = "";
						CopyToUrl = "";
					}

				}
				else //Invalid Method or Method Not Active
				{
					iErrorCode = 1212;
					Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo, 
						"Service Method " + MethodID.ToString() + " for Subcriber " + SubscriberID + " is Not Active ");

					Password = "";
					SessionKeyAlgo = "";
					HashAlgo = "";
					CopyToUrl = "";

				}


				#endregion
			}
			else
			{
				iErrorCode = 1212;
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo, 
					" Subcriber " + SubscriberID + " is Not registered ");

				Password = "";
				SessionKeyAlgo = "";
				HashAlgo = "";
				CopyToUrl = "";

			}

			return iErrorCode;
		}

	}*/
}
