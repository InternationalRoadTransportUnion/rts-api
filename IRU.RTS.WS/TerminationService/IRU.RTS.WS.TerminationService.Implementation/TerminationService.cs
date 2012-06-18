using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using IRU.Common.WCF.Wsdl.Schema;
using IRU.Common.WCF.Wsdl.Output;
using IRU.RTS.WS.TerminationService.Interface;

namespace IRU.RTS.WS.TerminationService.Implementation
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://rts.iru.org/services/TerminationService-1")]
    [WsdlReplacer("IRU.RTS.WS.TerminationService.Interface, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null", "TerminationService_1.TerminationService-1.wsdl", false)]
    [XsdReplacer(new string[] { "IRU.RTS.WS.TerminationService.Interface, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null" }, new string[] { "TerminationService_1.rts-termination-1.xsd", "TerminationService_1.rts-carnet-1.xsd", "TerminationService_1.tir-carnet-1.xsd", "TerminationService_1.tir-actor-1.xsd", "TerminationService_1.iso-3166-1-alpha-3.xsd" })]
    public class TerminationService : TerminationServiceSEI
    {
        public override transmitTIROperationTerminationsResponse transmitTIROperationTerminations(transmitTIROperationTerminationsRequest request)
        {
            throw new NotImplementedException();
        }

        public override getReconciliationRequestsResponse getReconciliationRequests(getReconciliationRequestsRequest request)
        {
            throw new NotImplementedException();
        }

        public override transmitReconciliationRequestRepliesResponse transmitReconciliationRequestReplies(transmitReconciliationRequestRepliesRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
