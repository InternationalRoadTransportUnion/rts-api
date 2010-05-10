using System;
using IRU.CommonInterfaces;
using IRU.RTS.CommonComponents;
using System.Xml;
using System.IO;
using System.Collections;


namespace Test_FileMover1
{
	/// <summary>
	/// Summary description for MoveAFile.
	/// </summary>
	public class MoveAFile:IPlugIn, IActiveObject
	{
		private IPlugInManager m_PluginManager;
		private string m_PluginName;
		private string m_DestinationFolderLocation;

		public MoveAFile()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IPlugIn Members

		public void Configure(IPlugInManager pluginManager)
		{
			m_PluginManager = pluginManager;
			///			< FileSystemListener DeliverToPlugin="WSST_Processor" PollFolderLocation="g:\SafeTirInDrop" WorkingfolderLocation="g:\SafeTIRWorkingfolder"
			///			ArchiveFolderLocation="g:\SafeTIRUploadFileArchive" FilePattern="*.cif" ForceScanInterval="3"></FileSystemListener>

			try
			{
				XmlNode sectionNode = m_PluginManager.GetConfigurationSection(
					m_PluginName);
            
				XmlNode parameterNode = XMLHelper.SelectSingleNode(sectionNode,
					"./MoveAFile");


				

				m_DestinationFolderLocation = parameterNode.Attributes["DestinationFolderLocation"].InnerXml;

				
			}
			catch(ApplicationException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure"
					+ e.Message);
				throw e;
			}
			catch(ArgumentNullException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceError ,
					"XMLNode not found in .Configure "
					+ e.Message);
				throw e;
			}
			catch(FormatException e)
			{
				Statics.IRUTrace(this,Statics.IRUTraceSwitch.TraceInfo ,
					"Invalid value while formating an XMLNode in .Configure "
					+ e.Message);
				throw e;
			}


		}

		public void Unload()
		{
			// TODO:  Add MoveAFile.Unload implementation
		}

		public string PluginName
		{
			get
			{
		
				return m_PluginName;
			}
			set
			{
				m_PluginName= value;
			}
		}

		#endregion

		#region IActiveObject Members

		public void Enqueue(object objToEnqueue, string queueName)
		{
			// TODO:  Add MoveAFile.Enqueue implementation
			throw new NotImplementedException();
		}

		void IRU.CommonInterfaces.IActiveObject.Enqueue(object objToEnqueue)
		{
			Hashtable ht = (Hashtable) objToEnqueue;
			FileStream fsw = new FileStream(m_DestinationFolderLocation +"\\" + ht["FileName"],FileMode.Create,FileAccess.Write,FileShare.None) ;
			byte[] aFileContents= (byte[])ht["FileContents"];
			try
			{
				fsw.Write(aFileContents,0,aFileContents.Length);
			}
			finally
			{
				fsw.Close();
			}
		}

		#endregion
		

	}
}
