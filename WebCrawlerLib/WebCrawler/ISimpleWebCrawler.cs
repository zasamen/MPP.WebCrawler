namespace WebCrawlerLib.WebCrawler
{
    public interface ISimpleWebCrawler
    {
        CrawlResult PerformCrawlingAsync(int depth, string[] rootUrls);
    }
}
