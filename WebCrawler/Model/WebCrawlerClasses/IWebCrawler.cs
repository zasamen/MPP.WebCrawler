namespace WebCrawlerProject.Model.WebCrawlerClasses
{
    public interface IWebCrawler
    {
        CrawlerResult PerformCrawlingAsync(string[] rootUrls);
    }
}
