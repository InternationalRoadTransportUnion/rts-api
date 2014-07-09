using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using IRU.RTS.TIREPD;

namespace TIREPDG2B
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://www.iru.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class TIREPDG2BService : System.Web.Services.WebService, ITIREPDG2BServiceClassSoap
    {

        [WebMethod]
        public TIREPDG2BUploadAck TIREPDG2B(TIREPDG2BUploadParams su)
        {
            TIREPDG2BUploadAck ack = new TIREPDG2BUploadAck();
            try
            {
                IG2BReceiver fileReceiver = (IG2BReceiver)Activator.GetObject(typeof(IRU.RTS.TIREPD.IG2BReceiver), System.Configuration.ConfigurationSettings.AppSettings["G2BReceiverEndPoint"]);

                string senderIP = HttpContext.Current.Request.UserHostAddress.ToString();
                long lG2BMessageId = -1;
                ack = fileReceiver.ProcessReceivedFile(su, senderIP, out lG2BMessageId);
            }
            catch (Exception ex)
            {
                //TODO: Replace this in Production to Trace
                string sMessage = ex.Message + " \n - " + ex.StackTrace + " \n- " + ex.Source;
                ack = new TIREPDG2BUploadAck();
                ack.ReturnCode = 1200;
                ack.SubscriberMessageID = su.SubscriberMessageID ;
                //ack.ResponseTime = DateTime.Now;
                //ack.Version = "1.0";
                
                TraceHelper.TraceHelper.TraceMessage(TraceHelper.TraceHelper.EAITraceSwitch.TraceError, sMessage);

            }
            return ack;
        }
    }
}
