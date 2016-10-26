using System.Windows;
using CrawlerTest.ViewModel;

namespace CrawlerTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CrawlerViewModel crawlerViewModel;
        public MainWindow()
        {
            InitializeComponent();
            crawlerViewModel = new CrawlerViewModel();
        }
    }
}
