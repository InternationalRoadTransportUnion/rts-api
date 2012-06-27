using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace IRU.RTS.WS.TerminationService.Implementation.ProtectionLevel
{
    public class ServiceProtectionLevelEnpointBehavior : IEndpointBehavior
    {
        private System.Net.Security.ProtectionLevel _ProtectionLevel = System.Net.Security.ProtectionLevel.None;

        public ServiceProtectionLevelEnpointBehavior(System.Net.Security.ProtectionLevel protectionLevel)
        {
            _ProtectionLevel = protectionLevel;
        }

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            //throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            //throw new NotImplementedException();
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            // Set Service contractDescription.ProtectionLevel with constructor's given ProtectionLevel
            endpoint.Contract.ProtectionLevel = _ProtectionLevel;
        }

        #endregion
    }
}
