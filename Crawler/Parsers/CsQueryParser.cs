using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CsQuery;

namespace Crawler.Parsers
{
    public class CsQueryParser : IWebParser
    {
        private readonly ConcurrentBag<Exception> parserExceptions = new ConcurrentBag<Exception>();

        public AggregateException ParserRuntimeExceptions => new AggregateException(parserExceptions);

        public async Task<IEnumerable<string>> ParsePageForUrlAsync(string parentUrl, string currentUrl)
        {

            IEnumerable<string> urls = new List<string>();
            currentUrl = CombineUrlsIfNeed(parentUrl, currentUrl);
            try
            {
                string page = await LoadPage(currentUrl);
                urls = GetAllLinksFromPage(page);
            }
            catch (Exception ex)
            {
                parserExceptions.Add(ex);
            }

            return urls;
        }

        private async Task<string> LoadPage(string currentUrl)
        {
            using (var httpClient = new HttpClient())
            {
                string response = await httpClient.GetStringAsync(currentUrl);
                return response;
            }
        }


        private IEnumerable<string> GetAllLinksFromPage(string page)
        {
            List<string> hrefsList = new List<string>();
            CQ csQuery = CQ.CreateDocument(page);
            
            foreach (IDomObject link in csQuery.Find("a"))
            {
                string href = link.GetAttribute("href");
                if (!string.IsNullOrEmpty(href))
                {
                    hrefsList.Add(href);
                }
            }
            return hrefsList;
        }

        /**
         * simple url validation
         */
        private string CombineUrlsIfNeed(string parentUrl, string currentUrl)
        {
            string validatedUrl = currentUrl;
            if ((currentUrl.Length < 4) || (currentUrl.Substring(0, 4) != "http"))
            {
                validatedUrl = parentUrl.TrimEnd('/') + '/' + currentUrl.TrimStart('/');
            }
            return validatedUrl;
        }
    }
}
