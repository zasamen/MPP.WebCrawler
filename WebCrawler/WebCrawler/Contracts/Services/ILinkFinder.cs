using System.Collections.Generic;

namespace WebCrawler.Contracts.Services
{
    internal interface ILinkFinder
    {
        IEnumerable<string> Find(string htlmFile);
    }
}
