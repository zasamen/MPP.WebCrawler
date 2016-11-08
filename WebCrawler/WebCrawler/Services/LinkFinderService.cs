using System.Linq;
using System.Collections.Generic;
using WebCrawler.Contracts.Services;
using HtmlAgilityPack;
using System;
using NLog;

namespace WebCrawler.Services
{
    internal class LinkFinderService : ILinkFinderService
    {
        #region Private Methods

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Internal Methods

        public IEnumerable<string> Find(string htmlFile)
        {
            IEnumerable<string> result = new List<string>();
            if (string.IsNullOrEmpty(htmlFile))
                return result;

            try
            {
                var htmlSnippet = new HtmlDocument();
                htmlSnippet.LoadHtml(htmlFile);
                result =  htmlSnippet.DocumentNode.SelectNodes("//a[@href]")
                                                .Select(x => x?.Attributes["href"].Value)
                                                .Where(x => !string.IsNullOrEmpty(x));
            }
            catch(Exception e)
            {
                _logger.Warn(e.Message);
            }
            return result;
        }

        #endregion
    }
}
