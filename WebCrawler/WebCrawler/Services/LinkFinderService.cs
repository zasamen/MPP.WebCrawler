using CsQuery;
using System.Linq;
using System.Collections.Generic;
using WebCrawler.Contracts.Services;

namespace WebCrawler.Services
{
    internal class LinkFinderService : ILinkFinderService
    {
        #region Internal Methods

        public IEnumerable<string> Find(string htlmFile)
        {
            CQ csQuery = CQ.CreateDocument(htlmFile);
            return csQuery.Select("a").Select(x => x.GetAttribute("href")).Where(x => !string.IsNullOrEmpty(x));
        }

        #endregion
    }
}
