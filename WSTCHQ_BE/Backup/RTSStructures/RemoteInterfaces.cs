using System;

namespace IRU.RTS
{

	#region TCHQ
	public interface ITCHQProcessor
	{
		TIRHolderResponse  ProcessQuery(TIRHolderQuery TirHolderQueryData, string SenderIP, out long IRUQueryId);
		void UpdateResponseResult(long QueryId, DateTime dtResponseSent, bool ResponseResult);
	}
	#endregion

	#region WSST
	public interface IWSSTFileReceiver
	{
	  SafeTIRUploadAck	ProcessReceivedFile(SafeTIRUploadParams TIRUploadParams, string SenderIP);
	}
	#endregion

}
