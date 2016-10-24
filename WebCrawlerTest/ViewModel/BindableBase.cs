using System.ComponentModel;

namespace WebCrawlerTest.ViewModel
{
    internal class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (HasObservers())
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool HasObservers()
        {
            return (PropertyChanged != null);
        }
    }
}
