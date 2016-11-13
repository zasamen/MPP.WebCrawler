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
        public int Counter { get
            {
                return counter;
            }
            private set
            {
                counter = value;
                OnPropertyChanged("Counter");
            }
            }

        private bool isExecuting = false;
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

        internal async void DoCrawling(object o)
        {
            isExecuting = true;
            Counter = 0;
            CrawlingResult cr = await DoImportantThingAsync();
            isExecuting = false;
        }
        internal async Task<CrawlingResult> DoImportantThingAsync()
        {

            return await new WebCrawlingPerformer(1).PerformCrawlingAsync(new string[]{ "http://motherfuckingwebsite.com/"});
        }


        internal bool CanCrawling(object o)
        {
            return !isExecuting;
        }
        

        internal void DoNothing(object o)
        {
            Counter++;
        }

    }
}
