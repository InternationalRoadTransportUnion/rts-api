using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using IRU.Common.WCF.Wsdl.Metadata;
using IRU.RTS.WS.TerminationService.Interface;

namespace IRU.RTS.WS.TerminationService.Implementation
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any, Namespace = "http://rts.iru.org/services/TerminationService-1")]
    [MetadataFixer(new string[] { "TerminationService_1.*.wsdl", "TerminationService_1.*.xsd" }, true)]
    public class TerminationService : TerminationServiceSEI
    {
        public override transmitTIROperationTerminationsResponse transmitTIROperationTerminations(transmitTIROperationTerminationsRequest request)
        {
            object oCallerSubscriberId;
            string sCallerSubscriberId = null;
            if (OperationContext.Current.IncomingMessageProperties.TryGetValue("RTS_SUBSCRIBER_ID", out oCallerSubscriberId))
                sCallerSubscriberId = (string)oCallerSubscriberId;
            RTSClient.RTSClientWrapper rtsClient = new RTSClient.RTSClientWrapper(sCallerSubscriberId, new Uri(Properties.Settings.Default.WsUrlWSST), Properties.Settings.Default.SubscriberIdWSST);

            transmitTIROperationTerminationsResponse res = rtsClient.SendTerminations(request);
            
            return res;
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
