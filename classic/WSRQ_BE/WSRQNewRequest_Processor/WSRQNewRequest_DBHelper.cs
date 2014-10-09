using System;
using System.Collections.Generic;
using System.Text;
using IRU.RTS;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;
using System.Data;
using System.Data.SqlClient;
using IRU.RTS.WSRQ;

namespace IRU.RTS.WSRQ
{
     /// <summary>
     /// LATA Created on September 18 ,2007 for reconciliation request.
     /// Wraps all calls to WSRQDB.
     /// </summary>
    public class WSRQNewRequest_DBHelper
    {
            private IDBHelper m_requestHelper;
            public WSRQNewRequest_DBHelper(IDBHelper requestHelper)
            {
                m_requestHelper = requestHelper;
            }

            public void InsertRequest(WSRQDetailsStruct wsrqdetailsstruct1)
            {
                Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError, "NewRequestDBHelper");
                try
                {
                    string sSql = "INSERT INTO [WSRQ_Detail] " +
                        "([ExchangeID], [RequestID], [RequestDate], [RequestReminderNum], [RequestDataSource], [TNO], [ICC], [DCL], [CNL], [COF], [DDI], [RND], [PFD], [TCO], [CWR], [VPN], [COM], [RBC], [PIC], [RequestRemark])" +
                         " VALUES " +
                        "(@EXCHANGEID, @RequestID,@RequestDate,@RequestReminderNum,@RequestDataSource,@TNO,@ICC,@DCL,@CNL,@COF,@DDI,@RND,@PFD,@TCO,@CWR,@VPN,@COM,@RBC,@PIC,@RequestRemark)";

                    DateTime nullDate ;                DateTime.TryParse("", out nullDate);
                    SqlCommand sCmd = new SqlCommand(sSql);
                    sCmd.CommandTimeout = 500;
                    sCmd.Parameters.Add("@EXCHANGEID", SqlDbType.Decimal).Value = 0;
                    sCmd.Parameters.Add("@RequestID", SqlDbType.NVarChar).Value = wsrqdetailsstruct1.RequestID;
                    sCmd.Parameters.Add("@RequestDate", SqlDbType.DateTime).Value = wsrqdetailsstruct1.RequestDate;
                    sCmd.Parameters.Add("@RequestReminderNum", SqlDbType.Int).Value = wsrqdetailsstruct1.RequestReminderNum;
                    sCmd.Parameters.Add("@RequestDataSource", SqlDbType.Int).Value = wsrqdetailsstruct1.RequestDataSource;
                    
                    sCmd.Parameters.Add("@TNO", SqlDbType.NVarChar).Value = wsrqdetailsstruct1.TNO;
                    sCmd.Parameters.Add("@ICC", SqlDbType.NVarChar).Value = wsrqdetailsstruct1.ICC;
                    if (wsrqdetailsstruct1.DCL != nullDate && wsrqdetailsstruct1.DCL.ToString() != null && wsrqdetailsstruct1.DCL.ToString().Trim() != "")
                    {
                        sCmd.Parameters.Add("@DCL", SqlDbType.DateTime).Value = wsrqdetailsstruct1.DCL;
                    }
                    else
                    {
                        sCmd.Parameters.Add("@DCL", SqlDbType.DateTime).Value = DBNull.Value;
                    }

                    if (wsrqdetailsstruct1.CNL == null || wsrqdetailsstruct1.CNL == "")
                    {
                        sCmd.Parameters.Add("@CNL", SqlDbType.NVarChar).Value = DBNull.Value;
                    }
                    else
                    {
                        sCmd.Parameters.Add("@CNL", SqlDbType.NVarChar).Value = wsrqdetailsstruct1.CNL;
                    }
                    sCmd.Parameters.Add("@COF", SqlDbType.NVarChar).Value = wsrqdetailsstruct1.COF;


                    if (wsrqdetailsstruct1.DDI != nullDate && wsrqdetailsstruct1.DDI.ToString() != null && wsrqdetailsstruct1.DDI.ToString() != "")
                    {
                        sCmd.Parameters.Add("@DDI", SqlDbType.DateTime).Value = wsrqdetailsstruct1.DDI;
                    }
                    else
                    { 
                        sCmd.Parameters.Add("@DDI", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    

                    if (wsrqdetailsstruct1.RND == null || wsrqdetailsstruct1.RND == "")
                    {
                        sCmd.Parameters.Add("@RND", SqlDbType.NVarChar).Value = DBNull.Value;
                    }
                    else
                    {
                        sCmd.Parameters.Add("@RND", SqlDbType.NVarChar).Value = wsrqdetailsstruct1.RND;
                    }
                    

                    if (wsrqdetailsstruct1.PFD == null || wsrqdetailsstruct1.PFD == "")
                    {
                        sCmd.Parameters.Add("@PFD", SqlDbType.NVarChar).Value = DBNull.Value;
                    }
                    else
                    {
                        sCmd.Parameters.Add("@PFD", SqlDbType.NVarChar).Value = wsrqdetailsstruct1.PFD;
                    }
                    if (wsrqdetailsstruct1.TCO == null || wsrqdetailsstruct1.TCO == "")
                    {
                        sCmd.Parameters.Add("@TCO", SqlDbType.NVarChar).Value = DBNull.Value;
                    }
                    else
                    {
                        sCmd.Parameters.Add("@TCO", SqlDbType.NVarChar).Value = wsrqdetailsstruct1.TCO;
                    }
                    if (wsrqdetailsstruct1.CWR == null || wsrqdetailsstruct1.CWR == "")
                    {
                        sCmd.Parameters.Add("@CWR", SqlDbType.NVarChar).Value = DBNull.Value;
                    }
                    else
                    {
                        sCmd.Parameters.Add("@CWR", SqlDbType.NVarChar).Value = wsrqdetailsstruct1.CWR;
                    }


                    if (wsrqdetailsstruct1.VPN.ToString() == null ||  wsrqdetailsstruct1.VPN.ToString() == "")
                    {
                        sCmd.Parameters.Add("@VPN", SqlDbType.Int).Value = DBNull.Value;
                    }
                    else
                    {
                        sCmd.Parameters.Add("@VPN", SqlDbType.Int).Value = wsrqdetailsstruct1.VPN;
                    }

                    if (wsrqdetailsstruct1.COM == null || wsrqdetailsstruct1.COM == "")
                    {
                        sCmd.Parameters.Add("@COM", SqlDbType.NVarChar).Value = DBNull.Value;
                    }
                    else
                    {
                        sCmd.Parameters.Add("@COM", SqlDbType.NVarChar).Value = wsrqdetailsstruct1.COM;
                    }
                    if (wsrqdetailsstruct1.RBC == null || wsrqdetailsstruct1.RBC == "")
                    {
                        sCmd.Parameters.Add("@RBC", SqlDbType.NVarChar).Value = DBNull.Value;
                    }
                    else
                    {
                        sCmd.Parameters.Add("@RBC", SqlDbType.NVarChar).Value = wsrqdetailsstruct1.RBC;
                    }
                    if (wsrqdetailsstruct1.PIC.ToString() == null || wsrqdetailsstruct1.PIC.ToString() == "")
                    {
                        sCmd.Parameters.Add("@PIC", SqlDbType.Int).Value = DBNull.Value;
                    }
                    else
                    {
                        sCmd.Parameters.Add("@PIC", SqlDbType.Int).Value = wsrqdetailsstruct1.PIC;
                    }


                    if (wsrqdetailsstruct1.RequestRemark == null || wsrqdetailsstruct1.RequestRemark == "")
                    {
                        sCmd.Parameters.Add("@RequestRemark", SqlDbType.NVarChar).Value = DBNull.Value;
                    }
                    else
                    {
                        sCmd.Parameters.Add("@RequestRemark", SqlDbType.NVarChar).Value = wsrqdetailsstruct1.RequestRemark;
                    }
                    
                    m_requestHelper.ExecuteNonQuery(sCmd);
                }
                catch (SqlException Sqlex)
                {
                    Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError,"DDI =" + wsrqdetailsstruct1.DDI + " " +  "DCL =" + wsrqdetailsstruct1.DCL  + Sqlex.Message + " - " + Sqlex.StackTrace);
                    throw Sqlex;
                }
                
            }
    }

    




        
        



            

            
}
