using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.Schema;


namespace IRU.RTS.TIREPD
{
    public class EPDXMLValidationHelper
    {

        internal static Hashtable m_SchemaCollectionDict = new Hashtable();
        //internal static XmlSchemaCollection m_SchemaCollection = new XmlSchemaCollection();//holds the 

        private string m_ValidationDescription;

        private bool m_IsValid;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentToValidate"></param>
        /// <param name="FailureReason"></param>
        /// <returns></returns>
        public bool ValidateXML(string DocumentToValidate, out string FailureReason)
        {

            NameTable nt = new NameTable();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
            //			nsmgr.AddNamespace("def", "http://www.iru.org/TCHQuery");

            //Create the XmlParserContext.
            XmlParserContext context = new XmlParserContext(null, null, null, XmlSpace.None);

            //Implement the reader. 


            foreach (DictionaryEntry DE in m_SchemaCollectionDict)
            {
                XmlSchemaCollection XSC = (XmlSchemaCollection)DE.Value;
                XmlValidatingReader v = new XmlValidatingReader(DocumentToValidate, XmlNodeType.Element, context);

                v.ValidationType = ValidationType.Schema;


                v.Schemas.Add(XSC);

                //v.ValidationEventHandler+=new ValidationEventHandler(v_ValidationEventHandler);

                m_IsValid = true;

                try
                {
                    while (v.Read())
                    {

                    }
                }
                catch (XmlSchemaException xes)
                {
                    m_IsValid = false;
                    m_ValidationDescription = xes.Message;
                    Exception ex = xes.GetBaseException();
                    xes.ToString();
                    object sch = xes.SourceSchemaObject;

                    //string str = "";
                }
                catch (XmlException xes)
                {
                    m_IsValid = false;
                    m_ValidationDescription = "Line Number:" + xes.LineNumber.ToString() + " - " + xes.Message + " - " + xes.Source;


                }
                /*catch (Exception ex)
                {
                    m_IsValid=false;
                    FailureReason="Invalid XML - "+ ex.Message + " - \r\n - " +ex.StackTrace + " - \r\n - " +ex.Source ;
                }*/

                v.Close();
                if (m_IsValid)
                {
                    break;
                }
            }
            FailureReason = m_ValidationDescription;
            return m_IsValid;


        }

        public static void PopulateSchemas(string NameSpace, string SchemaFile, string Type)
        {
            if (m_SchemaCollectionDict.Contains(Type))
            {
                ((XmlSchemaCollection)m_SchemaCollectionDict[Type]).Add(NameSpace, SchemaFile);
            }
            else
            {
                XmlSchemaCollection scoll = new XmlSchemaCollection();
                scoll.Add(NameSpace, SchemaFile);
                m_SchemaCollectionDict.Add(Type, scoll);
            }
            //m_SchemaCollection.Add(NameSpace, SchemaFile);

        }

        public static void ChangeEncoding(ref string XMLString, string Encoding)
        {
            XmlDocument xd = new XmlDocument();
            xd.PreserveWhitespace = true;
            xd.LoadXml(XMLString);
            XmlNode xnode = xd.FirstChild;
            if (xnode.NodeType == XmlNodeType.XmlDeclaration)
            {
                //XmlDeclaration xmlDecl = ((XmlDeclaration)xd.FirstChild);
                XmlDeclaration xmlDecl = ((XmlDeclaration)xnode);
                xmlDecl.Encoding = Encoding;
                XMLString = xd.OuterXml;
            }
            else
            {
                StringBuilder decl = new StringBuilder("<?xml version='1.0' encoding='" + Encoding + "'?>\n");
                decl.Append(xd.OuterXml);

                XMLString = decl.ToString();

            }


            //XmlDeclaration xDecNode = new XmlDeclaration();

        }

    }
}
