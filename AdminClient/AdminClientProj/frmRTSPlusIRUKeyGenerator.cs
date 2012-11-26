using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.AdminClient
{
    public partial class frmRTSPlusIRUKeyGenerator : Form
    {
        public frmRTSPlusIRUKeyGenerator()
        {
            InitializeComponent();
        }

        private void frmRTSPlusIRUKeyGenerator_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Now.Date;
            dtpExpiryDate.Value = DateTime.Now.Date.AddYears(1);
            cbKeyLength.SelectedIndex = 0;
            cbSignatureAlgorithm.SelectedIndex = 1;
            txtOutputFolder.Text = Properties.Settings.Default.RTSPlusCertPath;
            fbdOutput.SelectedPath = txtOutputFolder.Text;
            btnGenerate.Enabled = false;
        }

        private void txtSubscriber_TextChanged(object sender, EventArgs e)
        {
            if (txtEMail.Text.EndsWith("@iru.org"))
            {
                txtEMail.Text = String.Format("{0}@iru.org", txtSubscriber.Text.ToLowerInvariant());
            }
            btnGenerate.Enabled = !String.IsNullOrEmpty(txtSubscriber.Text);
        }

        private void txtSubscriber_KeyPress(object sender, KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString().ToUpperInvariant();
            if (s.Length == 1)
                e.KeyChar = s[0];
        }

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            if (fbdOutput.ShowDialog() == DialogResult.OK)
            {
                txtOutputFolder.Text = fbdOutput.SelectedPath;
            }
        }

        private void frmRTSPlusIRUKeyGenerator_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.RTSPlusCertPath = txtOutputFolder.Text;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo("makecert.exe");
            if (!fi.Exists)
            {
                throw new FileNotFoundException("MakeCert.exe should be in the same folder as Admin Client", "MakeCert.exe", null);
            }

            string sKeyName = String.Format("IRU_KEY_{0}_DO_NOT_DISTRIBUTE_{1}.pfx", txtSubscriber.Text, DateTime.Now.ToString("yyyyMMddHHmmss"));
            string sCertName = String.Format("IRU_KEY_{0}_PUBLIC_CERTIFICATE_{1}.cer", txtSubscriber.Text, DateTime.Now.ToString("yyyyMMddHHmmss"));

            string sKeyFile = Path.Combine(txtOutputFolder.Text, sKeyName);
            string sCertFile = Path.Combine(txtOutputFolder.Text, sCertName);

            string sCommandLine = String.Format("-r -pe -len {0} -n \"CN=IRU.org RTSPLUS, OU=Subscriber : {1}, O=IRU.org, E={2}\" -sr localmachine -ss IRUTEST -b {3} -e {4} -a {5} {6}",
                cbKeyLength.Text,
                txtSubscriber.Text,
                txtEMail.Text,
                dtpStartDate.Value.ToString("MM'/'dd'/'yyyy"),
                dtpExpiryDate.Value.ToString("MM'/'dd'/'yyyy"),
                cbSignatureAlgorithm.Text,
                sCertFile);

            txtOutputResult.Text = String.Format("makecert.exe {0}", sCommandLine);

            ProcessStartInfo psi = new ProcessStartInfo("makecert.exe");
            psi.Arguments = sCommandLine;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = Process.Start(psi);
            p.WaitForExit();

            txtOutputResult.Text += Environment.NewLine;
            txtOutputResult.Text += p.StandardOutput.ReadToEnd();
            txtOutputResult.Text += Environment.NewLine;
            txtOutputResult.Text += p.StandardError.ReadToEnd();

            try
            {
                X509Certificate2 x5c2 = new X509Certificate2(sCertFile);
                X509Store x5s = new X509Store("IRUTEST", StoreLocation.LocalMachine);
                x5s.Open(OpenFlags.ReadOnly);
                try
                {
                    X509Certificate2Collection x5c2c = x5s.Certificates.Find(X509FindType.FindByThumbprint, x5c2.Thumbprint, false);
                    if (x5c2c.Count == 0)
                        throw new Exception("No private key has been found in the store!");
                    if (x5c2c.Count > 1)
                        throw new Exception("More than one private key have been found in the store!");
                    string sPwd = null;
                    using (frmPasswordAsker fpa = new frmPasswordAsker())
                    {
                        fpa.ShowDialog();
                        sPwd = fpa.Password;
                    }
                    File.WriteAllBytes(sKeyFile, x5c2c[0].Export(X509ContentType.Pfx, sPwd));
                }
                finally
                {
                    x5s.Close();
                }

                txtOutputResult.Text += Environment.NewLine;
                if (p.ExitCode != 0)
                {
                    txtOutputResult.Text += "!!! FAILURE !!!";
                }
                else
                    txtOutputResult.Text += "DONE";
            }
            catch (Exception ex)
            {
                txtOutputResult.Text += Environment.NewLine;
                txtOutputResult.Text += String.Format("Exception: {0}", ex.Message);
                txtOutputResult.Text += Environment.NewLine;
                txtOutputResult.Text += ex.StackTrace;
                txtOutputResult.Text += Environment.NewLine;
                txtOutputResult.Text += "!!! FAILURE !!!";
                DialogResult = DialogResult.None;
                DialogResult = DialogResult.None;                    
            }
        }
    }
}
