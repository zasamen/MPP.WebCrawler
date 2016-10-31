using System.Collections.Generic;

namespace WebCrawler.Contracts.Models
{
    public interface ICrawlNode
    {
        string Url { get; set; }
        string LevelDescription { get; set; }
        IEnumerable<ICrawlNode> InternalNodes { get; set; }
    }
}
