using System.Collections.Generic;

namespace WebCrawler.Contracts.OutputModels
{
    public interface ICrawlResult
    {
        IEnumerable<ICrawlNode> RootNodes { get; set; }
    }
}
