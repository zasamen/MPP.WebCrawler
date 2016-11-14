using System;
using System.Collections.Generic;
using System.IO;
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


        internal async Task<IEnumerable<string>> ExtractLinks(string url)
        {
            return ConvertIElementsToStrings(
                (await parser.ParseAsync(
                    await downloader.DownloadContentAsync(url))).Links);
        }

        private LinkedList<string> ConvertIElementsToStrings(IEnumerable<IElement> elements)
        {
            LinkedList<string> stringList = new LinkedList<string>();
            foreach (var element in elements)
            {
                stringList.AddLast(element.GetAttribute("href"));
            }
            return stringList;
        }
    }
}
