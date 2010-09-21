using System;
using System.Collections.Generic;
using System.Text;

namespace IRU.RTS.TIREPD
{
    public interface IG2BReceiver
    {
        TIREPDG2BUploadAck ProcessReceivedFile(TIREPDG2BUploadParams B2GUploadParams, string SenderIP, out long G2BMessageId);
    }

    public interface IB2GSender
    {
        bool SendEPDFile(string sDocSend, string ISOCode, string LRN, out string sDocResponse); 
    }
}
