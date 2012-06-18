namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="transmitTIROperationTerminationsResponse", WrapperNamespace="http://rts.iru.org/services/TerminationService-1", IsWrapped=true)]
    public partial class transmitTIROperationTerminationsResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/TerminationService-1", Order=0)]
        public System.DateTime transmissionTime;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/TerminationService-1", Order=1)]
        public bool success;
        
        public transmitTIROperationTerminationsResponse()
        {
        }
        
        public transmitTIROperationTerminationsResponse(System.DateTime transmissionTime, bool success)
        {
            this.transmissionTime = transmissionTime;
            this.success = success;
        }
    }
}
