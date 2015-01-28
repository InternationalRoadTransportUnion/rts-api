using System;

using IRU.RTS;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;
using IRU.RTS.CryptoInterfaces;
using IRU.RTS.Common.Extension;

using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IRU.RTS.WSEGIS
{
	/// <summary>
	/// Wraps all calls to EGISDB.
	/// </summary>
	public class EGIS_DBHelper
	{
		private IDBHelper m_requestHelper;
		/// <summary>
		/// Initialize the EGIS_DB instance  of IDBHelper
		/// </summary>
		/// <param name="requestHelper"></param>
		public EGIS_DBHelper(/*IDBHelper subscriberHelper,*/IDBHelper requestHelper)
		{
			//m_subscriberHelper = subscriberHelper;
			m_requestHelper = requestHelper;
		}

		/// <summary>
		/// Log Initial Request in EGIS_REQUEST_LOG, as soon as the query is recieved 
		/// Insert a new query record in the Table all other step functions update this record
		/// </summary>
		/// <param name="egisLogRequestData"> Log data structure</param>
		/// <param name="validateQueryFailed"> boolean indicating succes or failure</param>
		public void LogRequestS5(EGISLogRequestStruct egisLogRequestData, bool validateQueryFailed)
		{			
			string sSql = "INSERT INTO [{0}].[EGIS_REQUEST_LOG] " +
				"([EGIS_QUERY_ID], [ENCRYPTED_QUERY_PARAMS], " +
				"[SENDER_TCP_IP_ADDRESS], [SESSION_KEY_USED_ENCRYPTED_IN], " +
				"[DECRYPTION_KEY_ID], [ROW_CREATED_TIME], [QUERY_PARAM_OK], " +
				"[RETURN_CODE], [LAST_STEP], [COMPLETION_TIME], [SENDER_ID], [SENDER_QUERY_ID]) VALUES " +
				"(@EGIS_QUERY_ID, @ENCRYPTED_QUERY_PARAMS, " +
				"@SENDER_TCP_IP_ADDRESS, @SESSION_KEY_USED_ENCRYPTED_IN, " +
				"@DECRYPTION_KEY_ID, @ROW_CREATED_TIME, @QUERY_PARAM_OK, " +
				"@RETURN_CODE, @LAST_STEP, @COMPLETION_TIME, @SENDER_ID, @SENDER_QUERY_ID ) ";
			sSql = String.Format(sSql, m_requestHelper.SchemaName);

			SqlCommand sCmd = new SqlCommand(sSql);
			sCmd.CommandTimeout = 500;

			sCmd.Parameters.Add("@EGIS_QUERY_ID", SqlDbType.Decimal).Value = egisLogRequestData.EGIS_QueryID;
			sCmd.Parameters.Add("@ENCRYPTED_QUERY_PARAMS", SqlDbType.Image).Value = egisLogRequestData.encryptedQueryParams;
			sCmd.Parameters.Add("@SENDER_TCP_IP_ADDRESS", SqlDbType.NVarChar).Value = egisLogRequestData.senderTCPAddress;
			sCmd.Parameters.Add("@SESSION_KEY_USED_ENCRYPTED_IN", SqlDbType.Image).Value = egisLogRequestData.encryptedSessionKeyIn;
			sCmd.Parameters.Add("@DECRYPTION_KEY_ID", SqlDbType.NVarChar).Value = egisLogRequestData.decryptionKeyID;
			sCmd.Parameters.Add("@ROW_CREATED_TIME", SqlDbType.DateTime).Value = egisLogRequestData.rowCreationTime;

			if (validateQueryFailed)
			{
				sCmd.Parameters.Add("@QUERY_PARAM_OK", SqlDbType.Int).Value = egisLogRequestData.returnCode;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = egisLogRequestData.returnCode;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = egisLogRequestData.lastStep;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = egisLogRequestData.rowCreationTime;
			}
			else
			{
				sCmd.Parameters.Add("@QUERY_PARAM_OK", SqlDbType.NVarChar).Value = 2;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
			}

			sCmd.Parameters.Add("@SENDER_ID", SqlDbType.NVarChar).Value = egisLogRequestData.senderID;
			sCmd.Parameters.Add("@SENDER_QUERY_ID", SqlDbType.NVarChar).Value = egisLogRequestData.senderQueryID;

			m_requestHelper.ExecuteNonQuery(sCmd);
		}

		/// <summary>
		/// Updates EGIS_REQUEST_LOG on session key decryption
		/// </summary>
		/// <param name="egisLogRequestData">Log data structure</param>
		/// <param name="decryptSessionFailed">boolean indicating step succeeded or failed</param>
		public void LogRequestS10(EGISLogRequestStruct egisLogRequestData, bool decryptSessionFailed)
		{
			string sSql = "UPDATE [{0}].[EGIS_REQUEST_LOG] " +
				"SET SESSION_KEY_USED_DECRYPTED_IN = @objSessionKeyUsedDecrIn, " +
				//"[ROW_CREATED_TIME] = @ROW_CREATED_TIME ," +
				"[RETURN_CODE] = @RETURN_CODE, " +
				"[LAST_STEP] = @LAST_STEP, " +
				"[COMPLETION_TIME] = @COMPLETION_TIME " +
				"WHERE [EGIS_QUERY_ID] = @EGIS_QUERY_ID";
			sSql = String.Format(sSql, m_requestHelper.SchemaName);

			//"WHERE [EGIS_QUERY_ID] = "+ egisLogRequestData.EGIS_QueryID.ToString() ;

			SqlCommand sCmd = new SqlCommand(sSql);
			sCmd.CommandTimeout = 500;

			//sCmd.Parameters.Add("@ROW_CREATED_TIME", SqlDbType.DateTime ).Value = egisLogRequestData.rowCreationTime ;

			if (decryptSessionFailed)
			{
				sCmd.Parameters.Add("@objSessionKeyUsedDecrIn", SqlDbType.Image).Value = DBNull.Value;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = egisLogRequestData.returnCode;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = egisLogRequestData.lastStep;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = egisLogRequestData.rowCreationTime;
			}
			else
			{
				sCmd.Parameters.Add("@objSessionKeyUsedDecrIn", SqlDbType.Image).Value = egisLogRequestData.decryptedSessionKeyIn;

				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
			}

			sCmd.Parameters.Add("@EGIS_QUERY_ID", SqlDbType.Decimal).Value = egisLogRequestData.EGIS_QueryID;

			m_requestHelper.ExecuteNonQuery(sCmd);
		}

		/// <summary>
		/// Logs into EGIS_REQUEST_LOG after query decryption step
		/// </summary>
		/// <param name="egisLogRequestData">Log data structure</param>
		/// <param name="decryptionFailed">boolean indicating failure of query encryption</param>
		public void LogRequestS15(EGISLogRequestStruct egisLogRequestData, bool decryptionFailed)
		{
			string sSql = "UPDATE [{0}].[EGIS_REQUEST_LOG] " +
				"SET [DECRYPTION_RESULT] = @DECRYPTION_RESULT, " +
				"[DECRYPTION_RESULT_DESCRIPTION] = @DECRYPTION_RESULT_DESCRIPTION , " +
				//"[ROW_CREATED_TIME] = @ROW_CREATED_TIME, " +
				"[QUERY_XML] = @QUERY_XML, " +
				"[RETURN_CODE] = @RETURN_CODE, " +
				"[LAST_STEP] = @LAST_STEP, " +
				"[COMPLETION_TIME] = @COMPLETION_TIME " +
				"WHERE [EGIS_QUERY_ID] = @EGIS_QUERY_ID";
			sSql = String.Format(sSql, m_requestHelper.SchemaName);

			SqlCommand sCmd = new SqlCommand(sSql);
			sCmd.CommandTimeout = 500;

			sCmd.Parameters.Add("@DECRYPTION_RESULT", SqlDbType.Int).Value = egisLogRequestData.decryptionResult;
			sCmd.Parameters.Add("@DECRYPTION_RESULT_DESCRIPTION", SqlDbType.NVarChar).Value = egisLogRequestData.decryptionResultDesc.Truncate(800);
			//sCmd.Parameters.Add("@ROW_CREATED_TIME", SqlDbType.DateTime ).Value = egisLogRequestData.rowCreationTime ;

			if (decryptionFailed)
			{
				sCmd.Parameters.Add("@QUERY_XML", SqlDbType.NVarChar).Value = DBNull.Value;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = egisLogRequestData.returnCode;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = egisLogRequestData.lastStep;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = egisLogRequestData.rowCreationTime;
			}
			else
			{
				sCmd.Parameters.Add("@QUERY_XML", SqlDbType.NVarChar).Value = egisLogRequestData.decryptedQueryParamXML;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
			}

			sCmd.Parameters.Add("@EGIS_QUERY_ID", SqlDbType.Decimal).Value = egisLogRequestData.EGIS_QueryID;

			m_requestHelper.ExecuteNonQuery(sCmd);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="egisLogRequestData"></param>
		/// <param name="hashVerificationFailed"></param>
		public void LogRequestS20(EGISLogRequestStruct egisLogRequestData, bool hashVerificationFailed)
		{
			string sSql = "UPDATE [{0}].[EGIS_REQUEST_LOG] " +
				"SET [ROW_CREATED_TIME] = @ROW_CREATED_TIME, " +
				"[RETURN_CODE] = @RETURN_CODE, " +
				"[LAST_STEP] = @LAST_STEP, " +
				"[COMPLETION_TIME] = @COMPLETION_TIME " +
				"WHERE [EGIS_QUERY_ID] = @EGIS_QUERY_ID";
			sSql = String.Format(sSql, m_requestHelper.SchemaName);

			if (hashVerificationFailed)
			{
				SqlCommand sCmd = new SqlCommand(sSql);
				sCmd.CommandTimeout = 500;

				sCmd.Parameters.Add("@ROW_CREATED_TIME", SqlDbType.DateTime).Value = egisLogRequestData.rowCreationTime;

				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = egisLogRequestData.returnCode;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = egisLogRequestData.lastStep;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = egisLogRequestData.rowCreationTime;

				sCmd.Parameters.Add("@EGIS_QUERY_ID", SqlDbType.Decimal).Value = egisLogRequestData.EGIS_QueryID;

				m_requestHelper.ExecuteNonQuery(sCmd);
			}


		}

		/// <summary>
		/// Updates EGIS_REQUEST_LOG with Query XML Validation status
		/// </summary>
		/// <param name="egisLogRequestData">Log data structure</param>
		/// <param name="xmlNotValid">boolean indicating if the query xml was valid or invalid</param>
		public void LogRequestS25(EGISLogRequestStruct egisLogRequestData, bool xmlNotValid)
		{
			string sSql = "UPDATE [{0}].[EGIS_REQUEST_LOG] " +
				"SET [QUERY_XML_VALID] = @QUERY_XML_VALID, " +
				"[QUERY_XML_INVALID_REASON] = @QUERY_XML_INVALID_REASON, " +
				"[RETURN_CODE] = @RETURN_CODE, " +
				"[LAST_STEP] = @LAST_STEP, " +
				"[COMPLETION_TIME] = @COMPLETION_TIME " +
				"WHERE [EGIS_QUERY_ID] = @EGIS_QUERY_ID";
			sSql = String.Format(sSql, m_requestHelper.SchemaName);

			SqlCommand sCmd = new SqlCommand(sSql);
			sCmd.CommandTimeout = 500;

			if (xmlNotValid)
			{
				sCmd.Parameters.Add("@QUERY_XML_VALID", SqlDbType.Bit).Value = 0;
				sCmd.Parameters.Add("@QUERY_XML_INVALID_REASON", SqlDbType.NVarChar).Value = egisLogRequestData.invalidQueryXMLReason;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = egisLogRequestData.returnCode;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = egisLogRequestData.lastStep;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = egisLogRequestData.rowCreationTime;
			}
			else
			{
				sCmd.Parameters.Add("@QUERY_XML_VALID", SqlDbType.Bit).Value = 1;
				sCmd.Parameters.Add("@QUERY_XML_INVALID_REASON", SqlDbType.NVarChar).Value = DBNull.Value;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
			}

			sCmd.Parameters.Add("@EGIS_QUERY_ID", SqlDbType.Decimal).Value = egisLogRequestData.EGIS_QueryID;

			m_requestHelper.ExecuteNonQuery(sCmd);
		}

		/// <summary>
		/// Updates EGIS_REQUEST_LOG with Sender Authorization status  
		/// </summary>
		/// <param name="egisLogRequestData">Log data structure</param>
		/// <param name="userNotAuthorised">boolean indicating success of authorization</param>
		public void LogRequestS30(EGISLogRequestStruct egisLogRequestData, bool userNotAuthorised)
		{
			string sSql = "UPDATE [{0}].[EGIS_REQUEST_LOG] " +
				"SET [SENDER_PASSWORD] = @SENDER_PASSWORD, " +
				"[SENDER_AUTHENTICATED]=@SENDER_AUTHENTICATED,  " +
				"[SENDER_ID] = @SENDER_ID, " +
				"[RETURN_CODE] = @RETURN_CODE, " +
				"[LAST_STEP] = @LAST_STEP, " +
				"[COMPLETION_TIME] = @COMPLETION_TIME " +
				"WHERE [EGIS_QUERY_ID] = @EGIS_QUERY_ID";
			sSql = String.Format(sSql, m_requestHelper.SchemaName);

			SqlCommand sCmd = new SqlCommand(sSql);
			sCmd.CommandTimeout = 500;

			if (userNotAuthorised)
			{
				sCmd.Parameters.Add("@SENDER_PASSWORD", SqlDbType.NVarChar).Value = DBNull.Value;
				sCmd.Parameters.Add("@SENDER_AUTHENTICATED", SqlDbType.Int).Value = egisLogRequestData.senderAuthenticated;
				sCmd.Parameters.Add("@SENDER_ID", SqlDbType.NVarChar).Value = DBNull.Value;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = egisLogRequestData.returnCode;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = egisLogRequestData.lastStep;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = egisLogRequestData.rowCreationTime;
			}
			else
			{
				if (egisLogRequestData.senderPassword != null)
				{
					sCmd.Parameters.Add("@SENDER_PASSWORD", SqlDbType.NVarChar).Value = egisLogRequestData.senderPassword;
				}
				else
				{
					sCmd.Parameters.Add("@SENDER_PASSWORD", SqlDbType.NVarChar).Value = DBNull.Value;
				}
				sCmd.Parameters.Add("@SENDER_AUTHENTICATED", SqlDbType.Int).Value = egisLogRequestData.senderAuthenticated;
				sCmd.Parameters.Add("@SENDER_ID", SqlDbType.NVarChar).Value = egisLogRequestData.senderID;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
			}
			sCmd.Parameters.Add("@EGIS_QUERY_ID", SqlDbType.Decimal).Value = egisLogRequestData.EGIS_QueryID;

			m_requestHelper.ExecuteNonQuery(sCmd);
		}

		/// <summary>
		/// Update EGIS_REQUEST_LOG with Carnet query results
		/// </summary>
		/// <param name="egisLogRequestData">Log data structure</param>
		/// <param name="cwQueryFailed">boolean indicating cuccess failure of query</param>
		public void LogRequestS35(EGISLogRequestStruct egisLogRequestData, bool cwQueryFailed)
		{
			string sSql = "UPDATE [{0}].[EGIS_REQUEST_LOG] " +
				"SET [ORIGINATOR]=@ORIGINATOR, " +
				"[ORIGIN_TIME]=@ORIGIN_TIME, " +
				"[SENDER_PASSWORD]=@SENDER_PASSWORD,  " +
				"[QUERY_TYPE]=@QUERY_TYPE,  " +
				//"[QUERY_REASON]=@QUERY_REASON,  " +
				"[CARNET_NUMBER]=@CARNET_NUMBER,  " +
				//"[SENDER_QUERY_ID]=@SENDER_QUERY_ID,  " +
				"[QUERY_RESULT_CODE]=@QUERY_RESULT_CODE, " +
				//"[SENDER_AUTHENTICATED]=@SENDER_AUTHENTICATED,  " +
				"[HOLDER_ID]=@HOLDER_ID,  " +
				"[VALIDITY_DATE]=@VALIDITY_DATE,  " +
				"[ASSOCIATION_SHORT_NAME]=@ASSOCIATION_SHORT_NAME,  " +
				"[NUMBER_TERMINATIONS]=@NUMBER_TERMINATIONS,  " +
				"[VOUCHER_NUMBER]=@VOUCHER_NUMBER,  " +
				"[RETURN_CODE] = @RETURN_CODE, " +
				"[LAST_STEP] = @LAST_STEP, " +
				"[COMPLETION_TIME] = @COMPLETION_TIME " +
				"WHERE [EGIS_QUERY_ID] = @EGIS_QUERY_ID ";
			sSql = String.Format(sSql, m_requestHelper.SchemaName);

			SqlCommand sCmd = new SqlCommand(sSql);
			sCmd.CommandTimeout = 500;

			sCmd.Parameters.Add("@ORIGINATOR", SqlDbType.NVarChar).Value = egisLogRequestData.originatorID;
			sCmd.Parameters.Add("@ORIGIN_TIME", SqlDbType.DateTime).Value = egisLogRequestData.originTime;
			sCmd.Parameters.Add("@SENDER_PASSWORD", SqlDbType.NVarChar).Value = egisLogRequestData.senderPassword;
			sCmd.Parameters.Add("@QUERY_TYPE", SqlDbType.Int).Value = egisLogRequestData.queryType;
			//sCmd.Parameters.Add("@QUERY_REASON", SqlDbType.Int).Value = egisLogRequestData.queryReason;
			sCmd.Parameters.Add("@CARNET_NUMBER", SqlDbType.NVarChar).Value = egisLogRequestData.tirCarnetNumber;

			if (cwQueryFailed)
			{
				sCmd.Parameters.Add("@HOLDER_ID", SqlDbType.NVarChar).Value = DBNull.Value;
				//sCmd.Parameters.Add("@SENDER_QUERY_ID", SqlDbType.NVarChar).Value = DBNull.Value ;
				sCmd.Parameters.Add("@QUERY_RESULT_CODE", SqlDbType.Int).Value = DBNull.Value;
				//sCmd.Parameters.Add("@SENDER_AUTHENTICATED", SqlDbType.Int).Value = DBNull.Value ;
				sCmd.Parameters.Add("@VALIDITY_DATE", SqlDbType.DateTime).Value = DBNull.Value;
				sCmd.Parameters.Add("@ASSOCIATION_SHORT_NAME", SqlDbType.NVarChar).Value = DBNull.Value;
				sCmd.Parameters.Add("@NUMBER_TERMINATIONS", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@VOUCHER_NUMBER", SqlDbType.NVarChar).Value = DBNull.Value;

				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = egisLogRequestData.returnCode;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = egisLogRequestData.lastStep;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = egisLogRequestData.rowCreationTime;
			}
			else
			{
				sCmd.Parameters.Add("@HOLDER_ID", SqlDbType.NVarChar).Value = egisLogRequestData.holderID;
				//sCmd.Parameters.Add("@SENDER_QUERY_ID", SqlDbType.NVarChar ).Value = egisLogRequestData.senderQueryID ;
				sCmd.Parameters.Add("@QUERY_RESULT_CODE", SqlDbType.Int).Value = egisLogRequestData.queryResultCode;
				//sCmd.Parameters.Add("@SENDER_AUTHENTICATED", SqlDbType.Int).Value = egisLogRequestData.senderAuthenticated ;
				sCmd.Parameters.Add("@VALIDITY_DATE", SqlDbType.DateTime).Value = (egisLogRequestData.validityDate == null ? DBNull.Value : egisLogRequestData.validityDate);
				sCmd.Parameters.Add("@ASSOCIATION_SHORT_NAME", SqlDbType.NVarChar).Value = egisLogRequestData.assocShortName;
				sCmd.Parameters.Add("@NUMBER_TERMINATIONS", SqlDbType.Int).Value = (egisLogRequestData.numberOfTerminations == null ? DBNull.Value : egisLogRequestData.numberOfTerminations);
				sCmd.Parameters.Add("@VOUCHER_NUMBER", SqlDbType.NVarChar).Value = (egisLogRequestData.voucherNumber == null ? DBNull.Value : egisLogRequestData.voucherNumber);

				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
			}
			sCmd.Parameters.Add("@EGIS_QUERY_ID", SqlDbType.Decimal).Value = egisLogRequestData.EGIS_QueryID;

			m_requestHelper.ExecuteNonQuery(sCmd);
		}

		/// <summary>
		/// Updates EGIS_REQUEST_LOG with Response XML encryption status
		/// </summary>
		/// <param name="egisLogRequestData">Log data structure</param>
		/// <param name="hashEncFailed">boolean indicating failure of response encryption</param>
		public void LogRequestS40(EGISLogRequestStruct egisLogRequestData, bool hashEncFailed)
		{
			string sSql = "UPDATE [{0}].[EGIS_REQUEST_LOG] " +
				"SET RESPONSE_ENCRYPTION_RESULT = @RESPONSE_ENCRYPTION_RESULT, " +
				"RESPONSE_ENCRYPTION_RESULT_DESCRIPTION = @RESPONSE_ENCRYPTION_RESULT_DESCRIPTION, " +
				"SESSION_KEY_USED_DECRYPTED_OUT = @SESSION_KEY_USED_DECRYPTED_OUT, " +
				"[RETURN_CODE] = @RETURN_CODE, " +
				"[LAST_STEP] = @LAST_STEP, " +
				"[COMPLETION_TIME] = @COMPLETION_TIME " +
				"WHERE [EGIS_QUERY_ID] = @EGIS_QUERY_ID ";
			sSql = String.Format(sSql, m_requestHelper.SchemaName);

			SqlCommand sCmd = new SqlCommand(sSql);
			sCmd.CommandTimeout = 500;

			sCmd.Parameters.Add("@RESPONSE_ENCRYPTION_RESULT", SqlDbType.Int).Value = egisLogRequestData.responseEncryptionResult;

			if (hashEncFailed)
			{
				sCmd.Parameters.Add("@RESPONSE_ENCRYPTION_RESULT_DESCRIPTION", SqlDbType.NVarChar).Value = egisLogRequestData.responseEncryptionResultDesc;
				sCmd.Parameters.Add("@SESSION_KEY_USED_DECRYPTED_OUT ", SqlDbType.Image).Value = DBNull.Value;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = egisLogRequestData.returnCode;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = egisLogRequestData.lastStep;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = egisLogRequestData.rowCreationTime;
			}
			else
			{
				sCmd.Parameters.Add("@RESPONSE_ENCRYPTION_RESULT_DESCRIPTION", SqlDbType.NVarChar).Value = DBNull.Value;
				sCmd.Parameters.Add("@SESSION_KEY_USED_DECRYPTED_OUT", SqlDbType.Image).Value = egisLogRequestData.decryptedSessionKeyOut;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
			}
			sCmd.Parameters.Add("@EGIS_QUERY_ID", SqlDbType.Decimal).Value = egisLogRequestData.EGIS_QueryID;

			m_requestHelper.ExecuteNonQuery(sCmd);
		}

		/// <summary>
		/// Updates EGIS_Request_log with Session Key encryption status
		/// </summary>
		/// <param name="egisLogRequestData">Log data structure</param>
		/// <param name="sessionKeyEncFailed">boolean indicating failure encrypting session key</param>
		public void LogRequestS45(EGISLogRequestStruct egisLogRequestData, bool sessionKeyEncFailed)
		{
			string sSql = "UPDATE [{0}].[EGIS_REQUEST_LOG] " +
				"SET SESSION_KEY_ENCRYPTION_KEY_ID_USED = @SESSION_KEY_ENCRYPTION_KEY_ID_USED, " +
				"SESSION_KEY_USED_ENCRYPTED_OUT = @SESSION_KEY_USED_ENCRYPTED_OUT, " +
				"[RETURN_CODE] = @RETURN_CODE, " +
				"[LAST_STEP] = @LAST_STEP, " +
				"[COMPLETION_TIME] = @COMPLETION_TIME " +
				"WHERE [EGIS_QUERY_ID] = @EGIS_QUERY_ID ";
			sSql = String.Format(sSql, m_requestHelper.SchemaName);

			SqlCommand sCmd = new SqlCommand(sSql);
			sCmd.CommandTimeout = 500;

			if (sessionKeyEncFailed)
			{
				sCmd.Parameters.Add("@SESSION_KEY_ENCRYPTION_KEY_ID_USED", SqlDbType.NVarChar).Value = DBNull.Value;
				sCmd.Parameters.Add("@SESSION_KEY_USED_ENCRYPTED_OUT ", SqlDbType.Image).Value = DBNull.Value;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = egisLogRequestData.returnCode;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = egisLogRequestData.lastStep;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = egisLogRequestData.rowCreationTime;
			}
			else
			{
				sCmd.Parameters.Add("@SESSION_KEY_ENCRYPTION_KEY_ID_USED", SqlDbType.NVarChar).Value = egisLogRequestData.sessionKeyEncryptionKeyIDUsed;
				sCmd.Parameters.Add("@SESSION_KEY_USED_ENCRYPTED_OUT", SqlDbType.Image).Value = egisLogRequestData.encryptedSessionKeyOut;
				sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
				sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
			}
			sCmd.Parameters.Add("@EGIS_QUERY_ID", SqlDbType.Decimal).Value = egisLogRequestData.EGIS_QueryID;

			m_requestHelper.ExecuteNonQuery(sCmd);
		}
		/// <summary>
		/// Final step update to the EGIS_REQUEST_LOG table
		/// </summary>
		/// <param name="egisLogRequestData">Log data structure</param>
		public void LogRequestS99(EGISLogRequestStruct egisLogRequestData)
		{
			string sSql = "UPDATE [{0}].[EGIS_REQUEST_LOG] " +
				"SET [RETURN_CODE] = @RETURN_CODE, " +
				"[LAST_STEP] = @LAST_STEP, " +
				//"[RESPONSE_TIME] = @RESPONSE_TIME, " +
				"[COMPLETION_TIME] = @COMPLETION_TIME " +
				"WHERE [EGIS_QUERY_ID] = @EGIS_QUERY_ID ";
			sSql = String.Format(sSql, m_requestHelper.SchemaName);

			SqlCommand sCmd = new SqlCommand(sSql);
			sCmd.CommandTimeout = 500;

			sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = egisLogRequestData.returnCode;
			sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = egisLogRequestData.lastStep;
			//sCmd.Parameters.Add("@RESPONSE_TIME", SqlDbType.DateTime ).Value = egisLogRequestData.rowCreationTime ;
			sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = egisLogRequestData.rowCreationTime;
			sCmd.Parameters.Add("@EGIS_QUERY_ID", SqlDbType.Decimal).Value = egisLogRequestData.EGIS_QueryID;

			m_requestHelper.ExecuteNonQuery(sCmd);
		}


		/// <summary>
		/// Method to update the EGIS_Sequence table
		/// </summary>
		/// <param name="EGIS_QueryID">ID generated  for the query</param>
		/// <param name="step">Step number</param>
		/// <param name="stepResult">Result of the step</param>
		/// <param name="stepDesc"></param>
		/// <param name="stepCompletionTime">Time stam</param>
		public void LogSequenceStep(Double EGIS_QueryID, Int64 step, Int64 stepResult, String stepDesc, DateTime stepCompletionTime)
		{
			string sSql = "INSERT INTO [{0}].[EGIS_SEQUENCE] " +
				"([EGIS_QUERY_ID], [EGIS_STEP], [EGIS_STEP_RESULT], " +
				" [EGIS_STEP_ERROR_DESC], [LAST_UPDATE_TIME]) " +
				" VALUES " +
				"(@EGIS_QUERY_ID, @EGIS_STEP, @EGIS_STEP_RESULT, " +
				" @EGIS_STEP_ERROR_DESC, @LAST_UPDATE_TIME)";
			sSql = String.Format(sSql, m_requestHelper.SchemaName);

			SqlCommand sCmd = new SqlCommand(sSql);
			sCmd.CommandTimeout = 500;

			sCmd.Parameters.Add("@EGIS_QUERY_ID", SqlDbType.Int).Value = EGIS_QueryID;
			sCmd.Parameters.Add("@EGIS_STEP", SqlDbType.Int).Value = step;
			sCmd.Parameters.Add("@EGIS_STEP_RESULT", SqlDbType.Int).Value = stepResult;
			sCmd.Parameters.Add("@EGIS_STEP_ERROR_DESC", SqlDbType.NVarChar).Value = stepDesc;
			sCmd.Parameters.Add("@LAST_UPDATE_TIME", SqlDbType.DateTime).Value = stepCompletionTime;
			m_requestHelper.ExecuteNonQuery(sCmd);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="EGIS_QueryID"></param>
		/// <param name="dtResponseSent"></param>
		/// <param name="ResponseResult"></param>
		public void LogRequestResponse(long EGIS_QueryID, DateTime dtResponseSent, bool ResponseResult)
		{
			string sSql = "UPDATE [{0}].[EGIS_REQUEST_LOG] " +
				"SET [RESPONSE_RESULT] = @RESPONSE_RESULT, " +
				"[RESPONSE_TIME] = @RESPONSE_TIME " +
				"WHERE [EGIS_QUERY_ID] = @EGIS_QUERY_ID ";
			sSql = String.Format(sSql, m_requestHelper.SchemaName);

			SqlCommand sCmd = new SqlCommand(sSql);
			sCmd.CommandTimeout = 500;

			if (ResponseResult)
			{
				sCmd.Parameters.Add("@RESPONSE_RESULT", SqlDbType.Int).Value = 1;
			}
			else
			{
				sCmd.Parameters.Add("@RESPONSE_RESULT", SqlDbType.Int).Value = 2;
			}

			sCmd.Parameters.Add("@RESPONSE_TIME", SqlDbType.DateTime).Value = dtResponseSent;

			sCmd.Parameters.Add("@EGIS_QUERY_ID", SqlDbType.Decimal).Value = EGIS_QueryID;

			m_requestHelper.ExecuteNonQuery(sCmd);
		}

	}
}
