using System.Collections.Generic;

namespace WebCrawler.Contracts.Services
{
    internal interface ILinkFinderService
    {
        IEnumerable<string> Find(string htlmFile);
    }
}
