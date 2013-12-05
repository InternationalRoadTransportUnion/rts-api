using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace IRU.RTS.AdminClient
{
	/// <summary>
	/// Summary description for CommonHelpers.
	/// </summary>
	public class CommonHelpers
	{
		public CommonHelpers()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		internal static  void PopulateSubsCombo(ComboBox SubsCombo )
		{
			SubsCombo.Items.Clear();
			CommonDBHelper subsConn = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);
			try
			{
				subsConn.ConnectToDB();
                string subsSelect = "Select SUBSCRIBER_ID from dbo.WS_SUBSCRIBER";
				IDataReader sReader = subsConn.GetDataReader(subsSelect, System.Data.CommandBehavior.SingleResult);
				while (sReader.Read())
				{
					SubsCombo.Items.Add(sReader["SUBSCRIBER_ID"].ToString());
				
				}
				sReader.Close();
			}
			finally
			{
				subsConn.Close();
				
			}
		
		}



		internal static string CommonDecryptData(string EncryptedString)
		{
			// do nothing as of now- replace with actual decryption later
			return EncryptedString;
		}

		internal static string CommonEncryptData(string DecryptedString)
		{
			// do nothing as of now- replace with actual Encryption later
			return DecryptedString;
		}
	}
}
