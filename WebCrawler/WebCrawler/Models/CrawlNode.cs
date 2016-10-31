using System.Collections.Generic;
using WebCrawler.Contracts.Models;

namespace WebCrawler.Models
{
    public class CrawlNode : ICrawlNode
    {
        public string Url { get; set; }
        public string LevelDescription { get; set; }
        public IEnumerable<ICrawlNode> InternalNodes { get; set; }
    }
}
