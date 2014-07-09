namespace IRU.RTS.WS.CarnetService.Interface
{
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getStoppedCarnetsResponse", WrapperNamespace="http://rts.iru.org/services/CarnetService-1", IsWrapped=true)]
    public partial class getStoppedCarnetsResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/CarnetService-1", Order=0)]
        public stoppedCarnetsTypeTotal total;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://rts.iru.org/services/CarnetService-1", Order=1)]
        public stoppedCarnetsTypeStoppedCarnets stoppedCarnets;
        
        public getStoppedCarnetsResponse()
        {
        }
        
        public getStoppedCarnetsResponse(stoppedCarnetsTypeTotal total, stoppedCarnetsTypeStoppedCarnets stoppedCarnets)
        {
            this.total = total;
            this.stoppedCarnets = stoppedCarnets;
        }
    }
}
