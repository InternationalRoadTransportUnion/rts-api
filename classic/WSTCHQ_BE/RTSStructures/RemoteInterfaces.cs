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

    #region WSRE
    // <summary>
    ///Lata changed for WSRQ on Sept05,2007
    /// </summary>>
    public interface IWSREFileReceiver
	{
        SafeTIRUploadAck ProcessReceivedFile(SafeTIRReconParams ReplyParams, string SenderIP);
	}
	#endregion

    #region WSRQ
    // <summary>
    ///Lata changed for WSRQ on Sept05,2007
    /// </summary>>
    public interface IWSRQProcessor
    {
      ReconciliationResponse ProcessQuery(ReconciliationQuery ReconciliationQuery, string SenderIP, out long QueryId);
        void UpdateResponseResult(long QueryId, DateTime dtResponseSent, bool ResponseResult);
    }
    #endregion

    #region TVQR
    public interface ITVQRProcessor
    {
        VoucherQueryResponseType ProcessQuery(VoucherQueryType VoucherQuery, string SenderIP, out long IRUQueryId);
        VoucherRegistrationResponseType ProcessQuery(VoucherRegistrationType VoucherRegistration, string SenderIP, out long IRUQueryId);
        void UpdateResponseResult(long QueryId, DateTime dtResponseSent, bool ResponseResult);
    }
    #endregion

    #region EGIS
    public interface IEGISProcessor
    {
        EGISResponseType ProcessQuery(EGISQueryType EGISQuery, string SenderIP, out long IRUQueryId);
        void UpdateResponseResult(long QueryId, DateTime dtResponseSent, bool ResponseResult);
    }
    #endregion
}
