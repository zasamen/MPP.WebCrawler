using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;

namespace CrawlingLib
{
    internal class UrlLinkExtractor
    {
        HtmlParser parser;
        HtmlContentDownloader downloader;

        internal UrlLinkExtractor()
        {
            parser = new HtmlParser();
            downloader = new HtmlContentDownloader();
        }


        internal async Task<IEnumerable<IElement>> ExtractLinks(string url)
        {
            return (await parser.ParseAsync(
                await downloader.DownloadContentAsync(url))).Links;
        }
    }
}
