using System;
using IRU.RTS;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;
using IRU.RTS.CryptoInterfaces; 

using System.Data;
using System.Data.SqlClient ;
using System.Text ;

namespace IRU.RTS.CommonComponents
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Subscriber_DBHelper
	{
		private IDBHelper m_subscriberHelper;
		#region Constructor Subscriber_DBHelper
		public Subscriber_DBHelper(IDBHelper subscriberHelper)
		{
			m_subscriberHelper = subscriberHelper;
		}
		#endregion

		#region AuthenticateQuerySender

		public int AuthenticateQuerySender(String SubscriberID,out String Password, 
			String ServiceID, Int32 MethodID, out String SessionKeyAlgo, out String HashAlgo, 
			out String CopyToID, out string CopyToAddress)
		{
			int iErrorCode = 0;
			StringBuilder strBuilder = new StringBuilder("SELECT ");
			
			strBuilder.Append("WS_SUBSCRIBER.SUBSCRIBER_ID, WS_SUBSCRIBER.SUBSCRIBER_PASSWORD, ") ;
			strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD.ACTIVE, WS_SUBSCRIBER_SERVICES.ACTIVE AS Expr1, ") ;
			strBuilder.Append("WS_SUBSCRIBER_SERVICES.SESSIONKEY_ENC_ALGO, WS_SUBSCRIBER_SERVICES.HASH_ALGO, ");
			strBuilder.Append("WS_SUBSCRIBER_SERVICES.COPY_TO_ID, COPY_TO_URLS.COPY_TO_ADDRESS ");

			/*strBuilder.Append("FROM WS_SUBSCRIBER INNER JOIN ");
			strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD ON WS_SUBSCRIBER.SUBSCRIBER_ID = WS_SUBSCRIBER_SERVICE_METHOD.SUBSCRIBER_ID INNER JOIN ");
			strBuilder.Append("WS_SUBSCRIBER_SERVICES ON WS_SUBSCRIBER.SUBSCRIBER_ID = WS_SUBSCRIBER_SERVICES.SUBSCRIBER_ID AND ");
			strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD.SERVICE_ID = WS_SUBSCRIBER_SERVICES.SERVICE_ID ");
			*/
			strBuilder.Append("FROM WS_SUBSCRIBER_SERVICES INNER JOIN ");
			strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD ON WS_SUBSCRIBER_SERVICES.SERVICE_ID = WS_SUBSCRIBER_SERVICE_METHOD.SERVICE_ID AND  ");
			strBuilder.Append("WS_SUBSCRIBER_SERVICES.SUBSCRIBER_ID = WS_SUBSCRIBER_SERVICE_METHOD.SUBSCRIBER_ID INNER JOIN ");
			strBuilder.Append("WS_SUBSCRIBER ON WS_SUBSCRIBER_SERVICES.SUBSCRIBER_ID = WS_SUBSCRIBER.SUBSCRIBER_ID LEFT OUTER JOIN ");
			strBuilder.Append("COPY_TO_URLS ON WS_SUBSCRIBER.SUBSCRIBER_ID = COPY_TO_URLS.COPY_TO_ID ");

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
						if(rdr.GetValue(6) == DBNull.Value)
						{
							CopyToID = "";
						}
						else
						{
							CopyToID = rdr.GetValue(6).ToString();
						}
						if(rdr.GetValue(7) == DBNull.Value)
						{
							CopyToAddress = "";
						}
						else
						{
							CopyToAddress = rdr.GetValue(7).ToString();
						}
					}
					else
					{
						iErrorCode = 1233; //Invalid Service of Service Not Active
						Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo, 
							"Service " + ServiceID  + " for Subcriber " + SubscriberID + " is Not Active ");

						Password = "";
						SessionKeyAlgo = "";
						HashAlgo = "";
						CopyToID = "";
						CopyToAddress = "";
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
					CopyToID = "";
					CopyToAddress = "";

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
				CopyToID = "";
				CopyToAddress ="";

			}

			return iErrorCode;
		}

        public int AuthenticateQuerySender(String SubscriberID, out String Password,
            String ServiceID, Int32 MethodID, out String SessionKeyAlgo, out String HashAlgo,
            out String CopyToID, out string CopyToAddress, out string sICC)
        {
            int iErrorCode = 0;
            StringBuilder strBuilder = new StringBuilder("SELECT ");

            strBuilder.Append("WS_SUBSCRIBER.SUBSCRIBER_ID, WS_SUBSCRIBER.SUBSCRIBER_PASSWORD, ");
            strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD.ACTIVE, WS_SUBSCRIBER_SERVICES.ACTIVE AS Expr1, ");
            strBuilder.Append("WS_SUBSCRIBER_SERVICES.SESSIONKEY_ENC_ALGO, WS_SUBSCRIBER_SERVICES.HASH_ALGO, ");
            strBuilder.Append("WS_SUBSCRIBER_SERVICES.COPY_TO_ID, COPY_TO_URLS.COPY_TO_ADDRESS, WS_SUBSCRIBER.SUBSCRIBER_ICC ");

            /*strBuilder.Append("FROM WS_SUBSCRIBER INNER JOIN ");
            strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD ON WS_SUBSCRIBER.SUBSCRIBER_ID = WS_SUBSCRIBER_SERVICE_METHOD.SUBSCRIBER_ID INNER JOIN ");
            strBuilder.Append("WS_SUBSCRIBER_SERVICES ON WS_SUBSCRIBER.SUBSCRIBER_ID = WS_SUBSCRIBER_SERVICES.SUBSCRIBER_ID AND ");
            strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD.SERVICE_ID = WS_SUBSCRIBER_SERVICES.SERVICE_ID ");
            */
            strBuilder.Append("FROM WS_SUBSCRIBER_SERVICES INNER JOIN ");
            strBuilder.Append("WS_SUBSCRIBER_SERVICE_METHOD ON WS_SUBSCRIBER_SERVICES.SERVICE_ID = WS_SUBSCRIBER_SERVICE_METHOD.SERVICE_ID AND  ");
            strBuilder.Append("WS_SUBSCRIBER_SERVICES.SUBSCRIBER_ID = WS_SUBSCRIBER_SERVICE_METHOD.SUBSCRIBER_ID INNER JOIN ");
            strBuilder.Append("WS_SUBSCRIBER ON WS_SUBSCRIBER_SERVICES.SUBSCRIBER_ID = WS_SUBSCRIBER.SUBSCRIBER_ID LEFT OUTER JOIN ");
            strBuilder.Append("COPY_TO_URLS ON WS_SUBSCRIBER.SUBSCRIBER_ID = COPY_TO_URLS.COPY_TO_ID ");

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
            IDataReader rdr = m_subscriberHelper.GetDataReader(sSql, CommandBehavior.SingleRow);
            if (rdr.Read())
            {
                #region Get all the values related to Subscriber
                int iServiceMethodId = Convert.ToInt32(rdr.GetValue(2));
                if (iServiceMethodId == 1)
                {
                    if (Convert.ToInt32(rdr.GetValue(3)) == 1)
                    {
                        if (rdr.GetValue(1) == DBNull.Value)
                        {
                            Password = null;
                        }
                        else
                        {
                            Password = rdr.GetValue(1).ToString();
                        }
                        SessionKeyAlgo = rdr.GetValue(4).ToString();
                        HashAlgo = rdr.GetValue(5).ToString();
                        if (rdr.GetValue(6) == DBNull.Value)
                        {
                            CopyToID = "";
                        }
                        else
                        {
                            CopyToID = rdr.GetValue(6).ToString();
                        }
                        if (rdr.GetValue(7) == DBNull.Value)
                        {
                            CopyToAddress = "";
                        }
                        else
                        {
                            CopyToAddress = rdr.GetValue(7).ToString();
                        }
                        if (rdr.GetValue(8) == DBNull.Value)
                        {
                            sICC  = "";
                        }
                        else
                        {
                            sICC = rdr.GetValue(8).ToString();
                        }
                    }
                    else
                    {
                        iErrorCode = 1233; //Invalid Service of Service Not Active
                        Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo,
                            "Service " + ServiceID + " for Subcriber " + SubscriberID + " is Not Active ");

                        Password = "";
                        SessionKeyAlgo = "";
                        HashAlgo = "";
                        CopyToID = "";
                        CopyToAddress = "";
                        sICC = "";
                    }

                }
                else //Invalid Method or Method Not Active
                {
                    iErrorCode = 1212;
                    Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo,
                        "Service Method " + MethodID.ToString() + " for Subcriber " + SubscriberID + " is Not Active ");

                    Password = "";
                    SessionKeyAlgo = "";
                    HashAlgo = "";
                    CopyToID = "";
                    CopyToAddress = "";
                    sICC = "";
                }


                #endregion
            }
            else
            {
                iErrorCode = 1212;
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo,
                    " Subcriber " + SubscriberID + " is Not registered ");

                Password = "";
                SessionKeyAlgo = "";
                HashAlgo = "";
                CopyToID = "";
                CopyToAddress = "";
                sICC = "";

            }

            return iErrorCode;
        }

		#endregion

	}
}
