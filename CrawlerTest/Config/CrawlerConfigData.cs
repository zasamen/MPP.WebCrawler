using Crawler.Config;

namespace CrawlerTest.Config
{
    public class CrawlerConfigData : ICrawlerConfig
    {
        public CrawlerConfigData(int depth)
        {
            SearchDepth = depth;
        }

        public int SearchDepth { get; }
    }
}
