using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JYLIB
{
    internal class XML
    {


        internal void CreateXmlFile(string xmlFilePath, List<string> values, string child, string node)
        {
            // Create a new XML document
            XmlDocument xmlDoc = new XmlDocument();

            // Create the XML declaration
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlDeclaration);

            // Create the root element
            XmlElement rootElement = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(rootElement);

            // Add child nodes for each value
            foreach (string value in values)
            {
                // Create child element (e.g., Locations)
                XmlElement childElement = xmlDoc.CreateElement(child);

                // Create node element (e.g., Name) and set its value
                XmlElement nodeElement = xmlDoc.CreateElement(node);
                nodeElement.InnerText = value;

                // Append node to child, and child to root
                childElement.AppendChild(nodeElement);
                rootElement.AppendChild(childElement);
            }

            // Save the XML document to the specified file
            xmlDoc.Save(xmlFilePath);
        }


        internal List<string> XMLfiletoList(string xmlFilePath, string child, string Node)
        {
            List<string> var = new List<string>();

            // Load the XML file
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            // Get the root element of the XML document
            XmlElement rootElement = xmlDoc.DocumentElement;
            
            // Loop through eac;h child node of the root element
            foreach (XmlNode childNode in rootElement.ChildNodes)
            {
                // Check if the child node is a "Locations" node
                if (childNode.Name == child)
                {
                    // Get the value of the "Name" element
                    XmlNode nameNode = childNode.SelectSingleNode(Node);
                    if (nameNode != null)
                    {
                        string str = nameNode.InnerText;
                        var.Add(str);
                    }
                }
            }

            return var;
        }

       internal void XMLfileToCB(string xmlFilePath, string child, string Node, ComboBox cb) {


            List<string> var =XMLfiletoList(xmlFilePath, child, Node);
            cb.Items.Clear();
            foreach (string item in var)
            {
                cb.Items.Add(item);
            }

        }

        internal void XMLfileToLB(string xmlFilePath, string child, string Node, ListBox lb)
        {
            List<string> var = XMLfiletoList(xmlFilePath, child, Node);
            lb.Items.Clear();
            foreach (string item in var)
            {
                lb.Items.Add(item);
            }
        }
        internal void XMLfileToDGV(string xmlFilePath, string child, string Node, DataGridView dgv)
        {
            List<string> var = XMLfiletoList(xmlFilePath, child, Node);
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Columns.Add(Node, Node);
            foreach (string item in var)
            {
                dgv.Rows.Add(item);
            }
        }











    }
}
