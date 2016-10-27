using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public sealed class SimpleWebCrawler : IWebCrawler
    {
        private readonly IHtmlParser _htmlParser;
        private int _nestingDepth;
        
        // Public

        public const int MaxNestingDepth = 6;
        public int NestingDepth
        {
            get
            {
                return _nestingDepth;
            }
            set
            {
                if (value <= MaxNestingDepth)
                {
                    _nestingDepth = value;
                }
                else
                {
                    throw new ArgumentException($"Nesting depth shouldn't be greater than {MaxNestingDepth}");
                }
            }
        }

        public SimpleWebCrawler(IHtmlParser htmlParser, int nestingDepth)
        {
            _htmlParser = htmlParser;
            NestingDepth = nestingDepth;
        }

        public SimpleWebCrawler(int nestingDepth) : this(new CsQueryParser(), nestingDepth)
        {
        }

        public async Task<CrawlResult> PerformCrawlingAsync(IEnumerable<string> rootUrls)
        {
            return await InternalPerformCrawlingAsync(string.Empty, rootUrls, 1);
        }

        // Internals

        private async Task<CrawlResult> InternalPerformCrawlingAsync(string baseUrl, IEnumerable<string> rootUrls,
            int currentDepth)
        {
            var crawledUrls = new Dictionary<string, CrawlResult>();
            if (currentDepth <= NestingDepth)
            {
                foreach (string url in rootUrls)
                {
                    string absoluteUrl = GetAbsoluteUrl(baseUrl, url);
                    if (!crawledUrls.ContainsKey(absoluteUrl))
                    {
                        string page = await LoadPageAsync(absoluteUrl);
                        IEnumerable<string> urlsToCrawl = _htmlParser.GetLinksFromPage(page);
                        CrawlResult nestedCrawlResult = await InternalPerformCrawlingAsync(absoluteUrl, urlsToCrawl, currentDepth + 1);
                        crawledUrls.Add(absoluteUrl, nestedCrawlResult);
                    }
                }
            }

            return new CrawlResult() {Urls = crawledUrls};
        }

        // Static internals

        private static string GetAbsoluteUrl(string parentUrl, string url)
        {
            string result;
            if (!string.IsNullOrEmpty(parentUrl))
            {
                result = new Uri(new Uri(parentUrl), url).AbsoluteUri;
            }
            else
            {
                result = url;
            }

            return result;
        }

        private static async Task<string> LoadPageAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetStringAsync(url);
            }
        }
    }
}
