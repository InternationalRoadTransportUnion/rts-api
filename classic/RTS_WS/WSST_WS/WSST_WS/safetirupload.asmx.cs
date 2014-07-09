using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Net;
using System.IO;
using IRU.RTS;

namespace SAFETIRUpload
{
	/// <summary>
	/// Summary description for SafeTirUpload.
	/// </summary>
	[WebService(Namespace="http://www.iru.org")]
	public class SafeTirUpload : System.Web.Services.WebService
	{
		public SafeTirUpload()
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


        private void SniffRequest(string webmethod)
        {
            if (HttpContext.Current != null)
            {
                string sFileName = "";
                try
                {
                    string sLogPath = System.Configuration.ConfigurationSettings.AppSettings["WSSTLogPath"] ?? String.Empty;
                    if ((sLogPath.Length == 0)  || (!Directory.Exists(sLogPath)))
                        return;
                    string sLogIP = System.Configuration.ConfigurationSettings.AppSettings["WSSTLogIP"] ?? String.Empty;
                    if (sLogIP.Length == 0)
                        return;
                    string[] sLogIPs = sLogIP.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    bool bSniff = false;
                    string sIPClient = HttpContext.Current.Request.UserHostAddress.ToString();
                    foreach (string sIP in sLogIPs)
                    {
                        if (sIPClient.StartsWith(sIP.Trim()) || sIP.StartsWith("all", StringComparison.InvariantCultureIgnoreCase))
                        {
                            bSniff = true;
                            break;
                        }
                    }

                    if (bSniff)
                    {
                        sFileName = String.Format("WS_{0}_{1}_{2}_{3}.log", webmethod, sIPClient, DateTime.UtcNow.ToString("yyyyMMdd-HHmmssfff"), System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("000000"));
                        sFileName = Path.Combine(sLogPath, sFileName);
                        using (Stream smReq = new FileStream(sFileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                        using (StreamWriter swReq = new StreamWriter(smReq))
                        {
                            swReq.WriteLine(HttpContext.Current.Request.HttpMethod + " " + HttpContext.Current.Request.Url.ToString() + " " + (HttpContext.Current.Request.ServerVariables["SERVER_PROTOCOL"] ?? String.Empty).ToString());
                            foreach (string sKey in HttpContext.Current.Request.Headers.Keys)
                            {
                                swReq.Write(sKey + ": ");
                                string sVals = "";
                                foreach (string sVal in HttpContext.Current.Request.Headers.GetValues(sKey))
                                {
                                    sVals += sVal + "; ";
                                }
                                if (sVals.EndsWith("; "))
                                    sVals = sVals.Substring(0, sVals.Length - 2);
                                swReq.WriteLine(sVals);
                            }
                            swReq.WriteLine();
                            swReq.Flush();
                            long ilPos = HttpContext.Current.Request.InputStream.Position;
                            HttpContext.Current.Request.InputStream.Position = 0;
                            byte[] abBuffer = new byte[HttpContext.Current.Request.InputStream.Length];
                            HttpContext.Current.Request.InputStream.Read(abBuffer, 0, abBuffer.Length);
                            HttpContext.Current.Request.InputStream.Position = ilPos;
                            smReq.Write(abBuffer, 0, abBuffer.Length);
                            smReq.Flush();
                        }
                    }
                }
                catch (Exception ex)
                {
                    TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, "Exception in WS " + webmethod + ": " + ex.Message + "\r\n" + ex.StackTrace);
                }
            }
        }

		// WEB SERVICE EXAMPLE
		// The HelloWorld() example service returns the string Hello World
		// To build, uncomment the following lines then save and build the project
		// To test this web service, press F5

		[WebMethod]
		public SafeTIRUploadAck WSST(SafeTIRUploadParams su)
		{
			SafeTIRUploadAck safeUploadAck ;
			try
			{
				IWSSTFileReceiver iFileReceiver =  (IWSSTFileReceiver)Activator.GetObject(typeof(IRU.RTS.IWSSTFileReceiver), System.Configuration.ConfigurationSettings.AppSettings["WSSTFileReceiverEndPoint"]);
				
				string senderIP = HttpContext.Current.Request.UserHostAddress.ToString();

                SniffRequest("WSST");

				safeUploadAck = iFileReceiver.ProcessReceivedFile(su , senderIP);
			}
			catch (Exception ex)
			{
				//TODO: Replace this in Production to Trace
				string sMessage = ex.Message + " \n - " + ex.StackTrace + " \n- " + ex.Source ;
				safeUploadAck = new SafeTIRUploadAck();
				safeUploadAck.ReturnCode=1200;
				safeUploadAck.Sender_MessageID=su.Sender_MessageID;
				safeUploadAck.ResponseTime=DateTime.Now;
				safeUploadAck.Version="1.0";

				TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError,sMessage);

			}
			return safeUploadAck;
		}
        [WebMethod]

        public SafeTIRUploadAck WSRE(SafeTIRReconParams su)
        {
            SafeTIRUploadAck reconcileReplyAck;
            try
            {
                IWSREFileReceiver iFileReceiver = (IWSREFileReceiver)Activator.GetObject(typeof(IRU.RTS.IWSREFileReceiver), System.Configuration.ConfigurationSettings.AppSettings["WSREFileReceiverEndPoint"]);

                string senderIP = HttpContext.Current.Request.UserHostAddress.ToString();

                SniffRequest("WSRE");

                reconcileReplyAck = iFileReceiver.ProcessReceivedFile(su, senderIP);
            }
            catch (Exception ex)
            {
                //TODO: Replace this in Production to Trace
                string sMessage = ex.Message + " \n - " + ex.StackTrace + " \n- " + ex.Source;
                reconcileReplyAck = new SafeTIRUploadAck();
                reconcileReplyAck.ReturnCode = 1200;
                reconcileReplyAck.Sender_MessageID = su.Sender_MessageID;
                reconcileReplyAck.ResponseTime = DateTime.Now;
                reconcileReplyAck.Version = "1.0";

                TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, sMessage);

            }
            return reconcileReplyAck;
        }

	}
}
