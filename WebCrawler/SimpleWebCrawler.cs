using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace WebCrawler
{
    public sealed class SimpleWebCrawler : IWebCrawler
    {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IHtmlParser _htmlParser;
        private int _nestingDepth;
        private ISet<string>[] _visitedUrls;

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
            Initialize(rootUrls.Length);
            return await InternalPerformCrawlingAsync(string.Empty, rootUrls, 1, -1);
        }

        // Internals

        private void Initialize(int rootUrlsCount)
        {
            _visitedUrls = new ISet<string>[rootUrlsCount];
            for (int i = 0; i < rootUrlsCount; i++)
            {
                _visitedUrls[i] = new HashSet<string>();
            }
        }

        private async Task<CrawlResult> InternalPerformCrawlingAsync(string baseUrl, string[] rootUrls, int currentDepth,
            int visitedUrlsIndex)
        {
            var crawledUrls = new Dictionary<string, CrawlResult>();

            if (currentDepth <= NestingDepth)
            {
                for (int i = 0; i < rootUrls.Length; i++)
                {
                    string absoluteUrl = GetAbsoluteUrl(baseUrl, rootUrls[i]);
                    ISet<string> visitedUrlsForBranch;

                    if (visitedUrlsIndex == -1)
                    {
                        visitedUrlsForBranch = _visitedUrls[i];
                        visitedUrlsIndex = i;
                    }
                    else
                    {
                        visitedUrlsForBranch = _visitedUrls[visitedUrlsIndex];
                    }

                    if (!(crawledUrls.ContainsKey(absoluteUrl) || visitedUrlsForBranch.Contains(absoluteUrl)))
                    {
                        visitedUrlsForBranch.Add(absoluteUrl);

                        string page = await LoadPageAsync(absoluteUrl);
                        if (!string.IsNullOrEmpty(page))
                        {
                            string[] urlsToCrawl = _htmlParser.GetLinksFromPage(page).ToArray();

                            CrawlResult nestedCrawlResult =
                                await
                                    InternalPerformCrawlingAsync(absoluteUrl, urlsToCrawl, currentDepth + 1,
                                        visitedUrlsIndex);
                            crawledUrls.Add(absoluteUrl, nestedCrawlResult);
                        }
                    }
                }
            }

            return new CrawlResult() {Urls = crawledUrls};
        }

        private async Task<string> LoadPageAsync(string url)
        {
            string result = string.Empty;
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

        private void LogException(Exception exception)
        {
            Logger.Warn(exception.Message);
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
    }
}
