using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using IRU.RTS;
using IRU.RTS.Common.WCF;

namespace IRU.RTS.WSTVQRService
{
	/// <summary>
	/// This webservice will act as the Entry point for the TIR Carnet Query Processing.
	/// This webservice will Delegate the Work to the Internal Query processor Subsystem
	/// and when the Result is got back is an encrpyted output.
	/// </summary>
	/// 
	
	[WebService(Namespace="http://rts.iru.org/TVQR")]
	public class TVQRServiceClass : System.Web.Services.WebService
	{
		/// <summary>
		/// The Constructor of the class
		/// </summary>
		public TVQRServiceClass()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
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

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

	
		/// <summary>
		/// This Method is exposed to the Web Client through the Web Service For Voucher Query And Registration.
		/// </summary>
		/// Voucher Query parameters
        /// <param name="voucherQuery" type = "VoucherQueryType"></param>
		/// Voucher Query Result
        /// <returns name="voucherQueryResponse" type = "VoucherQueryResponseType"></returns>	 
		[WebMethod]
        [SoapDocumentMethod(Action = "http://rts.iru.org/TVQR/Query", ResponseElementName = "VoucherQueryResponse")]
        [return: System.Xml.Serialization.XmlElementAttribute("QueryResult")]
        public VoucherQueryResponseType VoucherQuery(VoucherQueryType su)
		{
            VoucherQueryResponseType oResponse;
			try
			{
				using (NetTcpClient<ITVQRProcessor> client = new NetTcpClient<ITVQRProcessor>(System.Configuration.ConfigurationSettings.AppSettings["WSTVQR_ProcessorEndPoint"]))
				{
					ITVQRProcessor oTVQRProcessor = client.GetProxy();

					string senderIP = HttpContext.Current.Request.UserHostAddress.ToString();
					long IRUQueryID;
					oResponse = oTVQRProcessor.ProcessVoucherQuery(su, senderIP, out IRUQueryID);
					TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceVerbose, "Process Query Call Succeded");

					bool isClientConnected = HttpContext.Current.Response.IsClientConnected;

					oTVQRProcessor.UpdateResponseResult(IRUQueryID, DateTime.Now, isClientConnected);

					TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceVerbose, "Response Updation Call Succeded");
				}
			}
			catch (Exception ex)
			{
				//TODO: Replace this in Production to Trace
				string sMessage = ex.Message + " \r\n "+ ex.StackTrace ;
                oResponse = new VoucherQueryResponseType();
                oResponse.ReturnCode = 1200;
                oResponse.Query_ID = su.Query_ID;

				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError,sMessage);
				

			}
            return oResponse; //cant return null 			
		}

        /// <summary>
        /// This Method is exposed to the Web Client through the Web Service For Voucher Query And Registration.
        /// </summary>
        /// Voucher Registration parameters
        /// <param name="voucherRegistration" type = "VoucherRegistrationType"></param>
        /// Voucher Registration Result
        /// <returns name="voucherRegistrationResponse" type = "VoucherRegistrationResponseType"></returns>	 
        [WebMethod]        
        [SoapDocumentMethod(Action = "http://rts.iru.org/TVQR/Registration", ResponseElementName = "VoucherRegistrationResponse")]
        [return: System.Xml.Serialization.XmlElementAttribute("RegistrationResult")]
        public VoucherRegistrationResponseType VoucherRegistration(VoucherRegistrationType su)
        {
            VoucherRegistrationResponseType oResponse;
            try
            {
				using (NetTcpClient<ITVQRProcessor> client = new NetTcpClient<ITVQRProcessor>(System.Configuration.ConfigurationSettings.AppSettings["WSTVQR_ProcessorEndPoint"]))
				{
					ITVQRProcessor oTVQRProcessor = client.GetProxy();

					string senderIP = HttpContext.Current.Request.UserHostAddress.ToString();
					long IRUQueryID;
					oResponse = oTVQRProcessor.ProcessVoucherRegistration(su, senderIP, out IRUQueryID);
					TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceVerbose, "Process Query Call Succeded");

					bool isClientConnected = HttpContext.Current.Response.IsClientConnected;

					oTVQRProcessor.UpdateResponseResult(IRUQueryID, DateTime.Now, isClientConnected);

					TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceVerbose, "Response Updation Call Succeded");
				}
            }
            catch (Exception ex)
            {
                //TODO: Replace this in Production to Trace
                string sMessage = ex.Message + " \r\n " + ex.StackTrace;
                oResponse = new VoucherRegistrationResponseType();
                oResponse.ReturnCode = 1200;
                oResponse.Sender_MessageID = su.Sender_MessageID;

                TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, sMessage);


            }
            return oResponse; //cant return null 			
        }
	}
}
