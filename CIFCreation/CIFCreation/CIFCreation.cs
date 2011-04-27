using System;
using System.IO;
using System.Configuration;
using System.Text;
using System.Xml;
using System.Xml.Schema;


namespace CIFCreation
{

    //Summary
    //Lata Created Oct 19,2007
    //Summary

    public class CIFCreate
    {
        
        public string FileICC;
        public CIFCreate()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public string CIFCreateFile(string TempFolder, string FileName,string DestFolder,string SchemaFolder,int filetype)
		{
            

           #region Variables
            CIFStruct cifs1 = new CIFStruct();
            XmlTextReader xr = null;
            string Head1,ext;
            ext = "";
            byte[] line = Encoding.GetEncoding(1252).GetBytes("");
            XmlNodeList xl1;
            xl1 = null;
           #endregion

           #region Preparation to validate the XML against the schema
           // // Detail Log file
           // /*LogFiles Log = new LogFiles();
           // Log.ErrorLog(LogsFolder+"\\DetailLog","Processing file : "+OriFileName);
           // */
            NameTable nt = new NameTable();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);

           // //Create the XmlParserContext.
            XmlParserContext context = new XmlParserContext(null, null, null, XmlSpace.None);

            XmlDocument doc = new XmlDocument();
            doc.Load(TempFolder + "\\" + FileName);

            string mydoc = doc.OuterXml;
            XmlNamespaceManager xns = new XmlNamespaceManager(doc.NameTable);
            xns.AddNamespace("def", "http://www.iru.org/SafeTIRUpload");
           #endregion

			try 
			{

		        // Open the new temporary file
				string NewFileName = TempFolder+FileName.Substring(0,FileName.Length-6)+"tmp";
                FileStream fs1 = File.Create(NewFileName);
                
              
               
                if (filetype == 1)
                { 
                    #region CIF01 format
                    ext = ".CIF_01";
                	 

                    // Get the values we want from the XMLDocument
                    //XmlNode node = doc.DocumentElement.SelectSingleNode("/def:SafeTIR/def:Body/def:SubscriberID", xns);
                    XmlNode node = doc.DocumentElement.SelectSingleNode("/def:SafeTIR/def:Body/def:SubscriberID", xns);
                    string SUBSCRIBERID = node.InnerText;

                    node = doc.DocumentElement.SelectSingleNode("/def:SafeTIR/def:Body/def:SafeTIRRecords", xns);
                    xl1 = node.ChildNodes;

                    // Parse the record value
                     
                    for(int k=0;k < xl1.Count; k++)
                    {
                        cifs1 = CIFStructFill(xl1[k]);
                        cifs1.SUBSCRIBERID = SUBSCRIBERID;
                        if (k == 0)
                        {
                            FileICC = cifs1.ICC;
                            Head1 = "UNH" + cifs1.ICC + "007" + cifs1.ICC + ":+:Web Services:+:ASCII:+:First Record. TIR DATA Sent by " + cifs1.SUBSCRIBERID + " CIF_01=" + FileName + ":+:UNT" + Environment.NewLine;
                            line = Encoding.GetEncoding(1252).GetBytes(Head1);
                            fs1.Write(line, 0, line.Length);
                            Head1 = "UNH" + cifs1.ICC + "007" + cifs1.ICC + ":+:UNT" + Environment.NewLine;
                            line = Encoding.GetEncoding(1252).GetBytes(Head1);
                            fs1.Write(line, 0, line.Length);
                            fs1.Write(line, 0, line.Length);
                        }
                        Head1 = "UNH" + cifs1.ICC + "007" + cifs1.ICC + ":+:TNO" + cifs1.TNO + "TE*:+:DCL" + cifs1.DCL + "DE*:+:CNL" + cifs1.CNL + "CE*:+:COF" + cifs1.COF + "CE*:+:DDI" + cifs1.DDI + "DE*:+:RND" + cifs1.RND + "RE*:+:PFD" + cifs1.PFD + "PE*:+:CWR" + cifs1.CWR + "CE*:+:COM" + cifs1.COM + "CE*:+:PIC" + cifs1.PIC + "PE*:+:UPG" + cifs1.UPG + "UE*:+:RBC" + cifs1.RBC + "RE*:+:VPN" + cifs1.VPN + "VE*:+:UNT" + Environment.NewLine;
                        line = Encoding.GetEncoding(1252).GetBytes(Head1);
                        fs1.Write(line, 0, line.Length);
                        if (k ==xl1.Count - 1)
                        {
                            Head1 = "UNH" + cifs1.ICC + "007" + cifs1.ICC + ":+:End of File :+:EOF*+*+*+*+*+::::::+:UNT" + Environment.NewLine;
                            line = Encoding.GetEncoding(1252).GetBytes(Head1);
                            fs1.Write(line, 0, line.Length);

                            fs1.Close();
                        }
                     }
				

				// Detail Log file
                //Log.ErrorLog(LogsFolder+"\\DetailLog","Successfully processed file : "+OriFileName+"now named "+FinalFileName);
              #endregion
                }

                else if (filetype == 3)
                {
                    #region CIF03 format
                    ext = ".CIF_03";
                  
                    // Get the values we want from the XMLDocument
                    XmlNode node = doc.DocumentElement.SelectSingleNode("/def:SafeTIR/def:Body/def:SubscriberID", xns);
                   string SUBSCRIBERID = node.InnerText;
                    node = doc.DocumentElement.SelectSingleNode("/def:SafeTIR/def:Body/def:RequestReplyRecords", xns);
                    xl1 = node.ChildNodes;

                  
                    // Parse the record value

                    for (int k = 0; k < xl1.Count; k++)
                    {
                        cifs1 = CIFStructFill(xl1[k]);
                        cifs1.SUBSCRIBERID = SUBSCRIBERID;
                        if (k == 0)
                        {
                            FileICC = cifs1.ICC;
                            Head1 = "UNH" + cifs1.ICC + "007" + cifs1.ICC + ":+:Web Services:+:ASCII:+:First Record. TIR DATA Sent by " + cifs1.SUBSCRIBERID + " CIF_03=" + FileName + ":+:UNT" + Environment.NewLine;
                            line = Encoding.GetEncoding(1252).GetBytes(Head1);
                            fs1.Write(line, 0, line.Length);
                            Head1 = "UNH" + cifs1.ICC + "007" + cifs1.ICC + ":+:UNT" + Environment.NewLine;
                            line = Encoding.GetEncoding(1252).GetBytes(Head1);
                            fs1.Write(line, 0, line.Length);
                            fs1.Write(line, 0, line.Length);
                        }
                        if (cifs1.REQUESTREPLYTYPE == "1")
                        {
                            cifs1.UPG = "N";//The data in the reply is correct, according to customs
                        }
                        else if (cifs1.REQUESTREPLYTYPE == "2")
                        {
                            cifs1.UPG = "C"; //The  original data is totally wrong and needs to be deleted.  It can not be corrected.
                        }
                        else if (cifs1.REQUESTREPLYTYPE == "3")
                        {
                            cifs1.UPG = "Z"; //The customs say they have no information regarding this termination.
                        }
                        Head1 = "UNH" + cifs1.ICC + "007" + cifs1.ICC + ":+:TNO" + cifs1.TNO + "TE*:+:DCL" + cifs1.DCL + "DE*:+:CNL" + cifs1.CNL + "CE*:+:COF" + cifs1.COF + "CE*:+:DDI" + cifs1.DDI + "DE*:+:RND" + cifs1.RND + "RE*:+:PFD" + cifs1.PFD + "PE*:+:CWR" + cifs1.CWR + "CE*:+:COM" + cifs1.COM + "CE*:+:PIC" + cifs1.PIC + "PE*:+:UPG" + cifs1.UPG + "UE*:+:RBC" + cifs1.RBC + "RE*:+:VPN" + cifs1.VPN + "VE*:+:RID" + cifs1.REQUESTID + "RE*:+:UNT" + Environment.NewLine;
                        line = Encoding.GetEncoding(1252).GetBytes(Head1);
                     
                        fs1.Write(line, 0, line.Length);
                        if (k == xl1.Count - 1)
                        {
                            Head1 = "UNH" + cifs1.ICC + "007" + cifs1.ICC + ":+:End of File :+:EOF*+*+*+*+*+::::::+:UNT" + Environment.NewLine;
                            line = Encoding.GetEncoding(1252).GetBytes(Head1);
                            fs1.Write(line, 0, line.Length);
                            fs1.Close();
                        }
                    }
                    #endregion
                }


               #region Rename the temporary file
               int FirstUnderscore = FileName.IndexOf("_");
               int SecondUnderscore = FileName.IndexOf("_", FirstUnderscore + 1);

                string realt = FileName.Substring(FirstUnderscore + 1, SecondUnderscore - FirstUnderscore - 1);

                string realtNum = "";

                switch (realt.Length)
                {
                    case 8:
                        {
                            realtNum = realt;
                            break;
                        }
                    case 7:
                        {
                            realtNum = "0" + realt;
                            break;
                        }
                    case 6:
                        {
                            realtNum = "00" + realt;
                            break;
                        }
                    case 5:
                        {
                            realtNum = "000" + realt;
                            break;
                        }
                    case 4:
                        {
                            realtNum = "0000" + realt;
                            break;
                        }
                    case 3:
                        {
                            realtNum = "00000" + realt;
                            break;
                        }
                    case 2:
                        {
                            realtNum = "000000" + realt;
                            break;
                        }
                    case 1:
                        {
                            realtNum = "0000000" + realt;
                            break;
                        }
                    case 0:
                        {
                            realtNum = "00000000";
                            break;
                        }
                    default:
                        {
                            realtNum = realt.Substring(realt.Length - 8, 8);
                            break;
                        }
                }

                string FinalFileName = DestFolder + "C2I_" + FileICC + "_$$$_" + realtNum + "_" + FileName.Substring(FileName.Length - 21, 8) + "_" + FileName.Substring(FileName.Length - 13, 6) + ext;
                File.Move(NewFileName, FinalFileName);
                

                return FinalFileName;
               #endregion



            }
		catch (Exception ex)
			{
               throw ex;
				
			}
			finally
			{
				if (xr != null)
					xr.Close();
			}

			
        }


        #region Private Methods
        private  CIFStruct CIFStructFill(XmlNode x2)
        {
            CIFStruct cifs = new CIFStruct();
            string myRecord;
            int j,x ;
            j=x= 0;
          
       
                myRecord = x2.OuterXml;

                // RequestID
                string tempo = "";
                j = myRecord.IndexOf("RequestID=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 11);
                    x = tempo.IndexOf("\"");
                    cifs.REQUESTID = tempo.Substring(0, x);
                }

                 // TNO
              
                 j = myRecord.IndexOf("TNO=\"");
                 if (j >= 0)
                 {
                      tempo = myRecord.Substring(j + 5);
                      x = tempo.IndexOf("\"");
                      cifs.TNO = tempo.Substring(0, x);
                 }

                 // ICC
                 tempo = "";
                 j = myRecord.IndexOf("ICC=\"");
                 if (j >= 0)
                 {
                     tempo = myRecord.Substring(j + 5);
                     x = tempo.IndexOf("\"");
                     cifs.ICC = tempo.Substring(0, x);
                   
                 }

                 // DCL
                tempo = "";
                j = myRecord.IndexOf("DCL=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 5);
                    x = tempo.IndexOf("\"");
                    cifs.DCL = tempo.Substring(0, x);
                }
                //			Console.WriteLine("DCL={0}", DCL);

                // CNL
                tempo = "";
                j = myRecord.IndexOf("CNL=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 5);
                    x = tempo.IndexOf("\"");
                    cifs.CNL = tempo.Substring(0, x);
                }
                //		Console.WriteLine("CNL={0}", CNL);

                // COF 
                tempo = "";
                j = myRecord.IndexOf("COF=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 5);
                    x = tempo.IndexOf("\"");
                    cifs.COF = tempo.Substring(0, x);
                }
                //			Console.WriteLine("COF={0}", COF);

                // DDI
                tempo = "";
                j = myRecord.IndexOf("DDI=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 5);
                    x = tempo.IndexOf("\"");
                    cifs.DDI = tempo.Substring(0, x);
                }
                //			Console.WriteLine("DDI={0}", DDI);

                // RND
                tempo = "";
                j = myRecord.IndexOf("RND=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 5);
                    x = tempo.IndexOf("\"");
                    cifs.RND = tempo.Substring(0, x);
                }
                //			Console.WriteLine("RND={0}", RND);

                // PFD
                tempo = "";
                j = myRecord.IndexOf("PFD=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 5);
                    x = tempo.IndexOf("\"");
                    cifs.PFD = tempo.Substring(0, x);
                }
                //				Console.WriteLine("PFD={0}", PFD);

                // CWR
                tempo = "";
                j = myRecord.IndexOf("CWR=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 5);
                    x = tempo.IndexOf("\"");
                    cifs.CWR = tempo.Substring(0, x);
                }
                //				Console.WriteLine("CWR={0}", CWR);

                // VPN
                tempo = "";
                j = myRecord.IndexOf("VPN=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 5);
                    x = tempo.IndexOf("\"");
                    cifs.VPN = tempo.Substring(0, x);
                }
                //			Console.WriteLine("VPN={0}", VPN);

                // COM
                tempo = "";
                j = myRecord.IndexOf("COM=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 5);
                    x = tempo.IndexOf("\"");
                    cifs.COM = tempo.Substring(0, x);
                }
                //			Console.WriteLine("COM={0}", COM);

                // RBC
                tempo = "";
                j = myRecord.IndexOf("RBC=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 5);
                    x = tempo.IndexOf("\"");
                    cifs.RBC = tempo.Substring(0, x);
                }
                //			Console.WriteLine("RBC={0}", RBC);

                // UPG
                tempo = "";
                j = myRecord.IndexOf("UPG=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 5);
                    x = tempo.IndexOf("\"");
                    cifs.UPG = tempo.Substring(0, x);
                }
                //			Console.WriteLine("UPG={0}", UPG);

                // PIC
                tempo = "";
                j = myRecord.IndexOf("PIC=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 5);
                    x = tempo.IndexOf("\"");
                    cifs.PIC = tempo.Substring(0, x);
                }


                //RequestReplyType
                tempo = "";
                j = myRecord.IndexOf("RequestReplyType=\"");
                if (j >= 0)
                {
                    tempo = myRecord.Substring(j + 18);
                    x = tempo.IndexOf("\"");
                    cifs.REQUESTREPLYTYPE = tempo.Substring(0, x);
                }


                
                return cifs;

      
   }
        #endregion
  }

 }
