namespace WebCrawlerLib.WebCrawler
{
    public interface ISimpleWebCrawler
    {
        CrawlResult PerformCrawlingAsync(string[] rootUrls);
    }
}
