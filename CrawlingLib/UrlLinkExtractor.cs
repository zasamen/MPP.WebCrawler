using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;

namespace CrawlingLib
{
    internal class UrlLinkExtractor
    {
        HtmlParser parser;

        internal UrlLinkExtractor()
        {
            parser = new HtmlParser();
        }


        internal async Task<IEnumerable<string>> ExtractLinks(string url)
        {
            return (IsValidUrl(url))
                ? ConvertToStringList(await GetLinksAsync(url))
                : new List<string>();
        }

        private IEnumerable<string> ConvertToStringList(IEnumerable<IElement> elements)
        {
            return elements.ToList().ConvertAll<string>(x => x.GetAttribute("href"));
        }

        private async Task<IEnumerable<IElement>> GetLinksAsync(string url)
        {
            return (await parser.ParseAsync(await GetContentAsync(url))).Links;
        }

        private async Task<string> GetContentAsync(string url)
        {
            return await new WebClient().DownloadStringTaskAsync(url);
        }

        private bool IsValidUrl(string url)
        {
            return (!new Url(url).IsInvalid && url.StartsWith("http") && !url.StartsWith("/"));
        }

        
    }
}
