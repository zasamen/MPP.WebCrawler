using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crawler.Config;
using Crawler.Parsers;
using NLog;

namespace Crawler
{
    public sealed class WebCrawler : ISimpleWebCrawler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private const int MaxSearchDepth = 6;

        private readonly IWebParser parser;
        private readonly ICrawlerConfig config;
        
        public WebCrawler() : this(new CsQueryParser(), new DefaultCrawlerConfig())
        {
        }
        public WebCrawler(IWebParser parser) : this(parser, new DefaultCrawlerConfig())
        {
        }
        public WebCrawler(IWebParser parser, ICrawlerConfig config)
        {
            this.parser = parser;
            this.config = CheckSearchDepthRestriction(config);
        }
        
        public async Task<CrawlResult> PerformCrawlingAsync(IEnumerable<string> rootUrls)
        {
            CrawlResult result = await CrawlUrlsAsync(string.Empty, rootUrls, 1);
            LogException(parser.ParserRuntimeExceptions);
            return result;
        }

        private ICrawlerConfig CheckSearchDepthRestriction(ICrawlerConfig currentConfig)
        {
            return currentConfig.SearchDepth > MaxSearchDepth ? new DefaultCrawlerConfig() : currentConfig;
        }

        private async Task<CrawlResult> CrawlUrlsAsync(string parentUrl, IEnumerable<string> rootUrls, int currentDepth)
        {
            Dictionary<string, CrawlResult> crawlResult =
                new Dictionary<string, CrawlResult>();
            foreach (string url in rootUrls)
            {
                CrawlResult currentUrlCrawlResult = null;
                if (currentDepth < config.SearchDepth)
                {
                    IEnumerable<string> parsedUrls = await parser.ParsePageForUrlAsync(parentUrl, url);
                    currentUrlCrawlResult = await CrawlUrlsAsync(url, parsedUrls, currentDepth + 1);
                }

                if ((url!= null) && (!crawlResult.ContainsKey(url)))
                {
                    crawlResult.Add(url, currentUrlCrawlResult);
                }
                
            }

            return new CrawlResult() { UrlDictionary = crawlResult };
        }

        private void LogException(Exception exception)
        {
            Logger.Warn(exception);
        }
    }
}
