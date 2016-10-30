using HtmlAgilityPack;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawlerLib.WebCrawler
{
    public class WebCrawler : ISimpleWebCrawler, IDisposable
    {

        private const string SearchTag = "a";
        private const string SearchAttribute = "href";

        private bool wasDisposed;

        private HttpClient webClient;
        private int maxCrawlingDepth;
        private int MaxCrawlingDepth
        {
            set
            {
                if(outputAbroad(value))
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
            webClient = new HttpClient();
            wasDisposed = false;
        }

        public async Task<CrawlResult> PerformCrawlingAsync(int depth, string[] rootUrls)
        {
            if (wasDisposed)
            {
                throw new ObjectDisposedException("WebCrawler");
            }

            if(rootUrls.Length == 0)
            {
                return new CrawlResult("No nested urls");
            }

           MaxCrawlingDepth = depth;
           CrawlResult rootTree = new CrawlResult("Crawling result");
           
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
                string htmlPage = await LoadHtml(rootUrl);
                if(htmlPage == null)
                {
                    return crawlResult;
                }

                ConcurrentBag<string> urls = FindUrls(htmlPage);

                foreach (string url in urls)
                {
                    CrawlResult innerUrl = await CreateUrlTree(url, currentDepth+1);
                    crawlResult.AddNestedUrl(innerUrl);
                }
            }
            
            return crawlResult;
        }

        private async Task<string> LoadHtml(string url)
        {
            try
            {
                return await webClient.GetStringAsync(url);
                
            }
            catch (Exception e)
            {
                return null;
            }

        }

        private ConcurrentBag<string> FindUrls(string page)
        {
            HtmlDocument htmlPage = new HtmlDocument();
            htmlPage.LoadHtml(page);

            ConcurrentBag<string> hrefTags = new ConcurrentBag<string>();

            Parallel.ForEach(htmlPage.DocumentNode.Descendants(SearchTag), (link) => 
            {
                if (link.Attributes.Contains(SearchAttribute))
                {
                    string attribute = link.Attributes[SearchAttribute].Value;
                    if (attribute.StartsWith("http"))
                    {
                        hrefTags.Add(attribute);
                    }

                }
            });

            return hrefTags;
        }

        //check this method. Delete magin numbers.
        private bool outputAbroad(int value)
        {
            if (value < 1 || value > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (!wasDisposed)
            {
                webClient.Dispose();
                wasDisposed = true;
            }

        }
    }
}
