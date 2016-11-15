using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;

namespace CrawlingLib
{
    public class WebCrawlingPerformer : IWebCrawler
    {
        private int nestingNumber;
        private static readonly int maxNestingNumber = 6;
        private UrlLinkExtractor extractor;

        public WebCrawlingPerformer(int nestingNumber)
        {
            this.nestingNumber = nestingNumber < maxNestingNumber 
                ? nestingNumber 
                : maxNestingNumber;
            extractor = new UrlLinkExtractor();
        }

        public Task<CrawlingResult> PerformCrawlingAsync(string[] roots)
        {
            return Task.Run(() => CrawlRootAsync(roots, nestingNumber));
        }

        private async Task<CrawlingResult> CrawlLinkAsync(
            string link, int level)
        {
            return new CrawlingResult(link, level == -1
                ? null
                : await Task.WhenAll(CrawlEmbeddedLinksAsync(
                    await extractor.ExtractLinks(link), level - 1)));
        }

        private Task<CrawlingResult>[] CrawlEmbeddedLinksAsync(
            IEnumerable<string> links, int level)
        {
            List<Task<CrawlingResult>> TaskList = new List<Task<CrawlingResult>>();
            foreach (var embeddedLink in links)
            {
                TaskList.Add(CrawlLinkAsync(embeddedLink, level));
            }
            return TaskList.ToArray();
        }

        private async Task<CrawlingResult>
            CrawlRootAsync(string[] links, int level)
        {
            return new CrawlingResult("root", await Task.WhenAll(CrawlEmbeddedLinksAsync(
                    links, level - 1)));
        }
    }
}
