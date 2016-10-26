using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crawler
{
    public interface ISimpleWebCrawler
    {
        Task<CrawlResult> PerformCrawlingAsync(IEnumerable<string> rootUrls);
    }
}
