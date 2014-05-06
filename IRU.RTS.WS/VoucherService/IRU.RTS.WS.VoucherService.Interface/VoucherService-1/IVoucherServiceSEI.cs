namespace IRU.RTS.WS.VoucherService.Interface
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://rts.iru.org/services/VoucherService-1", ConfigurationName="IVoucherServiceSEI")]
    public interface IVoucherServiceSEI
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://rts.iru.org/services/VoucherService-1/queryVoucher", ReplyAction="http://rts.iru.org/services/VoucherService-1/queryVoucherResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CarnetBaseType))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        QueriedVoucherType queryVoucher(string voucherNumber);
    }
}
