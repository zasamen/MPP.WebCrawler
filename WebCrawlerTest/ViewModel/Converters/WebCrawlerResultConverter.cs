using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using WebCrawlerLib.WebCrawler;

namespace WebCrawlerTest.ViewModel.Converters
{
    class WebCrawlerResultConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CrawlResult crawlResult = value as CrawlResult;
            if(value == null)
            {
                return new object();
            }

            List<TreeViewItem> itemsList = new List<TreeViewItem>();
            itemsList.Add(CreateTreeView(crawlResult));

            return itemsList;
        }

        private TreeViewItem CreateTreeView(CrawlResult crawlResult)
        {
            TreeViewItem rootItem = CreateTreeViewItem(crawlResult.RootUrl);
            foreach(CrawlResult url in crawlResult)
            {
                TreeViewItem nestedItem = CreateTreeView(url);
                rootItem.Items.Add(nestedItem);
            }
            return rootItem;
        }

        private TreeViewItem CreateTreeViewItem(string header)
        {
            TreeViewItem newItem = new TreeViewItem();
            newItem.Header = header;

            return newItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
