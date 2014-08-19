using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols; 
using IRU.RTS;
//using TraceHelper=TC;

namespace IRU.RTS.WSEGIS
{
	/// <summary>
	/// This webservice will act as the Entry point for the TIR Carnet Query Processing.
	/// This webservice will Delegate the Work to the Internal Query processor Subsystem
	/// and when the Result is got back is an encrpyted output.
	/// </summary>
	/// 
	
	[WebService(Namespace="http://rts.iru.org/EGIS")]
	public class EGISClass : System.Web.Services.WebService
	{
		/// <summary>
		/// The Constructor of the class
		/// </summary>
		public EGISClass()
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
		/// This Method is exposed to the Web Client through the Web Service For Electronic Guarantee Information Service (EGIS).
		/// </summary>
		/// EGIS Query parameters
        /// <param name="EGISQuery" type = "EGISQueryType"></param>
		/// EGIS Query Result
        /// <returns name="EGISQueryResponse" type = "EGISQueryResponseType"></returns>	 
		[WebMethod]
        [SoapDocumentMethod(Action = "http://rts.iru.org/EGIS/EGISQuery", ResponseElementName = "EGISResponse")]
        [return: System.Xml.Serialization.XmlElementAttribute("EGISResult")]
        public EGISResponseType EGISQuery(EGISQueryType su)
		{
            EGISResponseType oResponse;
			try
			{
                IEGISProcessor oEGISProcessor = (IEGISProcessor)Activator.GetObject(typeof(IRU.RTS.IEGISProcessor), System.Configuration.ConfigurationSettings.AppSettings["WSEGIS_ProcessorEndPoint"]);

				string senderIP = HttpContext.Current.Request.UserHostAddress.ToString();
				long IRUQueryID;
                oResponse = oEGISProcessor.ProcessQuery(su, senderIP, out IRUQueryID);
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceVerbose ,"Process Query Call Succeded");
				
				bool isClientConnected = HttpContext.Current.Response.IsClientConnected;

                oEGISProcessor.UpdateResponseResult(IRUQueryID, DateTime.Now, isClientConnected);  

				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceVerbose ,"Response Updation Call Succeded");
				

			}
			catch (Exception ex)
			{
				//TODO: Replace this in Production to Trace
				string sMessage = ex.Message + " \r\n "+ ex.StackTrace ;
                oResponse = new EGISResponseType();
                oResponse.ReturnCode = 1200;
                oResponse.Query_ID = su.Query_ID;

				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError,sMessage);
				

			}
            return oResponse; //cant return null 			
		}
	}
}
