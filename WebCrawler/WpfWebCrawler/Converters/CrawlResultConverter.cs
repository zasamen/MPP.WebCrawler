using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using WebCrawler.Contracts.OutputModels;

namespace WpfWebCrawler.Converters
{
    internal class CrawlResultConverter : IValueConverter
    {
        #region Public  Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ICrawlResult crawlResult = value as ICrawlResult;
            return crawlResult?.RootNodes?.Select(Map);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Private Members

        //private IEnumerable<TreeViewItem> ConvertInternalNodes(ICrawlNode crawlNode)
        //{
        //    //var result = new List<TreeViewItem>();
        //    //foreach (var node in crawlNode.InternalNodes)
        //    //{
        //    //    TreeViewItem treeViewItem = new TreeViewItem()
        //    //    {
        //    //        Header = string.Format("{0} ({1})", crawlNode.LevelDescription, crawlNode.Url)
        //    //    };
        //    //    foreach (TreeViewItem nestedTreeViewItem in ConvertInternalNodes(node))
        //    //    {
        //    //        treeViewItem.Items.Add(nestedTreeViewItem);

        //    //    }
        //    //    result.Add(treeViewItem);
        //    //}
        //    //return result;
        //    if (crawlNode == null)
        //        return null;
        //    return crawlNode.InternalNodes.Select(Map);
        //}

        private TreeViewItem Map(ICrawlNode node)
        {
            return new TreeViewItem()
            {
                Header = string.Format("{0} ({1})", node?.LevelDescription, node?.Url),
                ItemsSource = node?.InternalNodes?.Select(Map)
            };
        }

        #endregion
    }
}
