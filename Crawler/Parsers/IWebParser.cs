using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crawler.Parsers
{
    public interface IWebParser
    {
        Task<IEnumerable<string>> ParsePageForUrlAsync(string parentUrl, string currentUrl);

        AggregateException ParserRuntimeExceptions { get; }
    }
}
