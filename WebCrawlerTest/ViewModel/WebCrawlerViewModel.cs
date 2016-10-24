using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WebCrawlerLib.WebCrawler;
using WebCrawlerTest.Model;

namespace WebCrawlerTest.ViewModel
{
    internal class WebCrawlerViewModel : BindableBase
    {
        private WebCrawlerModel webCrawlerModel;
        public StartCrawlingCommand CrawlingCommand { get; set; }

        private CrawlResult webCrawlResult;
        public CrawlResult WebCrawlResult
        {
            get
            {
                return webCrawlResult;
            }
            set
            {
                webCrawlResult = value;
                OnPropertyChanged(nameof(WebCrawlResult));
            }
        }

        public WebCrawlerViewModel()
        {
            webCrawlerModel = new WebCrawlerModel();
            CrawlingCommand = new StartCrawlingCommand(
                async () => 
                {
                    if (CrawlingCommand.CanExecute(new object()))
                    {
                        CrawlingCommand.Disable();
                        WebCrawlResult = await webCrawlerModel.GetWebCrawlingResultAsync();
                        CrawlingCommand.Enable();
                    }
                  
                }
            );
        }
    }
}
