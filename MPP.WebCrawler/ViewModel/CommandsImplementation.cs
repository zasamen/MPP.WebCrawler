using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP.WebCrawler.Model;

namespace MPP.WebCrawler.ViewModel
{
    internal static class CommandsImplementation
    {
        internal static async void Crawl(object o)
        {
            ((WebCrawlerModel)o).IsExecuting = true;
            ((WebCrawlerModel)o).Counter = 0;
            await ((WebCrawlerModel)o).CrawlAndWriteResultAsync();
            ((WebCrawlerModel)o).IsExecuting = false;
        }


        internal static bool CanCrawling(object o)
        {
            return o == null ? false : !((WebCrawlerModel)o).IsExecuting;
        }


        internal static void IncrementCounterForCheckingUI(object o)
        {
            ((WebCrawlerModel)o).Counter++;
        }

    }
}
