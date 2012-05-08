namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="transmitReconciliationRequestReplies", WrapperNamespace="http://rts.iru.org/services/TerminationService-1", IsWrapped=true)]
    public partial class transmitReconciliationRequestRepliesRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/TerminationService-1", Order=0)]
        public System.DateTime transmissionTime;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/TerminationService-1", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="token", Namespace="http://rts.iru.org/services/TerminationService-1")]
        public string transmissionId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/model/termination-1", Order=2)]
        public ReconciliationRequestRepliesType ReconciliationRequestReplies;
        
        public transmitReconciliationRequestRepliesRequest()
        {
        }
        
        public transmitReconciliationRequestRepliesRequest(System.DateTime transmissionTime, string transmissionId, ReconciliationRequestRepliesType ReconciliationRequestReplies)
        {
            this.transmissionTime = transmissionTime;
            this.transmissionId = transmissionId;
            this.ReconciliationRequestReplies = ReconciliationRequestReplies;
        }
    }
}
