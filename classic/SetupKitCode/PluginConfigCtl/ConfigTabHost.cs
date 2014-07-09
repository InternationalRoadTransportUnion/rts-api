	using System;
using System.Collections;
	using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml.XPath;
using System.Xml;
using System.IO;

namespace IRU.Common.Config.PluginConfigCtl
{
	/// <summary>
	/// Summary description for ConfigTabHost.
	/// </summary>
	public class ConfigTabHost : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TabControl tbMain;
		private System.Windows.Forms.GroupBox gpBorder;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		/// <summary>
		/// Used to pass in the CustomActionData parameters
		/// </summary>
		private  StringDictionary m_IContext;

		/// <summary>
		/// Dom containing the actual xml config file
		/// </summary>
		XmlDocument m_xDocConfig ;

		public ConfigTabHost()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.gpBorder = new System.Windows.Forms.GroupBox();
            this.tbMain = new System.Windows.Forms.TabControl();
            this.gpBorder.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpBorder
            // 
            this.gpBorder.Controls.Add(this.tbMain);
            this.gpBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpBorder.Location = new System.Drawing.Point(0, 0);
            this.gpBorder.Name = "gpBorder";
            this.gpBorder.Size = new System.Drawing.Size(336, 205);
            this.gpBorder.TabIndex = 0;
            this.gpBorder.TabStop = false;
            // 
            // tbMain
            // 
            this.tbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMain.Location = new System.Drawing.Point(3, 16);
            this.tbMain.Name = "tbMain";
            this.tbMain.SelectedIndex = 0;
            this.tbMain.Size = new System.Drawing.Size(330, 186);
            this.tbMain.TabIndex = 0;
            // 
            // ConfigTabHost
            // 
            this.Controls.Add(this.gpBorder);
            this.Name = "ConfigTabHost";
            this.Size = new System.Drawing.Size(336, 205);
            this.gpBorder.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Path to the Plugin Config File
		/// </summary>
		public string ConfigFilePath
		{
			get
			{
				return m_ConfigFilePath;
			}
			set
			{
				m_ConfigFilePath = value;
				gpBorder.Text=value;
			

			}
		}

		/// <summary>
		/// Path to the Plugin Config File
		/// </summary>
		public  StringDictionary Context
		{
			get
			{
				return m_IContext;
			}
			set
			{
				m_IContext = value;
				
			}
		}

		/// <summary>
		/// Path to the template XML file
		/// </summary>
		public string EditTemplateFilePath
		{
			get
			{
				return m_EditTemplateFilePath;
			}
			set
			{
				m_EditTemplateFilePath= value;
			}
		}

		private string m_EditTemplateFilePath;
		private string m_ConfigFilePath;

		public void DisplayFileEditor()
		{
			#region load template
			XmlDocument xDocTemplate = new XmlDocument();

			xDocTemplate.Load(m_EditTemplateFilePath);
#endregion

			#region load config
			m_xDocConfig = new XmlDocument();

			m_xDocConfig.Load(m_ConfigFilePath);

				
			#endregion

			XmlNodeList xNodeList = xDocTemplate.SelectNodes("//Tabs/Tab");

			m_aSectionHandlers = new SectionHandler[xNodeList.Count];

			int nSectionCount=-1;
			tbMain.SuspendLayout();
			tbMain.TabPages.Clear();

			#region layout
			foreach (XmlNode xTabNode in xNodeList)
			{
				nSectionCount++;

				SectionHandler thisSection = new SectionHandler(this.tbMain , xTabNode, m_xDocConfig.DocumentElement.FirstChild, m_IContext);
				m_aSectionHandlers[nSectionCount]= thisSection;
				thisSection.DisplayTab();
			}

			
			tbMain.ResumeLayout(true);
		
			#endregion
		
		
		}
		SectionHandler[] m_aSectionHandlers;

		public void SaveFile()
		{
		
			foreach (SectionHandler thisSection in m_aSectionHandlers)
				thisSection.SaveToDOM();
		
			FileInfo fConfig = new FileInfo(ConfigFilePath);

			#region create backup of original file

			//delete of backup file exists
			FileInfo fBackConfig = new FileInfo(ConfigFilePath+".installbak");

			if (fBackConfig.Exists)
			{
				//set attributes and delete

				fBackConfig.Attributes = System.IO.FileAttributes.Normal;
				fBackConfig.Delete();
			}
			

			fConfig.MoveTo(ConfigFilePath+".installbak");
			#endregion
			
			//start with original file

			m_xDocConfig.Save(ConfigFilePath);
			


			
		}

	}
}
