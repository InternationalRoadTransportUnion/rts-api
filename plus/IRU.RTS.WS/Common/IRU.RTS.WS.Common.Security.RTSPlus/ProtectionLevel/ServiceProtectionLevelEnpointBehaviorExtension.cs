using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace IRU.RTS.WS.Common.Security.RTSPlus.ProtectionLevel
{
    public class ServiceProtectionLevelEnpointBehaviorExtension : BehaviorExtensionElement
    {
        /// <summary>
        /// Gets or sets the service contract ProtectionLevel
        /// <value>Default: None</value>
        /// </summary>
        [ConfigurationProperty("ProtectionLevel", IsRequired = false, DefaultValue = System.Net.Security.ProtectionLevel.None)]
        public System.Net.Security.ProtectionLevel ProtectionLevel
        {
            get { return (System.Net.Security.ProtectionLevel)base["ProtectionLevel"]; }
            set { base["ProtectionLevel"] = value; }
        }

        /// <summary>
        /// Gets the type of behavior.
        /// <value><see cref="ServiceProtectionLevelEnpointBehavior"/> type.</value>
        /// </summary>
        public override Type BehaviorType
        {
            get { return typeof(ServiceProtectionLevelEnpointBehavior); }
        }

        /// <summary>
        /// Creates a behavior extension based on the current configuration settings.
        /// </summary>
        /// <returns>A new instance of <see cref="ServiceContractBehavior"/>.</returns>
        protected override object CreateBehavior()
        {
            return new ServiceProtectionLevelEnpointBehavior(ProtectionLevel);
        }
    }
}
