using System.Collections.Generic;
using System.Xml.Linq;

namespace WebCrawlerTest.AppConfig
{
    class XmlConfigReader : IConfigReader
    {
        private const string DepthTag = "depth";
        private const string RootResourcesTag = "rootResources";
        private const string ResourceTag = "resource";

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
            string depth = root.Element(DepthTag).Value;
            return int.Parse(depth);
        }

        private string[] GetRootUrls(XDocument document)
        {
            XElement root = document.Root;
            XElement rootResources = root.Element(RootResourcesTag);
            List<string> rootUrls = new List<string>();

            foreach(XElement resource in rootResources.Elements(ResourceTag))
            {
                rootUrls.Add(resource.Value);
            }

            return rootUrls.ToArray();
         }
    }
}
