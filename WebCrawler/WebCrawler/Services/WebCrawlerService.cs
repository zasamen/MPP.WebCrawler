using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WebCrawler.Contracts.OutputModels;
using WebCrawler.Contracts.Services;

namespace WebCrawler.Services
{
    public class WebCrawlerService : IWebCrawlerService
    {
        #region Private Members

        private const byte MaxSearchDepth = 6;
        private readonly ILinkFinderService _linkFinder = new LinkFinderService();
        private readonly IMapperService _mapper = new MapperService();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
        private readonly int _searchDepth;
        private Task<ICrawlResult> _mainTask;

        #endregion

        #region Ctor

        public WebCrawlerService(int searchDepth)
        {
            if (searchDepth <= 0 || searchDepth > MaxSearchDepth)
                throw new ArgumentException(string.Format("Nesting level must be between 1 and {0}", MaxSearchDepth));
            _searchDepth = searchDepth;
        }

        #endregion

        #region Public Methods

        public Task<ICrawlResult> PerformCrawlingAsync(IEnumerable<string> rootUrls)
        {
            var token = _cancellationToken.Token;
            _mainTask = Task.Run(() =>
            { 
                var result = rootUrls.AsParallel()
                                    .Select((string x) => GetInternalNodesAsync(x, 0,token).Result)
                                    .ToArray();
                return _mapper.Map<ICrawlResult>(result);
            },token);
            return _mainTask;
        }

        public void Dispose()
        {
            try
            {
                _cancellationToken.Cancel();
                _mainTask.Wait();
            }
            catch(Exception e)
            {
                _logger.Warn(e.Message);
            }
            finally
            {
                _mainTask.Dispose();
                _cancellationToken.Dispose();
            }
        }

        #endregion

        #region Private Methods

        private async Task<ICrawlNode> GetInternalNodesAsync(string url, byte nestingLevel,CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            nestingLevel++;

            if (nestingLevel >= _searchDepth)
                return _mapper.Map<ICrawlNode>(url, --nestingLevel, null);

            string htmlCode = await LoadPageAsync(url);
            var result = _linkFinder.Find(htmlCode)
                                .AsParallel()
                                .Select((string x) => GetInternalNodesAsync(x, nestingLevel,token).Result)
                                .ToArray();
            return _mapper.Map<ICrawlNode>(url, --nestingLevel, result);
        }

        private async Task<string> LoadPageAsync(string url)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    return await webClient.DownloadStringTaskAsync(url);
                }
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
                return string.Empty;
            }
        }

        #endregion
    }
}
