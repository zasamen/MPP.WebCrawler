using System.ComponentModel;

namespace WebCrawlerTest.ViewModel
{
    internal class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (HasListeners())
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool HasListeners()
        {
            return (PropertyChanged != null);
        }
    }
}
