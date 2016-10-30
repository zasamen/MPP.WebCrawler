using System.Windows;

namespace WebCrawlerTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int i = 0;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = (++i).ToString();
        }
    }
}
