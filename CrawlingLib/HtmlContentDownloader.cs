using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrawlingLib
{
    internal class HtmlContentDownloader
    {
        private HttpClient client;

        internal HtmlContentDownloader()
        {
            client = new HttpClient();
        }

        internal Task<string> DownloadContentAsync(string url)
        {
            return client.GetStringAsync(url);
        }

    }
}
