using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;

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
            if (!new Url(url).IsInvalid)
            {
                var T = await client.GetStringAsync(new Url(url));
                using (StreamWriter sw = new StreamWriter(".\\myfile"))
                {
                    await sw.WriteAsync(T);
                }
                return T;
            }
            return null;
        }

    }
}
