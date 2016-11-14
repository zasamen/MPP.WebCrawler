using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MPP.WebCrawler.Model;

namespace MPP.WebCrawler.ViewModel
{
    class WebCrawlerViewModel
    {

        public WebCrawlerModel CurrentModel { get; set; }

        private RelayCommand incrementCommand;

        public RelayCommand IncrementCommand
        {
            get
            {
                return incrementCommand;
            }
            private set
            {
                incrementCommand = value;
            }
        }

        private RelayCommand crawlCommand;

        public RelayCommand CrawlCommand
        {
            get
            {
                return crawlCommand;
            }
            private set
            {
                crawlCommand = value;
            }
        }

        public WebCrawlerViewModel()
        {
            CurrentModel = new WebCrawlerModel();
            incrementCommand = new RelayCommand(CommandsImplementation.IncrementCounterForCheckingUI);
            crawlCommand = new RelayCommand(CommandsImplementation.Crawl,CommandsImplementation.CanCrawling);
        }
        
    }
}
