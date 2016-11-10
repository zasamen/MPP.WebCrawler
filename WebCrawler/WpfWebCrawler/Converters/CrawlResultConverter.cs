using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Threading;
using WebCrawler.Contracts.OutputModels;

namespace WpfWebCrawler.Converters
{
    internal class CrawlResultConverter : MarkupExtension, IValueConverter
    {
        #region Private Members

        private readonly Dispatcher _currentDispatcher = Dispatcher.CurrentDispatcher;

        #endregion  

        #region Public  Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var crawlResult = value as ICrawlResult;
            return _currentDispatcher.Invoke(() => crawlResult?.RootNodes?.Select(Map),
                        DispatcherPriority.Background);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        #endregion


        #region Private Members

        private TreeViewItem Map(ICrawlNode node)
        {
            return new TreeViewItem()
            {
                Header = $"{node?.LevelDescription} ({node?.Url})",
                ItemsSource = _currentDispatcher.Invoke(() => node?.InternalNodes?.Select(Map),
                                    DispatcherPriority.Background)
            };
        }

        #endregion
    }
}
