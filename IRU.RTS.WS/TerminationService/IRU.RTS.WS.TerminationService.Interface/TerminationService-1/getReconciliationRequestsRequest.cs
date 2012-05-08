namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getReconciliationRequests", WrapperNamespace="http://rts.iru.org/services/TerminationService-1", IsWrapped=true)]
    public partial class getReconciliationRequestsRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/TerminationService-1", Order=0)]
        public System.DateTime from;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/TerminationService-1", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Namespace="http://rts.iru.org/services/TerminationService-1")]
        public System.Nullable<System.DateTime> to;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/TerminationService-1", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Namespace="http://rts.iru.org/services/TerminationService-1")]
        public System.Nullable<int> offset;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/TerminationService-1", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Namespace="http://rts.iru.org/services/TerminationService-1")]
        public System.Nullable<uint> maxCount;
        
        public getReconciliationRequestsRequest()
        {
        }
        
        public getReconciliationRequestsRequest(System.DateTime from, System.Nullable<System.DateTime> to, System.Nullable<int> offset, System.Nullable<uint> maxCount)
        {
            this.from = from;
            this.to = to;
            this.offset = offset;
            this.maxCount = maxCount;
        }
    }
}
