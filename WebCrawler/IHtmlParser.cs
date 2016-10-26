using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
   public interface IHtmlParser
   {
       Task<IEnumerable<string>> GetUrlsFromPageAsync(string page);
   }
}
