using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CsQuery.ExtensionMethods.Internal;
using NLog;

namespace WebCrawler
{
    public sealed class SimpleWebCrawler : IWebCrawler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IHtmlParser _htmlParser;
        private int _nestingDepth;

        // Public

        public const int MaxNestingDepth = 6;

        public int NestingDepth
        {
            get { return _nestingDepth; }
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

        public async Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls)
        {
            return await CrawlRootsAsync(rootUrls);
        }

        // Internals

        private async Task<CrawlResult> CrawlRootsAsync(string[] rootUrls)
        {
            var visitedUrls = new ISet<string>[rootUrls.Length];
            var crawledUrls = new Dictionary<string, CrawlResult>();

            for (int i = 0; i < rootUrls.Length; i++)
            {
                visitedUrls[i] = new HashSet<string>();
                string url = rootUrls[i];
                string[] urlsToCrawl = await GetUniqueLinksFromUrlAsync(url);
                if (urlsToCrawl != null)
                {
                    visitedUrls[i].Add(url);
                    CrawlResult nestedResult = await CrawlNestedAsync(urlsToCrawl, 2, visitedUrls[i]);
                    crawledUrls.Add(url, nestedResult);
                }
            }
            return new CrawlResult(crawledUrls);
        }

        private async Task<CrawlResult> CrawlNestedAsync(string[] rootUrls, int currentDepth, ISet<string> visitedUrls)
        {            
            var crawledUrls = new Dictionary<string, CrawlResult>();
            if (currentDepth <= NestingDepth)
            {
                crawledUrls.AddRange(rootUrls.Select(x => new KeyValuePair<string, CrawlResult>(x, new CrawlResult(new Dictionary<string, CrawlResult>()))));
                KeyValuePair<string, CrawlResult>[] newUrls = crawledUrls.Where(x => !visitedUrls.Contains(x.Key)).ToArray();
                visitedUrls.AddRange(rootUrls);
                foreach (KeyValuePair<string, CrawlResult> urlResult in newUrls)
                {
                    string url = urlResult.Key;
                    string[] urlsToCrawl = await GetUniqueLinksFromUrlAsync(url);
                    if (urlsToCrawl != null)
                    {
                        crawledUrls[url] = await CrawlNestedAsync(urlsToCrawl, currentDepth + 1, visitedUrls);
                    }                                               
                }
            }
            return new CrawlResult(crawledUrls);                       
        }

        private async Task<string[]> GetUniqueLinksFromUrlAsync(string url)
        {
            string[] result = null;
            string page = await LoadPageAsync(url);
            if (page != null)
            {
                result = _htmlParser.GetLinksFromPage(page).Select(x => GetAbsoluteUrl(url, x)).Distinct().ToArray();
            }
            return result;
        }

        // Static internals

        private static async Task<string> LoadPageAsync(string url)
        {
            string result = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    result = await httpClient.GetStringAsync(url);
                }
            }
            catch (Exception e)
            {
                LogException(e);
            }
            return result;
        }

        private static void LogException(Exception exception)
        {
            Logger.Warn(exception.Message);
        }

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
    }
}
