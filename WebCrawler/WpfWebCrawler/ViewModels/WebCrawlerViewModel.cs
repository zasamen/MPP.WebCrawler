using WebCrawler.Contracts.OutputModels;
using WebCrawler.Contracts.Services;
using WpfWebCrawler.AsyncCommands;
using WpfWebCrawler.Models;

namespace WpfWebCrawler.ViewModels
{
    internal class WebCrawlerViewModel : BaseViewModel
    {
        #region Private Members

        private readonly WebCrawlerModel _webCrawlerModel = new WebCrawlerModel();
        private ICrawlResult _crawlResult;

        #endregion

        #region Public Members

        public IAsyncCommand CrawlCommand { get; }
        public ICrawlResult CrawlResult
        {
            get
            {
                return _crawlResult;
            }
            set
            {
                if (_crawlResult == value)
                    return;

                _crawlResult = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Ctor

        public WebCrawlerViewModel()
        {
            _webCrawlerModel = new WebCrawlerModel();
            CrawlCommand = new AsyncCommand(
                async () => {CrawlResult = await _webCrawlerModel.GetCrawlResultAsync();});
        }

        #endregion


    }
}
