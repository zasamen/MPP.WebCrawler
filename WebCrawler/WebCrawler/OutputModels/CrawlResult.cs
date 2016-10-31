using System.Collections.Generic;
using WebCrawler.Contracts.OutputModels;

namespace WebCrawler.OutputModels
{
    public class CrawlResult: ICrawlResult
    {
        public IEnumerable<ICrawlNode> RootNodes { get; set; }
    }
}