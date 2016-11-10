using System.Collections.Generic;
using WebCrawler.Contracts.OutputModels;

namespace WebCrawler.OutputModels
{
    public class CrawlNode : ICrawlNode
    {
        public string Url { get; set; }
        public string LevelDescription { get; set; }
        public IEnumerable<ICrawlNode> InternalNodes { get; set; }
    }
}
