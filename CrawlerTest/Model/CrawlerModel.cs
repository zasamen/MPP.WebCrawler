using System.Collections.Generic;
using System.Threading.Tasks;
using Crawler;
using Crawler.Parsers;
using CrawlerTest.Config;

namespace CrawlerTest.Model
{
    internal class CrawlerModel
    {
        private readonly string configFilename = "config.xml";
        private readonly IConfigReader configReader;
        private readonly ISimpleWebCrawler crawler;
        internal CrawlerModel()
        {
            configReader = new XmlConfigReader(configFilename);
            crawler = new WebCrawler(new CsQueryParser(), configReader.GetCrawlerConfig());
        }

        internal Task<CrawlResult> GetCrawlerResult()
        {
            return GenerateCrawlResultAsync(configReader.GetRootUrls());
        }

        private Task<CrawlResult> GenerateCrawlResultAsync(IEnumerable<string> rootUrls)
        {
            return crawler.PerformCrawlingAsync(rootUrls);
        }

    }
}
