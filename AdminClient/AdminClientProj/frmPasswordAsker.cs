using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IRU.RTS.AdminClient
{
    public partial class frmPasswordAsker : Form
    {
        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }

        public frmPasswordAsker()
        {
            InitializeComponent();
        }
    }
}
