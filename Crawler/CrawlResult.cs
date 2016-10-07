using System.Collections.Generic;

namespace Crawler
{
    public sealed class CrawlResult
    {
        public Dictionary<string, CrawlResult> UrlDictionary { get; set; }
    }
}
