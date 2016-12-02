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
        private static readonly string LinkAttribute = "href";

        private List<string> ConvertToStringList(IEnumerable<IElement> elements)
        {
            var V = elements.ToList().ConvertAll<string>(x => x.GetAttribute(LinkAttribute));
            return V;
        }

        private async Task<IEnumerable<IElement>> GetLinksAsync(Url url)
        {
            return (await new HtmlParser().ParseAsync(await GetContentAsync(url))).Links;
        }

        private async Task<string> GetContentAsync(Url url)
        {
                return await new WebClient().DownloadStringTaskAsync(url);
        }

        private Url GetUrlIfValid(Url url)
        {
            return (url.IsInvalid
                || !url.Scheme.StartsWith("http"))
                ? null
                : url;
        }
        
        internal async Task<List<string>> ExtractLinks(string url)
        {
            var validUrl = GetUrlIfValid(new Url(url));
            return (validUrl != null)
                ? ConvertToStringList(await GetLinksAsync(validUrl))
                : new List<string>();
        }
    }
}
