using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;

namespace WebCrawler
{
    internal class CsQueryParser : IHtmlParser
    {
        public IEnumerable<string> GetLinksFromPage(string page)
        {
            CQ csQuery = CQ.CreateDocument(page);
            return csQuery.Select("a").Select(x => x.GetAttribute("href")).Where(x => !string.IsNullOrEmpty(x));
        }
    }
}
