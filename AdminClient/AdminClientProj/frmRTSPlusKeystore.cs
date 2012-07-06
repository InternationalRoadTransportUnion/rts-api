using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using IRU.Common.EnterpriseLibrary.Data;
using IRU.RTS.WS.Common.Data.WsSubscriber;
using IRU.RTS.WS.Common.Subscribers;

namespace IRU.RTS.AdminClient
{
    public partial class frmRTSPlusKeystore : Form
    {
        private DataTable _dtRTSPlusKeys = new DataTable();

        public frmRTSPlusKeystore()
        {
            InitializeComponent();

            dgwRTSPlusKeys.DataSource = _dtRTSPlusKeys;
            DataColumn dcWay = _dtRTSPlusKeys.Columns.Add("WAY", typeof(Image));
        }

        private void QueryExecuted(object sender, DataReaderEventArgs e)
        {
            _dtRTSPlusKeys.Clear();
            _dtRTSPlusKeys.Load(e.DataReader);                        
            foreach (DataRow row in _dtRTSPlusKeys.Rows)
            {
                if (row["PRIVATE_KEY"] is System.DBNull)
                    row["WAY"] = global::IRU.RTS.AdminClient.Properties.Resources.mailbox_incoming_32;
                else
                    row["WAY"] = global::IRU.RTS.AdminClient.Properties.Resources.mailbox_outgoing_32;
            }
            dgwRTSPlusKeys.CellFormatting += new DataGridViewCellFormattingEventHandler(dgwRTSPlusKeys_CellFormatting);            
        }

        void dgwRTSPlusKeys_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is byte[])
            {
                byte[] array = (byte[])e.Value;
                e.Value = "0x";
                foreach (byte b in array)
                {
                    e.Value += String.Format("{0:x2}", b);
                }
            }
        }

        private void frmRTSPlusKeystore_Load(object sender, EventArgs e)
        {
            using (DbQueries dq = new DbQueries())
            {
                dq.GetAllRtsplusSignatureKeys(false, QueryExecuted);
            }
        }

        private X509Certificate2 GetSelectedCertificate(out bool serverPurpose)
        {
            string sSubscriberId = (string)dgwRTSPlusKeys.SelectedRows[0].Cells["SUBSCRIBER_ID"].Value;
            string sThumbprint = (string)dgwRTSPlusKeys.SelectedRows[0].Cells["THUMBPRINT"].Value;
            serverPurpose = !(dgwRTSPlusKeys.SelectedRows[0].Cells["PRIVATE_KEY"].Value is System.DBNull);

            X509Certificate2Collection x5cs;
            if (serverPurpose)
                x5cs = CertificatesStore.GetCertificates(CertStore.RTS_PLUS, CertUsage.Server, false);
            else
                x5cs = CertificatesStore.GetCertificates(CertStore.RTS_PLUS, CertUsage.Client, false);

            x5cs = x5cs.Find(X509FindType.FindByThumbprint, sThumbprint, false);
            x5cs = x5cs.FindBySubscriberId(sSubscriberId);

            if (x5cs.Count > 0)
                return x5cs[0];
            else
                return null;
        }

        public void ChangeCertificate(X509Certificate2 certificate, bool serverPurpose, bool activation)
        {
            CertUsage certUsage;
            if (serverPurpose)
                certUsage = CertUsage.Server;
            else
                certUsage = CertUsage.Client;

            if (activation)
                CertificatesStore.ActivateCertificate(CertStore.RTS_PLUS, certUsage, certificate.SubscriberId(), certificate, frmMain.UserID);
            else
                CertificatesStore.DeactivateCertificate(CertStore.RTS_PLUS, certUsage, certificate.SubscriberId(), certificate, frmMain.UserID);
        }

        public void RefreshGrid()
        {
            int iSel = -1;
            if (dgwRTSPlusKeys.SelectedRows.Count > 0)
                iSel = dgwRTSPlusKeys.SelectedRows[0].Index;
            
            using (DbQueries dq = new DbQueries())
            {
                dq.GetAllRtsplusSignatureKeys(false, QueryExecuted);
            }

            if (iSel > -1)
            {
                dgwRTSPlusKeys.Rows[iSel].Selected = true;
                dgwRTSPlusKeys.CurrentCell = dgwRTSPlusKeys.Rows[iSel].Cells[0];
            }
        }

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form();
                prompt.Width = 500;
                prompt.Height = 200;
                prompt.Text = caption;
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(textBox);
                textBox.Focus();
                textBox.Select();
                prompt.ShowDialog();
                return textBox.Text;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            X509Certificate2 x5c = new X509Certificate2();
            CertUsage certUsage = CertUsage.Client;

            if (ofdCerts.ShowDialog() == DialogResult.OK)
            {
                if (ofdCerts.FilterIndex == 2)
                {
                    x5c.Import(ofdCerts.FileName, (string)null, X509KeyStorageFlags.Exportable);
                    certUsage = CertUsage.Server;
                }
                else
                    x5c.Import(ofdCerts.FileName);

                string sSubscriberId = Prompt.ShowDialog("Subscriber Id", "Subscriber for RTS+");

                CertificatesStore.AddCertificate(CertStore.RTS_PLUS, certUsage, sSubscriberId, x5c, frmMain.UserID);

                RefreshGrid();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            bool bServerMode;
            X509Certificate2 x5c = GetSelectedCertificate(out bServerMode);

            string sMode = "INCOMING";
            if (bServerMode)
            {
                sfdCerts.FilterIndex = 2;
                sMode = "OUTGOING";
            }
            else
                sfdCerts.FilterIndex = 1;
            sfdCerts.FileName = String.Format("{0}_RTS+_{1}_{2}", x5c.SubscriberId(), sMode, DateTime.Now.ToString("yyyyMMddss"));

            if (sfdCerts.ShowDialog() == DialogResult.OK)
            {
                byte[] abCert;

                if (sfdCerts.FilterIndex == 2)
                    abCert = x5c.Export(X509ContentType.Pfx, (string)null);
                else
                    abCert = x5c.Export(X509ContentType.Cert);

                File.WriteAllBytes(sfdCerts.FileName, abCert);
            }
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            bool bServerMode;
            X509Certificate2 x5c = GetSelectedCertificate(out bServerMode);
            ChangeCertificate(x5c, bServerMode, true);
            RefreshGrid();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            bool bServerMode;
            X509Certificate2 x5c = GetSelectedCertificate(out bServerMode);
            ChangeCertificate(x5c, bServerMode, false);
            RefreshGrid();
        }
    }
}
