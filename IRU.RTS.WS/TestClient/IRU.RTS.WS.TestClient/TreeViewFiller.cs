using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace IRU.RTS.WS.TestClient
{
    public class TreeViewFiller
    {
        public static void populateTree(String xml, TreeView tv)
        {
            tv.BeginUpdate();
            try
            {
                tv.Nodes.Clear();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                TreeNode firstNode = new TreeNode("Response");

                //call function below to add TreeNodes to 'firstNode' from XmlNodes in 'doc'
                doNodes(doc, firstNode.Nodes);

                //treeXML is the name of my TreeView
                tv.Nodes.Add(firstNode);

                tv.ExpandAll();

                //clean up
                doc = null;
            }
            finally
            {
                tv.EndUpdate();
                tv.SelectedNode = tv.Nodes[0];
            }
        }

        private static void doNodes(XmlNode xn, TreeNodeCollection tnc)
        {
            foreach (XmlNode child in xn.ChildNodes)
            {
                TreeNode newTN = null;

                //add a TreeNode to newTN, text depends on whether or not the current XmlNode has children
                if (!child.HasChildNodes && !(child.Value == null))
                {
                    newTN = tnc.Add(child.Value);
                    doAttributes(child, newTN);
                }
                else
                {
                    newTN = tnc.Add(child.Name);
                    doAttributes(child, newTN);

                    //call this function again to do the children of the current XmlNode
                    doNodes(child, newTN.Nodes);
                }
            }
        }

        private static void doAttributes(XmlNode xn, TreeNode tn)
        {
            if (xn.Attributes == null)
                return;

            foreach (XmlAttribute at in xn.Attributes)
            {
                if (!at.Name.StartsWith("xmlns"))
                    tn.Nodes.Add(at.Name + "=\"" + at.Value + "\"");
            }
        }
    }
}
