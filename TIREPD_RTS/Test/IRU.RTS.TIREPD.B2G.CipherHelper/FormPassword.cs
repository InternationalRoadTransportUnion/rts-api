using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IRU.RTS.TIREPD.B2G.CipherHelper
{
    public partial class FormPassword : Form
    {
        public FormPassword()
        {
            InitializeComponent();

            this.DialogResult = DialogResult.Cancel;
        }

        public string Password
        {
            get { return tbPassword.Text; }
        }

        private void bnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
