using System;
using System.Configuration;
using System.Windows.Forms;

using RTSDotNETClient.WSRQ;

namespace RTSDotNETClient.TestClient
{
    public partial class ReconciliationQueryTab : UserControl
    {
        public ReconciliationQueryTab()
        {
            InitializeComponent();
        }

        private void ReconciliationQueryTab_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;
            lblCount.Text = "";
            InitTestData();
        }        

        private void InitTestData()
        {
#if DEBUG
            tbSender.Text = "RTSJAVA";
#endif
        }

        private void btnReconciliationQuery_Click(object sender, EventArgs e)
        {
            try
            {
                // clear UI
                Cursor.Current = Cursors.WaitCursor;
                btnReconciliationQuery.Enabled = false;
                lblCount.Text = "";
                dataGridView1.DataSource = null;

                Query query = new Query();
                query.Body.SentTime = DateTime.Now;
                query.Body.QueryType = QueryType.AllNewRequests;
                query.SenderDocumentVersion = "nico";                

                ReconciliationClient reconciliationClient = new ReconciliationClient();
                reconciliationClient.WebServiceUrl = Global.ReconciliationWSUrl;
                reconciliationClient.PublicCertificate = EncryptionHelper.GetCertificateFromFile(Program.MainForm.CerFile);
                reconciliationClient.PrivateCertificate = EncryptionHelper.GetCertificateFromFile(Program.MainForm.PfxFile);
                Response response = reconciliationClient.DownloadReconciliationRequests(tbSender.Text, query);

                // update UI
                dataGridView1.DataSource = response.Body.RequestRecords;
                lblCount.Text = response.Body.NumberOfRecords.ToString();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnReconciliationQuery.Enabled = true;
            }
        }


    }
}
