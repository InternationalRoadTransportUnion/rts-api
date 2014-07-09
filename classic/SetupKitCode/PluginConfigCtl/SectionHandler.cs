using System;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;

namespace IRU.Common.Config.PluginConfigCtl
{
	/// <summary>
	/// Summary description for SectionHandler.
	/// </summary>
	public class SectionHandler
	{

		XmlNode m_xDocNode;
		XmlNode m_xTabNode;
		TabControl m_TabParent;
        TabPage m_tbThisPage;
		ArrayList m_aFields;
		StringDictionary m_IContext;
		internal static int s_LeftMargin =20;
		internal 		static int s_VertSpace=20;
		internal static int s_LabelTextSpace = 20;

		int m_Y;



		public SectionHandler( TabControl TabParent, XmlNode xTabNode, XmlNode xDocNode, StringDictionary IContext )
		{
			m_xDocNode= xDocNode;
			m_xTabNode = xTabNode;
			m_TabParent = TabParent;
			m_Y = s_VertSpace;
			m_aFields = new ArrayList();
			m_IContext = IContext;
		}

		/// <summary>
		/// Parses the Template XML and displays editor Tab
		/// </summary>
		public void DisplayTab()
		{
			string tabName = m_xTabNode.Attributes["Name"].Value;
			
			m_tbThisPage = new TabPage(tabName);
			m_TabParent.TabPages.Add(m_tbThisPage);

			XmlNodeList xFieldList = m_xTabNode.SelectNodes("./Field");

			m_tbThisPage.SuspendLayout();

			foreach (XmlNode xNode  in xFieldList)
			{

				FieldHandler thisField = new FieldHandler(m_Y,m_xDocNode,xNode, m_IContext);


				
				#region add to tab
				m_tbThisPage.Controls.AddRange(thisField.CreateFieldControls() );
				#endregion
				
				#region reset y
				m_Y = m_Y + thisField.Height + s_VertSpace;
				#endregion

				m_aFields.Add(thisField);
			}

			m_tbThisPage.ResumeLayout();


		}

		
		public void SaveToDOM()
		{
			foreach (object objField in m_aFields)
			{

				FieldHandler thisField = (FieldHandler)objField;
				thisField.SaveFieldToDOM();
			
			}
		
		
		}
	}

	internal class FieldHandler
	{
		int m_Y;
		XmlNode m_xDocNode;
		XmlNode m_xFieldNode;
		TextBox  m_txtValue;
		Label m_lbCaption ;
		internal int Height;
		StringDictionary m_IContext ;

		internal FieldHandler(int YPos,  XmlNode xDocNode, XmlNode xFieldNode, StringDictionary IContext)
		{
			m_Y = YPos;
			m_xDocNode= xDocNode;
			m_xFieldNode= xFieldNode;
		
			m_IContext = IContext;
		}
		internal Control[] CreateFieldControls()
		{
			
			#region add Label
			m_lbCaption = new Label();
			string sCaption = m_xFieldNode.Attributes["Caption"].Value;
			m_lbCaption.Text=sCaption;
			m_lbCaption.Left=SectionHandler.s_LeftMargin;
			m_lbCaption.Top=m_Y;
			if (m_xFieldNode.Attributes["LabelLengthPx"] !=null)
				m_lbCaption.Width= int.Parse(m_xFieldNode.Attributes["LabelLengthPx"].Value);

			#endregion


			XmlAttribute xPathAttrib= m_xFieldNode.Attributes["XPath"];

			

			if (xPathAttrib!=null)
			{
				#region add TextBox
				m_txtValue = new TextBox();
				m_txtValue.Left= m_lbCaption.Left + m_lbCaption.Width + SectionHandler.s_LabelTextSpace ;
				m_txtValue.Top=m_Y;
				m_txtValue.Width= int.Parse(m_xFieldNode.Attributes["TextLengthPx"].Value);
				m_txtValue.MaxLength= int.Parse(m_xFieldNode.Attributes["MaxLength"].Value);


				#endregion

				#region set TexBoxValue
				string sxPath = xPathAttrib.Value;
				XmlNode xNodeToEdit = m_xDocNode.SelectSingleNode(sxPath);

				m_txtValue.Tag = xNodeToEdit;
				m_txtValue.Text=GetNodeValue((XmlNode)m_txtValue.Tag);


				#endregion


			

				#region set Height

				Height = m_txtValue.Height;
				#endregion
			}
			else
			{
				
				
				Height = m_lbCaption.Height;
			
			}
			return new Control[] {m_lbCaption ,m_txtValue};
		
		}
	
	
		private string GetNodeValue(XmlNode xNode)
		{
			string sValue=null;
			if (xNode.NodeType==System.Xml.XmlNodeType.Attribute)
				sValue= xNode.Value;
			else if (xNode.NodeType==System.Xml.XmlNodeType.Element)
				sValue= xNode.InnerText;
			 
			return ReplaceContext(sValue);
			
		}

		private string ReplaceContext(string Value)
		{
			string sTokenReplaced = Value;
			#region loop through properties in context and replace in string 


			if ((m_xFieldNode.Attributes["ValueTemplate"] ==null) || (m_IContext==null))
			return sTokenReplaced;


			 sTokenReplaced = m_xFieldNode.Attributes["ValueTemplate"].Value;


				StringDictionary parameters = m_IContext;
				string[] keys =new string[parameters.Count];
				parameters.Keys.CopyTo(keys,0); 
					
				// Set the StateServer collection values
				for(int intKeys=0;intKeys<keys.Length;intKeys++)
				{
					string sKey = keys[intKeys];
					string sValue = parameters[keys[intKeys]].ToString();
					string sTokenToReplace = "[" + sKey + "]";
					
					//replace with setup project tokens
					// Known tokens

/*
---------------------------
Commit Key/val:assemblypath::/::c:\rts\servicecustomaction.dll
---------------------------
Commit Key/val:logfile::/::
Commit Key/val:installtype::/::notransaction
---------------------------
Commit Key/val:target::/::C:\RTS\

Commit Key/val:action::/::commit
*/

					sTokenReplaced= sTokenReplaced.Replace(sTokenToReplace,sValue);

				
				
			}
			

			#endregion

		
			return sTokenReplaced;
		
		}

		private void SetNodeValue(XmlNode xNode, string NodeString)
		{
			if (xNode.NodeType==System.Xml.XmlNodeType.Attribute)
				xNode.Value = NodeString;
			else if (xNode.NodeType==System.Xml.XmlNodeType.Element)
				xNode.InnerText = NodeString;
			
		
		}

	

		internal void SaveFieldToDOM()
		{
			if (m_txtValue!=null)
			SetNodeValue((XmlNode)m_txtValue.Tag ,m_txtValue.Text);
		}
	}

}
