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
        private readonly SemaphoreSlim _semaphore;
        private readonly int _searchDepth;

        #endregion

        #region Ctor

        public WebCrawlerService(int searchDepth, int concurrencyLevel)
        {
            if (searchDepth <= 0 || searchDepth > MaxSearchDepth)
                throw new ArgumentException($"Nesting level must be between 1 and {MaxSearchDepth}");
            if (concurrencyLevel <= 0)
                throw new ArgumentException("Nesting level must be greater than 1");
            _semaphore = new SemaphoreSlim(concurrencyLevel);
            _searchDepth = searchDepth;
        }

        #endregion

        #region Public Methods

        public async Task<ICrawlResult> PerformCrawlingAsync(IEnumerable<string> rootUrls)
        {
            var token = _cancellationToken.Token;
            var tasks = rootUrls.Select(x => GetInternalNodesAsync(x, 0, token));
            var result = await Task.WhenAll(tasks);
            return _mapper.Map<ICrawlResult>(result);
        }

        public void Dispose()
        {
            try
            {
                _cancellationToken.Cancel();
            }
            catch(Exception e)
            {
                _logger.Warn(e.Message);
            }
            finally
            {
                _cancellationToken.Dispose();
            }
        }

        #endregion

        #region Private Methods

        private async Task<ICrawlNode> GetInternalNodesAsync(string url, int nestingLevel,CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (nestingLevel >= _searchDepth)
                return _mapper.Map<ICrawlNode>(url, nestingLevel, null);

            var htmlCode = await LoadPageAsync(url);

            var tasks = _linkFinder.Find(htmlCode).Select(x=>GetInternalNodesAsync(x, nestingLevel+1, token));
            var result = await Task.WhenAll(tasks);
            return _mapper.Map<ICrawlNode>(url, nestingLevel, result);
        }

        private async Task<string> LoadPageAsync(string url)
        {
            var result = string.Empty;
            await _semaphore.WaitAsync();
            try
            {
                using (var webClient = new WebClient())
                {
                    result = await webClient.DownloadStringTaskAsync(url);
                }
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
            }
            finally
            {
                _semaphore.Release();
            }
            return result;
        }

        #endregion
    }
}
