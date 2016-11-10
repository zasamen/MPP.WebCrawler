using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCrawler.Contracts.OutputModels;

namespace WebCrawler.Contracts.Services
{
    public interface IWebCrawlerService : IDisposable
    {
        Task<ICrawlResult> PerformCrawlingAsync(IEnumerable<string> rootUrls);
    }
}
