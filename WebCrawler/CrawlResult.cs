using System.Collections.Generic;

namespace WebCrawler
{
    public sealed class CrawlResult
    {
        public Dictionary<string, CrawlResult> Urls { get; set; }

        public CrawlResult(Dictionary<string, CrawlResult> urls)
        {
            Urls = urls;
        }
    }
}