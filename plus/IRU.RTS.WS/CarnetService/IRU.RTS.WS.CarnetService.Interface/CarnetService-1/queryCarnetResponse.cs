namespace IRU.RTS.WS.CarnetService.Interface
{
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="queryCarnetResponse", WrapperNamespace="http://rts.iru.org/services/CarnetService-1", IsWrapped=true)]
    public partial class queryCarnetResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/CarnetService-1", Order=0)]
        public CarnetType TIRCarnet;
        
        public queryCarnetResponse()
        {
        }
        
        public queryCarnetResponse(CarnetType TIRCarnet)
        {
            this.TIRCarnet = TIRCarnet;
        }
    }
}
