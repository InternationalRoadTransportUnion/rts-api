using System;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;
using System.Collections;
using WSCscc_Client.WSSTstc;
using System.Xml;
using System.IO;





namespace IRU.RTS.WSCscc
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class WSCsccCopyFileProcessor:IPlugIn, IActiveObject
	{


		#region Declare Variables

		
		private static string m_DoubleAgentDropPath; 
		private static string m_TemporaryFolderPath;

		private IPlugInManager m_PluginManager;

		

		private string m_PluginName;

		private string m_SenderID ;// sent to ASMAP as copy from 

		
		#endregion
		public WSCsccCopyFileProcessor()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IPlugIn Members

		public void Configure(IPlugInManager pluginManager)
		{
			m_PluginManager = pluginManager;
			try
			{
				XmlNode sectionNode = m_PluginManager.GetConfigurationSection(
					m_PluginName);
            
				XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,
					"./WSCsccCopyFileProcessorSettings");

							m_TemporaryFolderPath =XMLHelper.GetAttributeNode(parameterNode,
					"TemporaryFolderPath").InnerText;

				if (m_TemporaryFolderPath.LastIndexOf("\\")!= m_TemporaryFolderPath.Length-1)
					m_TemporaryFolderPath+="\\";

				m_DoubleAgentDropPath = XMLHelper.GetAttributeNode(parameterNode,
					"DoubleAgentDropPath").InnerText;

				if (m_DoubleAgentDropPath.LastIndexOf("\\")!= m_DoubleAgentDropPath.Length-1)
					m_DoubleAgentDropPath+="\\";

				m_SenderID = XMLHelper.GetAttributeNode(parameterNode,
					"SenderID").InnerText;

				

				
			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of WSCsccCopyFileProcessor"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of WSCsccCopyFileProcessor"
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure of WSCsccCopyFileProcessor"
					+ e.Message);
				throw e;
			}

		}

		public void Unload()
		{
			// TODO:  Add WSST_Processor.Unload implementation
		}

	

		public string PluginName
		{
			get
			{
				return m_PluginName;
			}
			set
			{
				m_PluginName=value;
			}
		}

		#endregion

		#region IActiveObject Members

		public void Enqueue(object objToEnqueue, string queueName)
		{
			
			throw new NotImplementedException("");
		



		}

		void IRU.CommonInterfaces.IActiveObject.Enqueue(object objToEnqueue)
		{
			
			structStepResult[] aStepResult = new structStepResult[3]; 
			/*
			25 = SafeTIR File Received on Internet 
			30 = Copy Send process complete 
			35 = Copy Result File Created for Internal 
			*/
			Hashtable hFileData = null;
			WSCscc_Client.WSSTstc.SafeTIRUploadCopy safeTirCopy= null;
			long safetir_message_in_id =-1;
			string MessageTag ="";
			byte[] aESessionKey=null;
			byte[] aUploadData=null;

			string CopyToID="" ;
			string CopyFromID ="";
			string TargetURL ="";

			try
			{
				hFileData = (Hashtable) objToEnqueue;
				#region Extract File Contents from object
			
				byte[] baFileContents = (byte[])hFileData["FileContents"];
				#endregion

				#region Split String
				string sFileContents = System.Text.Encoding.Unicode.GetString(baFileContents);
				string [] aFileContentsList = sFileContents.Split('\t');
				/*
				SafeTIR_Message_In_ID	-
				MessageTag	Key used for Encryption
				ESessionKey	Base64 Encoded ESessionKey
				EncryptedXML	Base64 encoded Encrypted XML
				CopyToID	-(e.g. ASMAP)
				CopyFromID	e.g. FCS
				TargetURL	URL of the recipients webservice
				*/


				safetir_message_in_id = long.Parse(aFileContentsList[0].Trim());
				 MessageTag = aFileContentsList[1].Trim();
				 aESessionKey = System.Convert.FromBase64String(aFileContentsList[2].Trim());
				 aUploadData = System.Convert.FromBase64String(aFileContentsList[3].Trim());

				CopyToID = aFileContentsList[4].Trim();
				CopyFromID = aFileContentsList[5].Trim();
				TargetURL = aFileContentsList[6].Trim();


				safeTirCopy = new WSCscc_Client.WSSTstc.SafeTIRUploadCopy();

				safeTirCopy.CopyFromID=CopyFromID;
				safeTirCopy.CopyToID=CopyToID;
				safeTirCopy.ESessionKey=aESessionKey;
				safeTirCopy.safeTIRUploadData=aUploadData;
				safeTirCopy.MessageTag=MessageTag;
				#endregion



				aStepResult[0] = new structStepResult();
				aStepResult[0].SetMembers(25,1,DateTime.Now,"");

			}
			catch (Exception exx)
			{
					aStepResult[0].SetMembers(25,2,DateTime.Now,exx.Message + "\r\n" + exx.StackTrace);
			}

			

			if (aStepResult[0].StepResult==1)
				CallWebService(safeTirCopy,TargetURL ,aStepResult);
			else
			{
				aStepResult[1] = new structStepResult();
				aStepResult[1].SetMembers(30,1200 ,DateTime.Now,"Not attempted Error in previous step");
	
			}

			aStepResult[2] = new structStepResult(); //assume success in writing the file
			aStepResult[2].SetMembers(35,1 ,DateTime.Now,"");
			
		
	
			CreateFile(safetir_message_in_id, CopyToID, aStepResult);
			

		}

		#endregion


		#region Private methods

		private void CallWebService (WSCscc_Client.WSSTstc.SafeTIRUploadCopy SafeTirCopy, string TargetURL, structStepResult[] aStructResults)
		{
			
			WSCscc_Client.WSSTstc.SafeTIRUploadCopyService wscstc = new WSCscc_Client.WSSTstc.SafeTIRUploadCopyService();

			wscstc.Url=TargetURL;

			

			WSCscc_Client.WSSTstc.SafeTIRUploadCopyAck safeAck=null;
			try
			{
				safeAck= wscstc.WSSTstc(SafeTirCopy);

				aStructResults[1] = new structStepResult();
				if (safeAck.ReturnCode!=2) //not success
					aStructResults[1].SetMembers(30,4 ,DateTime.Now,"");
				else
					aStructResults[1].SetMembers(30,1 ,DateTime.Now,"");


			}
			catch (System.Web.Services.Protocols.SoapException exx) //for exceptions on copy to side
			{
				//error with web service
				aStructResults[1] = new structStepResult();
				aStructResults[1].SetMembers(30,5,DateTime.Now,exx.Message + exx.StackTrace);

			}
			catch (System.Net.WebException exx) //for timeouts
			{
				//error with web service
					aStructResults[1] = new structStepResult();
				//check for 404 -not found- set the return code to 3
				if ((exx.Message.IndexOf("404")> 0) ||(exx.Message.ToLower().IndexOf("underlying connection was closed")> 0) )
				{
				
				
					aStructResults[1].SetMembers(30,3,DateTime.Now,exx.Message + exx.StackTrace);
				}
				else
				{
					aStructResults[1].SetMembers(30,5,DateTime.Now,exx.Message + exx.StackTrace);
				}

			}
			catch (Exception exx)
			{
				//error with web service
				aStructResults[1] = new structStepResult();
				aStructResults[1].SetMembers(30,5,DateTime.Now,exx.Message + exx.StackTrace);

			}
			



			
		

		}

		private string CreateFile (long SafeTiRMessageInID, string CopytoID,structStepResult[] aStructResults)
		{
		
			
			
			
				string  FileName = SafeTiRMessageInID.ToString() + "_CopyToResponse_" + CopytoID + DateTime.Now.ToString("yyyyMMddHHmmss") +
					".SafeTIR_COPY_RESULT";
				StreamWriter sw = null;
				try
				{
					sw = new StreamWriter(m_TemporaryFolderPath+ FileName, false , System.Text.Encoding.Unicode);
					sw.WriteLine(SafeTiRMessageInID.ToString() );

					foreach (structStepResult stepStruct in aStructResults)
					{
						sw.WriteLine(stepStruct.Step.ToString());
						sw.WriteLine(stepStruct.StepResult.ToString());
						sw.WriteLine(stepStruct.StepTimeStamp.ToString("yyyy/MM/dd HH:mm:ss"));
						if (stepStruct.ResultDescription!=null)
						{
							string sStepDescription = stepStruct.ResultDescription;
							byte[] aResultBytes = System.Text.Encoding.Unicode.GetBytes(sStepDescription);
						
							sw.WriteLine(System.Convert.ToBase64String(aResultBytes));
						}
						else
						{
							sw.WriteLine("");
						}
					
					}
				}
				finally
				{
					sw.Close();
				}
				File.Move(m_TemporaryFolderPath+ FileName, m_DoubleAgentDropPath  + FileName);

				return FileName ;
			
		
		}
		#endregion

	}


	struct structStepResult
	{
		public int Step;
		public int StepResult;
		public DateTime StepTimeStamp;
		public string ResultDescription;
	
		public void SetMembers(int StepNumber, int StepResultValue, DateTime TimeStamp, string Description)
		{
		
			Step= StepNumber;
			StepResult = StepResultValue;
			StepTimeStamp= TimeStamp;
			ResultDescription=Description;
		
		}
	}
}
