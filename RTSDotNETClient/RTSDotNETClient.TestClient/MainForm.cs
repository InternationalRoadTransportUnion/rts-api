using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Configuration;

using RTSDotNETClient.TCHQ;
using System.Diagnostics;

namespace RTSDotNETClient.TestClient
{
    public partial class MainForm : Form
    {        
        public string CerFile { get { return tbCerFile.Text; } }
        public string PfxFile { get { return tbPfxFile.Text; } }

        private string appDirectory;

        public MainForm()
        {
            InitializeComponent();
            Global.LoadConfiguration();
            this.appDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            InitTestData();
            InitTrace();
        }

        private void InitTrace()
        {
            if (Global.TraceEnabled)
                traceTab1.InitTrace();
            else
                tabControl.TabPages.Remove(tabTrace);
        }

        private void InitTestData()
        {
#if DEBUG
            tbPfxFile.Text = Path.Combine(appDirectory, "certificates\\RTSJAVA_recv.pfx");
            tbCerFile.Text = Path.Combine(appDirectory, "certificates\\RTSJAVA_send.cer");
#endif
        }

        private void btnCerFile_Click(object sender, EventArgs e)
        {
            if (openFileDialogCer.ShowDialog(this) == DialogResult.OK)
            {
                 tbCerFile.Text = openFileDialogCer.FileName;
            }
        }

        private void btnPfxFile_Click(object sender, EventArgs e)
        {
            if (openFileDialogPfx.ShowDialog(this) == DialogResult.OK)
            {
                tbPfxFile.Text = openFileDialogPfx.FileName;
            }
        }
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabTrace)
            {
                traceTab1.ScrollToBottom();
            }
        }
 

    }
}
