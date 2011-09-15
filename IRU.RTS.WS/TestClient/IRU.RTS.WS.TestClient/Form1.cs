using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IRU.RTS.WS.TestClient.CarnetService;

namespace IRU.RTS.WS.TestClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (CarnetServiceClient csc = new CarnetServiceClient())
            {
                csc.ClientCredentials.UserName.UserName = "test";
                csc.ClientCredentials.UserName.Password = "test";
                stoppedCarnetsType sct = csc.GetInvalidatedCarnets(new DateTime(2011, 01, 01), null, null, null);
            }
        }
    }
}
