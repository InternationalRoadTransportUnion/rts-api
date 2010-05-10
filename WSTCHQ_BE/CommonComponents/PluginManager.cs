using System;
using IRU.CommonInterfaces;
using System.Collections;
using System.Xml;
using System.Diagnostics;

namespace IRU.RTS.CommonComponents
{

	/// <summary>
	/// This class holds references to all of the plugins.
	/// In the load config method, it loads the plugin from
	/// config file.
	/// It laods all the plugins and unloads them at while
	/// shutting down. This class is exposed by remoting
	/// so a plugin can be loaded or unloaded dynamically.
	/// It will maintain a hashtable of the plugins.
	/// </summary>
	public class PlugInManager: MarshalByRefObject,
		IRU.CommonInterfaces.IPlugInManager
                
	{
		private string      configFile;
		private Hashtable   pluginTable;
		private XmlDocument configDoc;
        
		/// <summary>
		/// Default constructor
		/// </summary>
		public PlugInManager()
		{
		}

		/// <summary>
		/// Absolute Path to the configuration File to load.
		/// This property should be set before calling loadplugins method
		/// </summary>
		public string ConfigFile
		{
			set
			{
				this.configFile = value;
			}
			get
			{
				return configFile;
			}
		}


		/// <summary>
		/// Returns the plugin that is asked for.
		/// All the plugins are loaded in the memory with a name.
		/// Used by Connectors, Drivers, Routers and Processors to get instance
		/// of a given plugin.
		/// </summary>
		/// <param name="sPluginName">Name of the plugin</param>
		/// <returns>Returns the reference plugin</returns>
		public object GetPluginByName(string sPluginName)
		{
			object obj = pluginTable[sPluginName];
			if(obj == null)
			{
				throw new ApplicationException("Plugin Of Name : " + sPluginName +  
					" is not Loaded " + "\nPluginManager::GetPluginByName");
			}
			return obj;
		}


		/// <summary>
		/// This function runs through the plugin section of configuration 
		/// file and load all the plugins.
		/// </summary>
		public void LoadPlugins()
		{
			configDoc = new System.Xml.XmlDocument();
			configDoc.PreserveWhitespace = false;
			//Tracer.LogEvent("1 : LoadPlugin Config File name" + configFile);
			configDoc.Load(configFile);
			//Tracer.LogEvent("2 : LoadPlugin After load Config File");
			pluginTable = new Hashtable();
			pluginTable = Hashtable.Synchronized(pluginTable);
			//Tracer.LogEvent("3 : LoadPlugin Before Fill hashtable");
			FillHashtable();
			//Tracer.LogEvent("4 : LoadPlugin After Fill hashtable");
		}


		/// <summary>
		/// Function fills up the hastable of plugins.
		/// Loads all the plugins whose names are stored in config
		/// file and puts them into hashtable against the name of the plugin
		/// If the plugin is of type IRunnable then call start method
		/// to get the object's processing going.
		/// </summary>
		private void FillHashtable()
		{
			XmlNode plgMgrNode = GetConfigurationSection("PluginManager");
			XmlNodeList pluginList = ((XmlElement)plgMgrNode).GetElementsByTagName("Plugin");
			string pluginName = "";
			string pluginObjectType="";
			string pluginType="";

			//Tracer.LogEvent("1 : FillHashtable, Inside FillHashtable");
			foreach(XmlNode pluginNode in pluginList)
			{
				pluginName = pluginNode.Attributes.GetNamedItem("name").InnerText;
				pluginObjectType = pluginNode.Attributes.GetNamedItem("objectType").InnerText;
				pluginType = pluginNode.Attributes.GetNamedItem("isRunnable").InnerText;
				Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo ,
					"PlugInManager.FillHashtable  " + "Creating plugin " + pluginName);
				//Tracer.LogEvent("2 : FillHashtable, pluginName" + pluginName);
				try
				{
					Type objType = Type.GetType(pluginObjectType);
					object plugin = Activator.CreateInstance(objType);
					pluginTable.Add(pluginName, plugin);
					((IPlugIn)plugin).PluginName = pluginName;
				}
				catch (Exception e)
				{
					string ErrorToLog;
					ErrorToLog = "PlugInManager.FillHashtable" + 
						" Could not load or add plugin " + pluginName +
						" \n Error is " + e.Message;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError ,
						ErrorToLog);
					
					//Tracer.LogEvent("3 : FillHashtable, Inside Catch block PluginName :" + pluginName + "Error log :" + ErrorToLog );
					throw e;
					
				}
				//Tracer.LogEvent("4 : FillHashtable  leaving  FillHashtable");
			}

			foreach(XmlNode pluginNode in pluginList)
			{
				try
				{
					pluginName = pluginNode.Attributes.GetNamedItem("name").InnerText;
					pluginObjectType = pluginNode.Attributes.GetNamedItem("objectType").InnerText;
					pluginType = pluginNode.Attributes.GetNamedItem("isRunnable").InnerText;
					object plugin = pluginTable[pluginName];
					((IPlugIn)plugin).Configure(this);
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo ,
						"PlugInManager.FillHashtable  " + "Configured plugin " + pluginName);
				}
				catch (Exception e)
				{
					string ErrorToLog;
					ErrorToLog = "PlugInManager.FillHashtable" + 
						" Could not configure " + pluginName +
						" \n Error is " + e.Message;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError ,
						ErrorToLog);

					throw e;

				}

			}

			foreach(XmlNode pluginNode in pluginList)
			{
				try
				{
					pluginName = pluginNode.Attributes.GetNamedItem("name").InnerText;
					pluginObjectType = pluginNode.Attributes.GetNamedItem("objectType").InnerText;
					pluginType = pluginNode.Attributes.GetNamedItem("isRunnable").InnerText;
					object plugin = pluginTable[pluginName];
					if(pluginType == "true" )
					{
						((IRunnable)plugin).Start();
						Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo ,
							"PlugInManager.FillHashtable  " + "started plugin " + pluginName);
					}
				}
				catch (Exception e)
				{
					string ErrorToLog;
					ErrorToLog = "PlugInManager.FillHashtable" + 
						" Could not start " + pluginName +
						" \n Error is " + e.Message;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceInfo ,
						ErrorToLog);

					this.Unload();
					throw e;

				}
			}

		}

		/// <summary>
		/// This function acts like the destructor for the collection 
		/// of plugins. It calls unload for all plugins. 
		/// </summary>
		public void Unload()
		{
			foreach(object key in pluginTable.Keys )
			{
				try
				{

					Debug.WriteLine("Unloading plugin " + key);
					((IPlugIn)pluginTable[key]).Unload();
				}
				catch (Exception ex)
				{
					string ErrorToLog;
					ErrorToLog = "PlugInManager.Unload" + 
						" Error Unloading " + key +
						" \n Error is " + ex.Message + "\n" + ex.StackTrace;
					Statics.IRUTrace(this, Statics.IRUTraceSwitch.TraceError ,
						ErrorToLog);

				   
					return;
			   
				}
			}
		}


		/// <summary>
		/// This function returns the section of the configuration file
		/// for which the name is specified. If the name is not found in
		/// the config file then it will throw error.
		/// </summary>
		/// <param name="sectionName">XML Node name</param>
		/// <returns>XML Node for the hive of name specified in parameter</returns>
		public XmlNode GetConfigurationSection(string sectionName)
		{
			string xpath;
			xpath = "//Section[@name=\"" + sectionName + "\"]";
			XmlNode xmlNode = XMLHelper.SelectSingleNode(
				configDoc.DocumentElement, xpath);
			return xmlNode;
		}



		/// <summary>
		/// This method is used by remoting client to stop a plugin
		/// by name. It is mandatory that there exists an entry in
		/// the plugin section for this plugin name.
		/// </summary>
		/// <param name="pluginName">Name of the plugin</param>
		/// <returns></returns>
		public  void StopPluginByName(string pluginName)
		{
         
			IRunnable runnablePlugin;
			runnablePlugin =  pluginTable[pluginName] as IRunnable;
			if (runnablePlugin!=null)
				runnablePlugin.Stop();

		}


						
	}
}






