using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WebCrawlerProject.Model.WebCrawlerClasses
{
    public class UrlTree : IEnumerable<UrlTree>
    {
        public string RootUrl { get; }
        public ObservableCollection<UrlTree> ChildUrls { get; }

        public UrlTree(string rootUrl)
        {
            if (rootUrl == null)
                throw new ArgumentOutOfRangeException(nameof(rootUrl));

            RootUrl = rootUrl;
            ChildUrls = new ObservableCollection<UrlTree>();
        }

        public void AddChildUrl(UrlTree url)
        {
            if (url == null)
                throw new ArgumentOutOfRangeException(nameof(url));

            ChildUrls.Add(url);
        }

        public IEnumerator<UrlTree> GetEnumerator()
        {
            return ChildUrls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
