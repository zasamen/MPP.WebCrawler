using System.Collections.Generic;

namespace CrawlerTest.Config
{
    internal interface IConfigReader
    {
        IEnumerable<string> GetRootUrls();
        CrawlerConfigData GetCrawlerConfig();
    }
}
