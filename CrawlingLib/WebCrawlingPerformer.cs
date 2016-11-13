﻿using System;
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
            return CrawlingResultFactoryForExtractedLinksAsync(roots, 
                nestingNumber + 1);
        }

        private async Task<CrawlingResult> CrawlingResultFactoryAsync(
            string link, int level)
        {
            return new CrawlingResult(link, level == 0
                ? null
                : await Task.WhenAll(CrawlLinksAsync(
                    await extractor.ExtractLinks(link), level - 1)));
        }

        private Task<CrawlingResult>[] CrawlLinksAsync(
            IEnumerable<IElement> links, int level)
        {
            List<Task<CrawlingResult>> TaskList = new List<Task<CrawlingResult>>();
            foreach (var embeddedLink in links)
            {
                TaskList.Add(CrawlingResultFactoryAsync(
                    embeddedLink.NodeValue, level));
            }
            return TaskList.ToArray();
        }

        private async Task<CrawlingResult> 
            CrawlingResultFactoryForExtractedLinksAsync(string[] links, int level)
        {
            return new CrawlingResult(string.Empty, await Task.WhenAll(CrawlLinksAsync(
                    links, level - 1)));
        }

        private Task<CrawlingResult>[] CrawlLinksAsync(string[] links, int level)
        {
            List<Task<CrawlingResult>> TaskList = new List<Task<CrawlingResult>>();
            foreach (var embeddedLink in links)
            {
                TaskList.Add(CrawlingResultFactoryAsync(
                    embeddedLink, level));
            }
            return TaskList.ToArray();
        }


    }
}
