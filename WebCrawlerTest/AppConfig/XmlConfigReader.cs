using System.Collections.Generic;
using System.Xml.Linq;

namespace WebCrawlerTest.AppConfig
{
    class XmlConfigReader : IConfigReader
    {
        public ConfigData ReadApplicationConfig(string filePath)
        {
            XDocument xmlDocument = XDocument.Load(filePath);
            ConfigData configData = new ConfigData();

            configData.Depth = GetDepth(xmlDocument);
            configData.RootResources = GetRootUrls(xmlDocument);

            return configData;
        }

        private int GetDepth(XDocument document)
        {
            XElement root = document.Root;
            string depth = root.Element("depth").Value;
            return int.Parse(depth);
        }

        private string[] GetRootUrls(XDocument document)
        {
            XElement root = document.Root;
            XElement rootResources = root.Element("rootResources");
            List<string> rootUrls = new List<string>();

            foreach(XElement resource in rootResources.Elements("resource"))
            {
                rootUrls.Add(resource.Value);
            }

            return rootUrls.ToArray();
         }
    }
}
