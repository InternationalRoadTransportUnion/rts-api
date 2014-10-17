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

namespace WSRESPACE
{
	/// <summary>
	/// Summary description for SafeTirUpload.
	/// </summary>
	[WebService(Namespace="http://www.iru.org")]
	public class WSREService : System.Web.Services.WebService
	{
        public WSREService()
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

		// WEB SERVICE EXAMPLE
		// The HelloWorld() example service returns the string Hello World
		// To build, uncomment the following lines then save and build the project
		// To test this web service, press F5

		[WebMethod]

        public SafeTIRUploadAck WSRE(SafeTIRReconParams su)
		{
            SafeTIRUploadAck reconcileReplyAck;
			try
			{
				using (NetTcpClient<IWSREFileReceiver> client = new NetTcpClient<IWSREFileReceiver>(System.Configuration.ConfigurationSettings.AppSettings["WSREFileReceiverEndPoint"]))
				{
					IWSREFileReceiver iFileReceiver = client.GetProxy();

					string senderIP = HttpContext.Current.Request.GetCallerIP();

					reconcileReplyAck = iFileReceiver.ProcessReceivedFile(su, senderIP);
				}
			}
			catch (Exception ex)
			{
				//TODO: Replace this in Production to Trace
				string sMessage = ex.Message + " \n - " + ex.StackTrace + " \n- " + ex.Source ;
                reconcileReplyAck = new SafeTIRUploadAck();
                reconcileReplyAck.ReturnCode = 1200;
               // reconcileReplyAck.Sender_MessageID = su.Sender_MessageID;
                reconcileReplyAck.ResponseTime = DateTime.Now;
                //reconcileReplyAck.Version = "1.0";

				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError,sMessage);

			}
            return reconcileReplyAck;
		}
	}
}
