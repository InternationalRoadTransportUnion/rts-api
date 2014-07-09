using System;
using IRU.RTS;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;
using IRU.RTS.CryptoInterfaces;

using System.Data;
using System.Data.SqlClient;
using System.Text;

using IRU.RTS.TIREPD; 

namespace IRU.RTS.TIREPD
{
    //public class TIRCarnetHolderQueryParamsStruct
    //{
    //    public byte[] Signature;
    //    public string KeyId;

    //    public string Sender;
    //    public DateTime SentTime;
    //    public string Originator;
    //    public DateTime OriginTime;
    //    public string Password;
    //    public int Query_Type;
    //    public string Carnet_Number; //Digits & Number
    //}

    //public class G2BLogRequestStruct
    //{
    //    public double G2B_QueryID;
    //    public string decryptionKeyID; //MessageTag - Key ID Used to encrypt by FCS
    //    public byte[] encryptedQueryParams;
    //    public String senderTCPAddress;
    //    public Int32 queryParamValidResult;
    //    public Int32 decryptionResult;
    //    public String decryptionResultDesc;
    //    public String decryptedQueryParamXML;
    //    public byte[] encryptedSessionKeyIn;
    //    public byte[] decryptedSessionKeyIn;
    //    //public Byte[]		decryptionKeyUsed;
    //    //public Byte[]		signatureKeyUsed;
    //    public Boolean validQueryXML;
    //    public String invalidQueryXMLReason;
    //    public String senderID;
    //    public String originatorID;
    //    public DateTime originTime;
    //    //public String senderPassword;
    //    //public Int32 queryType;
    //    //public Int32 queryReason;
    //    //public String tirCarnetNumber;
    //    public String senderQueryID;
    //    public Int32 senderAuthenticated;
    //    //public Int32 queryResultCode;
    //    //public String holderID;
    //    //public object validityDate;	//to store null value
    //    //public String assocShortName;
    //    //public object numberOfTerminations; // to store null values
    //    public DateTime rowCreationTime;
    //    public String sessionKeyEncryptionKeyIDUsed;
    //    public byte[] encryptedSessionKeyOut;
    //    public byte[] decryptedSessionKeyOut;
    //    public int responseEncryptionResult;
    //    public String responseEncryptionResultDesc;
    //    public int returnCode;
    //    public int lastStep;
    //}

    public class SubscriberDetailsStruct
    {
        public String subscriberID;
        public String password;
        public String HashAlgo;
        public String SessionKeyAlgo;
        public String CopyToId;
        public String CopyToAddress;
    }
    public class KeyInformationStruct
    {
        public string sServiceID;
        public string sEncryptKeyID;
        public long lKeyIndexUsed;
        public byte[] byEncryption;
        public byte[] bySignature;
        public string sSubscriberID;
        public int iKeyStatus;
    }

    /// <summary>
    /// Wraps all calls to TIREPD_DB.
    /// </summary>
    public class B2G_DBHelper
    {
        private IDBHelper m_requestHelper;
        /// <summary>
        /// Initialize the G2B_DB instance  of IDBHelper
        /// </summary>
        /// <param name="requestHelper"></param>
        public B2G_DBHelper(/*IDBHelper subscriberHelper,*/IDBHelper requestHelper)
        {
            //m_subscriberHelper = subscriberHelper;
            m_requestHelper = requestHelper;
        }


        ///// <summary>
        ///// Log Initial Request in G2B_REQUEST_LOG, as soon as the query is recieved 
        ///// Insert a new query record in the Table all other step functions update this record
        ///// </summary>
        ///// <param name="tchQLogRequestData"> Log data structure</param>
        ///// <param name="validateQueryFailed"> boolean indicating succes or failure</param>
        //public void LogRequestS5(G2BLogRequestStruct tchQLogRequestData, bool validateQueryFailed)
        //{

        //    string sSql = "INSERT INTO [G2B_REQUEST_LOG] " +
        //        "([G2B_QUERY_ID], [ENCRYPTED_QUERY_PARAMS], " +
        //        "[SENDER_TCP_IP_ADDRESS], [SESSION_KEY_USED_ENCRYPTED_IN], " +
        //        "[DECRYPTION_KEY_ID], [ROW_CREATED_TIME], [QUERY_PARAM_OK], " +
        //        "[RETURN_CODE], [LAST_STEP], [COMPLETION_TIME], [SENDER_ID], [SENDER_QUERY_ID], [PROCESSED_BY_BT]) VALUES " +
        //        "(@G2B_QUERY_ID, @ENCRYPTED_QUERY_PARAMS, " +
        //        "@SENDER_TCP_IP_ADDRESS, @SESSION_KEY_USED_ENCRYPTED_IN, " +
        //        "@DECRYPTION_KEY_ID, @ROW_CREATED_TIME, @QUERY_PARAM_OK, " +
        //        "@RETURN_CODE, @LAST_STEP, @COMPLETION_TIME, @SENDER_ID, @SENDER_QUERY_ID, 0) ";

        //    //				"( " + tchQLogRequestData.G2B_QueryID.ToString() + ", " + 
        //    //				"@objQueryParam" + ", " +
        //    //				 "'" + tchQLogRequestData.senderTCPAddress  + "', " +
        //    //                "@objSessionKeyUsedEncrIn" + ", " + 
        //    //				"'" + tchQLogRequestData.decryptionKeyID  +"', " + 
        //    //				"'" + tchQLogRequestData.rowCreationTime.ToString("yyyy-MM-dd HH:mm:ss") + "' ) ";

        //    SqlCommand sCmd = new SqlCommand(sSql);
        //    sCmd.CommandTimeout = 500;

        //    sCmd.Parameters.Add("@G2B_QUERY_ID", SqlDbType.Decimal).Value = tchQLogRequestData.G2B_QueryID;
        //    sCmd.Parameters.Add("@ENCRYPTED_QUERY_PARAMS", SqlDbType.Image).Value = tchQLogRequestData.encryptedQueryParams;
        //    sCmd.Parameters.Add("@SENDER_TCP_IP_ADDRESS", SqlDbType.NVarChar).Value = tchQLogRequestData.senderTCPAddress;
        //    sCmd.Parameters.Add("@SESSION_KEY_USED_ENCRYPTED_IN", SqlDbType.Image).Value = tchQLogRequestData.encryptedSessionKeyIn;
        //    sCmd.Parameters.Add("@DECRYPTION_KEY_ID", SqlDbType.NVarChar).Value = tchQLogRequestData.decryptionKeyID;
        //    sCmd.Parameters.Add("@ROW_CREATED_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.rowCreationTime;

        //    if (validateQueryFailed)
        //    {
        //        sCmd.Parameters.Add("@QUERY_PARAM_OK", SqlDbType.Int).Value = tchQLogRequestData.returnCode;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = tchQLogRequestData.returnCode;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = tchQLogRequestData.lastStep;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.rowCreationTime;
        //    }
        //    else
        //    {
        //        sCmd.Parameters.Add("@QUERY_PARAM_OK", SqlDbType.NVarChar).Value = 2;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
        //    }

        //    sCmd.Parameters.Add("@SENDER_ID", SqlDbType.NVarChar).Value = tchQLogRequestData.senderID;
        //    sCmd.Parameters.Add("@SENDER_QUERY_ID", SqlDbType.NVarChar).Value = tchQLogRequestData.senderQueryID;

        //    m_requestHelper.ExecuteNonQuery(sCmd);
        //}

        ///// <summary>
        ///// Updates G2B_REQUEST_LOG on session key decryption
        ///// </summary>
        ///// <param name="tchQLogRequestData">Log data structure</param>
        ///// <param name="decryptSessionFailed">boolean indicating step succeeded or failed</param>
        //public void LogRequestS10(G2BLogRequestStruct tchQLogRequestData, bool decryptSessionFailed)
        //{
        //    string sSql = "UPDATE [G2B_REQUEST_LOG] " +
        //        "SET SESSION_KEY_USED_DECRYPTED_IN = @objSessionKeyUsedDecrIn, " +
        //        //"[ROW_CREATED_TIME] = @ROW_CREATED_TIME ," +
        //        "[RETURN_CODE] = @RETURN_CODE, " +
        //        "[LAST_STEP] = @LAST_STEP, " +
        //        "[COMPLETION_TIME] = @COMPLETION_TIME " +
        //        "WHERE [G2B_QUERY_ID] = @G2B_QUERY_ID";

        //    //"WHERE [G2B_QUERY_ID] = "+ tchQLogRequestData.G2B_QueryID.ToString() ;

        //    SqlCommand sCmd = new SqlCommand(sSql);
        //    sCmd.CommandTimeout = 500;

        //    //sCmd.Parameters.Add("@ROW_CREATED_TIME", SqlDbType.DateTime ).Value = tchQLogRequestData.rowCreationTime ;

        //    if (decryptSessionFailed)
        //    {
        //        sCmd.Parameters.Add("@objSessionKeyUsedDecrIn", SqlDbType.Image).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = tchQLogRequestData.returnCode;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = tchQLogRequestData.lastStep;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.rowCreationTime;
        //    }
        //    else
        //    {
        //        sCmd.Parameters.Add("@objSessionKeyUsedDecrIn", SqlDbType.Image).Value = tchQLogRequestData.decryptedSessionKeyIn;

        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
        //    }

        //    sCmd.Parameters.Add("@G2B_QUERY_ID", SqlDbType.Decimal).Value = tchQLogRequestData.G2B_QueryID;

        //    m_requestHelper.ExecuteNonQuery(sCmd);
        //}

        ///// <summary>
        ///// Logs into G2B_REQUEST_LOG after query decryption step
        ///// </summary>
        ///// <param name="tchQLogRequestData">Log data structure</param>
        ///// <param name="decryptionFailed">boolean indicating failure of query encryption</param>
        //public void LogRequestS15(G2BLogRequestStruct tchQLogRequestData, bool decryptionFailed)
        //{
        //    string sSql = "UPDATE [G2B_REQUEST_LOG] " +
        //        "SET [DECRYPTION_RESULT] = @DECRYPTION_RESULT, " +
        //        "[DECRYPTION_RESULT_DESCRIPTION] = @DECRYPTION_RESULT_DESCRIPTION , " +
        //        //"[ROW_CREATED_TIME] = @ROW_CREATED_TIME, " +
        //        "[QUERY_XML] = @QUERY_XML, " +
        //        "[RETURN_CODE] = @RETURN_CODE, " +
        //        "[LAST_STEP] = @LAST_STEP, " +
        //        "[COMPLETION_TIME] = @COMPLETION_TIME " +
        //        "WHERE [G2B_QUERY_ID] = @G2B_QUERY_ID";

        //    SqlCommand sCmd = new SqlCommand(sSql);
        //    sCmd.CommandTimeout = 500;

        //    sCmd.Parameters.Add("@DECRYPTION_RESULT", SqlDbType.Int).Value = tchQLogRequestData.decryptionResult;
        //    sCmd.Parameters.Add("@DECRYPTION_RESULT_DESCRIPTION", SqlDbType.NVarChar).Value = tchQLogRequestData.decryptionResultDesc;
        //    //sCmd.Parameters.Add("@ROW_CREATED_TIME", SqlDbType.DateTime ).Value = tchQLogRequestData.rowCreationTime ;

        //    if (decryptionFailed)
        //    {
        //        sCmd.Parameters.Add("@QUERY_XML", SqlDbType.NVarChar).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = tchQLogRequestData.returnCode;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = tchQLogRequestData.lastStep;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.rowCreationTime;
        //    }
        //    else
        //    {
        //        sCmd.Parameters.Add("@QUERY_XML", SqlDbType.NVarChar).Value = tchQLogRequestData.decryptedQueryParamXML;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
        //    }

        //    sCmd.Parameters.Add("@G2B_QUERY_ID", SqlDbType.Decimal).Value = tchQLogRequestData.G2B_QueryID;

        //    //			string sSql = "UPDATE [G2B_REQUEST_LOG] " + 
        //    //				"SET [DECRYPTION_RESULT] = " + tchQLogRequestData.decryptionResult.ToString() + "," +
        //    //				"[DECRYPTION_RESULT_DESCRIPTION] = '" + tchQLogRequestData.decryptionResultDesc + "'," +
        //    //				"[ROW_CREATED_TIME] = '" + tchQLogRequestData.rowCreationTime.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
        //    //				"[LAST_STEP] = 10 " +
        //    //				"WHERE [G2B_QUERY_ID] = "+ tchQLogRequestData.G2B_QueryID.ToString() ;

        //    m_requestHelper.ExecuteNonQuery(sCmd);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="tchQLogRequestData"></param>
        ///// <param name="hashVerificationFailed"></param>
        //public void LogRequestS25(G2BLogRequestStruct tchQLogRequestData, bool hashVerificationFailed)
        //{
        //    string sSql = "UPDATE [G2B_REQUEST_LOG] " +
        //        "SET [ROW_CREATED_TIME] = @ROW_CREATED_TIME, " +
        //        "[RETURN_CODE] = @RETURN_CODE, " +
        //        "[LAST_STEP] = @LAST_STEP, " +
        //        "[COMPLETION_TIME] = @COMPLETION_TIME " +
        //        "WHERE [G2B_QUERY_ID] = @G2B_QUERY_ID";

        //    if (hashVerificationFailed)
        //    {
        //        SqlCommand sCmd = new SqlCommand(sSql);
        //        sCmd.CommandTimeout = 500;

        //        sCmd.Parameters.Add("@ROW_CREATED_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.rowCreationTime;

        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = tchQLogRequestData.returnCode;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = tchQLogRequestData.lastStep;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.rowCreationTime;

        //        sCmd.Parameters.Add("@G2B_QUERY_ID", SqlDbType.Decimal).Value = tchQLogRequestData.G2B_QueryID;

        //        m_requestHelper.ExecuteNonQuery(sCmd);
        //    }


        //}

        ///*
        ///// <summary>
        ///// Updates G2B_REQUEST_LOG with Query XML Validation status
        ///// </summary>
        ///// <param name="tchQLogRequestData">Log data structure</param>
        ///// <param name="xmlNotValid">boolean indicating if the query xml was valid or invalid</param>
        //public void LogRequestS25(G2BLogRequestStruct tchQLogRequestData, bool xmlNotValid)
        //{
        //    string sSql = "UPDATE [G2B_REQUEST_LOG] " +
        //        "SET [QUERY_XML_VALID] = @QUERY_XML_VALID, " +
        //        "[QUERY_XML_INVALID_REASON] = @QUERY_XML_INVALID_REASON, " +
        //        "[RETURN_CODE] = @RETURN_CODE, " +
        //        "[LAST_STEP] = @LAST_STEP, " +
        //        "[COMPLETION_TIME] = @COMPLETION_TIME " +
        //        "WHERE [G2B_QUERY_ID] = @G2B_QUERY_ID";

        //    SqlCommand sCmd = new SqlCommand(sSql);
        //    sCmd.CommandTimeout = 500;

        //    if (xmlNotValid)
        //    {
        //        sCmd.Parameters.Add("@QUERY_XML_VALID", SqlDbType.Bit).Value = 0;
        //        sCmd.Parameters.Add("@QUERY_XML_INVALID_REASON", SqlDbType.NVarChar).Value = tchQLogRequestData.invalidQueryXMLReason;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = tchQLogRequestData.returnCode;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = tchQLogRequestData.lastStep;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.rowCreationTime;
        //    }
        //    else
        //    {
        //        sCmd.Parameters.Add("@QUERY_XML_VALID", SqlDbType.Bit).Value = 1;
        //        sCmd.Parameters.Add("@QUERY_XML_INVALID_REASON", SqlDbType.NVarChar).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
        //    }

        //    sCmd.Parameters.Add("@G2B_QUERY_ID", SqlDbType.Decimal).Value = tchQLogRequestData.G2B_QueryID;

        //    m_requestHelper.ExecuteNonQuery(sCmd);
        //}

        ///// <summary>
        ///// Updates G2B_REQUEST_LOG with Sender Authorization status  
        ///// </summary>
        ///// <param name="tchQLogRequestData">Log data structure</param>
        ///// <param name="userNotAuthorised">boolean indicating success of authorization</param>
        //public void LogRequestS30(G2BLogRequestStruct tchQLogRequestData, bool userNotAuthorised)
        //{
        //    string sSql = "UPDATE [G2B_REQUEST_LOG] " +
        //        "SET [SENDER_PASSWORD] = @SENDER_PASSWORD, " +
        //        "[SENDER_AUTHENTICATED]=@SENDER_AUTHENTICATED,  " +
        //        "[SENDER_ID] = @SENDER_ID, " +
        //        "[RETURN_CODE] = @RETURN_CODE, " +
        //        "[LAST_STEP] = @LAST_STEP, " +
        //        "[COMPLETION_TIME] = @COMPLETION_TIME " +
        //        "WHERE [G2B_QUERY_ID] = @G2B_QUERY_ID";

        //    SqlCommand sCmd = new SqlCommand(sSql);
        //    sCmd.CommandTimeout = 500;

        //    if (userNotAuthorised)
        //    {
        //        sCmd.Parameters.Add("@SENDER_PASSWORD", SqlDbType.NVarChar).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@SENDER_AUTHENTICATED", SqlDbType.Int).Value = tchQLogRequestData.senderAuthenticated;
        //        sCmd.Parameters.Add("@SENDER_ID", SqlDbType.NVarChar).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = tchQLogRequestData.returnCode;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = tchQLogRequestData.lastStep;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.rowCreationTime;
        //    }
        //    else
        //    {
        //        if (tchQLogRequestData.senderPassword != null)
        //        {
        //            sCmd.Parameters.Add("@SENDER_PASSWORD", SqlDbType.NVarChar).Value = tchQLogRequestData.senderPassword;
        //        }
        //        else
        //        {
        //            sCmd.Parameters.Add("@SENDER_PASSWORD", SqlDbType.NVarChar).Value = DBNull.Value;
        //        }
        //        sCmd.Parameters.Add("@SENDER_AUTHENTICATED", SqlDbType.Int).Value = tchQLogRequestData.senderAuthenticated;
        //        sCmd.Parameters.Add("@SENDER_ID", SqlDbType.NVarChar).Value = tchQLogRequestData.senderID;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
        //    }
        //    sCmd.Parameters.Add("@G2B_QUERY_ID", SqlDbType.Decimal).Value = tchQLogRequestData.G2B_QueryID;

        //    m_requestHelper.ExecuteNonQuery(sCmd);
        //}

        ///// <summary>
        ///// Update G2B_REQUEST_LOG with Carnet query results
        ///// </summary>
        ///// <param name="tchQLogRequestData">Log data structure</param>
        ///// <param name="cwQueryFailed">boolean indicating cuccess failure of query</param>
        //public void LogRequestS35(G2BLogRequestStruct tchQLogRequestData, bool cwQueryFailed)
        //{
        //    string sSql = "UPDATE [G2B_REQUEST_LOG] " +
        //        "SET [ORIGINATOR]=@ORIGINATOR, " +
        //        "[ORIGIN_TIME]=@ORIGIN_TIME, " +
        //        "[SENDER_PASSWORD]=@SENDER_PASSWORD,  " +
        //        "[QUERY_TYPE]=@QUERY_TYPE,  " +
        //        "[QUERY_REASON]=@QUERY_REASON,  " +
        //        "[CARNET_NUMBER]=@CARNET_NUMBER,  " +
        //        //"[SENDER_QUERY_ID]=@SENDER_QUERY_ID,  " +
        //        "[QUERY_RESULT_CODE]=@QUERY_RESULT_CODE, " +
        //        //"[SENDER_AUTHENTICATED]=@SENDER_AUTHENTICATED,  " +
        //        "[HOLDER_ID]=@HOLDER_ID,  " +
        //        "[VALIDITY_DATE]=@VALIDITY_DATE,  " +
        //        "[ASSOCIATION_SHORT_NAME]=@ASSOCIATION_SHORT_NAME,  " +
        //        "[NUMBER_TERMINATIONS]=@NUMBER_TERMINATIONS,  " +
        //        "[RETURN_CODE] = @RETURN_CODE, " +
        //        "[LAST_STEP] = @LAST_STEP, " +
        //        "[COMPLETION_TIME] = @COMPLETION_TIME " +
        //        "WHERE [G2B_QUERY_ID] = @G2B_QUERY_ID ";

        //    SqlCommand sCmd = new SqlCommand(sSql);
        //    sCmd.CommandTimeout = 500;

        //    sCmd.Parameters.Add("@ORIGINATOR", SqlDbType.NVarChar).Value = tchQLogRequestData.originatorID;
        //    sCmd.Parameters.Add("@ORIGIN_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.originTime;
        //    sCmd.Parameters.Add("@SENDER_PASSWORD", SqlDbType.NVarChar).Value = tchQLogRequestData.senderPassword;
        //    sCmd.Parameters.Add("@QUERY_TYPE", SqlDbType.Int).Value = tchQLogRequestData.queryType;
        //    sCmd.Parameters.Add("@QUERY_REASON", SqlDbType.Int).Value = tchQLogRequestData.queryReason;
        //    sCmd.Parameters.Add("@CARNET_NUMBER", SqlDbType.NVarChar).Value = tchQLogRequestData.tirCarnetNumber;

        //    if (cwQueryFailed)
        //    {
        //        sCmd.Parameters.Add("@HOLDER_ID", SqlDbType.NVarChar).Value = DBNull.Value;
        //        //sCmd.Parameters.Add("@SENDER_QUERY_ID", SqlDbType.NVarChar).Value = DBNull.Value ;
        //        sCmd.Parameters.Add("@QUERY_RESULT_CODE", SqlDbType.Int).Value = DBNull.Value;
        //        //sCmd.Parameters.Add("@SENDER_AUTHENTICATED", SqlDbType.Int).Value = DBNull.Value ;
        //        sCmd.Parameters.Add("@VALIDITY_DATE", SqlDbType.DateTime).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@ASSOCIATION_SHORT_NAME", SqlDbType.NVarChar).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@NUMBER_TERMINATIONS", SqlDbType.Int).Value = DBNull.Value;

        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = tchQLogRequestData.returnCode;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = tchQLogRequestData.lastStep;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.rowCreationTime;
        //    }
        //    else
        //    {
        //        sCmd.Parameters.Add("@HOLDER_ID", SqlDbType.NVarChar).Value = tchQLogRequestData.holderID;
        //        //sCmd.Parameters.Add("@SENDER_QUERY_ID", SqlDbType.NVarChar ).Value = tchQLogRequestData.senderQueryID ;
        //        sCmd.Parameters.Add("@QUERY_RESULT_CODE", SqlDbType.Int).Value = tchQLogRequestData.queryResultCode;
        //        //sCmd.Parameters.Add("@SENDER_AUTHENTICATED", SqlDbType.Int).Value = tchQLogRequestData.senderAuthenticated ;
        //        sCmd.Parameters.Add("@VALIDITY_DATE", SqlDbType.DateTime).Value = (tchQLogRequestData.validityDate == null ? DBNull.Value : tchQLogRequestData.validityDate);
        //        sCmd.Parameters.Add("@ASSOCIATION_SHORT_NAME", SqlDbType.NVarChar).Value = tchQLogRequestData.assocShortName;
        //        sCmd.Parameters.Add("@NUMBER_TERMINATIONS", SqlDbType.Int).Value = (tchQLogRequestData.numberOfTerminations == null ? DBNull.Value : tchQLogRequestData.numberOfTerminations);

        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
        //    }
        //    sCmd.Parameters.Add("@G2B_QUERY_ID", SqlDbType.Decimal).Value = tchQLogRequestData.G2B_QueryID;

        //    m_requestHelper.ExecuteNonQuery(sCmd);
        //}

        ///// <summary>
        ///// Updates G2B_REQUEST_LOG with Response XML encryption status
        ///// </summary>
        ///// <param name="tchQLogRequestData">Log data structure</param>
        ///// <param name="hashEncFailed">boolean indicating failure of response encryption</param>
        //public void LogRequestS40(G2BLogRequestStruct tchQLogRequestData, bool hashEncFailed)
        //{
        //    string sSql = "UPDATE [G2B_REQUEST_LOG] " +
        //        "SET RESPONSE_ENCRYPTION_RESULT = @RESPONSE_ENCRYPTION_RESULT, " +
        //        "RESPONSE_ENCRYPTION_RESULT_DESCRIPTION = @RESPONSE_ENCRYPTION_RESULT_DESCRIPTION, " +
        //        "SESSION_KEY_USED_DECRYPTED_OUT = @SESSION_KEY_USED_DECRYPTED_OUT, " +
        //        "[RETURN_CODE] = @RETURN_CODE, " +
        //        "[LAST_STEP] = @LAST_STEP, " +
        //        "[COMPLETION_TIME] = @COMPLETION_TIME " +
        //        "WHERE [G2B_QUERY_ID] = @G2B_QUERY_ID ";

        //    SqlCommand sCmd = new SqlCommand(sSql);
        //    sCmd.CommandTimeout = 500;

        //    sCmd.Parameters.Add("@RESPONSE_ENCRYPTION_RESULT", SqlDbType.Int).Value = tchQLogRequestData.responseEncryptionResult;

        //    if (hashEncFailed)
        //    {
        //        sCmd.Parameters.Add("@RESPONSE_ENCRYPTION_RESULT_DESCRIPTION", SqlDbType.NVarChar).Value = tchQLogRequestData.responseEncryptionResultDesc;
        //        sCmd.Parameters.Add("@SESSION_KEY_USED_DECRYPTED_OUT ", SqlDbType.Image).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = tchQLogRequestData.returnCode;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = tchQLogRequestData.lastStep;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.rowCreationTime;
        //    }
        //    else
        //    {
        //        sCmd.Parameters.Add("@RESPONSE_ENCRYPTION_RESULT_DESCRIPTION", SqlDbType.NVarChar).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@SESSION_KEY_USED_DECRYPTED_OUT", SqlDbType.Image).Value = tchQLogRequestData.decryptedSessionKeyOut;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
        //    }
        //    sCmd.Parameters.Add("@G2B_QUERY_ID", SqlDbType.Decimal).Value = tchQLogRequestData.G2B_QueryID;

        //    m_requestHelper.ExecuteNonQuery(sCmd);
        //}

        ///// <summary>
        ///// Updates G2B_Request_log with Session Key encryption status
        ///// </summary>
        ///// <param name="tchQLogRequestData">Log data structure</param>
        ///// <param name="sessionKeyEncFailed">boolean indicating failure encrypting session key</param>
        //public void LogRequestS45(G2BLogRequestStruct tchQLogRequestData, bool sessionKeyEncFailed)
        //{
        //    string sSql = "UPDATE [G2B_REQUEST_LOG] " +
        //        "SET SESSION_KEY_ENCRYPTION_KEY_ID_USED = @SESSION_KEY_ENCRYPTION_KEY_ID_USED, " +
        //        "SESSION_KEY_USED_ENCRYPTED_OUT = @SESSION_KEY_USED_ENCRYPTED_OUT, " +
        //        "[RETURN_CODE] = @RETURN_CODE, " +
        //        "[LAST_STEP] = @LAST_STEP, " +
        //        "[COMPLETION_TIME] = @COMPLETION_TIME " +
        //        "WHERE [G2B_QUERY_ID] = @G2B_QUERY_ID ";

        //    SqlCommand sCmd = new SqlCommand(sSql);
        //    sCmd.CommandTimeout = 500;

        //    if (sessionKeyEncFailed)
        //    {
        //        sCmd.Parameters.Add("@SESSION_KEY_ENCRYPTION_KEY_ID_USED", SqlDbType.NVarChar).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@SESSION_KEY_USED_ENCRYPTED_OUT ", SqlDbType.Image).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = tchQLogRequestData.returnCode;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = tchQLogRequestData.lastStep;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.rowCreationTime;
        //    }
        //    else
        //    {
        //        sCmd.Parameters.Add("@SESSION_KEY_ENCRYPTION_KEY_ID_USED", SqlDbType.NVarChar).Value = tchQLogRequestData.sessionKeyEncryptionKeyIDUsed;
        //        sCmd.Parameters.Add("@SESSION_KEY_USED_ENCRYPTED_OUT", SqlDbType.Image).Value = tchQLogRequestData.encryptedSessionKeyOut;
        //        sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = DBNull.Value;
        //        sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = DBNull.Value;
        //    }
        //    sCmd.Parameters.Add("@G2B_QUERY_ID", SqlDbType.Decimal).Value = tchQLogRequestData.G2B_QueryID;

        //    m_requestHelper.ExecuteNonQuery(sCmd);
        //}
        //*/

        ///// <summary>
        ///// Final step update to the G2B_REQUEST_LOG table
        ///// </summary>
        ///// <param name="tchQLogRequestData">Log data structure</param>
        //public void LogRequestS99(G2BLogRequestStruct tchQLogRequestData)
        //{
        //    string sSql = "UPDATE [G2B_REQUEST_LOG] " +
        //        "SET [RETURN_CODE] = @RETURN_CODE, " +
        //        "[LAST_STEP] = @LAST_STEP, " +
        //        //"[RESPONSE_TIME] = @RESPONSE_TIME, " +
        //        "[COMPLETION_TIME] = @COMPLETION_TIME " +
        //        "WHERE [G2B_QUERY_ID] = @G2B_QUERY_ID ";

        //    SqlCommand sCmd = new SqlCommand(sSql);
        //    sCmd.CommandTimeout = 500;

        //    sCmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Value = tchQLogRequestData.returnCode;
        //    sCmd.Parameters.Add("@LAST_STEP", SqlDbType.Int).Value = tchQLogRequestData.lastStep;
        //    //sCmd.Parameters.Add("@RESPONSE_TIME", SqlDbType.DateTime ).Value = tchQLogRequestData.rowCreationTime ;
        //    sCmd.Parameters.Add("@COMPLETION_TIME", SqlDbType.DateTime).Value = tchQLogRequestData.rowCreationTime;
        //    sCmd.Parameters.Add("@G2B_QUERY_ID", SqlDbType.Decimal).Value = tchQLogRequestData.G2B_QueryID;

        //    m_requestHelper.ExecuteNonQuery(sCmd);
        //}


        ///// <summary>
        ///// Method to update the g2b_Sequence table
        ///// </summary>
        ///// <param name="G2B_QueryID">ID generated  for the query</param>
        ///// <param name="step">Step number</param>
        ///// <param name="stepResult">Result of the step</param>
        ///// <param name="stepDesc"></param>
        ///// <param name="stepCompletionTime">Time stam</param>
        //public void LogSequenceStep(Double G2B_QueryID, Int64 step, Int64 stepResult, String stepDesc, DateTime stepCompletionTime)
        //{
        //    //			string sSql = "INSERT INTO [G2B_SEQUENCE] " + 
        //    //				"([G2B_QUERY_ID], [G2B_STEP], [G2B_STEP_RESULT],  [G2B_STEP_ERROR_DESC], [TIME_STAMP]) " +
        //    //				" VALUES " + 
        //    //				"( " +G2B_QueryID.ToString() +", " + 
        //    //				step.ToString() + ", " +
        //    //                stepResult.ToString() + ", " + stepDesc.Trim() + ", " +
        //    //                "'" + stepCompletionTime.ToString("yyyy-MM-dd HH:mm:ss") + "' ) ";

        //    string sSql = "INSERT INTO [G2B_SEQUENCE] " +
        //        "([G2B_QUERY_ID], [G2B_STEP], [G2B_STEP_RESULT], " +
        //        " [G2B_STEP_ERROR_DESC], [LAST_UPDATE_TIME]) " +
        //        " VALUES " +
        //        "(@G2B_QUERY_ID, @G2B_STEP, @G2B_STEP_RESULT, " +
        //        " @G2B_STEP_ERROR_DESC, @LAST_UPDATE_TIME)";

        //    SqlCommand sCmd = new SqlCommand(sSql);
        //    sCmd.CommandTimeout = 500;

        //    sCmd.Parameters.Add("@G2B_QUERY_ID", SqlDbType.Int).Value = G2B_QueryID;
        //    sCmd.Parameters.Add("@G2B_STEP", SqlDbType.Int).Value = step;
        //    sCmd.Parameters.Add("@G2B_STEP_RESULT", SqlDbType.Int).Value = stepResult;
        //    sCmd.Parameters.Add("@G2B_STEP_ERROR_DESC", SqlDbType.NVarChar).Value = stepDesc;
        //    sCmd.Parameters.Add("@LAST_UPDATE_TIME", SqlDbType.DateTime).Value = stepCompletionTime;
        //    m_requestHelper.ExecuteNonQuery(sCmd);
        //}
        /////// <summary>
        /////// 
        /////// </summary>
        /////// <param name="G2B_QueryID"></param>
        /////// <param name="dtResponseSent"></param>
        /////// <param name="ResponseResult"></param>
        ////public void LogRequestResponse(long G2B_QueryID, DateTime dtResponseSent, bool ResponseResult)
        ////{
        ////    string sSql = "UPDATE [G2B_REQUEST_LOG] " +
        ////        "SET [RESPONSE_RESULT] = @RESPONSE_RESULT, " +
        ////        "[RESPONSE_TIME] = @RESPONSE_TIME " +
        ////        "WHERE [G2B_QUERY_ID] = @G2B_QUERY_ID ";

        ////    SqlCommand sCmd = new SqlCommand(sSql);
        ////    sCmd.CommandTimeout = 500;

        ////    if (ResponseResult)
        ////    {
        ////        sCmd.Parameters.Add("@RESPONSE_RESULT", SqlDbType.Int).Value = 1;
        ////    }
        ////    else
        ////    {
        ////        sCmd.Parameters.Add("@RESPONSE_RESULT", SqlDbType.Int).Value = 2;
        ////    }

        ////    sCmd.Parameters.Add("@RESPONSE_TIME", SqlDbType.DateTime).Value = dtResponseSent;

        ////    sCmd.Parameters.Add("@G2B_QUERY_ID", SqlDbType.Decimal).Value = G2B_QueryID;

        ////    m_requestHelper.ExecuteNonQuery(sCmd);
        ////}

    }

    //	public class Subscriber_DBHelper
    //	{
    //		private IDBHelper m_subscriberHelper;
    //		public Subscriber_DBHelper(IDBHelper subscriberHelper)
    //		{
    //			m_subscriberHelper = subscriberHelper;
    //		}
    //		public int AuthenticateQuerySender(String SubscriberID,out String Password, 
    //			String ServiceID, Int32 MethodID, out String SessionKeyAlgo, out String HashAlgo, 
    //			out String CopyToUrl)
    //		{
    //			int iErrorCode = 0;
    //			StringBuilder strBuilder = new StringBuilder("SELECT ");
    //			
    //			strBuilder.Append("WS_SUBSCRIBER.SUBSCRIBER_ID, WS_SUBSCRIBER.SUBSCRIBER_PASSWORD, ") ;
    //			strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD.ACTIVE, WS_SUBSCRIBER_SERVICES.ACTIVE AS Expr1, ") ;
    //            strBuilder.Append("WS_SUBSCRIBER_SERVICES.SESSIONKEY_ENC_ALGO, WS_SUBSCRIBER_SERVICES.HASH_ALGO, ");
    //			strBuilder.Append("WS_SUBSCRIBER_SERVICES.COPY_TO_TARGET_URL ");
    //
    //			strBuilder.Append("FROM WS_SUBSCRIBER INNER JOIN ");
    //			strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD ON WS_SUBSCRIBER.SUBSCRIBER_ID = WS_SUBSCRIBER_SERVICE_METHOD.SUBSCRIBER_ID INNER JOIN ");
    //			strBuilder.Append("WS_SUBSCRIBER_SERVICES ON WS_SUBSCRIBER.SUBSCRIBER_ID = WS_SUBSCRIBER_SERVICES.SUBSCRIBER_ID AND ");
    //			strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD.SERVICE_ID = WS_SUBSCRIBER_SERVICES.SERVICE_ID ");
    //
    //			strBuilder.Append("WHERE (WS_SUBSCRIBER.SUBSCRIBER_ID = N'");
    //			strBuilder.Append(SubscriberID);
    //			strBuilder.Append("') ");
    //			strBuilder.Append(" AND (WS_SUBSCRIBER_SERVICE_METHOD.SERVICE_ID = N'");
    //			strBuilder.Append(ServiceID); 
    //			strBuilder.Append("') ");
    //			strBuilder.Append(" AND (WS_SUBSCRIBER_SERVICE_METHOD.METHOD_ID = ");
    //			strBuilder.Append(MethodID);
    //			strBuilder.Append(" ) ");
    ////			strBuilder.Append(" AND (WS_SUBSCRIBER_SERVICE_METHOD.ACTIVE = 1) ");
    ////			strBuilder.Append(" AND (WS_SUBSCRIBER_SERVICES.ACTIVE = 1) ");
    //
    //			string sSql = strBuilder.ToString();
    //			IDataReader rdr =  m_subscriberHelper.GetDataReader(sSql, CommandBehavior.SingleRow);  
    //			if(rdr.Read())
    //			{
    //				#region Get all the values related to Subscriber
    //				int iServiceMethodId = Convert.ToInt32(rdr.GetValue(2));
    //				if(iServiceMethodId == 1)
    //				{
    //					if(Convert.ToInt32(rdr.GetValue(3)) == 1) 
    //					{
    //						if(rdr.GetValue(1) == DBNull.Value)  
    //						{
    //							Password = null;
    //						}
    //						else
    //						{
    //							Password = rdr.GetValue(1).ToString();
    //						}
    //						SessionKeyAlgo = rdr.GetValue(4).ToString();
    //						HashAlgo = rdr.GetValue(5).ToString();
    //						CopyToUrl = rdr.GetValue(6).ToString();
    //					}
    //					else
    //					{
    //						iErrorCode = 1233; //Invalid Service of Service Not Active
    //						Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo, 
    //							"Service " + ServiceID  + " for Subcriber " + SubscriberID + " is Not Active ");
    //
    //						Password = "";
    //						SessionKeyAlgo = "";
    //						HashAlgo = "";
    //						CopyToUrl = "";
    //					}
    //
    //				}
    //				else //Invalid Method or Method Not Active
    //				{
    //					iErrorCode = 1212;
    //					Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo, 
    //						"Service Method " + MethodID.ToString() + " for Subcriber " + SubscriberID + " is Not Active ");
    //
    //					Password = "";
    //					SessionKeyAlgo = "";
    //					HashAlgo = "";
    //					CopyToUrl = "";
    //
    //				}
    //
    //
    //				#endregion
    //			}
    //			else
    //			{
    //				iErrorCode = 1212;
    //				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo, 
    //					" Subcriber " + SubscriberID + " is Not registered ");
    //
    //				Password = "";
    //				SessionKeyAlgo = "";
    //				HashAlgo = "";
    //				CopyToUrl = "";
    //
    //			}
    //
    //			return iErrorCode;
    //		}
    //
    //	}
}
