using System;
using IRU.CommonInterfaces;
using System.Collections;
using System.IO;
using System.Xml;
using IRU.RTS.CommonComponents;

namespace IRU.RTS.CommonComponents
{
	/// <summary>
	/// Summary description for FileCache.
	/// </summary>
	public class FileCache:ICache,IPlugIn
	{
		#region Member Variables
		System.Collections.Hashtable m_FileCache;
		System.Collections.Hashtable m_FileStringCache;
		
		
		string              m_PluginName;
		
		IPlugInManager      m_PluginManager;
		string              m_FileExtension;
		string              m_ScanDir;
		
		#endregion
		
		#region Constructor

		/// <summary>
		/// Initialise Hashtable
		/// </summary>
		
		public FileCache()
		{
		
			m_FileCache = new Hashtable();
			m_FileCache= Hashtable.Synchronized(m_FileCache);


			m_FileStringCache = new Hashtable();
			m_FileStringCache= Hashtable.Synchronized(m_FileStringCache);


		}

		#endregion


		#region ICache Members

		/// <summary>
		/// Fetch the Filecontents from the hash table load in XMLDocument class and return
		/// </summary>
		/// <param name="FileName">Name of the File</param>
		/// <returns>XMLDocument containing the file contents</returns>
		public System.Xml.XmlDocument GetXMLDomFromCache(string FileName)
		{
			XmlDocument FileDoc;
			FileDoc = (XmlDocument) m_FileCache[FileName];
			if(FileDoc == null)
			{
				throw new ApplicationException(
					"File of name [" + FileName + "] not found");
			}
			XmlDocument xDocToReturn =  (XmlDocument)FileDoc.Clone();
			xDocToReturn.PreserveWhitespace=true;
			return xDocToReturn;
		}

		/// <summary>
		/// Fetch the Filecontents from the hash table load as string and return
		/// Dont pass the File Extentension e.g. if you need "myfile.xml" just pass in "myFile"
		/// </summary>
		/// <param name="sFileName">Name of File</param>
		/// <returns>string containing the file contents</returns>
		public string GetStringFromCache(string FileName)
		{
			

			string FileString = null;
			FileString  = (string)m_FileStringCache[FileName];
			if(FileString == null)
			{
				throw new ApplicationException(
					"File of name [" + FileName + "] not found");
			}
			return (string)FileString.Clone();

		}

		#endregion

		#region IPlugIn Members

		/// <summary>
		/// Load the configuration file and load the file contents
		/// </summary>
		/// <param name="pluginManager"></param>
		public void Configure(IPlugInManager pluginManager)
		{

			// InMemoryFileCacheSettings ScanDirectory="c:\SafeTIR\XSD" FilePattern="*.x*" 
			m_PluginManager = pluginManager;
			XmlNode cacheNode = m_PluginManager.GetConfigurationSection(
				m_PluginName);
			m_FileExtension = (cacheNode.SelectSingleNode
				("./InMemoryFileCacheSettings/@FilePattern")).InnerText;
			m_ScanDir   = (cacheNode.SelectSingleNode
				("./InMemoryFileCacheSettings/@ScanDirectory")).InnerText;
            
			
			LoadFiles(m_FileExtension, m_ScanDir);
			
			
		}
			
			
		/// <summary>
		/// do Nothing
		/// </summary>
		public void Unload()
		{
			m_FileCache = null;
			m_FileStringCache=null;
			// TODO:  Add FileCache.Unload implementation
		}

		/// <summary>
		/// Name of Plugin
		/// </summary>
		public string PluginName
		{
			get
			{
				
				return m_PluginName;
			}
			set
			{
				m_PluginName=value;
			}
		}

		#endregion

		#region Internal functions
		/// <summary>
		/// Method to load Files in memory and adds to hashtable
		/// If the key already exists in hashtable then it removes
		/// the key and then inserts the new object with same key. 
		/// </summary>
		/// <param name="mFileExtension">Extension of files that are
		/// to be loaded in memory</param>
		/// <param name="mFileDir">Directory where to search for
		/// Files</param>
		private void LoadFiles(string FileExtension, string FileDir)
		{
			DirectoryInfo di = new DirectoryInfo(FileDir);
			FileInfo[] files = di.GetFiles(FileExtension);
            
			foreach(FileInfo fi in files)
			{
				try
				{
                
					//verifying if the XSDs are valid XML files
					XmlDocument doc = new XmlDocument();
					doc.PreserveWhitespace=true; //required in case we need to generate hash
					string Filestring = null;
					doc.Load(fi.FullName);
					Filestring = doc.OuterXml ;
                    
					// Updat XML Cache
					if(m_FileCache.ContainsKey(
						fi.Name.Substring(0, fi.Name.LastIndexOf("."))))
					{
						m_FileCache.Remove(
							fi.Name.Substring(0, fi.Name.LastIndexOf(".")));
					}
					m_FileCache.Add(
						fi.Name.Substring(0, fi.Name.LastIndexOf(".")), doc);
    				
					
					if(m_FileStringCache.ContainsKey(
						fi.Name.Substring(0, fi.Name.LastIndexOf("."))))
					{
						m_FileStringCache.Remove(
							fi.Name.Substring(0, fi.Name.LastIndexOf(".")));
					}
					m_FileStringCache.Add(
						fi.Name.Substring(0, fi.Name.LastIndexOf(".")), Filestring);
				}
					
				catch(Exception ex)
				{
					
					Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError, "Could not load File  " +
						fi.FullName + " in DOM " +
						"\n Function Name: FileCache::LoadFiles");              
					throw ex;
				}
				
			}
		}


		#endregion



	}
}
