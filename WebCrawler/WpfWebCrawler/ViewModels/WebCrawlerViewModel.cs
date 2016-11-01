using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WebCrawler.Contracts.OutputModels;
using WebCrawler.Contracts.Services;
using WpfWebCrawler.Commands;
using WpfWebCrawler.Models;

namespace WpfWebCrawler.ViewModels
{
    internal class WebCrawlerViewModel : BaseViewModel
    {
        #region Private Members

        private readonly WebCrawlerModel _webCrawlerModel = new WebCrawlerModel();
        private ICrawlResult _crawlResult;
        private int _clickCount;

        #endregion

        #region Public Members

        public IAsyncCommand CrawlCommand { get; }
        public ICrawlResult CrawlResult
        {
            get
            {
                return _crawlResult;
            }
            private set
            {
                if (_crawlResult == value)
                    return;

                _crawlResult = value;
                OnPropertyChanged();
            }
        }

        public int ClickCount
        {
            get
            {
                return _clickCount;
            }
            private set
            {
                if (_clickCount == value)
                    return;

                _clickCount = value;
                OnPropertyChanged();
            }
        }

        public ICommand ClickCommand { get; }

        #endregion

        #region Ctor

        public WebCrawlerViewModel()
        {
            _webCrawlerModel = new WebCrawlerModel();
            _clickCount = 0;

            CrawlCommand = new AsyncCommand(CrawlCommandHandler);
            ClickCommand = new SyncCommand(p => ClickCommandHandler(),p => true);
        }



        #endregion

        #region Private Methods

        private async Task CrawlCommandHandler()
        {
            try
            {
                CrawlCommand.SetCanExecuteStatus(false);
                CrawlResult = await _webCrawlerModel.GetCrawlResultAsync();
                CrawlCommand.SetCanExecuteStatus(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClickCommandHandler()
        {
            ClickCount++;
        }

        #endregion
    }


}
