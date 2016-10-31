using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebCrawler.Contracts.Services;
using WebCrawler.Services;

namespace WpfWebCrawler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            //try
            {
                using (IWebCrawlerService crawler = new WebCrawlerService(3))
                {
                    var time = DateTime.Now;
                    //var result = await crawler.PerformCrawlingAsync(new[] { "http://tut.by", "http://tut.by", "http://tut.by", "http://tut.by" });
                    var result = await crawler.PerformCrawlingAsync(new[] { "http://google.com", "http://google.com", "http://google.com", "http://google.com" });
                    MessageBox.Show((DateTime.Now - time).ToString());
                }
            }
            //catch(Exception exception)
            //{
            //    Console.WriteLine(exception.Message);
            //}
        }
    }
}
