namespace IRU.RTS.WS.TerminationService.Interface
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://rts.iru.org/services/TerminationService-1", ConfigurationName="ITerminationServiceSEI")]
    public interface ITerminationServiceSEI
    {
        
        // CODEGEN: Parameter 'transmissionId' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://rts.iru.org/services/TerminationService-1/transmitTIROperationTerminations" +
            "", ReplyAction="http://rts.iru.org/services/TerminationService-1/transmitTIROperationTerminations" +
            "Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        transmitTIROperationTerminationsResponse transmitTIROperationTerminations(transmitTIROperationTerminationsRequest request);
        
        // CODEGEN: Parameter 'to' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://rts.iru.org/services/TerminationService-1/getReconciliationRequests", ReplyAction="http://rts.iru.org/services/TerminationService-1/getReconciliationRequestsRespons" +
            "e")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        getReconciliationRequestsResponse getReconciliationRequests(getReconciliationRequestsRequest request);
        
        // CODEGEN: Generating message contract since the operation transmitReconciliationRequestReplies is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://rts.iru.org/services/TerminationService-1/transmitReconciliationRequestRep" +
            "lies", ReplyAction="http://rts.iru.org/services/TerminationService-1/transmitReconciliationRequestRep" +
            "liesResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        transmitReconciliationRequestRepliesResponse transmitReconciliationRequestReplies(transmitReconciliationRequestRepliesRequest request);
    }
}
