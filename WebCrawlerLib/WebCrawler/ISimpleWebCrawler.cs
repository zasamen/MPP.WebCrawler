using System.Threading.Tasks;

namespace WebCrawlerLib.WebCrawler
{
    public interface ISimpleWebCrawler
    {
        Task<CrawlResult> PerformCrawlingAsync(int depth, string[] rootUrls);
    }
}
