using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using IRU.RTS; 
//using TraceHelper=TC;

namespace IRU.RTS.WSTCHQService
{
	/// <summary>
	/// This webservice will act as the Entry point for the TIR Carnet Query Processing.
	/// This webservice will Delegate the Work to the Internal Query processor Subsystem
	/// and when the Result is got back is an encrpyted output.
	/// </summary>
	/// 
	
	[WebService(Namespace="http://www.iru.org")]
	public class SafeTIRHolderQueryServiceClass : System.Web.Services.WebService
	{
		/// <summary>
		/// The Constructor of the class
		/// </summary>
		public SafeTIRHolderQueryServiceClass()
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
		/// This Method is exposed to the Web Client through the Web Service For TIR Carnet Query Processing.
		/// </summary>
		/// TIR Carnet Query parameters
		/// <param name="stTCHQ_Param" type = "TIRHolderQuery"></param>
		/// TIR Carnet Query Result
		/// <returns name="TCHQResponseObj" type = "TIRHolderResponse"></returns>	 

		[WebMethod]
		public TIRHolderResponse  WSTCHQ(TIRHolderQuery su)
		{
			//IRU.RTS.WSTCHQ.TCHQ_QueryProcessor
			TIRHolderResponse tResponse;
			try
			{
				ITCHQProcessor iQueryProcessor =  (ITCHQProcessor )Activator.GetObject(typeof(IRU.RTS.ITCHQProcessor), System.Configuration.ConfigurationSettings.AppSettings["WSTCHQ_ProcessorEndPoint"]);

				string senderIP = HttpContext.Current.Request.UserHostAddress.ToString();
				long IRUQueryID;
				tResponse= iQueryProcessor.ProcessQuery(su, senderIP, out IRUQueryID);
				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceVerbose ,"Process Query Call Succeded");
				
				bool isClientConnected = HttpContext.Current.Response.IsClientConnected;
				
				iQueryProcessor.UpdateResponseResult(IRUQueryID, DateTime.Now,isClientConnected);  

				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceVerbose ,"Response Updation Call Succeded");
				

			}
			catch (Exception ex)
			{
				//TODO: Replace this in Production to Trace
				string sMessage = ex.Message + " \r\n "+ ex.StackTrace ;
				tResponse = new TIRHolderResponse();
				tResponse.ReturnCode=1200;
				tResponse.Query_ID=su.Query_ID;

				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError,sMessage);
				

			}
			return tResponse; //cant return null 
			
		}
	}
}
