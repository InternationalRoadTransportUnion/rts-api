using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RTSDotNETClient.WSRE;
using System.Configuration;
using System.IO;

namespace RTSDotNETClient.TestClient
{
    public partial class ReconciliationRequestRepliesTab : UserControl
    {
        private BindingList<RequestReplyRecord> records = new BindingList<RequestReplyRecord>(); 

        public ReconciliationRequestRepliesTab()
        {
            InitializeComponent();
        }

        private void ReconciliationRequestRepliesTab_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;
            dataGridView1.DataSource = records;            
            InitComboBoxEnum("CWR", typeof(CWR));
            InitComboBoxEnum("RBC", typeof(RBC));
            InitComboBoxEnum("RequestReplyType", typeof(RequestReplyType));
            InitTestData();
        }

        private void InitTestData()
        {
#if DEBUG
            btAddTestRecord.Visible = true;
            tbSender.Text = "RTSJAVA";
            tbMessageId.Text = "123456789";
            AddTestRecord();
#endif
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (records.Count == 0)
                {
                    MessageBox.Show("You must send at least one record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                btSend.Enabled = false;

                Query query = new Query();
                query.Body.RequestReplyRecords = records.ToList<RequestReplyRecord>();                
                query.Body.NumberOfRecords = records.Count;                

                ReconciliationRequestRepliesClient cli = new ReconciliationRequestRepliesClient();
                cli.WebServiceUrl = Global.SafeTirUploadWSUrl;
                cli.PublicCertificate = EncryptionHelper.GetCertificateFromFile(Program.MainForm.CerFile);                
                cli.Send(query, tbMessageId.Text);
                records.Clear();

                MessageBox.Show("Data sent", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btSend.Enabled = true;
            }
        }

        private void InitComboBoxEnum(string colName, Type type)
        {
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            col.DataSource = Enum.GetValues(type);
            col.DataPropertyName = colName;
            col.Name = colName;
            col.HeaderText = colName;
            int index = dataGridView1.Columns[colName].Index;
            dataGridView1.Columns.Remove(dataGridView1.Columns[colName]);
            dataGridView1.Columns.Insert(index, col);
        }

        private void btAddTestRecord_Click(object sender, EventArgs e)
        {
            AddTestRecord();
        }

        private void AddTestRecord()
        {
            RequestReplyRecord r = new RequestReplyRecord();
            r.RequestReplyType = RequestReplyType.DataSentWithThisReplyIsTheCorrectData;
            r.TNO = "DX62690713";
            r.ICC = "RUS";
            r.DCL = DateTime.Now.Date;
            r.CNL = "007222";
            r.COF = "10225000";
            r.DDI = DateTime.Now.Date;
            r.PFD = "FD";
            r.CWR = CWR.OK;
            r.VPN = 4;
            r.RBC = RBC.CarnetRetained;
            r.PIC = 0;
            records.Add(r);
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string columnName = (sender as DataGridView).Columns[e.ColumnIndex].HeaderText;
            MessageBox.Show(this, string.Format("Column {0}: {1}", columnName,  e.Exception.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btLoadQuery_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string xml = File.ReadAllText(openFileDialog1.FileName);
                Query q = QueryResponseFactory.Deserialize<Query>(xml, Query.Xsd);
                records.Clear();
                foreach (RequestReplyRecord rec in q.Body.RequestReplyRecords)
                    records.Add(rec);
            }
        }

    }
}
