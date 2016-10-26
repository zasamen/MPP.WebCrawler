using System.Collections.Generic;

namespace WebCrawler
{
    public sealed class CrawlResult
    {
        public Dictionary<string, CrawlResult> Urls { get; set; }
    }
}