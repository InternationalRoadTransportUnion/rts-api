using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace IRU.RTS.TIREPD.B2G.CipherHelper
{
    public partial class FormMain : Form
    {
        private Crypto _crypto = null;

        public FormMain()
        {
            InitializeComponent();
            
            tpEncrypt.Enabled = false;
            tpDecrypt.Enabled = false;
            tcCrypto.SelectedIndex = 0;

            cbMessageName.SelectedIndex = 0;
            cbMessageVersion.SelectedIndex = 0;
        }

        private void OnCryptoMessage(Object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bnLoadCert_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofdCert = new OpenFileDialog())
            using (FormPassword fpPwd = new FormPassword())
            {
                ofdCert.Filter = "Personal Information Exchange File (*.pfx)|*.pfx|Internet Security Certificate File (*.cer)|*.cer";
                if ((ofdCert.ShowDialog() == DialogResult.OK) && (fpPwd.ShowDialog() == DialogResult.OK))
                {
                    _crypto = null;
                    tpEncrypt.Enabled = false;
                    tpDecrypt.Enabled = false;
                    try
                    {
                        _crypto = new Crypto(ofdCert.FileName, fpPwd.Password);
                        tpEncrypt.Enabled = true;
                        tpDecrypt.Enabled = _crypto.Certificate.HasPrivateKey;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void bnDecrypt_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofdB2G = new OpenFileDialog())
            {
                try
                {
                    ofdB2G.Filter = "B2G SOAP Request (*.xml)|*.xml";
                    if (ofdB2G.ShowDialog() == DialogResult.OK)
                    {
                        tbPayload.Clear();
                        using (Stream smB2G = new MemoryStream())
                        {
                            XmlDocument xdB2G = new XmlDocument();
                            xdB2G.Load(ofdB2G.FileName);
                            xdB2G.Save(smB2G);
                            smB2G.Position = 0;
                            tbPayload.Text = _crypto.Decrypt(smB2G);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bnEncrypt_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofdEPD = new OpenFileDialog())
            {
                try
                {
                    ofdEPD.Filter = "EPD015 Pre-declaration (*.xml)|*.xml";
                    if (ofdEPD.ShowDialog() == DialogResult.OK)
                    {
                        tbPayload.Clear();
                        _crypto.SubscriberID = tbSubscriberID.Text;
                        _crypto.SubscriberMessageID = tbSubscriberMessageID.Text;
                        _crypto.InformationExchangeVersion = cbMessageVersion.Text;
                        _crypto.MessageName = cbMessageName.Text;
                        _crypto.TimeSent = dtpTimestamp.Value;
                        using (Stream smEPD = new MemoryStream())
                        {
                            XmlDocument xdEPD = new XmlDocument();
                            xdEPD.Load(ofdEPD.FileName);
                            xdEPD.Save(smEPD);
                            smEPD.Position = 0;
                            tbSOAP.Text = _crypto.Encrypt(smEPD);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
