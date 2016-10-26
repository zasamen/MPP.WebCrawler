using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Crawler;
using CrawlerTest.Commands;
using CrawlerTest.Model;

namespace CrawlerTest.ViewModel
{
    internal class CrawlerViewModel : ViewModelBase
    {
        private readonly AsyncCommand command;
        private CrawlResult crawlResult;
        private readonly CrawlerModel crawlerModel;

        public CrawlerViewModel()
        {
            crawlerModel = new CrawlerModel();
            command = new AsyncCommand(
                async () =>
                {
                    if (command.CanExecute)
                    {
                        DisableCommand();
                        CrawlResult = await crawlerModel.GetCrawlerResult();
                        EnableCommand();
                    }

                });
            
        }

        public CrawlResult CrawlResult
        {
            get
            {
                return crawlResult;
            }
            set
            {
                if (crawlResult != value)
                {
                    crawlResult = value;
                    OnPropertyChanged(nameof(CrawlResult));
                }
            }
        }

        public AsyncCommand Comand => command;

        private void DisableCommand()
        {
            command.CanExecute = false;
        }

        private void EnableCommand()
        {
            command.CanExecute = true;
        }
    }
}
