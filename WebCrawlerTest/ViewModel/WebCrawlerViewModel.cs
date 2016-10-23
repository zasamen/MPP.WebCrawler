using WebCrawlerLib.WebCrawler;
using WebCrawlerTest.Model;

namespace WebCrawlerTest.ViewModel
{
    internal class WebCrawlerViewModel : BindableBase
    {
        private WebCrawlerModel webCrawlerModel;
        public StartCrawlingCommand StartCrawling { get; set; }
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
            StartCrawling = new StartCrawlingCommand(OnStartCrawlingAction);
        }

        private void OnStartCrawlingAction()
        {
            WebCrawlResult = webCrawlerModel.GetWebCrawlingResult();
        }
    }
}
