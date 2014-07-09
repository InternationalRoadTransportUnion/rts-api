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
  
	public class frmQueryXMLGeneratorWSRQ : System.Windows.Forms.Form
    {
        private IContainer components;

		private bool bTemplateFileMissing;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.SaveFileDialog sfd; 

		private DataSet dsXML;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.OpenFileDialog ofd;
        private TextBox SentTime;
        private Label label3;
        private TextBox Password;
        private string m_LoadedFileName;
        
		public frmQueryXMLGeneratorWSRQ()
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

			string sQueryTemplate = sAppPath+ "\\"+"WSRQueryDataTemplate.xml";

			if (!File.Exists(sQueryTemplate))
			{
				MessageBox.Show("WSRQueryDataTemplate.xml file not found in the application folder \r\n: " + Application.StartupPath);
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.SentTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
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
            this.groupBox1.Location = new System.Drawing.Point(0, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 348);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "-";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(253, 75);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(144, 75);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
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
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.Password);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(368, 93);
            this.pnlMain.TabIndex = 20;
            // 
            // SentTime
            // 
            this.SentTime.Location = new System.Drawing.Point(144, 12);
            this.SentTime.Name = "SentTime";
            this.SentTime.Size = new System.Drawing.Size(184, 20);
            this.SentTime.TabIndex = 1;
            this.SentTime.Text = "textBox1";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 28;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 27;
            this.label2.Text = "Sent Time";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(144, 52);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(184, 20);
            this.Password.TabIndex = 29;
            this.Password.Text = "textBox1";
            this.Password.UseSystemPasswordChar = true;
            this.Password.Enter += new System.EventHandler(this.txtClick_1);
            // 
            // ofd
            // 
            this.ofd.DefaultExt = "xml";
            this.ofd.Title = "Select Query XML file to Open";
            // 
            // frmQueryXMLGeneratorWSRQ
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(368, 441);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "frmQueryXMLGeneratorWSRQ";
            this.Text = "Query XML File  Generator";
            this.groupBox1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
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
			string sQueryTemplate = sAppPath+"\\"+"WSRQueryDataTemplate.xml";

			dsXML.ReadXml(sQueryTemplate,XmlReadMode.InferSchema);
            		 
			SetDataBindings();
			QueryFileName = "WSRQNewQueryDataMessage.xml";
            
		}

		private void SetDataBindings()
		{
            ClearBindings();
            //SubscriberID.DataBindings.Add("Text", dsXML.Tables[1], "SubscriberID");
            try
            {
                SentTime.DataBindings.Add("Text", dsXML.Tables["Body"], "SentTime");
                if( dsXML.Tables["Body"].Columns.Contains("Password"))
                {
                    Password.DataBindings.Add("Text", dsXML.Tables["Body"], "Password");
                }

                dsXML.Tables["Body"].Rows[0]["QueryType"] = 1;
            }
            catch (IOException io)
            {
            }
            //Information_Exchange_Version.DataBindings.Add("Text", dsXML.Tables[1], "Information_Exchange_Version");

			

		
		}
		private void ClearBindings()
		{
			try
			{
				//clear bindings may cause exception
                SentTime.DataBindings.RemoveAt(0);
			    Password.DataBindings.RemoveAt(0);
              //Query_Type.DataBindings.RemoveAt(0);
                //Sender_Document_Version.DataBindings.RemoveAt(0);
                //Information_Exchange_Version.DataBindings.RemoveAt(0);
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
            try
            {
                if(dsXML.Tables["Body"].Rows[0]["Password"] == null || dsXML.Tables["Body"].Rows[0]["Password"].ToString().Trim() == "")
                {
                    dsXML.Tables["Body"].Columns.Remove("Password"); 
                }
            }
            catch
            {
            }
			
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
            sData = sData.Replace("http://www.iru.org/WSRQuery", "http://www.iru.org/SafeTIRReconciliation");

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
            if (dsXML.Tables.Contains("Body"))
            {
                if (!(dsXML.Tables["Body"].Columns.Contains("Password")))
                {
                    dsXML.Tables["Body"].Columns.Add("Password");
                }
            }
			QueryFileName = ofd.FileName;



		
			SetDataBindings();

		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmQueryXMLGeneratorWSRQ_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			TestParent.m_MesseageFileName=QueryFileName;
        }

        private void txtClick_1(object sender, EventArgs e)
        {

            if (dsXML.Tables.Contains("Body"))
            {
                if (!(dsXML.Tables["Body"].Columns.Contains("Password")))
                {
                    dsXML.Tables["Body"].Columns.Remove("QueryType");
                    dsXML.Tables["Body"].Columns.Add("Password");
                    dsXML.Tables["Body"].Columns.Add("QueryType");
                    SetDataBindings();
                }
            }
        }

		
	}
}
