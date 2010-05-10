using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using IRU.RTS;

namespace WSSTstc
{
	/// <summary>
	/// Summary description for SafeTirUpload.
	/// </summary>
	[WebService(Namespace="http://www.iru.org")]
	public class SafeTIRUploadCopyService : System.Web.Services.WebService
	{
		public SafeTIRUploadCopyService()
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
		public SafeTIRUploadCopyAck  WSSTstc(SafeTIRUploadCopy su)
		{
			SafeTIRUploadCopyAck safeUploadAck ;
			try
			{
				//IWSSTFileReceiver iFileReceiver =  (IWSSTFileReceiver)Activator.GetObject(typeof(IRU.RTS.IWSSTFileReceiver), System.Configuration.ConfigurationSettings.AppSettings["WSSTFileReceiverEndPoint"]);
				
				string senderIP = HttpContext.Current.Request.UserHostAddress.ToString();

				safeUploadAck = ProcessReceivedFile(su , senderIP);
			}
			catch (Exception ex)
			{
				//TODO: Replace this in Production to Trace
				string sMessage = ex.Message;
				safeUploadAck = new SafeTIRUploadCopyAck();
				safeUploadAck.ReturnCode=1200;
				

			}
			return safeUploadAck;
		}

		private SafeTIRUploadCopyAck ProcessReceivedFile(SafeTIRUploadCopy scopy, string senderIP)
		{
		
			SafeTIRUploadCopyAck sack =  new SafeTIRUploadCopyAck();
			sack.ReturnCode = 2;
			return sack;

		}
	}
}
