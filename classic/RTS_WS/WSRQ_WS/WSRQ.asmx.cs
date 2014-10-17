using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using IRU.RTS;
using IRU.RTS.Common.WCF;
using IRU.RTS.Common.Extension; 

namespace IRU.RTS.WSRQService
{
	/// <summary>
	/// This webservice will act as the Entry point for the Reconciliation Query Processing.
	/// This webservice will Delegate the Work to the Internal Query processor Subsystem
	/// and when the Result is got back is an encrpyted output.
	/// </summary>
	/// 
	
    [WebService(Namespace="http://www.iru.org")]
    public class ReconciliationQueryServiceClass : System.Web.Services.WebService
    {
        public ReconciliationQueryServiceClass()
        {

            //Uncomment the following line if using designed components 
            InitializeComponent(); 
        }
        #region Component Designer generated code

        //Required by the Web Services Designer 
        private IContainer components = null;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
    #endregion
        [WebMethod]
        public ReconciliationResponse WSRQ(ReconciliationQuery su)
        {
            ReconciliationResponse wResponse;
            try
            {
				using (NetTcpClient<IWSRQProcessor> client = new NetTcpClient<IWSRQProcessor>(System.Configuration.ConfigurationSettings.AppSettings["WSRQ_ProcessorEndPoint"]))
				{
					IWSRQProcessor iWSRQQueryProcessor = client.GetProxy();

					string senderIP = HttpContext.Current.Request.GetCallerIP();
					long QueryID;
					wResponse = iWSRQQueryProcessor.ProcessQuery(su, senderIP, out QueryID);
					TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceVerbose, "Process Query Call Succeded");

					bool isClientConnected = HttpContext.Current.Response.IsClientConnected;

					iWSRQQueryProcessor.UpdateResponseResult(QueryID, DateTime.Now, isClientConnected);

					TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceVerbose, "Response Updation Call Succeded");
				}
            }
            catch (Exception ex)
            {
                string sMessage = ex.Message + " \r\n " + ex.StackTrace;
                wResponse = new ReconciliationResponse();
                wResponse.ReturnCode = 1200;
                //wResponse.Query_ID = su.Query_ID;

                TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, sMessage);
            }
            return wResponse;
        }
    }
    
}
