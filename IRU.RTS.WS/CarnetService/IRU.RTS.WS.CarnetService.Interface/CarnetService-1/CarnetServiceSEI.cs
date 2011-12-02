namespace IRU.RTS.WS.CarnetService.Interface
{
    
    
    [System.ServiceModel.ServiceBehaviorAttribute(InstanceContextMode=System.ServiceModel.InstanceContextMode.PerCall, ConcurrencyMode=System.ServiceModel.ConcurrencyMode.Single, UseSynchronizationContext=false)]
    public abstract class CarnetServiceSEI : ICarnetServiceSEI
    {
        
        public abstract queryCarnetResponse queryCarnet(queryCarnetRequest request);
        
        public abstract getStoppedCarnetsResponse getStoppedCarnets(getStoppedCarnetsRequest request);
    }
}
