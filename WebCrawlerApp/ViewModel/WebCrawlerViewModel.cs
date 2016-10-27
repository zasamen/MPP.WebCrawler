using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler;
using WebCrawlerApp.Model;

namespace WebCrawlerApp.ViewModel
{
    internal class WebCrawlerViewModel : BaseViewModel
    {
        private readonly WebCrawlerModel _webCrawlerModel;
        private CrawlResult _crawlResult;

        public AsyncCommand CrawlCommand { get; }

        public WebCrawlerViewModel()
        {
            _webCrawlerModel =  new WebCrawlerModel();
            CrawlCommand = new AsyncCommand(
            async () => {
                if (CrawlCommand.CanExecute)
                {
                    CrawlCommand.CanExecute = false;
                    CrawlResult = await _webCrawlerModel.PerformCrawlingAsync();
                    CrawlCommand.CanExecute = true;
                }
            });
        }

        public CrawlResult CrawlResult
        {
            get
            {
                return _crawlResult;
            }
            set
            {
                if (_crawlResult != value)
                {
                    _crawlResult = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
