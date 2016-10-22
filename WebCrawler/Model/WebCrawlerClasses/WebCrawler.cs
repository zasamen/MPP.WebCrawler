using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace WebCrawlerProject.Model.WebCrawlerClasses
{
    public class WebCrawler : IWebCrawler
    {
        private int MaxCrawlingDepth = 5;

        public CrawlerResult PerformCrawlingAsync(string[] rootUrls)
        {
            CrawlerResult result = new CrawlerResult();

            foreach(string rootUrl in rootUrls)
            {
                UrlTree crawlUrl = CreateUrlTree(rootUrl, 0);
                result.AddChildUrl(crawlUrl);
                
            }

            return result;
        }


        private UrlTree CreateUrlTree(string rootUrl, int depth)
        {
            UrlTree urlTree = null;
            if(depth < MaxCrawlingDepth)
            {
                urlTree = new UrlTree(rootUrl);
                HtmlNode htmlPage = LoadHtml(rootUrl);
                if(htmlPage != null)
                {
                    string[] urls = FindUrls(htmlPage);
                    foreach (string url in urls)
                    {
                        UrlTree innerUrl = CreateUrlTree(url, depth++);
                        if(innerUrl != null)
                        {
                            urlTree.AddChildUrl(innerUrl);
                        }
                    }
                }
                
            }
            
            return urlTree;
        }

        private HtmlNode LoadHtml(string url)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(url);
                return document.DocumentNode;
            }
            catch(Exception e)
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

            foreach (HtmlNode link in page.SelectNodes("//a[@href]"))
            {
                HtmlAttribute hrefAttribute = link.Attributes["href"];
                if (!hrefAttribute.Value.Equals(string.Empty))
                {
                    hrefTags.Add(hrefAttribute.Value);
                }
                
            }

            return hrefTags.ToArray();
        }
    }
}

