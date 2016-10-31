using System.Collections.Generic;

namespace WebCrawler.Contracts.Models
{
    public interface ICrawlResult
    {
        IEnumerable<ICrawlNode> RootNodes { get; set; }
    }
}
