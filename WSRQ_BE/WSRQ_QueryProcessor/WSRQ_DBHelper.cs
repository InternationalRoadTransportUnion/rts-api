using System;
using System.Collections.Generic;
using System.Text;
using IRU.RTS;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;
using IRU.RTS.CryptoInterfaces;
using System.Data;
using System.Data.SqlClient;


namespace IRU.RTS.WSWSRQ
    {
        /// <summary>
        /// LATA Created on September 18 ,2007 for reconciliation request.
        /// Wraps all calls to WSRQDB.
        /// </summary>
        public class WSRQ_DBHelper
        {
            private IDBHelper m_requestHelper;
            public WSRQ_DBHelper(IDBHelper requestHelper)
            {
                m_requestHelper = requestHelper;
            }



            public void LogRequestS5(WSRQLogQueryData wsrqLogQueryData, bool validateQueryFailed)
            {

                string sSql = "INSERT INTO [WSRQ_LOG] " +
                    "([EXCHANGEID],[DECRYPTION_KEY_ID],[QUERY_PARAM_OK],[RETURN_CODE],[SENDER_TCP_IP_ADDRESS]," +
                    "[ENCRYPTION_KEY_ID_IN],[NUMBER_OF_REQUESTS_SENT]," +
                    " [LAST_STEP],[ENCRYPTED_QUERY_PARAMS],[ROW_CREATED_TIME] " +
                    "  ) VALUES " +
                    "(@EXCHANGEID, @DECRYPTION_KEY_ID,@QUERY_PARAM_OK,@RETURN_CODE,@SENDER_TCP_IP_ADDRESS," +
                    "@ENCRYPTION_KEY_ID_IN, 0," +
                    "@LAST_STEP,@ENCRYPTED_QUERY_PARAMS,@ROW_CREATED_TIME) ";

           
                SqlCommand sCmd = new SqlCommand(sSql);
                sCmd.CommandTimeout = 500;

                sCmd.Parameters.Add("@EXCHANGEID", SqlDbType.Decimal).Value = wsrqLogQueryData.Exchange_ID;
                sCmd.Parameters.Add("@DECRYPTION_KEY_ID", SqlDbType.NVarChar).Value = wsrqLogQueryData.decryptionKeyID;
                sCmd.Parameters.Add("@SENDER_TCP_IP_ADDRESS", SqlDbType.NVarChar).Value = wsrqLogQueryData.senderTCPAddress;
               // sCmd.Parameters.Add("@SENDER_ID", SqlDbType.NVarChar).Value = wsrqLogQueryData.senderID;
                sCmd.Parameters.Add("@ENCRYPTION_KEY_ID_IN", SqlDbType.Image).Value = wsrqLogQueryData.encryptedSessionKeyIn;
                sCmd.Parameters.Add("@ENCRYPTED_QUERY_PARAMS", SqlDbType.Image).Value = wsrqLogQueryData.encryptedQueryParams;
                sCmd.Parameters.Add("@ROW_CREATED_TIME", SqlDbType.DateTime).Value = wsrqLogQueryData.rowCreationTime;
                if (validateQueryFailed)
                {
                    int num1 = 0;
                    sCmd.Parameters.Add("@QUERY_PARAM_OK", SqlDbType.Int).Value = wsrqLogQueryData.returnCode;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = wsrqLogQueryData.returnCode;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = wsrqLogQueryData.lastStep;
                }
                else
                {
                    sCmd.Parameters.Add("@QUERY_PARAM_OK", SqlDbType.Int).Value = 2;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = 0;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
                
                }
                m_requestHelper.ExecuteNonQuery(sCmd);
            }

            /// <summary>
            /// Updates WSRQ_LOG on session key decryption
            /// </summary>
            /// <param name="wsrqLogQueryData">Log data structure</param>
            /// <param name="decryptSessionFailed">boolean indicating step succeeded or failed</param>
            public void LogRequestS10(WSRQLogQueryData wsrqLogQueryData, bool decryptSessionFailed)
            {
                string sSql = "UPDATE [WSRQ_LOG] " +
                    "SET [SESSION_KEY_IN] = @objSessionKeyUsedDecrIn, " +
                    "[RETURN_CODE] = @RETURN_CODE, " +
                    "[LAST_STEP] = @LAST_STEP " +
                    "WHERE [EXCHANGEID] = @EXCHANGEID";

                SqlCommand sCmd = new SqlCommand(sSql);
                sCmd.CommandTimeout = 500;

                if (decryptSessionFailed)
                {
                    sCmd.Parameters.Add("@objSessionKeyUsedDecrIn", SqlDbType.Image).Value = DBNull.Value;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = wsrqLogQueryData.returnCode;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = wsrqLogQueryData.lastStep;
                    //sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = wsrqLogQueryData.rowCreationTime;
                }
                else
                {
                    sCmd.Parameters.Add("@objSessionKeyUsedDecrIn", SqlDbType.Image).Value = wsrqLogQueryData.decryptedSessionKeyIn;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = 0;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
                }

                sCmd.Parameters.Add("@EXCHANGEID", SqlDbType.Decimal).Value = wsrqLogQueryData.Exchange_ID;
                m_requestHelper.ExecuteNonQuery(sCmd);
            }

            /// <summary>
            /// Logs into WSRQ_REQUEST_LOG after query decryption step
            /// </summary>
            /// <param name="wsrqLogQueryData">Log data structure</param>
            /// <param name="decryptionFailed">boolean indicating failure of query encryption</param>
            public void LogRequestS15(WSRQLogQueryData wsrqLogQueryData, bool decryptionFailed)
            {
                string sSql = "UPDATE [WSRQ_LOG] " +
                    "SET [DECRYPTION_RESULT] = @DECRYPTION_RESULT, " +
                    "[DECRYPTION_RESULT_DESCRIPTION] = @DECRYPTION_RESULT_DESCRIPTION , " +
                    //"[ROW_CREATED_TIME] = @ROW_CREATED_TIME, " +
                    "[DECRYPTED_QUERY_PARAMXML] = @QUERY_PARAMXML, " +
                    "[RETURN_CODE] = @RETURN_CODE, " +
                    "[LAST_STEP] = @LAST_STEP " +
                    "WHERE [EXCHANGEID] = @EXCHANGEID";

                SqlCommand sCmd = new SqlCommand(sSql);
                sCmd.CommandTimeout = 500;
                sCmd.Parameters.Add("@DECRYPTION_RESULT", SqlDbType.Int).Value = wsrqLogQueryData.decryptionResult;
                sCmd.Parameters.Add("@DECRYPTION_RESULT_DESCRIPTION", SqlDbType.NVarChar).Value = wsrqLogQueryData.decryptionResultDesc;
                sCmd.Parameters.Add("@EXCHANGEID", SqlDbType.Decimal).Value = wsrqLogQueryData.Exchange_ID;
                if (decryptionFailed)
                {
                    sCmd.Parameters.Add("@QUERY_PARAMXML", SqlDbType.NVarChar).Value = DBNull.Value;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = wsrqLogQueryData.returnCode;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = wsrqLogQueryData.lastStep;
                }
                else
                {
                    sCmd.Parameters.Add("@QUERY_PARAMXML", SqlDbType.NVarChar).Value = wsrqLogQueryData.decryptedQueryParamXML;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = 0;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
                }
                m_requestHelper.ExecuteNonQuery(sCmd);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="wsrqLogQueryData"></param>
            /// <param name="hashVerificationFailed"></param>
            public void LogRequestS20(WSRQLogQueryData wsrqLogQueryData, bool hashVerificationFailed)
            {
                string sSql = "UPDATE [WSRQ_LOG] " +
                    "SET [ROW_CREATED_TIME] = @ROW_CREATED_TIME, " +
                    "[RETURN_CODE] = @RETURN_CODE, " +
                    "[LAST_STEP] = @LAST_STEP " +
                    "WHERE [EXCHANGEID] = @EXCHANGEID";
                SqlCommand sCmd = new SqlCommand(sSql);
                sCmd.CommandTimeout = 500;
                sCmd.Parameters.Add("@ROW_CREATED_TIME", SqlDbType.DateTime).Value = wsrqLogQueryData.rowCreationTime;
                sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = wsrqLogQueryData.lastStep;
                sCmd.Parameters.Add("@EXCHANGEID", SqlDbType.Decimal).Value = wsrqLogQueryData.Exchange_ID;
                if (hashVerificationFailed)
                {
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = wsrqLogQueryData.returnCode;
                
                }
                else
                {
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = 1;
                }
                m_requestHelper.ExecuteNonQuery(sCmd);


            }

            /// <summary>
            /// Updates TCHQ_REQUEST_LOG with Query XML Validation status
            /// </summary>
            /// <param name="wsrqLogQueryData">Log data structure</param>
            /// <param name="xmlNotValid">boolean indicating if the query xml was valid or invalid</param>
            public void LogRequestS25(WSRQLogQueryData wsrqLogQueryData, bool xmlNotValid)
            {
                string sSql = "UPDATE [WSRQ_LOG] " +
                    "SET [QUERY_XML_VALID] = @QUERY_XML_VALID, " +
                    "[QUERY_XML_INVALID_REASON] = @QUERY_XML_INVALID_REASON, " +
                    "[RETURN_CODE] = @RETURN_CODE, " +
                    "[LAST_STEP] = @LAST_STEP " +
                    "WHERE [EXCHANGEID] = @EXCHANGEID";

                SqlCommand sCmd = new SqlCommand(sSql);
                sCmd.CommandTimeout = 500;

                if (xmlNotValid)
                {
                    sCmd.Parameters.Add("@QUERY_XML_VALID", SqlDbType.Bit).Value = 0;
                    sCmd.Parameters.Add("@QUERY_XML_INVALID_REASON", SqlDbType.NVarChar).Value = wsrqLogQueryData.invalidQueryXMLReason;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = wsrqLogQueryData.returnCode;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = wsrqLogQueryData.lastStep;
                }
                else
                {
                    sCmd.Parameters.Add("@QUERY_XML_VALID", SqlDbType.Bit).Value = 1;
                    sCmd.Parameters.Add("@QUERY_XML_INVALID_REASON", SqlDbType.NVarChar).Value = DBNull.Value;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = 0;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
                
                }

                sCmd.Parameters.Add("@EXCHANGEID", SqlDbType.Decimal).Value = wsrqLogQueryData.Exchange_ID;
                m_requestHelper.ExecuteNonQuery(sCmd);
            }

            /// <summary>
            /// Updates WSRQ_REQUEST_LOG 
            /// </summary>
            /// <param name="wsrqLogQueryData">Log data structure</param>
            /// <param name="userNotAuthorised">boolean indicating success of authorization</param>
            public void LogRequestS30(WSRQLogQueryData wsrqLogQueryData, bool userNotAuthorised)
            {
                string sSql = "UPDATE [WSRQ_LOG] " +
                    "SET  [SUBSCRIBER_ID] = @SENDER_ID, [SENDER_AUTHENTICATED] = @SENDER_AUTHENTICATED,[SENDER_PASSWORD] = @SENDER_PASSWORD, " +
                    "[RETURN_CODE] = @RETURN_CODE, " +
                    "[LAST_STEP] = @LAST_STEP " +
                    "WHERE [EXCHANGEID] = @EXCHANGEID";

                SqlCommand sCmd = new SqlCommand(sSql);
                sCmd.CommandTimeout = 500;

                if (userNotAuthorised)
                {
                    sCmd.Parameters.Add("@SENDER_ID", SqlDbType.NVarChar).Value = DBNull.Value;
                    sCmd.Parameters.Add("@SENDER_PASSWORD", SqlDbType.NVarChar).Value = DBNull.Value;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = wsrqLogQueryData.returnCode;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = wsrqLogQueryData.lastStep;
                }
                else
                {
                    sCmd.Parameters.Add("@SENDER_ID", SqlDbType.NVarChar).Value = wsrqLogQueryData.senderID;
                    if (wsrqLogQueryData.senderPassword == null)
                    {
                        sCmd.Parameters.Add("@SENDER_PASSWORD", SqlDbType.NVarChar).Value = DBNull.Value ;
                    }
                    else
                    {
                        sCmd.Parameters.Add("@SENDER_PASSWORD", SqlDbType.NVarChar).Value = wsrqLogQueryData.senderPassword;
                    }
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = 0;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
                }
                sCmd.Parameters.Add("@EXCHANGEID", SqlDbType.Decimal).Value = wsrqLogQueryData.Exchange_ID;
                sCmd.Parameters.Add("@SENDER_AUTHENTICATED ", SqlDbType.Int).Value = wsrqLogQueryData.senderAuthenticated;

                m_requestHelper.ExecuteNonQuery(sCmd);
            }

            /// <summary>
            /// Update TCHQ_REQUEST_LOG with Carnet query results
            /// </summary>
            /// <param name="wsrqLogQueryData">Log data structure</param>
            /// <param name="WSRQQueryFailed">boolean indicating cuccess failure of query</param>
            public void LogRequestS35(WSRQLogQueryData wsrqLogQueryData, bool WSRQQueryFailed)
            {
                string sSql = "UPDATE [WSRQ_LOG] SET " +
                    "[ORIGIN_TIME]=@ORIGIN_TIME,  " +
                    "[SENDER_MESSAGEID]=@SENDER_MESSAGEID,  " +
                    "[NUMBER_OF_REQUESTS_SENT]=@NUMBER_OF_REQUESTS_SENT,  " +
                    "[QUERY_TYPE]=@QUERY_TYPE,  " +
                    "[INFORMATION_EXCHANGE_VERSION]=@INFORMATION_EXCHANGE_VERSION,  " +
                    "[QUERY_RESULT_CODE]=@QUERY_RESULT_CODE, " +
                    "[RETURN_CODE] = @RETURN_CODE, " +
                    "[LAST_STEP] = @LAST_STEP " +
                    "WHERE [EXCHANGEID] = @EXCHANGEID ";

                SqlCommand sCmd = new SqlCommand(sSql);
                sCmd.CommandTimeout = 500;

                sCmd.Parameters.Add("@QUERY_TYPE", SqlDbType.Int).Value = wsrqLogQueryData.queryType;
                sCmd.Parameters.Add("@ORIGIN_TIME", SqlDbType.DateTime).Value = wsrqLogQueryData.originTime;
                sCmd.Parameters.Add("@SENDER_MESSAGEID", SqlDbType.NVarChar).Value = wsrqLogQueryData.Sender_MessageID;
                sCmd.Parameters.Add("@NUMBER_OF_REQUESTS_SENT", SqlDbType.Int).Value = wsrqLogQueryData.No_Of_Requests_Sent;
                sCmd.Parameters.Add("@INFORMATION_EXCHANGE_VERSION", SqlDbType.NVarChar).Value = wsrqLogQueryData.Information_Exchange_Version;

                if (WSRQQueryFailed)
                {
                    sCmd.Parameters.Add("@QUERY_RESULT_CODE", SqlDbType.Int).Value = DBNull.Value;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = wsrqLogQueryData.returnCode;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = wsrqLogQueryData.lastStep;
                }
                else
                {
                    sCmd.Parameters.Add("@QUERY_RESULT_CODE", SqlDbType.Int).Value = wsrqLogQueryData.queryResultCode;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = 0;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
                }
                sCmd.Parameters.Add("@EXCHANGEID", SqlDbType.Decimal).Value = wsrqLogQueryData.Exchange_ID;

                m_requestHelper.ExecuteNonQuery(sCmd);
            }

            /// <summary>
            /// Updates WSRQ_LOG with Response XML encryption status
            /// </summary>
            /// <param name="wsrqLogQueryData">Log data structure</param>
            /// <param name="hashEncFailed">boolean indicating failure of response encryption</param>
            public void LogRequestS40(WSRQLogQueryData wsrqLogQueryData, bool hashEncFailed)
            {
                string sSql = "UPDATE [WSRQ_LOG] " +
                    "SET RESPONSE_ENCRYPTION_RESULT = @RESPONSE_ENCRYPTION_RESULT, " +
                    "RESPONSE_ENCRYPTION_RESULT_DESCRIPTION = @RESPONSE_ENCRYPTION_RESULT_DESCRIPTION, " +
                    "SESSION_KEY_OUT = @SESSION_KEY_USED_DECRYPTED_OUT, " +
                    "[RETURN_CODE] = @RETURN_CODE, " +
                    "[LAST_STEP] = @LAST_STEP " +
                    "WHERE [EXCHANGEID] = @ExchangeID ";

                SqlCommand sCmd = new SqlCommand(sSql);
                sCmd.CommandTimeout = 500;

                sCmd.Parameters.Add("@RESPONSE_ENCRYPTION_RESULT", SqlDbType.Int).Value = wsrqLogQueryData.responseEncryptionResult;

                if (hashEncFailed)
                {
                    sCmd.Parameters.Add("@RESPONSE_ENCRYPTION_RESULT_DESCRIPTION", SqlDbType.NVarChar).Value = wsrqLogQueryData.responseEncryptionResultDesc;
                    sCmd.Parameters.Add("@SESSION_KEY_USED_DECRYPTED_OUT", SqlDbType.Image).Value = DBNull.Value;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = wsrqLogQueryData.returnCode;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = wsrqLogQueryData.lastStep;
                }
                else
                {
                    sCmd.Parameters.Add("@RESPONSE_ENCRYPTION_RESULT_DESCRIPTION", SqlDbType.NVarChar).Value = DBNull.Value;
                    sCmd.Parameters.Add("@SESSION_KEY_USED_DECRYPTED_OUT", SqlDbType.Image).Value = wsrqLogQueryData.decryptedSessionKeyOut;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = 0;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = 0;
                    
                }
                sCmd.Parameters.Add("@EXCHANGEID", SqlDbType.Decimal).Value = wsrqLogQueryData.Exchange_ID;
                m_requestHelper.ExecuteNonQuery(sCmd);
            }

            /// <summary>
            /// Updates TCHQ_Request_log with Session Key encryption status
            /// </summary>
            /// <param name="wsrqLogQueryData">Log data structure</param>
            /// <param name="sessionKeyEncFailed">boolean indicating failure encrypting session key</param>
            public void LogRequestS45(WSRQLogQueryData wsrqLogQueryData, bool sessionKeyEncFailed)
            {
                string sSql = "UPDATE [WSRQ_LOG] " +
                    //"SET SESSION_KEY_ENCRYPTION_KEY_ID_USED = @SESSION_KEY_ENCRYPTION_KEY_ID_USED, " +
                    " SET ENCRYPTION_KEY_ID_OUT = @SESSION_KEY_USED_ENCRYPTED_OUT, " +
                    "[RETURN_CODE] = @RETURN_CODE, " +
                    "[LAST_STEP] = @LAST_STEP " +
                    "WHERE [EXCHANGEID] = @EXCHANGEID ";

                SqlCommand sCmd = new SqlCommand(sSql);
                sCmd.CommandTimeout = 500;

                if (sessionKeyEncFailed)
                {
                    //sCmd.Parameters.Add("@SESSION_KEY_ENCRYPTION_KEY_ID_USED", SqlDbType.NVarChar).Value = DBNull.Value;
                    sCmd.Parameters.Add("@SESSION_KEY_USED_ENCRYPTED_OUT ", SqlDbType.Image).Value = DBNull.Value;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = wsrqLogQueryData.returnCode;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = wsrqLogQueryData.lastStep;
                }
                else
                {
                //    sCmd.Parameters.Add("@SESSION_KEY_ENCRYPTION_KEY_ID_USED", SqlDbType.NVarChar).Value = wsrqLogQueryData.sessionKeyEncryptionKeyIDUsed;
                    sCmd.Parameters.Add("@SESSION_KEY_USED_ENCRYPTED_OUT", SqlDbType.Image).Value = wsrqLogQueryData.encryptedSessionKeyOut;
                    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = 0;
                    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
                }
                sCmd.Parameters.Add("@EXCHANGEID", SqlDbType.Decimal).Value = wsrqLogQueryData.Exchange_ID;

                m_requestHelper.ExecuteNonQuery(sCmd);
            }
            /// <summary>
            /// Final step update to the WSRQ_LOG table
            /// </summary>
            /// <param name="wsrqLogQueryData">Log data structure</param>
            public void LogRequestS99(WSRQLogQueryData wsrqLogQueryData)
            {
                string sSql = "UPDATE [WSRQ_LOG] " +
                    "SET [RETURN_CODE] = @RETURN_CODE, " +
                    "[LAST_STEP] = @LAST_STEP " +
                    "WHERE [EXCHANGEID] = @EXCHANGEID";

                SqlCommand sCmd = new SqlCommand(sSql);
                sCmd.CommandTimeout = 500;

                sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = wsrqLogQueryData.returnCode;
                sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = wsrqLogQueryData.lastStep;
                sCmd.Parameters.Add("@EXCHANGEID", SqlDbType.Decimal).Value = wsrqLogQueryData.Exchange_ID;
                m_requestHelper.ExecuteNonQuery(sCmd);
            }


            /// <summary>
            /// Method to update the wsrq_Sequence table
            /// </summary>
            /// <param name="Exchange_ID">ID generated  for the query</param>
            /// <param name="step">Step number</param>
            /// <param name="stepResult">Result of the step</param>
            /// <param name="stepDesc"></param>
            /// <param name="stepCompletionTime">Time stam</param>
            public void LogSequenceStep(Double ExchangeID, Int64 step, Int64 stepResult, String stepDesc, DateTime stepCompletionTime)
            {
                string sSql = "INSERT INTO [WSRQ_SEQUENCE] " +
                   "([EXCHANGEID], [WSRQ_STEP], [WSRQ_STEP_RESULT], " +
                   " [WSRQ_STEP_ERROR_DESC], [LAST_UPDATE_TIME]) " +
                   " VALUES " +
                   "(@ExchangeID, @WSRQ_STEP, @WSRQ_STEP_RESULT, " +
                   " @WSRQ_STEP_ERROR_DESC, @LAST_UPDATE_TIME)";

                SqlCommand sCmd = new SqlCommand(sSql);
                sCmd.CommandTimeout = 500;

                sCmd.Parameters.Add("@ExchangeID", SqlDbType.Int).Value = ExchangeID;
                sCmd.Parameters.Add("@WSRQ_STEP", SqlDbType.Int).Value = step;
                sCmd.Parameters.Add("@WSRQ_STEP_RESULT", SqlDbType.Int).Value = stepResult;
                sCmd.Parameters.Add("@WSRQ_STEP_ERROR_DESC", SqlDbType.NVarChar).Value = stepDesc;
                sCmd.Parameters.Add("@LAST_UPDATE_TIME", SqlDbType.DateTime).Value = stepCompletionTime;
                m_requestHelper.ExecuteNonQuery(sCmd);
            }


            /// <summary>
            /// Method to fetch detail  from wsrq_detail table
            /// </summary>
            public DataSet GetReconciliationRequestData(string sICC)
            {         
                DataSet DSWSRQDTL=new DataSet();
                //string sSql = "SELECT [EXCHANGEID], [RequestID], [RequestDate], " +
                //   " [RequestReminderNum], [RequestDataSource],[TNO], [ICC], [DCL], [CNL], [COF], [DDI], [RND], [PFD], [CWR], [VPN], [COM], [RBC], [PIC],[RequestRemark]  " +
                //   " FROM  [WSRQ_DETAIL]  WHERE EXCHANGEID=0 AND [ICC] =N'" + sICC + "'";
                string sSql = "BEGIN TRANSACTION " +
                    "SELECT [EXCHANGEID], [RequestID], [RequestDate], " +
                    "[RequestReminderNum], [RequestDataSource],[TNO], [ICC], [DCL], [CNL], [COF], [DDI], [RND], [PFD], [CWR], [VPN], [COM], [RBC], [PIC],[RequestRemark]  " +
                    "FROM [WSRQ_DETAIL] WHERE EXCHANGEID = 0 AND [ICC] = N'" + sICC + "'" +
                    "UPDATE [WSRQ_DETAIL] SET [EXCHANGEID]= -1 WHERE [EXCHANGEID] = 0 AND [ICC] = N'" + sICC + "'"  +
                    "COMMIT TRANSACTION ";
                         
              
                //DSWSRQDTL = m_requestHelper.GetDataSet(sSql,"WSRQ_DETAIL");
                 m_requestHelper.FillDataSetTable(sSql,DSWSRQDTL,"WSRQ_DETAIL");
               
                return DSWSRQDTL;
            }


            /// <summary>
            /// Method to update ExchangeID in  from wsrq_details table.
            /// </summary>
            public void UpdateWSRQDetails(string RequestID,int ExchangeID, string sICC)
            {
                 string sSql = "UPDATE [WSRQ_DETAIL] " +
                    "SET [EXCHANGEID] = @ExchangeID " +
                    "WHERE [EXCHANGEID] = -1 and [REQUESTID] = @REQUESTID AND [ICC] = @ICC";

                SqlCommand sCmd = new SqlCommand(sSql);
                sCmd.CommandTimeout = 500;
                sCmd.Parameters.Add("@ExchangeID", SqlDbType.Int).Value = ExchangeID;
                sCmd.Parameters.Add("@REQUESTID", SqlDbType.NVarChar).Value = RequestID;
                sCmd.Parameters.Add("@ICC", SqlDbType.NVarChar).Value = sICC;
                m_requestHelper.ExecuteNonQuery(sCmd);
                return;
            }
           




            /// <summary>
            /// 
            /// </summary>
            /// <param name="Exchange_ID"></param>
            /// <param name="dtResponseSent"></param>
            /// <param name="ResponseResult"></param>
            public void LogRequestResponse(long ExchangeID, DateTime dtResponseSent, bool ResponseResult)
            {
                string sSql = "UPDATE [WSRQ_LOG] " +
                    "SET [RESPONSE_RESULT] = @RESPONSE_RESULT, " +
                    "[RESPONSE_TIME] = @RESPONSE_TIME " +
                    "WHERE [EXCHANGEID] = @ExchangeID ";

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

                sCmd.Parameters.Add("@ExchangeID", SqlDbType.Decimal).Value = ExchangeID;

                m_requestHelper.ExecuteNonQuery(sCmd);
            }
        }

    }

