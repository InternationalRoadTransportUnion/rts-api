using System;
using System.Collections.Generic;
using System.Text;

namespace IRU.RTS.TIREPD
{
    public interface IG2BReceiver
    {
        TIREPDG2BUploadAck ProcessReceivedFile(TIREPDG2BUploadParams B2GUploadParams, string SenderIP, out long G2BMessageId);
    }
}
