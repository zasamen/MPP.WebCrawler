using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler;

namespace WebCrawlerApp.Model
{
    internal class WebCrawlerModel
    {
        private const string ConfigFilename = "config.xml";
        private readonly IWebCrawler _crawler;
        private readonly IConfigProvider _configProvider;

        public WebCrawlerModel()
        {
            _configProvider = new XmlConfigReader(ConfigFilename);
            _crawler = new SimpleWebCrawler(_configProvider.NestingDepth);
        }

        public Task<CrawlResult> PerformCrawlingAsync()
        {
            return _crawler.PerformCrawlingAsync(_configProvider.RootUrls);
        }
    }
}
