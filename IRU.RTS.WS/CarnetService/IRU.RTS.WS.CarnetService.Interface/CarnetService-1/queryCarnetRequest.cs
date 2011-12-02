namespace IRU.RTS.WS.CarnetService.Interface
{
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="queryCarnet", WrapperNamespace="http://rts.iru.org/services/CarnetService-1", IsWrapped=true)]
    public partial class queryCarnetRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/model/carnet-1", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="token")]
        public string TIRCarnetNumber;
        
        public queryCarnetRequest()
        {
        }
        
        public queryCarnetRequest(string TIRCarnetNumber)
        {
            this.TIRCarnetNumber = TIRCarnetNumber;
        }
    }
}
