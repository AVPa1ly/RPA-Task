using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ParserApp
{
    class XmlModelParser : IXmlParser
    {
        public List<ModelItem> ParseXmlData(string xmlSourcePath)
        {
            List<ModelItem> modelItems = new List<ModelItem>();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlSourcePath);

            XmlElement? xmlRoot = xmlDocument.DocumentElement;
            if (xmlRoot is null)
            {
                throw new XmlException("Root element does not exist");
            }

            XmlNodeList? itemNodes = xmlRoot.SelectNodes("item");
            if (itemNodes is not null)
            {
                foreach (XmlNode node in itemNodes)
                {
                    string title = node.SelectSingleNode("title").InnerText;
                    string link = node.SelectSingleNode("link").InnerText;
                    string description = node.SelectSingleNode("description").InnerText;
                    string category = node.SelectSingleNode("category").InnerText;
                    DateTime pubDate = DateTime.Parse(node.SelectSingleNode("pubDate").InnerText);
                    modelItems.Add(new ModelItem(title, link, description, category, pubDate));
                }
            }
            return modelItems;
        }
    }
}
