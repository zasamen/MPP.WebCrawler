using System.Collections.Generic;
using WebCrawler.Contracts.Models;

namespace WebCrawler.Models
{
    public class CrawlResult: ICrawlResult
    {
        public IEnumerable<ICrawlNode> RootNodes { get; set; }
    }
}