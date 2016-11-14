using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MPP.WebCrawler.Model;

namespace MPP.WebCrawler.ViewModel
{
    class WebCrawlerViewModel : NotifyPropertyChanged
    {

        public WebCrawlerModel CurrentModel { get; set; }

        public ObservableCollection<ObservableResult> roots { get; set; }

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
            addCommand = new RelayCommand(CurrentModel.IncrementCounterForCheckingUIAsync);
            doCommand = new RelayCommand(CurrentModel.DoCrawling,CurrentModel.CanCrawling);
        }
        
    }
}
