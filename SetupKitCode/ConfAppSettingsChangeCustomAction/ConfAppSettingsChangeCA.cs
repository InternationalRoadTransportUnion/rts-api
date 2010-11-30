using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Install;
using System.Xml;
using System.IO;

namespace WebConfChangeCustomAction
{
	/// <summary>
	/// Summary description for WebConfChangeCA.
	/// </summary>
	[RunInstaller(true)]
	public class ConfAppSettingsChangeCA : System.Configuration.Install.Installer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ConfAppSettingsChangeCA()
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
			base.Commit (savedState);


			
			System.IO.StreamWriter installlog = new System.IO.StreamWriter ( "Installation_" + this.Context.Parameters["ProductName"] + ".log");
			installlog.AutoFlush=true;

			StringDictionary sdParameters = this.Context.Parameters;


			bool bIsWebConfig = false;
			
			string sIsWebConfig = sdParameters["IsWeb"];

			if (sIsWebConfig!=null)
			{
				if (sIsWebConfig=="true")
					bIsWebConfig=true;
			}

			System.IO.FileInfo fileInfo ;
			string traceLogFilePath = null; 
						
			if (bIsWebConfig==false)
			{
				#region extract config file name 

				

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

		
				string traceLogFile =  sdParameters["TraceLogFile"];

				if (traceLogFile!=null)
				{
					traceLogFilePath  = sTargetPath + traceLogFile;
				}
				string sAppConfigFilePath  = sTargetPath + sAppConfigFileName;
				
				fileInfo = new System.IO.FileInfo(sAppConfigFilePath );

			if (!fileInfo.Exists)
			{
				throw new InstallException("Missing config file :" + fileInfo.FullName );
			}



				#endregion
			}
			else
			{

				#region Figure out the Config file path
				//' Uses reflection to find the location of the config file.
				System.Reflection.Assembly Asm=  System.Reflection.Assembly.GetExecutingAssembly();
				string strConfigLoc ;
				strConfigLoc = Asm.Location;

				string strTemp;
				strTemp = strConfigLoc;
				installlog.WriteLine("Assembly location " + strTemp);
				strTemp = strTemp.Remove(strTemp.LastIndexOf("\\"), strTemp.Length - strTemp.LastIndexOf("\\"));
				strTemp = strTemp.Remove(strTemp.LastIndexOf("\\"), strTemp.Length - strTemp.LastIndexOf("\\"));

				installlog.WriteLine("Before adding web.config to path:" + strTemp);
				fileInfo = new System.IO.FileInfo(strTemp +"\\" + "web.config" );

				installlog.WriteLine("File info: " + strTemp);

				if( !fileInfo.Exists)
				{
					throw new InstallException("Missing config file :" + fileInfo.FullName );
				}

				#endregion

			}
			XmlDocument xConfigDoc = new XmlDocument();
			xConfigDoc.Load(fileInfo.FullName);

			#region Loop through all keys in app.config 

			//loop through all appsettings if we find parameter with same name then replace
			foreach (System.Xml.XmlNode xNode in xConfigDoc["configuration"]["appSettings"])
			{
				//' Skips any comments.
				if (xNode.Name == "add")
				{
					
					string sKey =xNode.Attributes.GetNamedItem("key").Value;

					string sParameterValue = this.Context.Parameters[sKey];

					if (sParameterValue != null)
					{
					xNode.Attributes.GetNamedItem("value").Value =sParameterValue;
					
					}

					
				}
			}
			#endregion


			#region replace the trace log file

			//Full path is expected including the filename
			traceLogFilePath =  sdParameters["TraceLogFilePath"];

			if (traceLogFilePath!=null)
			{
				
				foreach (System.Xml.XmlNode xNode in xConfigDoc["configuration"]["system.diagnostics"]["trace"]["listeners"])
				{
					//' Skips any comments.
					if (xNode.Name == "add")
					{
						if (xNode.Attributes.GetNamedItem("type").Value == "System.Diagnostics.TextWriterTraceListener") 
						{
							xNode.Attributes.GetNamedItem("initializeData").Value = traceLogFilePath;

						}
					}
				}
				
			}
			#endregion

			xConfigDoc.Save(fileInfo.FullName);
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
