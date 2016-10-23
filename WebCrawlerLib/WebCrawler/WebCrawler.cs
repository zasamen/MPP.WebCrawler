using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace WebCrawlerLib.WebCrawler
{
    public class WebCrawler : ISimpleWebCrawler
    {
        private int MaxCrawlingDepth = 5;

        public CrawlResult PerformCrawlingAsync(string[] rootUrls)
        {
            CrawlResult rootTree = new CrawlResult();

            foreach (string rootUrl in rootUrls)
            {
                CrawlResult crawlUrl = CreateUrlTree(rootUrl, 0);
                rootTree.AddNestedUrl(crawlUrl);

            }

            return rootTree;
        }


        private CrawlResult CreateUrlTree(string rootUrl, int depth)
        {
            if (depth >= MaxCrawlingDepth)
            {
                return null;
            }

            HtmlNode htmlPage = LoadHtml(rootUrl);
            if (htmlPage == null)
            {
                return null;
            }

            string[] urls = FindUrls(htmlPage);
            CrawlResult crawlResult = new CrawlResult(rootUrl);

            foreach (string url in urls)
            {
                CrawlResult innerUrl = CreateUrlTree(url, depth++);
                crawlResult.AddNestedUrl(innerUrl);
            }

            return crawlResult;
        }

        private HtmlNode LoadHtml(string url)
        {

            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(url);
                return document.DocumentNode;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        private bool UrlIsValid(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }

        private string[] FindUrls(HtmlNode page)
        {
            List<string> hrefTags = new List<string>();

            foreach (HtmlNode link in page.Descendants("a"))
            {

                HtmlAttribute hrefAttribute = link.Attributes["href"];
                if (hrefAttribute != null && !hrefAttribute.Value.Equals(string.Empty))
                {
                    hrefTags.Add(hrefAttribute.Value);
                }

            }

            return hrefTags.ToArray();
        }
    }
}
