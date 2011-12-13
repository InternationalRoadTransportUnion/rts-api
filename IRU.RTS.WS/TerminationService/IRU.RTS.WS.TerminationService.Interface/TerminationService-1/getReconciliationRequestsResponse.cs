namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getReconciliationRequestsResponse", WrapperNamespace="http://rts.iru.org/services/TerminationService-1", IsWrapped=true)]
    public partial class getReconciliationRequestsResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/TerminationService-1", Order=0)]
        public reconciliationRequestsType getReconciliationRequestsResult;
        
        public getReconciliationRequestsResponse()
        {
        }
        
        public getReconciliationRequestsResponse(reconciliationRequestsType getReconciliationRequestsResult)
        {
            this.getReconciliationRequestsResult = getReconciliationRequestsResult;
        }
    }
}
