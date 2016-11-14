using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
            set
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
            set
            {
                isExecuting = value;
            }
        }


        internal WebCrawlerModel()
        {
            try
            {
                reader = new ConfigurationReader("config.config");
                crawler = new WebCrawlingPerformer(reader.NestingDepth);
            }
            catch (ConfigurationReadingException cre)
            {
                MessageBox.Show(cre.Message + "\r\nApplication shutdowns");
                Application.Current.Shutdown();        
            }
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


    }
}
