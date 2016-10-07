namespace Crawler.Config
{
    internal class DefaultCrawlerConfig : ICrawlerConfig
    {
        internal DefaultCrawlerConfig()
        {
            SearchDepth = 6;
        }

        public int SearchDepth { get; }
    }
}
