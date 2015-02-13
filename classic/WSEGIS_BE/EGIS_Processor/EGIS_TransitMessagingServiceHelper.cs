using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using IRU.RTS.Common.WCF;
using IRU.RTS.WSEGIS.BizTalk.eTIR.TransitMessagingService;

namespace IRU.RTS.WSEGIS
{
    public class EGIS_TransitMessagingServiceHelper
    {
        private string _remoteAddress;

        public EGIS_TransitMessagingServiceHelper(string remoteAddress)
        {
            _remoteAddress = remoteAddress;
        }

        public Hashtable GetTransitMessagesOfGuarantee(string tirCarnetNumber)
        {
            Hashtable result = new Hashtable();

            using (BasicHttpClient<IRU_EPD_BizTalk_eTIR_Orchestrations_services_TransitMessagingService_1_TransitMessagingSEIPort> transitMessagingClient = new BasicHttpClient<IRU_EPD_BizTalk_eTIR_Orchestrations_services_TransitMessagingService_1_TransitMessagingSEIPort>(_remoteAddress))
            {
                IRU_EPD_BizTalk_eTIR_Orchestrations_services_TransitMessagingService_1_TransitMessagingSEIPort transitMessagingProxy = transitMessagingClient.GetProxy();
                
                getTransitMessagesOfGuaranteeRequest request = new getTransitMessagesOfGuaranteeRequest();
                request.getTransitMessagesOfGuarantee = new getTransitMessagesOfGuarantee();
                request.getTransitMessagesOfGuarantee.GuaranteeNumber = tirCarnetNumber;
                 
                getTransitMessagesOfGuaranteeResponse1 response = transitMessagingProxy.getTransitMessagesOfGuarantee(request);

                result.Add("StartMessages", null);
                result.Add("DischargeMessages", null);
                result.Add("UpdateSealsMessages", null);

                if ((response != null) && (response.getTransitMessagesOfGuaranteeResponse != null) && (response.getTransitMessagesOfGuaranteeResponse.GetTransitMessagesOfGuaranteeResult != null))
                {
                    List<TransitMessageType> startMessages = response.getTransitMessagesOfGuaranteeResponse.GetTransitMessagesOfGuaranteeResult.Where(m => m.MessageTypeId == 29).OrderBy(m => m.LAST_UPDATE_DATETIME).ToList();
                    List<TransitMessageType> dischargeMessages = response.getTransitMessagesOfGuaranteeResponse.GetTransitMessagesOfGuaranteeResult.Where(m => m.MessageTypeId == 45).OrderBy(m => m.LAST_UPDATE_DATETIME).ToList();
                    List<TransitMessageType> updateSealsMessages = response.getTransitMessagesOfGuaranteeResponse.GetTransitMessagesOfGuaranteeResult.Where(m => m.MessageTypeId == 25).OrderBy(m => m.LAST_UPDATE_DATETIME).ToList();

                    if (startMessages.Count > 0)
                    {
                        result["StartMessages"] = startMessages;
                    }
                    if (dischargeMessages.Count > 0)
                    {
                        result["DischargeMessages"] = dischargeMessages;
                    }
                    if (updateSealsMessages.Count > 0)
                    {
                        result["UpdateSealsMessages"] = updateSealsMessages;
                    }
                }
            }

            return result;
        }
    }
}
