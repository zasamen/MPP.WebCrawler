using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MPP.WebCrawler.Model;

namespace MPP.WebCrawler.ViewModel
{
    class WebCrawlerViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<CustomItem> roots { get; set; }

        public CustomItem root { get; set; }

        private RelayCommand addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return (new RelayCommand(o => roots.Add(new CustomItem { Title = "myCI" })));
            }
        }

        public WebCrawlerViewModel()
        {
            roots = new ObservableCollection<CustomItem> {
                new CustomItem {Title="root1" },
                new CustomItem {Title="root2" },
                new CustomItem {Title="root3" }
            };
            int i = 0;
            foreach (var root in roots)
            {
                CustomItem item = new CustomItem { Title = "subroot" + i++ };
                root.Items.Add(item);
                for (int j = 0; j <= i; j++)
                {
                    item.Items.Add(new CustomItem { Title = "leaf" + j });
                }
            }
            root = new CustomItem { Title = "mytitle" };
            foreach(var subroot in roots)
            {
                root.Items.Add(subroot);
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
