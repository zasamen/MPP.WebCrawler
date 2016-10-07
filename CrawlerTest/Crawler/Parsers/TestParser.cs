using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Crawler.Parsers
{
    public class TestParser : IWebParser
    {
        public AggregateException ParserRuntimeExceptions => null;

        public async Task<IEnumerable<string>> ParsePageForUrlAsync(string parentUrl, string currentUrl)
        {
            return await Task.Run(()=> new List<string>()
            {
                "test1" + new Random().Next(),
                "test2",
                "test3",
                "test4"
            });
        }
    }
}
