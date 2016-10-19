using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CrawlerTest.Config.Exceptions;

namespace CrawlerTest.Config
{
    internal class XmlConfigReader : IConfigReader
    {
        private readonly string filename;
        private IEnumerable<string> urlList;
        private int searchDepth;
        private bool isLoaded;

        internal XmlConfigReader(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("Invalid path", nameof(filename));
            }
            this.filename = filename;
            isLoaded = false;
            urlList = new List<string>();
            searchDepth = 0;
        }

        public IEnumerable<string> GetRootUrls()
        {
            LoadConfigFromFile();
            IEnumerable<string> newUrlList = urlList.ToList(); //make a copy
            return newUrlList;
        }

        public CrawlerConfigData GetCrawlerConfig()
        {
            LoadConfigFromFile();
            return new CrawlerConfigData(searchDepth);
        }

        private void LoadConfigFromFile()
        {
            if(isLoaded) return;

            try
            {
                XDocument xDoc = XDocument.Load(filename);
                XElement rootElem = xDoc.Root;

                searchDepth = int.Parse(rootElem.Element("depth").Value);
                urlList = (from resource in rootElem.Element("rootResources").Elements("resource")
                        where !string.IsNullOrWhiteSpace(resource.Value)
                        select resource.Value).
                    ToList();

                isLoaded = true;
            }
            catch (Exception ex)
            {
                throw new ConfigLoadingException(ex);
            }
        }
    }
}
