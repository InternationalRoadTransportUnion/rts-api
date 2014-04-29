using System;
using IRU.RTS;
using IRU.RTS.CommonComponents;
using System.Data;
using System.Data.SqlClient ;
using IRU.CommonInterfaces;
using IRU.RTS.CryptoInterfaces; 
using IRU.CryptEngine ;
using System.Xml;
using System.Collections;
using IRU.RTS.Crypto;


namespace IRU.RTS.WSTVQR
{
	/// <summary>
	/// Summary description for TCHQ_QueryProcessor.
	/// </summary>
	public class TVQR_QueryProcessor:MarshalByRefObject, ITVQRProcessor
	{
		private ICryptoOperations m_iCryptoOperations;

		private string m_sTVQR_Query_Id;

//		private SqlCommand m_cmdTCHQSeqTbl;
//		private string m_subscriberPassword;

		//private string m_sEncryptKeyTblName;

		private static string GetBlankIfNull(object Obj)
		{
			if(Obj == null)
			{
				return "";
			}
			else
			{
				return Obj.ToString();
			}
		}

		private static string GetBlankDateIfNull(object DateTimeobj)
		{
			if(DateTimeobj == null)
			{
				return "";
			}
			else
			{
				//return XmlConvert.ToString((DateTime)DateTimeobj); 
				return ((DateTime)DateTimeobj).ToString("yyyy-MM-dd"); 
			}
		}

		public TVQR_QueryProcessor()
		{
			m_sTVQR_Query_Id = "TVQR_Query_ID";
		}

//////		public void SetEncryptKeyTableName(string EncryptKeyTableName)
//////		{
//////			m_sEncryptKeyTblName = EncryptKeyTableName;
//////		}


		/// <summary>
		/// Updates the RESPONSE_RESULT and time columns. The WEb Service Layer will call back this method after calling 
		/// processquery and checking if the client is still connected
		/// </summary>
		/// <param name="QueryId"> Query ID , key to row in log table </param>
		/// <param name="dtResponseSent">Time in the WS layer</param>
		/// <param name="ResponseResult">true=client was still connected, false= client was not connected </param>
		[System.Runtime.Remoting.Messaging.OneWay] 
		public void UpdateResponseResult(long QueryId, DateTime dtResponseSent, bool ResponseResult)
		{
			#region Get IDBHelper instances from Plugin Manager
			IDBHelper dbHelperTVQR = TVQR_RemotingHelper.m_dbHelperFactoryPlugin.GetDBHelper("TVQRDB") ;//  null; //dbhelper for tvqrdb
			TVQR_DBHelper tvqrDbHelper = new TVQR_DBHelper(dbHelperTVQR); 
			#endregion

            throw new NotImplementedException();

			try
			{
                dbHelperTVQR.ConnectToDB();
                tvqrDbHelper.LogRequestResponse(QueryId, dtResponseSent, ResponseResult);
			}
			catch(Exception ex)
			{
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, ex.Message + " - " + ex.StackTrace );  
			}
			finally
			{
                dbHelperTVQR.Close();
			}
			

		}

        public VoucherQueryResponseType ProcessQuery(VoucherQueryType VoucherQuery, string SenderIP, out long IRUQueryId)
        {
            throw new NotImplementedException();
        }

        public VoucherRegistrationResponseType ProcessQuery(VoucherRegistrationType VoucherRegistration, string SenderIP, out long IRUQueryId)
        {
            throw new NotImplementedException();
        }
    }
}