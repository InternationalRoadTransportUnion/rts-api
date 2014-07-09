using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace RTSTestClient
{
	/// <summary>
	/// Summary description for TCHQStressTest.
	/// </summary>
	public class WSRQStressTest
	{
		private XmlDocument xmlDoc, newXMLDoc ;
		private bool bTemplateFileMissing;

		public WSRQStressTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region GetDataFromCSV
		/// <summary>
		/// 
		/// </summary>
		/// <param name="csvFileName"></param>
		/// <returns></returns>
		public ArrayList GetDataFromCSV(string csvFileName)
		{
			string sRow= "";
			string [] sCols = null;
			ArrayList Data = new ArrayList();;
			try
			{
				StreamReader sr =null;
				try
				{
					sr = new StreamReader(csvFileName);
					sr.ReadLine();//Forget first line for titles

					while ((sRow = sr.ReadLine()) != null) 
					{
						sCols = sRow.Split(',');
						Data.Add(sCols);
					}

				}
				finally
				{
					sr.Close();
				}
			}
			catch(Exception ex)
			{
				string sMsg = ex.Message + "\r\n"+ex.StackTrace ;
			}

			return Data ;
		}

		#endregion
		#region
		/// <summary>
		/// 
		/// </summary>
		/// <param name="MsgFolderName">Location for Messages</param>
		/// <param name="ArrData">ArrayList contating String arrays</param>
		public void GenerateMsgFiles(string MsgFolderName, ArrayList ArrData)
		{
	
			xmlDoc = new XmlDocument();
			bTemplateFileMissing=false;
			string AppStartUpPath = Application.StartupPath.Trim() ;
			if(AppStartUpPath.Substring(AppStartUpPath.Length-1,1)!=@"\")
			{
				AppStartUpPath+= "\\";
			}
            if (!File.Exists(AppStartUpPath + "WSRQueryDataTemplate.xml"))
			{
                MessageBox.Show("WSRQueryDataTemplate.xml file not found in the application folder \r\n: " + Application.StartupPath);
				bTemplateFileMissing=true;
			}

			if(!bTemplateFileMissing)
			{
                /*byte[] xmlarray = null;
                FileStream fs1 = new FileStream(AppStartUpPath + "WSRQueryDataTemplate.xml", FileMode.Open, FileAccess.Read);
                xmlarray = new byte[fs1.Length];
                fs1.Read(xmlarray, 0, xmlarray.Length);
                fs1.Close();
                string xmlstring = System.Text.Encoding.Unicode.GetString(xmlarray);
                xmlstring = xmlstring.Replace("http://www.iru.org/WSRQuery", "http://www.iru.org/SafeTIRReconciliation");
                xmlstring = "<?xml version=\"1.0\" standalone=\"yes\"?>\r\n" + xmlstring;
                xmlstring = xmlstring.Trim();
                xmlDoc.Load(xmlstring);*/
                xmlDoc.Load(AppStartUpPath + "WSRQueryDataTemplate.xml");

				for(int i=0;i<ArrData.Count;i++)
				{
					newXMLDoc = (XmlDocument)xmlDoc.Clone();

					SetXMLData((string[])ArrData[i]);
					SaveMsgFile(MsgFolderName, (string [])ArrData[i]);
				}
			}
		}
		private bool SetXMLData(string [] OneRow)
		{
            #region Sample XML
            //<ReconciliationQuery Sender_Document_Version="1.0.0" xmlns="http://www.iru.org/SafeTIRReconciliation">
            //<Body>
            //<SentTime>2004-05-19T13:54:50+04:00</SentTime>
            //<Password>abcdefghijklmnopqrstuvwxyz123456</Password>
            //<QueryType>1</QueryType>
            //</Body>
            //</ReconciliationQuery>
            #endregion

			try
			{
				XmlNamespaceManager xns = new XmlNamespaceManager( newXMLDoc.NameTable);
                xns.AddNamespace("def", "http://www.iru.org/WSRQuery");
                newXMLDoc.SelectSingleNode("/def:ReconciliationQuery/def:Body/def:SentTime", xns).InnerText = OneRow[3];
                newXMLDoc.SelectSingleNode("/def:ReconciliationQuery/def:Body/def:Password", xns).InnerText = OneRow[4];
                if (OneRow[4].ToString().Trim() == "")
                {
                    XmlNode Password = null;
                    XmlNode nodeResponseBody = null;
                    nodeResponseBody = newXMLDoc.SelectSingleNode("/def:ReconciliationQuery/def:Body", xns);
                    Password = nodeResponseBody.ChildNodes.Item(1);
                    nodeResponseBody.RemoveChild(Password);
                   //Password.
//newXMLDoc.RemoveChild(Password);
  //                  newXMLDoc.InnerXml.Replace("<Password>","");
    //                newXMLDoc.InnerXml.Replace("</Password>","");
                    //newXMLDoc.SelectSingleNode("/def:ReconciliationQuery/def:Body", xns).RemoveChild(Password);
                }
                newXMLDoc.SelectSingleNode("/def:ReconciliationQuery/def:Body/def:QueryType", xns).InnerText = OneRow[5];
				return true;
			}
			catch(XmlException ex)
			{
				string sMsg = ex.Message ;
				return false;
			}
		}

		private void SaveMsgFile(string MsgFolderName, string [] OneRow)
		{
			try
			{
				string fileName = OneRow[0]+".xml";

				if(MsgFolderName.Substring(MsgFolderName.Length-1,1)!=@"\")
				{
					MsgFolderName += "\\";
				}
			
				newXMLDoc.Save(MsgFolderName + fileName);
                StreamReader sr1 = new StreamReader(MsgFolderName + fileName);
                string sData = sr1.ReadToEnd();
                sr1.Close();
                sData = sData.Replace("http://www.iru.org/WSRQuery", "http://www.iru.org/SafeTIRReconciliation");
                StreamWriter sw1 = new StreamWriter(MsgFolderName + fileName, false,System.Text.Encoding.Unicode);
                sw1.Write(sData.Trim());
                sw1.Close();
			}
			catch(IOException io)
			{
                throw io;
			}
		}

		#endregion
	}
}
