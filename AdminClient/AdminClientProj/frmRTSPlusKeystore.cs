using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IRU.Common.EnterpriseLibrary.Data;
using IRU.RTS.WS.Common.Data.WsSubscriber;
using IRU.RTS.WS.Common.Subscribers;

namespace IRU.RTS.AdminClient
{
    public partial class frmRTSPlusKeystore : Form
    {
        private DataTable _dtRTSPlusKeys;

        public frmRTSPlusKeystore()
        {
            InitializeComponent();            
        }

        private void QueryExecuted(object sender, DataReaderEventArgs e)
        {
            dgwRTSPlusKeys.DataSource = null;
            _dtRTSPlusKeys = new DataTable();

            _dtRTSPlusKeys.Load(e.DataReader);
            dgwRTSPlusKeys.CellFormatting += new DataGridViewCellFormattingEventHandler(dgwRTSPlusKeys_CellFormatting);
            dgwRTSPlusKeys.DataSource = _dtRTSPlusKeys;
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
    }
}
