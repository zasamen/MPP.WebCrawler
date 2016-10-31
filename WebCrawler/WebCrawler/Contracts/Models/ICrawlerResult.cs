using System.Collections.Generic;

namespace WebCrawler.Contracts.Models
{
    public interface ICrawlerResult
    {
        IEnumerable<ICrawlerNode> RootNodes { get; set; }
    }
}
