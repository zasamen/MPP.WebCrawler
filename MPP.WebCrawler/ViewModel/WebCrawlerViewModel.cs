using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MPP.WebCrawler.Model;

namespace MPP.WebCrawler.ViewModel
{
    class WebCrawlerViewModel : INotifyPropertyChanged
    {

        public WebCrawlerModel CurrentModel { get; set; }

        public ObservableCollection<CustomItem> roots { get; set; }

        private RelayCommand addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand;
            }
            private set
            {
                addCommand = value;
            }
        }

        private RelayCommand doCommand;

        public RelayCommand DoCommand
        {
            get
            {
                return doCommand;
            }
            private set
            {
                doCommand = value;
            }
        }

        public WebCrawlerViewModel()
        {
            CurrentModel = new WebCrawlerModel();
            addCommand = new RelayCommand(CurrentModel.DoNothing);
            doCommand = new RelayCommand(CurrentModel.DoCrawling,CurrentModel.CanCrawling);
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
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
