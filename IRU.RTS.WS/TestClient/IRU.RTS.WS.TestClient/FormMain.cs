using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IRU.RTS.WS.TestClient.CarnetService;

namespace IRU.RTS.WS.TestClient
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            DateTime dtNow = DateTime.Now.ToLocalTime();

            dtpFrom.Value = dtNow.AddDays(-7.0);
            dtpTo.Value = dtNow;
        }

        private void cbTo_CheckedChanged(object sender, EventArgs e)
        {
            dtpTo.Enabled = cbTo.Checked;
        }

        private void cbOffset_CheckedChanged(object sender, EventArgs e)
        {
            nudOffset.Enabled = cbOffset.Checked;
        }

        private void cbCount_CheckedChanged(object sender, EventArgs e)
        {
            nudCount.Enabled = cbCount.Checked;
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            using (CarnetServiceClient csc = new CarnetServiceClient())
            {
                csc.ClientCredentials.UserName.UserName = "test";
                csc.ClientCredentials.UserName.Password = "test";

                DateTime dtFrom = dtpFrom.Value;
                DateTime? dtTo = dtpTo.Enabled ? (DateTime?)dtpTo.Value : null;
                int? iOffset = nudOffset.Enabled ? (int?) nudOffset.Value : null;
                int? iCount = nudCount.Enabled ? (int?)nudCount.Value : null;

                stoppedCarnetsType sct = csc.GetInvalidatedCarnets(dtFrom, dtTo, iOffset, iCount);

                TreeViewFiller.populateTree(sct.Serialize(), tvResult);
            }
        }
    }
}
