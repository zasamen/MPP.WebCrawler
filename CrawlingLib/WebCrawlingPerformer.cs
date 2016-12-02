using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Serilog;

namespace CrawlingLib
{
    public class WebCrawlingPerformer : IWebCrawler
    {
        private int nestingNumber;
        private static readonly int maxNestingNumber = 6;
        private UrlLinkExtractor extractor;
        private ILogger logger;

        public WebCrawlingPerformer(int nestingNumber,string LogPath)
        {
            this.nestingNumber = nestingNumber < maxNestingNumber
                ? nestingNumber
                : maxNestingNumber;
            extractor = new UrlLinkExtractor();
            logger = new LoggerConfiguration().WriteTo.
                RollingFile(LogPath + "L{Date}.txt").CreateLogger();

        }

        public async Task<CrawlingResult> PerformCrawlingAsync(string[] roots)
        {
            return await CrawlLinksAsync("root", roots.ToList(), nestingNumber).ConfigureAwait(false);
        }

        private async Task<CrawlingResult> CrawlLinksAsync(
            string root, List<string> embeddedLinks, int level)
        {
            var result = new CrawlingResult(root);
            try
            {
                result.Children.AddRange(await CrawlEmbeddedLinksAsync(
                        root, embeddedLinks, level - 1));
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
            return result;
        }

        private async Task<CrawlingResult[]> CrawlEmbeddedLinksAsync(
            string root, List<string> links, int level)
        {
            return level > -1
                ? await CrawlLinkListAsync(links, level)
                : new CrawlingResult[0];
        }

        private Task<CrawlingResult[]> CrawlLinkListAsync(
            List<string> links, int level)
        {
            return Task.WhenAll(links.ConvertAll(async l 
                => await GetLinksAndCrawlThemAsync(l, level)).ToArray());
        }

        private async Task<CrawlingResult> GetLinksAndCrawlThemAsync(
            string link, int level)
        {
            return await CrawlLinksAsync(link, 
                await extractor.ExtractLinks(link), level);
        }

    }
}
