using System;
using IRU.RTS.CommonComponents;
using IRU.CommonInterfaces;
using System.Net;
using System.Web.Mail;
using System.Xml;
using System.IO;



namespace IRU.RTS.NotificationHandlers
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class EmailNotifier:IPlugIn, INotificationHandler
	{

		
		#region Declare Variables

		

		private IPlugInManager m_PluginManager;

		private string m_PluginName;


		string m_SMTPServer ;
			

		string m_SMTPServerUID ;
			

		string m_SMTPServerPassword ;

		string m_MailFrom ;
			

		string m_MailTo ;

		int m_SMTPPort = 25;

		string m_TempFolder ;//used for attachments;
			

		#endregion

		public EmailNotifier()
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
					"./EmailSettings");

				  m_SMTPServer = XMLHelper.GetAttributeNode(parameterNode,
					"SMTPServer").InnerText;

				  m_SMTPServerUID = XMLHelper.GetAttributeNode(parameterNode,
					"SMTPServerUserID").InnerText;

				  m_SMTPServerPassword = XMLHelper.GetAttributeNode(parameterNode,					"SMTPServerPassword").InnerText;

					m_TempFolder= XMLHelper.GetAttributeNode(parameterNode,					"TempFolderLocation").InnerText;
				m_TempFolder=m_TempFolder.Trim();
				if(m_TempFolder.Substring(m_TempFolder.Length-1)!="\\")
				{
					m_TempFolder+="\\";
				}
				
				if(XMLHelper.GetAttributeNode(parameterNode,
					"SMTPPort").InnerText.Trim() != "")
				{
					m_SMTPPort = int.Parse(XMLHelper.GetAttributeNode(parameterNode,"SMTPPort").InnerText) ;
				}

				SmtpMail.SmtpServer=m_SMTPServer;
				
			
				m_MailFrom = XMLHelper.GetAttributeNode(parameterNode,
				"FromEmailAddress").InnerText;

				m_MailTo = XMLHelper.GetAttributeNode(parameterNode,
				"ToEmailAddress").InnerText;
				
				
				
			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of EmailNotifier"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure of EmailNotifier"
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure of LogListener"
					+ e.Message);
				throw e;
			}

						


		}


		public void Unload()
		{
			// TODO:  Add LogListener.Unload implementation
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

		#region INotificationHandler Members

		public void Notify(string NotificationMessage, System.Collections.Hashtable NotificationParameters)
		{
			MailMessage mMsg = new MailMessage();
			mMsg.To=m_MailTo;
			mMsg.From=m_MailFrom;
			bool bCleanup=false; //cleanup attachment files
			string sAttachPath="";

			if (NotificationParameters.ContainsKey("subject"))
				mMsg.Subject=(string)NotificationParameters["subject"];

			#region attachments
			if (NotificationParameters.ContainsKey("attachmentName"))

			{
				
				string sAttachmentName = (string)NotificationParameters["attachmentName"];
				
			
				if (NotificationParameters.ContainsKey("attachmentContents"))
				{
				
					sAttachPath=m_TempFolder+sAttachmentName;

					string sReportContents=(string)NotificationParameters["attachmentContents"];

					StreamWriter swAttachmentWriter= new StreamWriter(sAttachPath);

					try
					{
						swAttachmentWriter.Write(sReportContents);
						swAttachmentWriter.Flush();

					}
					catch (Exception exx)
					{
						Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError,"EmailNotifier exception swallowed ." + exx.Message + "\r\n" +  exx.StackTrace);
							return;

					}
					finally
					{
						swAttachmentWriter.Close();
						bCleanup=true;
					}

					
					
				
					MailAttachment atchReport = new MailAttachment(sAttachPath,MailEncoding.Base64);

					mMsg.Attachments.Add(atchReport);
				}
				
			}
			#endregion

			mMsg.Fields[
				"http://schemas.microsoft.com/cdo/configuration/sendusing"]  = 2;
			mMsg.Fields[
				"http://schemas.microsoft.com/cdo/configuration/smtpserverport"]  = m_SMTPPort;
			//sendusing network = 2
			if (m_SMTPServerUID != "")
			{
				mMsg.Fields[
					"http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
				mMsg.Fields[
					"http://schemas.microsoft.com/cdo/configuration/sendusername"] = 
					m_SMTPServerUID;
				mMsg.Fields[
					"http://schemas.microsoft.com/cdo/configuration/sendpassword"] = 
					m_SMTPServerPassword;
			}

			mMsg.Body=NotificationMessage;
			mMsg.BodyEncoding = System.Text.Encoding.ASCII;
			
			try
			{
				
				
				SmtpMail.Send(mMsg);
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceVerbose ,
					"MailSent" + mMsg.Subject);
				if (bCleanup==true)	File.Delete(sAttachPath);
			}
			catch(System.Exception exx)
			{				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError,"EmailNotifier exception swallowed ." + exx.Message + "\r\n" +  exx.StackTrace);
			}

			
			
		}

		#endregion
	}
}
