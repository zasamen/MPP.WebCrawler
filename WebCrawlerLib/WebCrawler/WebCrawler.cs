using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace WebCrawlerLib.WebCrawler
{
    public class WebCrawler : ISimpleWebCrawler
    {
        private int maxCrawlingDepth;
        private int MaxCrawlingDepth
        {
            set
            {
                if(value < 1 || value > 6)
                {
                    maxCrawlingDepth = 6;
                }
                else
                {
                    maxCrawlingDepth = value;
                }
            }
        }

        public CrawlResult PerformCrawlingAsync(int depth, string[] rootUrls)
        {
            MaxCrawlingDepth = depth;
            CrawlResult rootTree = new CrawlResult();

            foreach (string rootUrl in rootUrls)
            {
                CrawlResult crawlUrl = CreateUrlTree(rootUrl, 1);
                rootTree.AddNestedUrl(crawlUrl);

            }

            return rootTree;
        }


        private CrawlResult CreateUrlTree(string rootUrl, int currentDepth)
        {
            CrawlResult crawlResult = new CrawlResult(rootUrl);
            if(currentDepth < maxCrawlingDepth)
            {
                HtmlNode htmlPage = LoadHtml(rootUrl);
                List<String> urls = FindUrls(htmlPage);

                foreach (string url in urls)
                {
                    CrawlResult innerUrl = CreateUrlTree(url, currentDepth+1);
                    crawlResult.AddNestedUrl(innerUrl);
                }
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

        private List<string> FindUrls(HtmlNode page)
        {
            List<string> hrefTags = new List<string>();

            foreach (HtmlNode link in page.Descendants("a"))
            {
                if (link.Attributes.Contains("href"))
                {
                    HtmlAttribute hrefAttribute = link.Attributes["href"];
                    if (hrefAttribute.Value.StartsWith("http"))
                    {
                        hrefTags.Add(hrefAttribute.Value);
                    }
                    
                }

            }

            return hrefTags;
        }
    }
}
