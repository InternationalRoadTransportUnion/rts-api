using System;
using IRU.CommonInterfaces;
using System.IO;

namespace IRU.RTS.CommonComponents
{
	/// <summary>
	/// Summary description for InMemoryCache.
	/// </summary>
	public class InMemoryCache:ICache,IPlugIn
	{
		/// <summary>
		/// Initialises internal stores for stored the cached files. System.Hashtable will be used store the file contents as string in key value pairs. The Hashtable will be synchronised using the Synchronised() method available in teh hashtable class.
		/// </summary>
		public InMemoryCache()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region ICache Members

		/// <summary>
		/// Fethces the contents correponding to FileName
		/// </summary>
		/// <param name="FileName"></param>
		/// <returns>The file contents loaded into an XMLDocument object</returns>
		/// <exception cref="FileNotFoundException">In case the key is not found in the Hashtable.</exception>
		public System.Xml.XmlDocument GetXMLDomFromCache(string FileName)
		{
			// TODO:  Add InMemoryCache.GetXMLDomFromCache implementation
			return null;
		}
		/// <summary>
		/// Fethces the contents correponding to FileName as string
		/// </summary>
		/// <param name="FileName">Name of the file to Fetch</param>
		/// <returns>The file contents loaded as a string</returns>
		/// <exception cref="FileNotFoundException">In case the key is not found in the Hashtable.</exception>
		public string GetStringFromCache(string FileName)
		{
			// TODO:  Add InMemoryCache.GetStringFromCache implementation
			return null;
		}

		#endregion

		#region IPlugIn Members

		/// <summary>
		/// Called in by the Plugin Manager, store reference to the manager.
		/// 
		/// The configuration parameters are
		/// <list type="table">
		/// <item><term>ScanDirectory</term><description>Directory which contains the xml files</description></item>
		/// <item><term>FilePattern</term><description>e.g. *.x*</description></item>
		/// </list>
		/// Operation steps
		/// <list type="number">
		/// <item><description>Get reference to the config section</description></item>
		/// <item><description>Use the FileInfo class to get list of files in the directory to scan along with the extension e.g. .x*</description></item>
		/// <item><description>Use the XMLDocument class to .Load the file. This will verify whether the files are valid XMLs</description></item>
		/// <item><description>Store the string value of the XMLs in the memory hashtable(synchronised) against the name of the file as the key </description></item>
		/// </list>
		/// 
		/// </summary>
		/// <param name="pluginManager">reference to the plugin manager</param>
		public void Configure(IPlugInManager pluginManager)
		{
			// TODO:  Add InMemoryCache.Configure implementation
		}
/// <summary>
/// Do nothing
/// </summary>
		public void Unload()
		{
			// TODO:  Add InMemoryCache.Unload implementation
		}
/// <summary>
/// Name assigned in the Config file passed in by the plugin manager
/// </summary>
		public string PluginName
		{
			get
			{
				// TODO:  Add InMemoryCache.PluginName getter implementation
				return null;
			}
			set
			{
				// TODO:  Add InMemoryCache.PluginName setter implementation
			}
		}

		#endregion
	}
}
