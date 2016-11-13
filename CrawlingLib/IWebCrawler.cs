using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlingLib
{
    public interface IWebCrawler
    {

        Task<CrawlingResult> PerformCrawlingAsync(string[] urls);
    }
}
