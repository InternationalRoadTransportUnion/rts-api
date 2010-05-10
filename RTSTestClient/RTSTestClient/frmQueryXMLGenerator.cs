using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.IO;

namespace RTSTestClient
{
	/// <summary>
	/// Summary description for frmQueryXMLGenerator.
	/// </summary>
	public class frmQueryXMLGenerator : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private bool bTemplateFileMissing;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.SaveFileDialog sfd; 

		private DataSet dsXML;
		private System.Windows.Forms.Button cmdSave;
		private System.Windows.Forms.TextBox SentTime;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox Carnet_Number;
		private System.Windows.Forms.TextBox Query_Reason;
		private System.Windows.Forms.TextBox Query_Type;
		private System.Windows.Forms.TextBox Password;
		private System.Windows.Forms.TextBox OriginTime;
		private System.Windows.Forms.TextBox Originator;
		private System.Windows.Forms.TextBox Sender;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.OpenFileDialog ofd;

		private string m_LoadedFileName;

		public frmQueryXMLGenerator()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			//Load the template file in DataSet
			
			bTemplateFileMissing=true;
			string sAppPath = Application.ExecutablePath;
			sAppPath= Path.GetDirectoryName(sAppPath);

			string sQueryTemplate = sAppPath+"\\"+"QueryTemplate.xml";

			if (!File.Exists(sQueryTemplate))
			{
				MessageBox.Show("QueryTemplate.xml file not found in the application folder \r\n: " + Application.StartupPath);
				bTemplateFileMissing=true;
			}
			menuItem1_Click(null,null);// call the new message click;
			
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdSave = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.SentTime = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Carnet_Number = new System.Windows.Forms.TextBox();
			this.Query_Reason = new System.Windows.Forms.TextBox();
			this.Query_Type = new System.Windows.Forms.TextBox();
			this.Password = new System.Windows.Forms.TextBox();
			this.OriginTime = new System.Windows.Forms.TextBox();
			this.Originator = new System.Windows.Forms.TextBox();
			this.Sender = new System.Windows.Forms.TextBox();
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			this.groupBox1.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmdCancel);
			this.groupBox1.Controls.Add(this.cmdSave);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 369);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(368, 72);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "-";
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(280, 32);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdSave
			// 
			this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSave.Location = new System.Drawing.Point(184, 32);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.TabIndex = 0;
			this.cmdSave.Text = "Save";
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "New Query";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Load File";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// pnlMain
			// 
			this.pnlMain.AutoScroll = true;
			this.pnlMain.Controls.Add(this.SentTime);
			this.pnlMain.Controls.Add(this.label8);
			this.pnlMain.Controls.Add(this.label7);
			this.pnlMain.Controls.Add(this.label6);
			this.pnlMain.Controls.Add(this.label5);
			this.pnlMain.Controls.Add(this.label4);
			this.pnlMain.Controls.Add(this.label3);
			this.pnlMain.Controls.Add(this.label2);
			this.pnlMain.Controls.Add(this.label1);
			this.pnlMain.Controls.Add(this.Carnet_Number);
			this.pnlMain.Controls.Add(this.Query_Reason);
			this.pnlMain.Controls.Add(this.Query_Type);
			this.pnlMain.Controls.Add(this.Password);
			this.pnlMain.Controls.Add(this.OriginTime);
			this.pnlMain.Controls.Add(this.Originator);
			this.pnlMain.Controls.Add(this.Sender);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 0);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(368, 369);
			this.pnlMain.TabIndex = 20;
			// 
			// SentTime
			// 
			this.SentTime.Location = new System.Drawing.Point(144, 56);
			this.SentTime.Name = "SentTime";
			this.SentTime.Size = new System.Drawing.Size(184, 20);
			this.SentTime.TabIndex = 1;
			this.SentTime.Text = "textBox1";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 304);
			this.label8.Name = "label8";
			this.label8.TabIndex = 33;
			this.label8.Text = "Carnet_Number";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 264);
			this.label7.Name = "label7";
			this.label7.TabIndex = 32;
			this.label7.Text = "Query_Reason";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 224);
			this.label6.Name = "label6";
			this.label6.TabIndex = 31;
			this.label6.Text = "Query_Type";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 176);
			this.label5.Name = "label5";
			this.label5.TabIndex = 30;
			this.label5.Text = "Password";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 136);
			this.label4.Name = "label4";
			this.label4.TabIndex = 29;
			this.label4.Text = "Origin Time";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 88);
			this.label3.Name = "label3";
			this.label3.TabIndex = 28;
			this.label3.Text = "Originator";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.TabIndex = 27;
			this.label2.Text = "Sent Time";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 26;
			this.label1.Text = "Sender";
			// 
			// Carnet_Number
			// 
			this.Carnet_Number.Location = new System.Drawing.Point(144, 312);
			this.Carnet_Number.Name = "Carnet_Number";
			this.Carnet_Number.Size = new System.Drawing.Size(184, 20);
			this.Carnet_Number.TabIndex = 7;
			this.Carnet_Number.Text = "textBox1";
			// 
			// Query_Reason
			// 
			this.Query_Reason.Location = new System.Drawing.Point(144, 272);
			this.Query_Reason.Name = "Query_Reason";
			this.Query_Reason.Size = new System.Drawing.Size(184, 20);
			this.Query_Reason.TabIndex = 6;
			this.Query_Reason.Text = "textBox1";
			// 
			// Query_Type
			// 
			this.Query_Type.Location = new System.Drawing.Point(144, 232);
			this.Query_Type.Name = "Query_Type";
			this.Query_Type.Size = new System.Drawing.Size(184, 20);
			this.Query_Type.TabIndex = 5;
			this.Query_Type.Text = "textBox1";
			// 
			// Password
			// 
			this.Password.Location = new System.Drawing.Point(144, 192);
			this.Password.Name = "Password";
			this.Password.Size = new System.Drawing.Size(184, 20);
			this.Password.TabIndex = 4;
			this.Password.Text = "textBox1";
			// 
			// OriginTime
			// 
			this.OriginTime.Location = new System.Drawing.Point(144, 144);
			this.OriginTime.Name = "OriginTime";
			this.OriginTime.Size = new System.Drawing.Size(184, 20);
			this.OriginTime.TabIndex = 3;
			this.OriginTime.Text = "textBox1";
			// 
			// Originator
			// 
			this.Originator.Location = new System.Drawing.Point(144, 96);
			this.Originator.Name = "Originator";
			this.Originator.Size = new System.Drawing.Size(184, 20);
			this.Originator.TabIndex = 2;
			this.Originator.Text = "textBox1";
			// 
			// Sender
			// 
			this.Sender.Location = new System.Drawing.Point(144, 16);
			this.Sender.Name = "Sender";
			this.Sender.Size = new System.Drawing.Size(184, 20);
			this.Sender.TabIndex = 0;
			this.Sender.Text = "textBox1";
			// 
			// ofd
			// 
			this.ofd.DefaultExt = "xml";
			this.ofd.Title = "Select Query XML file to Open";
			// 
			// frmQueryXMLGenerator
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(368, 441);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.Menu = this.mainMenu1;
			this.Name = "frmQueryXMLGenerator";
			this.Text = "Query XML File  Generator";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmQueryXMLGenerator_Closing);
			this.groupBox1.ResumeLayout(false);
			this.pnlMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdLoadTemplate_Click(object sender, System.EventArgs e)
		{
			
		
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			dsXML = new DataSet();

			bTemplateFileMissing=true;
			string sAppPath = Application.ExecutablePath;
			sAppPath= Path.GetDirectoryName(sAppPath);
			string sQueryTemplate = sAppPath+"\\"+"QueryTemplate.xml";

			//dsXML.ReadXml("QueryTemplate.xml",XmlReadMode.InferSchema);
			dsXML.ReadXml(sQueryTemplate,XmlReadMode.InferSchema);
			 
			SetDataBindings();
			QueryFileName = "NewQueryMessage.xml";
		}

		private void SetDataBindings()
		{
			ClearBindings();

			Sender.DataBindings.Add("Text",dsXML.Tables[1],"Sender");
			SentTime.DataBindings.Add("Text",dsXML.Tables[1],"SentTime");
			Originator.DataBindings.Add("Text",dsXML.Tables[1],"Originator");
			OriginTime.DataBindings.Add("Text",dsXML.Tables[1],"OriginTime");
			Password.DataBindings.Add("Text",dsXML.Tables[1],"Password");
			Query_Type.DataBindings.Add("Text",dsXML.Tables[1],"Query_Type");
			Query_Reason.DataBindings.Add("Text",dsXML.Tables[1],"Query_Reason");
			Carnet_Number.DataBindings.Add("Text",dsXML.Tables[1],"Carnet_Number");

			

		
		}
		private void ClearBindings()
		{
			try
			{
				//clear bindings may cause exception
				Sender.DataBindings.RemoveAt(0);
				SentTime.DataBindings.RemoveAt(0);
				Originator.DataBindings.RemoveAt(0);
				OriginTime.DataBindings.RemoveAt(0);
				Password.DataBindings.RemoveAt(0);
				Query_Type.DataBindings.RemoveAt(0);
				Query_Reason.DataBindings.RemoveAt(0);
				Carnet_Number.DataBindings.RemoveAt(0);
			}
			catch (Exception bex)
			{
				//swallowed 
			}
			

		
		
		}

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			sfd.Filter="XML Files (*.xml)|*.xml";
			sfd.Title="Select Folder and enter File Name to save the message XML file";
			sfd.ValidateNames=true;

			sfd.DefaultExt= ".xml";
			sfd.FileName=QueryFileName;
			DialogResult dr = sfd.ShowDialog(this);
			if (dr!=DialogResult.OK)
				return;

			string fileName = sfd.FileName;
			
			dsXML.WriteXml(fileName,XmlWriteMode.IgnoreSchema );
			//get rid of the opening xml version 
			StreamReader sr = null;// = new StreamReader(fileName);

			string sData;
			try
			{
				sr = new StreamReader(fileName);
				sData = sr.ReadToEnd();
			}
			finally
			{
				sr.Close();
			}
			
			//get rid of processing instructions
			sData = sData.Replace("<?xml version=\"1.0\" standalone=\"yes\"?>\r\n","");

			StreamWriter sw = null;
			try
			{
				sw = new StreamWriter(fileName,false);
				sw.Write(sData);
			}
			finally
			{
				sw.Close();
			}
			
			QueryFileName= fileName;


		}

		private string QueryFileName
		{
			get
			{
				return m_LoadedFileName;
			}

			set
			{
				m_LoadedFileName =value;
				this.Text= "Query XML File " + m_LoadedFileName;	
			
			}
		
		
		}
		private void menuItem2_Click(object sender, System.EventArgs e)
		{

			//load file
			ofd.Filter="XML Files (*.xml)|*.xml";
			DialogResult dr = ofd.ShowDialog();

			if (dr!=DialogResult.OK)
				return;

			dsXML= new DataSet();
			dsXML.ReadXml(ofd.FileName,XmlReadMode.InferSchema);
			QueryFileName = ofd.FileName;



		
			SetDataBindings();

		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmQueryXMLGenerator_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			TestParent.m_MesseageFileName=QueryFileName;
		}

		
	}
}
