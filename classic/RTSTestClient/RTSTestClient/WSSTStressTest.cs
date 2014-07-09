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
	public class WSSTStressTest
	{
		private XmlDocument xmlDoc, newXMLDoc ;
		private bool bTemplateFileMissing;

		public WSSTStressTest()
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
		public ArrayList GetDataFromXML(string [] csvFileNames)
		{
			string sRow= "";
			string [] sCols = null;
			ArrayList Data = new ArrayList();;
			try
			{
				StreamReader sr =null;
				try
				{
					sr = new StreamReader(csvFileNames[0]);
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
			if (!File.Exists(AppStartUpPath+"QueryTemplate.xml"))
			{
				MessageBox.Show("QueryTemplate.xml file not found in the application folder \r\n: " + Application.StartupPath);
				bTemplateFileMissing=true;
			}

			if(!bTemplateFileMissing)
			{
				xmlDoc.Load(AppStartUpPath + "QueryTemplate.xml");

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
/*
  <Query xmlns="http://www.iru.org/TCHQuery">
  <Envelope>
    <Hash />
  </Envelope>
  <Body>
    <Sender>FCS</Sender>
    <SentTime>2004-05-19T13:54:50Z</SentTime>
    <Originator>Saurabh</Originator>
    <OriginTime>2004-05-19T13:54:50Z</OriginTime>
    <Password />
    <Query_Type>1</Query_Type>
    <Query_Reason>1</Query_Reason>
    <Carnet_Number>XW44062317</Carnet_Number>
  </Body>
</Query>
*/

			try
			{
				XmlNamespaceManager xns = new XmlNamespaceManager( newXMLDoc.NameTable);
				xns.AddNamespace("def","http://www.iru.org/TCHQuery");

				
				
				newXMLDoc.SelectSingleNode("/def:Query/def:Body/def:Sender",xns).InnerText = OneRow[2];
				newXMLDoc.SelectSingleNode("/def:Query/def:Body/def:SentTime",xns).InnerText = OneRow[3];
				newXMLDoc.SelectSingleNode("/def:Query/def:Body/def:Originator",xns).InnerText = OneRow[4];
				newXMLDoc.SelectSingleNode("/def:Query/def:Body/def:OriginTime",xns).InnerText = OneRow[5];
				newXMLDoc.SelectSingleNode("/def:Query/def:Body/def:Password",xns).InnerText = OneRow[6];
				newXMLDoc.SelectSingleNode("/def:Query/def:Body/def:Query_Type",xns).InnerText = OneRow[7];
				newXMLDoc.SelectSingleNode("/def:Query/def:Body/def:Query_Reason",xns).InnerText = OneRow[8];
				newXMLDoc.SelectSingleNode("/def:Query/def:Body/def:Carnet_Number",xns).InnerText = OneRow[9];

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
			
				newXMLDoc .Save(MsgFolderName + fileName); 
			}
			catch
			{
			}
		}

//		private void SetDataBindings()
//		{
//			ClearBindings();
//
//			Sender.DataBindings.Add("Text",dsXML.Tables[1],"Sender");
//			SentTime.DataBindings.Add("Text",dsXML.Tables[1],"SentTime");
//			Originator.DataBindings.Add("Text",dsXML.Tables[1],"Originator");
//			OriginTime.DataBindings.Add("Text",dsXML.Tables[1],"OriginTime");
//			Password.DataBindings.Add("Text",dsXML.Tables[1],"Password");
//			Query_Type.DataBindings.Add("Text",dsXML.Tables[1],"Query_Type");
//			Query_Reason.DataBindings.Add("Text",dsXML.Tables[1],"Query_Reason");
//			Carnet_Number.DataBindings.Add("Text",dsXML.Tables[1],"Carnet_Number");
//		}
//		private void ClearBindings()
//		{
//			try
//			{
//				//clear bindings may cause exception
//				Sender.DataBindings.RemoveAt(0);
//				SentTime.DataBindings.RemoveAt(0);
//				Originator.DataBindings.RemoveAt(0);
//				OriginTime.DataBindings.RemoveAt(0);
//				Password.DataBindings.RemoveAt(0);
//				Query_Type.DataBindings.RemoveAt(0);
//				Query_Reason.DataBindings.RemoveAt(0);
//				Carnet_Number.DataBindings.RemoveAt(0);
//			}
//			catch (Exception bex)
//			{
//				//swallowed 
//			}
//		}
//



		#endregion
	}
}
