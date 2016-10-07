using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsQuery;

namespace Crawler.Parsers
{
    public class CsQueryParser : IWebParser
    {
        public Task<IEnumerable<string>> ParsePageForUrlAsync(string parentUrl, string currentUrl)
        {
            /**todo exception aggregation*/
            return Task.Run(() =>
            {
                IEnumerable<string> urls = new List<string>();
                currentUrl = CombineUrlsIfNeed(parentUrl, currentUrl);
                try
                {
                     urls = GetAllLinks(currentUrl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return urls;
            });
        }

        private IEnumerable<string> GetAllLinks(string url)
        {
            List<string> hrefsList = new List<string>();
            CQ csQuery = CQ.CreateFromUrl(url);
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
