using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

using System.Windows.Forms;
using System.Collections.Specialized;

namespace PluginConfigEditCustomAction
{
	/// <summary>
	/// Summary description for ServiceCA.
	/// </summary>
	[RunInstaller(true)]
	public class PluginConfigEditCA : System.Configuration.Install.Installer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PluginConfigEditCA()
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

//////		public override void Commit(IDictionary savedState)
//////		{
//////			MessageBox.Show("heluuu commiitiing");
//////			base.Commit (savedState);
//////			try
//////			{
//////				if(this.Context!=null)
//////				{
//////										StringDictionary parameters = Context.Parameters;
//////										string[] keys =new string[parameters.Count];
//////										parameters.Keys.CopyTo(keys,0); 
//////					
//////										// Set the StateServer collection values
//////										for(int intKeys=0;intKeys<keys.Length;intKeys++)
//////										{
//////								
////////											if(keys[intKeys].Equals("target"))
////////											{
//////											MessageBox.Show("Commit Key/val:" +keys[intKeys]+ "::/::" +  parameters[keys[intKeys]].ToString());
////////											}
//////										}
//////					MessageBox.Show("Commit Context Not null") ;
//////				}
//////				else
//////				{
//////					MessageBox.Show("Commit Context null") ;
//////				}
//////			}
//////			catch (Exception exx)
//////			{
//////				MessageBox.Show( "Commit" + exx.Message + exx.StackTrace);
//////			}
//////
//////			MessageBox.Show("heluuu commiitiing");
//////		}
//////
//////
		
		
		public override void Commit(IDictionary savedState)
		{
			base.Commit(savedState);
			if(this.Context==null)
			{
				return;
			}

			//create form
			ConfigEditor frmCE = new ConfigEditor();
			#region create ConfigFilePath
			StringDictionary parameters = Context.Parameters;
			string sTargetPath = parameters["target"] ; //will have trailing slash

			if (sTargetPath==null)
			{
				throw new InstallException("target: value not set to /target=\"[TARGETDIR]\\\" in CustomActionData");
			}

			string pluginConfigFile =  parameters["PluginConfigFile"];

			if (pluginConfigFile==null)
			{
				throw new InstallException("PluginConfigFile: value not set in CustomActionData");
			}

			pluginConfigFile  = sTargetPath + pluginConfigFile;
			#endregion

			#region create template file path

			string pluginEditTemplateFile =  parameters["PluginEditTemplateFile"];

			if (pluginEditTemplateFile==null)
			{
				throw new InstallException("PluginEditTemplateFile: value not set in CustomActionData");
			}


			pluginEditTemplateFile  = sTargetPath + pluginEditTemplateFile;
			#endregion
		
			#region set properties of the control
			
			frmCE.configTabHost1.ConfigFilePath=pluginConfigFile;
			frmCE.configTabHost1.EditTemplateFilePath=pluginEditTemplateFile;

			frmCE.configTabHost1.Context = this.Context.Parameters;
			frmCE.configTabHost1.DisplayFileEditor();

			frmCE.ShowDialog();

			#endregion
		}
		
		
////		public override void Install(IDictionary stateSaver)
////		{
////			base.Install (stateSaver);
////
////			
////			MessageBox.Show("heluuu");
////		}


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
