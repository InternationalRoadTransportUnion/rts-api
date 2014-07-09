using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using IRU.RTS.TIREPD;
namespace TIREPDB2G
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://www.iru.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class TIREPDB2GServiceClass : System.Web.Services.WebService, ITIREPDB2GServiceClassSoap
    {

        [WebMethod]
        public TIREPDB2GUploadAck TIREPDB2G(TIREPDB2GUploadParams su)
        {
            TIREPDB2GUploadAck ack = new TIREPDB2GUploadAck();
            ack.SubscriberMessageID = su.SubscriberMessageID;
            ack.HostID = su.SubscriberID;
            return ack;
        }
    }
}
