using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WebCrawlerLib.WebCrawler
{
    public class CrawlResult : IEnumerable<CrawlResult>
    {
        public string RootUrl { get; }
        public ObservableCollection<CrawlResult> NestedUrls { get; }

        public CrawlResult()
        {
            NestedUrls = new ObservableCollection<CrawlResult>();
            RootUrl = string.Empty;
        }

        public CrawlResult(string url) : this()
        {
            RootUrl = url;
        }

        public void AddNestedUrl(CrawlResult url)
        {
            if (url != null)
            {
                NestedUrls.Add(url);
            }
        }

        public IEnumerator<CrawlResult> GetEnumerator()
        {
            return NestedUrls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
