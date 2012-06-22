using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace IRU.RTS.WS.TerminationService.Implementation
{
    public class ServiceContractProtectionLevel : Attribute, IContractBehavior
    {
        //private System.Net.Security.ProtectionLevel _ProtectionLevel = SuccessOrFailure.None;

        public ServiceContractProtectionLevel()
        {
        }

        //public ServiceContractProtectionLevel(System.Net.Security.ProtectionLevel protectionLevel)
        //{
        //    _ProtectionLevel = protectionLevel;
        //}

        #region IContractBehavior Members

        public void  AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }

        public void  ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            //throw new NotImplementedException();
        }

        public void  ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.DispatchRuntime dispatchRuntime)
        {
            //throw new NotImplementedException();
        }

        public void  Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            //throw new NotImplementedException();
            BindingElementCollection bindingParameters = endpoint.Binding.CreateBindingElements();
            AsymmetricSecurityBindingElement sec = bindingParameters.Find<AsymmetricSecurityBindingElement>();
            //AsymmetricSecurityBindingElement sec = endpoint.Binding.GetProperty<AsymmetricSecurityBindingElement>();

            //System.Reflection.PropertyInfo s = endpoint.Binding.GetType().GetProperty("Security");

            if (sec != null)
            {
                switch (sec.MessageProtectionOrder)
                {
                    case System.ServiceModel.Security.MessageProtectionOrder.SignBeforeEncrypt:
                        contractDescription.ProtectionLevel = System.Net.Security.ProtectionLevel.Sign;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion
        }
}
