using System.Windows;
using WebCrawlerProject.Model.WebCrawlerClasses;

namespace webCrawlerProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            IWebCrawler crawler = new WebCrawler();
            string[] urls = new string[1];
            urls[0] = "http://stackoverflow.com/questions/15240326/treeview-hierarchicaldatatemplate-and-recursive-data";
            CrawlerResult result = crawler.PerformCrawlingAsync(urls);
            urlTreeView.Items.Clear();
            urlTreeView.Items.Add(result);
        }
    }
}