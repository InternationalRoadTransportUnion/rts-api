namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    [System.ServiceModel.ServiceBehaviorAttribute(InstanceContextMode=System.ServiceModel.InstanceContextMode.PerCall, ConcurrencyMode=System.ServiceModel.ConcurrencyMode.Single, UseSynchronizationContext=false)]
    public abstract class TerminationServiceSEI : ITerminationServiceSEI
    {
        
        public abstract transmitTIROperationTerminationsResponse transmitTIROperationTerminations(transmitTIROperationTerminationsRequest request);
        
        public abstract getReconciliationRequestsResponse getReconciliationRequests(getReconciliationRequestsRequest request);
        
        public abstract transmitReconciliationRequestRepliesResponse transmitReconciliationRequestReplies(transmitReconciliationRequestRepliesRequest request);
    }
}
