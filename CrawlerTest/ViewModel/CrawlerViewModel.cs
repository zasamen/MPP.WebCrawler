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
        private readonly Command command;
        private CrawlResult crawlResult;
        private readonly CrawlerModel crawlerModel;

        public CrawlerViewModel()
        {
            crawlerModel = new CrawlerModel();
            command = new Command(
                () =>
                {
                    if (command.CanExecute)
                    {
                        DisableCommand();
                        var dispatcher = Dispatcher.CurrentDispatcher;
                        Task.Run(() =>
                        {
                            CrawlResult = crawlerModel.GetCrawlerResult().Result;
                            dispatcher.Invoke(EnableCommand);
                        });

                    }

                });
            
        }
        public Command Comand => command;
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
                    OnPropertyChanged("CrawlResult");
                }
            }
        }
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
