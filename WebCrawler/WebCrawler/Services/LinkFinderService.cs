using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebCrawler.Contracts.Services;

namespace WebCrawler.Services
{
    internal class LinkFinderService : ILinkFinderService
    {
        #region Private Members

        private const string _pattern = @"<a[^>]*href=(?:""([\S]+)"")[^>]*>";

        #endregion

        #region Internal Methods

        public IEnumerable<string> Find(string htlmFile)
        {
            var result = new List<string>();
            var matches = Regex.Matches(htlmFile, _pattern, RegexOptions.IgnoreCase);
            foreach(Match match in matches)
            {
                result.Add(match.Groups[1].Value);
            }
            return result;
        }

        #endregion
    }
}
