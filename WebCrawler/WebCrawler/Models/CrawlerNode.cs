using System.Collections.Generic;
using WebCrawler.Contracts.Models;

namespace WebCrawler.Models
{
    public class CrawlerNode : ICrawlerNode
    {
        public string Url { get; set; }
        public string LevelDescription { get; set; }
        public IEnumerable<ICrawlerNode> InternalNodes { get; set; }
    }
}
