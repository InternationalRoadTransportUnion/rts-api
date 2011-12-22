using System;
using System.Windows.Forms;

using RTSDotNETClient.TCHQ;
using System.Configuration;
using System.Diagnostics;

namespace RTSDotNETClient.TestClient
{
    public partial class CarnetHolderQueryTab : UserControl
    {
        public CarnetHolderQueryTab()
        {
            InitializeComponent();            
        }

        private void CarnetHolderQueryTab_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;
            InitTestData();
        }

        private void InitTestData()
        {
#if DEBUG            
            tbSender.Text = "RTSJAVA";
            tbOriginator.Text = "test";
            tbTirCarnet.Text = "AX66950772";
#endif
        }

        private void btnCarnetHolderQuery_Click(object sender, EventArgs e)
        {
            try
            {                
                ClearUI();
                btnCarnetHolderQuery.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                // Generate the query
                Query query = new Query();
                query.Body.OriginTime = DateTime.Now;
                query.Body.SentTime = DateTime.Now;
                query.Body.Sender = tbSender.Text;
                query.Body.Originator = tbOriginator.Text;
                query.Body.QueryType = QueryType.CarnetHolder;
                query.Body.QueryReason = QueryReason.Entry;
                query.Body.CarnetNumber = tbTirCarnet.Text;
                query.CalculateHash();

                // call the web service
                HolderQueryClient holderQueryClient = new HolderQueryClient();
                holderQueryClient.WebServiceUrl = Global.TirCarnetQueryWSUrl;
                holderQueryClient.PublicCertificate = EncryptionHelper.GetCertificateFromFile(Program.MainForm.CerFile);
                holderQueryClient.PrivateCertificate = EncryptionHelper.GetCertificateFromFile(Program.MainForm.PfxFile);
                Response response = holderQueryClient.QueryCarnet(query);

                UpdateUI(response);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnCarnetHolderQuery.Enabled = true;
            }
        }

        private void UpdateUI(Response response)
        {
            tbReturnCode.Text = string.Format("{0} - {1}", (int)response.Body.Result, response.Body.Result);
            tbAssociation.Text = (response.Body.Association != null) ? response.Body.Association : "";
            tbHolderID.Text = (response.Body.HolderID != null) ? response.Body.HolderID : "";
            tbNumTerminations.Text = response.Body.NumTerminations.ToString();
            tbValidityDate.Text = (response.Body.ValidityDate > DateTime.MinValue) ? response.Body.ValidityDate.ToShortDateString() : "";
        }

        private void ClearUI()
        {
            tbReturnCode.Text = "";
            tbNumTerminations.Text = "";
            tbAssociation.Text = "";
            tbHolderID.Text = "";
            tbValidityDate.Text = "";
        }

    }
}
