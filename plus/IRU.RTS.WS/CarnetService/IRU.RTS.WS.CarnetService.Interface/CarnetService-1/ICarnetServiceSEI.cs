namespace IRU.RTS.WS.CarnetService.Interface
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://rts.iru.org/services/CarnetService-1", ConfigurationName="ICarnetServiceSEI")]
    public interface ICarnetServiceSEI
    {
        
        // CODEGEN: Generating message contract since message part namespace (http://rts.iru.org/model/carnet-1) does not match the default value (http://rts.iru.org/services/CarnetService-1)
        [System.ServiceModel.OperationContractAttribute(Action="http://rts.iru.org/services/CarnetService-1/queryCarnet", ReplyAction="http://rts.iru.org/services/CarnetService-1/queryCarnetResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CarnetBaseType))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="TIRCarnet")]
        queryCarnetResponse queryCarnet(queryCarnetRequest request);
        
        // CODEGEN: Parameter 'to' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://rts.iru.org/services/CarnetService-1/getStoppedCarnets", ReplyAction="http://rts.iru.org/services/CarnetService-1/getStoppedCarnetsResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CarnetBaseType))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="total")]
        getStoppedCarnetsResponse getStoppedCarnets(getStoppedCarnetsRequest request);
    }
}
