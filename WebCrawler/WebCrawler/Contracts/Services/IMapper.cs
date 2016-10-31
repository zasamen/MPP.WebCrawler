using System.Collections.Generic;
using WebCrawler.Contracts.Models;

namespace WebCrawler.Contracts.Services
{
    internal interface IMapper
    {
        T Map<T>(IEnumerable<ICrawlerNode> nodeList);
        T Map<T>(string url, int nestingLevel, IEnumerable<ICrawlerNode> nodes);
    }
}
