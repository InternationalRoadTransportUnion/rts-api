using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace IRU.RTS.TIREPD
{
	[ServiceContract]
    public interface IG2BReceiver
    {
		[OperationContract]
        TIREPDG2BUploadAck ProcessReceivedFile(TIREPDG2BUploadParams B2GUploadParams, string SenderIP, out long G2BMessageId);
    }

	[ServiceContract]
    public interface IB2GSender
    {
		[OperationContract]
        bool SendEPDFile(string sDocSend, string ISOCode, string LRN, out string sDocResponse); 
    }
}
