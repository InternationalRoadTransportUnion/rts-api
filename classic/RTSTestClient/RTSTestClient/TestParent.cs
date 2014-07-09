using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace RTSTestClient
{
	/// <summary>
	/// Summary description for TestParent.
	/// </summary>
	public class TestParent : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem2;

        //lata added for WSRQ testing on 24 sept,2007
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem menuItem16;

		// Used to pass variables between forms
		static internal string m_MesseageFileName;
		static internal string m_IRUCertFilePath;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem11;
        private MenuItem menuItem17;
        private MenuItem menuItem18;
        private MenuItem menuItem19;
        private MenuItem menuItem20;
        private MenuItem menuItem22;
        private MenuItem menuItem23;
        private MenuItem menuItem24;
        private MenuItem menuItem21;
        private MenuItem menuItem25;
        private MenuItem menuItem26;
        private MenuItem menuItem27;
        private MenuItem menuItem28;
        private IContainer components;

		public TestParent()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem1,
            this.menuItem8,
            this.menuItem12,
            this.menuItem17,
            this.menuItem22,
            this.menuItem21});
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
            this.menuItem6.Text = "File";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Exit";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem5,
            this.menuItem3,
            this.menuItem4,
            this.menuItem7});
            this.menuItem1.Text = "RTS Query Testing";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 0;
            this.menuItem5.Text = "Generate Query XML";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "Generate Enc. Message";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "Call WS";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 3;
            this.menuItem7.Text = "Stress Test";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 2;
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem10,
            this.menuItem9,
            this.menuItem11});
            this.menuItem8.Text = "SafeTirUploadTesting";
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 0;
            this.menuItem10.Text = "Generate Enc Message";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.menuItem9.Text = "Call WS";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 2;
            this.menuItem11.Text = "Stress Test";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 3;
            this.menuItem12.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem13,
            this.menuItem14,
            this.menuItem15,
            this.menuItem16});
            this.menuItem12.Text = "WSRQTesting";
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 0;
            this.menuItem13.Text = "Generate Query XML";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 1;
            this.menuItem14.Text = "Generate Enc. Message";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 2;
            this.menuItem15.Text = "Call WS";
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 3;
            this.menuItem16.Text = "Stress Test";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 4;
            this.menuItem17.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem18,
            this.menuItem19,
            this.menuItem20});
            this.menuItem17.Text = "WSRETesting";
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 0;
            this.menuItem18.Text = "Generate Query XML";
            this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 1;
            this.menuItem19.Text = "Generate Enc. Message";
            this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 2;
            this.menuItem20.Text = "Call WS";
            this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 5;
            this.menuItem22.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem24,
            this.menuItem23});
            this.menuItem22.Text = "WSRQ New Request Upload Testing";
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 0;
            this.menuItem24.Text = "Generate Request XML";
            this.menuItem24.Click += new System.EventHandler(this.menuItem24_Click);
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 1;
            this.menuItem23.Text = "Upload File /Call FileListener";
            this.menuItem23.Visible = false;
            this.menuItem23.Click += new System.EventHandler(this.menuItem23_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 6;
            this.menuItem21.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem26,
            this.menuItem25,
            this.menuItem27,
            this.menuItem28});
            this.menuItem21.Text = "BLR-EPD";
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 0;
            this.menuItem26.Text = "Generate Encrypted Message";
            this.menuItem26.Click += new System.EventHandler(this.menuItem26_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 1;
            this.menuItem25.Text = "SendMessage";
            this.menuItem25.Click += new System.EventHandler(this.menuItem25_Click);
            // 
            // menuItem27
            // 
            this.menuItem27.Index = 2;
            this.menuItem27.Text = "Stress Test";
            this.menuItem27.Click += new System.EventHandler(this.menuItem27_Click);
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 3;
            this.menuItem28.Text = "G2BTest";
            this.menuItem28.Click += new System.EventHandler(this.menuItem28_Click);
            // 
            // TestParent
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(816, 338);
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu1;
            this.Name = "TestParent";
            this.Text = "Test Client";
            this.Closed += new System.EventHandler(this.TestParent_Closed);
            this.ResumeLayout(false);

		}
		#endregion

        [STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new TestParent());
		
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			TCHQSampleMessageGenerator smg = new TCHQSampleMessageGenerator();
			smg.m_RequestNSURN="http://www.iru.org/TCHQuery";

			smg.MdiParent=this;
			smg.Show();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			TCHQTest tt = new TCHQTest();
			tt.MdiParent=this;
			tt.Show();
		}

        private void TestParent_Closed(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			frmQueryXMLGenerator frmQmsg = new frmQueryXMLGenerator();
			frmQmsg.MdiParent=this;
			frmQmsg.Show();
		}


		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			WSRQStressTester tc = new WSRQStressTester();
			tc.m_RequestNSURN="http://www.iru.org/TCHQuery";

			tc.MdiParent=this;
			tc.Show();
		}

      
		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			WSSTTest tt = new WSSTTest();
			tt.MdiParent=this;
			tt.Show();
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			TCHQSampleMessageGenerator smg = new TCHQSampleMessageGenerator();
			smg.m_RequestNSURN="http://www.iru.org/SafeTIRUpload";
			smg.MdiParent=this;
			smg.Show();
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			WSSTStressTester tc = new WSSTStressTester();
			tc.m_RequestNSURN="http://www.iru.org/SafeTIRUpload";
			tc.MdiParent=this;
			tc.Show();

		}

        //Lata Added for WSRQ testing on 24th Sept,2007


        private void menuItem13_Click(object sender, System.EventArgs e)
        {
            frmQueryXMLGeneratorWSRQ wsrQmsg = new frmQueryXMLGeneratorWSRQ();
            wsrQmsg.MdiParent = this;
            wsrQmsg.Show();
        }

        private void menuItem14_Click(object sender, System.EventArgs e)
        {
            WSRQSampleMessageGenerator smg = new WSRQSampleMessageGenerator();
            smg.m_RequestNSURN = "http://www.iru.org/SafeTIRReconciliation";

            smg.MdiParent = this;
            smg.Show();
         
        }

        private void menuItem15_Click(object sender, System.EventArgs e)
        {
            WSRQTest tt = new WSRQTest();
            tt.MdiParent = this;
            tt.Show();
        }

        private void menuItem16_Click(object sender, System.EventArgs e)
        {
            WSRQStressTester tc = new WSRQStressTester();
            tc.m_RequestNSURN = "http://www.iru.org/SafeTIRReconciliation";
            tc.MdiParent = this;
            tc.Show();
        }


        private void menuItem18_Click(object sender, System.EventArgs e)
        {
            frmQueryXMLGeneratorWSRE wsrEmsg = new frmQueryXMLGeneratorWSRE();
            wsrEmsg.MdiParent = this;
            wsrEmsg.Show();

        }

        private void menuItem20_Click(object sender, System.EventArgs e)
        {
            WSRETest wsre = new WSRETest();
            wsre.MdiParent = this;
            wsre.Show();
        }

        private void menuItem19_Click(object sender, System.EventArgs e)
        {
            WSRESampleMessageGenerator smg = new WSRESampleMessageGenerator();
            smg.m_RequestNSURN = "http://www.iru.org/SafeTIRUpload";
            smg.MdiParent = this;
            smg.Show();
        }

        private void menuItem21_Click(object sender, System.EventArgs e)
        {
            WSREStressTester wst = new WSREStressTester();
            wst.m_RequestNSURN = "http://www.iru.org/SafeTIRUpload";
            wst.MdiParent = this;
            wst.Show();

        }

        private void menuItem23_Click(object sender, System.EventArgs e)
        {
            WSRQNewRequestFileUpload wrfp = new WSRQNewRequestFileUpload();
            wrfp.MdiParent = this;
            wrfp.Show();

        }

        private void menuItem24_Click(object sender, EventArgs e)
        {
            frmQueryXMLGeneratorWSRQNR wsrqnrmsg = new frmQueryXMLGeneratorWSRQNR();
            wsrqnrmsg.MdiParent = this;
            wsrqnrmsg.Show();


        }

        private void menuItem25_Click(object sender, EventArgs e)
        {
            TIREPDB2GTest tt = new TIREPDB2GTest();
            tt.MdiParent = this;
            tt.Show();

        }

        private void menuItem26_Click(object sender, EventArgs e)
        {
            TIREPDB2GSampleMessageGenerator smg = new TIREPDB2GSampleMessageGenerator();
            smg.m_RequestNSURN = "http://www.iru.org/TIREPDB2G";

            smg.MdiParent = this;
            smg.Show();

        }

        private void menuItem27_Click(object sender, EventArgs e)
        {
            TIREPDB2GStressTester wst = new TIREPDB2GStressTester();
            wst.m_RequestNSURN = "http://www.iru.org/TIREPDB2G";
            wst.MdiParent = this;
            wst.Show();

        }

        private void menuItem28_Click(object sender, EventArgs e)
        {
            TIREPDG2BTest tt = new TIREPDG2BTest();
            tt.MdiParent = this;
            tt.Show();

        }

      
	}
}
