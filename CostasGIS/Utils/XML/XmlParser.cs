using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using System.Xml.Linq;

namespace Utils.XML
{
    public class XmlParser
    {
        public Stream StreamDoc { get; set; }
        private XmlTextReader xmlTextReader;

        public XmlParser() { }

        public XmlParser(Stream document)
        {
            this.StreamDoc = document;
        }

        public XmlParser(string xmlFilePath)
        {
            // Load the xml file into XmlDocument object.
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(xmlFilePath);
            }
            catch (XmlException e)
            {
                Console.WriteLine(e.Message);
            }
            // Now create StringWriter object to get data from xml document.
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xmlDoc.WriteTo(xw);
            byte[] byteArray = Encoding.ASCII.GetBytes(sw.ToString());
            MemoryStream documento = new MemoryStream(byteArray);
            this.StreamDoc = documento;
        }

        public string GetNodeValue(string nodeName)
        {
            string nodeValue = "";
            Hashtable nodeTable = new Hashtable();

            xmlTextReader = getXmlTextReader();
            // Load the reader with the data file and ignore all white space nodes.
            xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;

            // Parse the file and display each of the nodes.
            while (xmlTextReader.Read())
            {
                if (nodeName == xmlTextReader.Name)
                {
                    nodeValue = xmlTextReader.Value;
                }
            }
            this.StreamDoc.Seek(0, SeekOrigin.Begin);
            return nodeValue;
        }

        public Hashtable GetNodesValue(List<string> nodeNames)
        {
            Hashtable nodeTable = new Hashtable();
            xmlTextReader = getXmlTextReader();
            // Load the reader with the data file and ignore all white space nodes.
            xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;

            // Parse the file and display each of the nodes.
            while (xmlTextReader.Read())
            {
                foreach (string nodeName in nodeNames)
                {
                    if (nodeName == xmlTextReader.Name)
                    {
                        nodeTable.Add(nodeName, xmlTextReader.ReadString());
                    }
                }
            }
            this.StreamDoc.Seek(0, SeekOrigin.Begin);
            return nodeTable;
        }

        public List<string> GetNodeValues(string nodeName)
        {
            List<string> valueList = new List<string>();
            xmlTextReader = getXmlTextReader();
            // Load the reader with the data file and ignore all white space nodes.
            xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;

            // Parse the file and display each of the nodes.
            while (xmlTextReader.Read())
            {
                if (nodeName == xmlTextReader.Name)
                {
                    valueList.Add(xmlTextReader.ReadString());
                }
            }
            this.StreamDoc.Seek(0, SeekOrigin.Begin);
            return valueList;
        }

        public string GetXMLPart(string nodeName)
        {
            string result = "";
            xmlTextReader = getXmlTextReader();

            xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;

            // Parse the file and display each of the nodes.
            while (xmlTextReader.Read())
            {
                if (xmlTextReader.Name == nodeName)
                {
                    result = result + xmlTextReader.ReadOuterXml();
                }
            }
            this.StreamDoc.Seek(0, SeekOrigin.Begin);
            return result;
        }

        protected XmlTextReader getXmlTextReader()
        {
            return xmlTextReader = new XmlTextReader(this.StreamDoc);
        }
    }
}
    
