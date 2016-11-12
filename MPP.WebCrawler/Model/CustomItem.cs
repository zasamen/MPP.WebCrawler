using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MPP.WebCrawler.ViewModel;

namespace MPP.WebCrawler.Model
{
    class CustomItem : NotifyPropertyChanged
    {
        private string title;
        private readonly ObservableCollection<CustomItem> items;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public ObservableCollection<CustomItem> Items {
            get
            {
                return items;
            }
        }

        public CustomItem() :base()
        {
            items = new ObservableCollection<CustomItem>();
        }        
    }
}
