using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Collections.Specialized;
using System.Xml;

namespace AppConfChangeCustomAction
{
	/// <summary>
	/// Summary description for AppConfigChangeCA.
	/// </summary>
	[RunInstaller(true)]
	public class AppConfigChangeCA : System.Configuration.Install.Installer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AppConfigChangeCA()
		{
			// This call is required by the Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public override void Commit(IDictionary savedState)
		{
			base.Commit(savedState);
			if(this.Context==null)
			{
				return;
			}
			System.IO.StreamWriter installlog = new System.IO.StreamWriter ( "Installation_" + this.Context.Parameters["ProductName"]   +".log");

			installlog.AutoFlush = true;
			#region extract config file name 

			StringDictionary sdParameters = this.Context.Parameters;

			string sAppConfigFileName = sdParameters["AppConfigFile"];

			if (sAppConfigFileName==null)
			{
				throw new InstallException("AppConfigFile: value not set to name of app config file in CustomActionData. Should be web.config for web projects.");
			}

			string sTargetPath = sdParameters["target"] ; //will have trailing slash

			if (sTargetPath==null)
			{
				throw new InstallException("target: value not set to /target=\"[TARGETDIR]\\\" in CustomActionData");
			}

		

			string pluginConfigFile =  sdParameters["PluginConfigFile"];

			if (pluginConfigFile==null)
			{
				throw new InstallException("PluginConfigFile: value not set in CustomActionData");
			}

			string traceLogFile =  sdParameters["TraceLogFile"];

			if (traceLogFile==null)
			{
				throw new InstallException("TraceLogFile: value not set in CustomActionData");
			}


			
			 string pluginConfigFilePath =sTargetPath + pluginConfigFile; 
			 string sAppConfigFilePath  = sTargetPath + sAppConfigFileName;
			 string traceLogFilePath  = sTargetPath + traceLogFile;


			#endregion
			
		

		
			try
			{
      
				installlog.WriteLine("Starting Edit of the config file");
				
				//' Loads the config file into the XML DOM.
				System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();



				xmlDocument.Load(sAppConfigFilePath);

				#region set the config file
       
				bool FoundIt = false;
				foreach (System.Xml.XmlNode xNode in xmlDocument["configuration"]["appSettings"])
				{
					//' Skips any comments.
					if (xNode.Name == "add")
					{
						if (xNode.Attributes.GetNamedItem("key").Value == "ConfigXMLFile") 
						{
							xNode.Attributes.GetNamedItem("value").Value = pluginConfigFilePath;
							FoundIt = true;
						}
					}
				}

				if ( !FoundIt )
					throw new InstallException("Config file appSettings did not contain a ConfigXMLFile key ");

				#endregion

				#region Log File
				FoundIt = false;
				foreach (System.Xml.XmlNode xNode in xmlDocument["configuration"]["system.diagnostics"]["trace"]["listeners"])
				{
					//' Skips any comments.
					if (xNode.Name == "add")
					{
						if (xNode.Attributes.GetNamedItem("type").Value == "System.Diagnostics.TextWriterTraceListener") 
						{
							xNode.Attributes.GetNamedItem("initializeData").Value = traceLogFilePath;
							FoundIt = true;
						}
					}
				}

			
				#endregion

         
				// Writes out the new config file.
				xmlDocument.Save(sAppConfigFilePath);
			}
			finally
			{
   
				installlog.WriteLine("Ending edit of config file");
				installlog.Close();
			}

		



			//===================




		}
		

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
