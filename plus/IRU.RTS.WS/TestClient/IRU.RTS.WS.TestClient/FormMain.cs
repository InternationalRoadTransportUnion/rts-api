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
            using (CarnetServiceSEIClient ws = new CarnetServiceSEIClient("CarnetServicePort"))
            {
                ws.ClientCredentials.UserName.UserName = Properties.Settings.Default.UserName;
                ws.ClientCredentials.UserName.Password = Properties.Settings.Default.Password;

                DateTime dtFrom = dtpFrom.Value;
                DateTime? dtTo = dtpTo.Enabled ? (DateTime?)dtpTo.Value : null;
                int? iOffset = nudOffset.Enabled ? (int?) nudOffset.Value : null;
                uint? iCount = nudCount.Enabled ? (uint?)nudCount.Value : null;

                stoppedCarnetsTypeStoppedCarnets scCar;
                stoppedCarnetsTypeTotal scTot = ws.getStoppedCarnets(dtFrom, dtTo, iOffset, iCount, out scCar);
                getStoppedCarnetsResponse scResp = new getStoppedCarnetsResponse(scTot, scCar);

                TreeViewFiller.populateTree(scResp.Serialize(), tvResult);
            }
        }
    }
}
