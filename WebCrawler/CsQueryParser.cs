using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    internal class CsQueryParser : IHtmlParser
    {
        public Task<IEnumerable<string>> GetUrlsFromPageAsync(string page)
        {
            throw new NotImplementedException();
        }
    }
}
