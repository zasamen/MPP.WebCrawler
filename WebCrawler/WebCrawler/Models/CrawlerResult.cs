using System.Collections.Generic;
using WebCrawler.Contracts.Models;

namespace WebCrawler.Models
{
    public class CrawlerResult: ICrawlerResult
    {
        public IEnumerable<ICrawlerNode> RootNodes { get; set; }
    }
}