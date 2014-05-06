namespace IRU.RTS.WS.VoucherService.Interface
{
    
    
    [System.ServiceModel.ServiceBehaviorAttribute(InstanceContextMode=System.ServiceModel.InstanceContextMode.PerCall, ConcurrencyMode=System.ServiceModel.ConcurrencyMode.Single, UseSynchronizationContext=false)]
    public abstract class VoucherServiceSEI : IVoucherServiceSEI
    {
        
        public abstract QueriedVoucherType queryVoucher(string voucherNumber);
    }
}
