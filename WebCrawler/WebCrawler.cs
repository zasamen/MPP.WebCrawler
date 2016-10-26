using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public sealed class WebCrawler : IWebCrawler
    {
        public Task<CrawlResult> PerformCrawlingAsync(IEnumerable<string> rootUrls)
        {
            throw new NotImplementedException();
        }
    }
}
