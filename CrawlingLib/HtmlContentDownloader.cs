using System;
using System.Collections.Generic;
using System.IO;
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

        internal async Task<string> DownloadContentAsync(string url)
        {
            return await client.GetStringAsync(url);
        }

    }
}
