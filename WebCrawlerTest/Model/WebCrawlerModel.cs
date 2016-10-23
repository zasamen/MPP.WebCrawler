using WebCrawlerLib.WebCrawler;

namespace WebCrawlerTest.Model
{
    internal class WebCrawlerModel
    {
        private ISimpleWebCrawler webCrawler;

        public WebCrawlerModel()
        {
            webCrawler = new WebCrawler();
        }

        public CrawlResult GetWebCrawlingResult()
        {
            string[] urls = new string[2];
            urls[0] = "http://www.codeproject.com/Articles/9949/Hierarchical-TreeView-control-with-data-binding-en";
            urls[1] = "http://stackoverflow.com/questions/8497673/html-agility-pack-parsing-an-href-tag";
            CrawlResult result = webCrawler.PerformCrawlingAsync(urls);

            return result;
        }
    }
}
