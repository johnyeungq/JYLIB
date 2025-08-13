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
    }
}
