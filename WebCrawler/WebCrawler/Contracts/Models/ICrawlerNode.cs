using System.Collections.Generic;

namespace WebCrawler.Contracts.Models
{
    public interface ICrawlerNode
    {
        string Url { get; set; }
        string LevelDescription { get; set; }
        IEnumerable<ICrawlerNode> InternalNodes { get; set; }
    }
}
