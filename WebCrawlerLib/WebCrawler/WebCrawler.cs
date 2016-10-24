using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCrawlerLib.WebCrawler
{
    public class WebCrawler : ISimpleWebCrawler
    {

        private const string SearchTag = "a";
        private const string SearchAttribute = "href";

        private HtmlWeb webClient;
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

        public WebCrawler()
        {
            webClient = new HtmlWeb();
        }

        public async Task<CrawlResult> PerformCrawlingAsync(int depth, string[] rootUrls)
        {
            MaxCrawlingDepth = depth;
            CrawlResult rootTree = new CrawlResult();

            foreach (string rootUrl in rootUrls)
            {
                CrawlResult crawlUrl = await CreateUrlTree(rootUrl, 1);
                rootTree.AddNestedUrl(crawlUrl);
            }
            
            return rootTree;
        }


        private async Task<CrawlResult> CreateUrlTree(string rootUrl, int currentDepth)
        {
            CrawlResult crawlResult = new CrawlResult(rootUrl);
            if(currentDepth < maxCrawlingDepth)
            {
                HtmlNode htmlPage = LoadHtml(rootUrl);
                if(htmlPage == null)
                {
                    return crawlResult;
                }

                List<String> urls = FindUrls(htmlPage);

                foreach (string url in urls)
                {
                    CrawlResult innerUrl = await CreateUrlTree(url, currentDepth+1);
                    crawlResult.AddNestedUrl(innerUrl);
                }
            }
            
            return crawlResult;
        }

        private HtmlNode LoadHtml(string url)
        {

            try
            {
                HtmlDocument document = webClient.Load(url);
                return document.DocumentNode;
            }
            catch (Exception e)
            {
                return null;
            }

        }


        private List<string> FindUrls(HtmlNode page)
        {
            List<string> hrefTags = new List<string>();

            foreach (HtmlNode link in page.Descendants(SearchTag))
            {
                if (link.Attributes.Contains(SearchAttribute))
                {
                    string attribute = link.Attributes[SearchAttribute].Value;
                    if (attribute.StartsWith("http"))
                    {
                        hrefTags.Add(attribute);
                    }
                    
                }
            }

            return hrefTags;
        }
    }
}
