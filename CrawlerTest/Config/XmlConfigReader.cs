using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CrawlerTest.Config
{
    internal class XmlConfigReader : IConfigReader
    {
        private readonly string filename;
        private IEnumerable<string> urlList = null;
        private int? searchDepth = null;

        internal XmlConfigReader(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("Invalid path", nameof(filename));
            }
            this.filename = filename;
        }

        public IEnumerable<string> GetRootUrls()
        {
            if (urlList == null)
            {
                LoadConfigFromFile();
            }

            IEnumerable<string> newUrlList = urlList.ToList(); //make a copy
            return newUrlList;
        }

        public CrawlerConfigData GetCrawlerConfig()
        {
            if (searchDepth == null)
            {
                LoadConfigFromFile();
            }             
            return new CrawlerConfigData(searchDepth.Value);
        }
        
        private void LoadConfigFromFile()
        {
            XDocument xDoc = XDocument.Load(filename);
            XElement rootElem = xDoc.Root;

            searchDepth = int.Parse(rootElem.Element("depth").Value);
            urlList = (from resource in rootElem.Element("rootResources").Elements("resource")
                where !string.IsNullOrWhiteSpace(resource.Value)
                select resource.Value).
                ToList();

        }
    }
}
