using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Data;
using WebCrawler;

namespace WebCrawlerApp.Converters 
{
    internal class CrawlResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<TreeViewItem> result = new List<TreeViewItem>();

            CrawlResult crawlResult = value as CrawlResult;
            if (crawlResult != null)
            {
                result = CrawlResultToTreeViewItems(crawlResult);
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();   
        }

        // Static internals

        private static IEnumerable<TreeViewItem> CrawlResultToTreeViewItems(CrawlResult crawlResult)
        {
            var result = new List<TreeViewItem>();
            foreach (var url in crawlResult.Urls)
            {
                TreeViewItem treeViewItem = new TreeViewItem() {Header = url.Key};
                foreach (TreeViewItem nestedTreeViewItem in CrawlResultToTreeViewItems(url.Value))
                {
                    treeViewItem.Items.Add(nestedTreeViewItem);
                    
                }
                result.Add(treeViewItem);
            }
            return result;
        }
    }
}
