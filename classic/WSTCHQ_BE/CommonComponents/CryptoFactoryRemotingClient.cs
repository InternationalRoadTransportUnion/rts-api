using System;
using IRU.CommonInterfaces;
using IRU.RTS.CryptoInterfaces;


namespace IRU.RTS.CommonComponents
{
	/// <summary>
	/// A IPlugin which is used by clients who need to access the CryptoProvider in remote Crypto Host.
	/// The Configuration Parameters are as follows:
	/// <list type="table">
	/// <item><term>RemotingURI</term><description>URI used to activate the remote CryptoProvider</description></item>
	/// <item><term>ActivationMode</term><description></description>CAO/Singleton</item>
	/// </list>
	/// </summary>
	public class CryptoFactoryRemotingClient:IPlugIn
	{
		#region IPlugIn Members

		/// <summary>
		/// Called in by the Plugin Manager
		/// Read in the Configuration
		/// </summary>
		/// <param name="pluginManager">Reference passed in by the plugin Manager</param>
		public void Configure(IPlugInManager pluginManager)
		{
			// TODO:  Add CryptoFactoryRemotingClient.Configure implementation
		}

		/// <summary>
		/// Do Nothing
		/// </summary>
		public void Unload()
		{
			// TODO:  Add CryptoFactoryRemotingClient.Unload implementation
		}

		/// <summary>
		/// Name of the plugin
		/// </summary>
		public string PluginName
		{
			get
			{
				// TODO:  Add CryptoFactoryRemotingClient.PluginName getter implementation
				return null;
			}
			set
			{
				// TODO:  Add CryptoFactoryRemotingClient.PluginName setter implementation
			}
		}

		#endregion

		/// <summary>
		/// Uses the configuration files and creates the remote CryptoOperations object using the <c>(ICryptoOperations)Activator.GetObject(typeof(ICryptoOperations),URI)</c>
		/// 
		/// </summary>
		/// <returns>Reference to the Remote ICryptoOperations implementation</returns>
		public  ICryptoOperations GetCryptoImplementation()
		{return null;}
	}
}
