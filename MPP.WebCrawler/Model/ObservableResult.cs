using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CrawlingLib;
using MPP.WebCrawler.ViewModel;

namespace MPP.WebCrawler.Model
{
    class ObservableResult : NotifyPropertyChanged
    {
        private string title;
        private readonly ObservableCollection<ObservableResult> items;

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

        public ObservableCollection<ObservableResult> Items
        {
            get
            {
                return items;
            }
        }

        public ObservableResult() : base()
        {
            items = new ObservableCollection<ObservableResult>();
        }

        public static ObservableResult CreateFromCrawlingResult(CrawlingResult result)
        {
            ObservableResult ci = new ObservableResult();
            ci.Title = result.RootURL;
            foreach (var nestedResults in result.Children)
            {
                ci.Items.Add(CreateFromCrawlingResult(nestedResults));
            }
            return ci;
        }
    }
}
