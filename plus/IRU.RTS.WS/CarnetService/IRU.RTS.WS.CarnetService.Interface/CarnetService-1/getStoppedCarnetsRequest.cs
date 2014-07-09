namespace IRU.RTS.WS.CarnetService.Interface
{
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getStoppedCarnets", WrapperNamespace="http://rts.iru.org/services/CarnetService-1", IsWrapped=true)]
    public partial class getStoppedCarnetsRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/CarnetService-1", Order=0)]
        public System.DateTime from;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/CarnetService-1", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> to;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/CarnetService-1", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> offset;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/CarnetService-1", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<uint> maxCount;
        
        public getStoppedCarnetsRequest()
        {
        }
        
        public getStoppedCarnetsRequest(System.DateTime from, System.Nullable<System.DateTime> to, System.Nullable<int> offset, System.Nullable<uint> maxCount)
        {
            this.from = from;
            this.to = to;
            this.offset = offset;
            this.maxCount = maxCount;
        }
    }
}
