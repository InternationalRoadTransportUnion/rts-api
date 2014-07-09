using System;
using System.Xml;
using System.Xml.Schema;
using System.Text ;


namespace IRU.RTS.CommonComponents
{
	/// <summary>
	/// Summary description for Util.
	/// </summary>
	public class XMLHelper
	{
		public XMLHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Wrapper function which returns XML node from the given XPATH
		/// </summary>
		/// <param name="nodeToSearch">XmlNode which is to be searched</param>
		/// <param name="XPATH">XPATH to search</param>
		/// <returns>XML Node for the given XPATH</returns>
		public static XmlNode SelectSingleNode(XmlNode nodeToSearch, string XPATH)
		{
			if(nodeToSearch == null || XPATH == null)
			{
				throw new ArgumentNullException();
			}
            
			XmlNode xmlNodeRequested = nodeToSearch.SelectSingleNode(XPATH);
			if(xmlNodeRequested == null)
			{
				throw new ApplicationException("XmlNode represented by " + XPATH + " is not found");
			}
			else
				return xmlNodeRequested  ;
			
		}


		/// <summary>
		/// Wrapper function which returns XML nodes for the given XPATH
		/// </summary>
		/// <param name="nodeToSearch">XmlNode which is to be searched</param>
		/// <param name="XPATH">XPATH to search</param>
		/// <returns>XML Nodes for the given XPATH</returns>
		public static XmlNodeList SelectNodes(XmlNode nodeToSearch, string XPATH)
		{
			if(nodeToSearch == null || XPATH == null)
			{
				throw new ArgumentNullException();
			}
            
			XmlNodeList xmlNodesRequested = nodeToSearch.SelectNodes(XPATH);
			if(xmlNodesRequested == null || xmlNodesRequested.Count == 0 )
			{
				throw new ApplicationException("XmlNodes represented by " + XPATH + " are not found");
			}
			return	xmlNodesRequested;
			
		}

		
		
		/// <summary>
		/// Wrapper function which returns XML Attribute for the given Attribute Name
		/// </summary>
		/// <param name="nodeToSearch">XmlNode containing that attribute</param>
		/// <param name="attributeName">Atttribute to search</param>
		/// <returns>Attribute</returns>
		public static XmlAttribute GetAttributeNode(XmlNode nodeToSearch ,string attributeName)
		{
			XmlElement xSearchElement = nodeToSearch as XmlElement; // casts without throwing exception.. If fails to cast returns null
			
			if(xSearchElement == null)
			{
				throw new ArgumentNullException();					
			}
			XmlAttribute attributeRequested =  xSearchElement.GetAttributeNode(attributeName);
			if(attributeRequested == null)
			{
				throw new ApplicationException("XML Attribute '" + attributeName + "' is not found");
			}
			
			return attributeRequested;
		}


		


	
	}


	/// <summary>
	/// Class used to perform XML validation. This was created separate from xmlhelper since it requires callbacks for
	/// validating reader and we cant have static callbacks
	/// </summary>
	public  class XMLValidationHelper
	{
		internal static XmlSchemaCollection  m_SchemaCollection = new XmlSchemaCollection();//holds the 

		private string m_ValidationDescription;

		private bool m_IsValid;


////		public bool IsValid
////		{
////			get
////			{
////				return m_IsValid;
////			}
////	
////	
////		}

//////		public string ValidationDescription
//////		{
//////		
//////			get
//////			{
//////				return m_ValidationDescription;
//////			}
//////		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="DocumentToValidate"></param>
		/// <param name="FailureReason"></param>
		/// <returns></returns>
		public bool  ValidateXML(string DocumentToValidate, out string FailureReason)
		{

			NameTable nt = new NameTable();
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
//			nsmgr.AddNamespace("def", "http://www.iru.org/TCHQuery");

			//Create the XmlParserContext.
			XmlParserContext context = new XmlParserContext(null, null, null, XmlSpace.None);

			//Implement the reader. 
						
			XmlValidatingReader v = new XmlValidatingReader(DocumentToValidate, XmlNodeType.Element, context);

			v.ValidationType = ValidationType.Schema;

			v.Schemas.Add(m_SchemaCollection);

			//v.ValidationEventHandler+=new ValidationEventHandler(v_ValidationEventHandler);

			m_IsValid=true;  

			try
			{
				while (v.Read())
				{
				
				}
			}
			catch (XmlSchemaException xes)
			{
				m_IsValid=false;
				m_ValidationDescription=xes.Message;
				Exception ex = xes.GetBaseException();
				xes.ToString();			
				object sch = xes.SourceSchemaObject;
			
				//string str = "";
			}
			catch (XmlException xes)
			{
				m_IsValid=false;
				m_ValidationDescription="Line Number:"+ xes.LineNumber.ToString()+" - "+ xes.Message + " - " + xes.Source ;
			
			
			}
			/*catch (Exception ex)
			{
				m_IsValid=false;
				FailureReason="Invalid XML - "+ ex.Message + " - \r\n - " +ex.StackTrace + " - \r\n - " +ex.Source ;
			}*/

			v.Close();
			FailureReason = m_ValidationDescription;
			return m_IsValid;

		
		}

		public static void PopulateSchemas(string NameSpace, string SchemaFile)
		{
			m_SchemaCollection.Add(NameSpace, SchemaFile);		
		
		}

		public static void ChangeEncoding(ref string XMLString,string Encoding)
		{
			XmlDocument xd = new XmlDocument();
			xd.PreserveWhitespace = true;
			xd.LoadXml(XMLString);
			XmlNode xnode = xd.FirstChild;
			if(xnode.NodeType == XmlNodeType.XmlDeclaration)
			{
				//XmlDeclaration xmlDecl = ((XmlDeclaration)xd.FirstChild);
				XmlDeclaration xmlDecl = ((XmlDeclaration)xnode);
				xmlDecl.Encoding = Encoding;
				XMLString = xd.OuterXml ;
			}
			else
			{
				StringBuilder decl = new StringBuilder("<?xml version='1.0' encoding='"+Encoding+"'?>\n");
				decl.Append(xd.OuterXml); 

				XMLString = decl.ToString() ;

			}

			
			//XmlDeclaration xDecNode = new XmlDeclaration();
			
		}

////		private void v_ValidationEventHandler(object sender, ValidationEventArgs e)
////		{
////			m_IsValid=false;
////			m_ValidationDescription += e.Message;
////		}

		

	}
}
