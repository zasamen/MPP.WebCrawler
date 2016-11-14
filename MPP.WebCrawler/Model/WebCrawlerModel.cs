using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrawlingLib;
using MPP.WebCrawler.ViewModel;

namespace MPP.WebCrawler.Model
{
    internal class WebCrawlerModel : NotifyPropertyChanged
    {
        private int counter = 0;

        private bool isExecuting = false;

        private ObservableResult mainResult;

        private WebCrawlingPerformer crawler;

        private ConfigurationReader reader;

        public ObservableResult MainResult
        {
            get
            {
                return mainResult;
            }
            set
            {
                mainResult = value;
                OnPropertyChanged("MainResult");
            }
        }

        public int Counter
        {
            get
            {
                return counter;
            }
            private set
            {
                counter = value;
                OnPropertyChanged("Counter");
            }
        }

        public bool IsExecuting
        {
            get
            {
                return isExecuting;
            }
            private set
            {
                isExecuting = value;
            }
        }


        internal WebCrawlerModel()
        {
            reader = new ConfigurationReader("config.config"); 
            crawler = new WebCrawlingPerformer(reader.NestingDepth);
        }

        internal async void DoCrawling(object o)
        {
            isExecuting = true;
            Counter = 0;
            await CrawlAndWriteResultAsync();
            isExecuting = false;
        }


        internal async Task CrawlAndWriteResultAsync()
        {
            MainResult = await CrawlAndAdaptResultAsync();
        }

        internal async Task<ObservableResult> CrawlAndAdaptResultAsync()
        {
            return ObservableResult.CreateFromCrawlingResult(
                await crawler.PerformCrawlingAsync(reader.UrlsToCrawl));
        }

        internal bool CanCrawling(object o)
        {
            return !isExecuting;
        }
        

        internal void IncrementCounterForCheckingUIAsync(object o)
        {
            Counter++;
        }

    }
}
