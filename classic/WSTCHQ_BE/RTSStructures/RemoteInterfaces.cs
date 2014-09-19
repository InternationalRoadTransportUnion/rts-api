using System;
using System.ServiceModel;

namespace IRU.RTS
{

	#region TCHQ
	[ServiceContract]
	public interface ITCHQProcessor
	{
		[OperationContract]
		TIRHolderResponse  ProcessQuery(TIRHolderQuery TirHolderQueryData, string SenderIP, out long IRUQueryId);
		
		[OperationContract]
		void UpdateResponseResult(long QueryId, DateTime dtResponseSent, bool ResponseResult);
	}
	#endregion

	#region WSST
	[ServiceContract]
	public interface IWSSTFileReceiver
	{
		[OperationContract]
		SafeTIRUploadAck	ProcessReceivedFile(SafeTIRUploadParams TIRUploadParams, string SenderIP);
	}
	#endregion

    #region WSRE
    // <summary>
    ///Lata changed for WSRQ on Sept05,2007
    /// </summary>>
	[ServiceContract]
	public interface IWSREFileReceiver
	{
		[OperationContract]
        SafeTIRUploadAck ProcessReceivedFile(SafeTIRReconParams ReplyParams, string SenderIP);
	}
	#endregion

    #region WSRQ
    // <summary>
    ///Lata changed for WSRQ on Sept05,2007
    /// </summary>>
	[ServiceContract]
	public interface IWSRQProcessor
    {
		[OperationContract]
		ReconciliationResponse ProcessQuery(ReconciliationQuery ReconciliationQuery, string SenderIP, out long QueryId);

		[OperationContract]
        void UpdateResponseResult(long QueryId, DateTime dtResponseSent, bool ResponseResult);
    }
    #endregion

    #region TVQR
	[ServiceContract]
	public interface ITVQRProcessor
    {
		[OperationContract]
        VoucherQueryResponseType ProcessQuery(VoucherQueryType VoucherQuery, string SenderIP, out long IRUQueryId);

		[OperationContract]
		VoucherRegistrationResponseType ProcessQuery(VoucherRegistrationType VoucherRegistration, string SenderIP, out long IRUQueryId);

		[OperationContract]
		void UpdateResponseResult(long QueryId, DateTime dtResponseSent, bool ResponseResult);
    }
    #endregion

    #region EGIS
	[ServiceContract]
	public interface IEGISProcessor
    {
		[OperationContract]
        EGISResponseType ProcessQuery(EGISQueryType EGISQuery, string SenderIP, out long IRUQueryId);

		[OperationContract]
		void UpdateResponseResult(long QueryId, DateTime dtResponseSent, bool ResponseResult);
    }
    #endregion
}
