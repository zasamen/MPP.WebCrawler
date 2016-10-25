using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrawlerTest.ViewModel
{
    internal class StartCrawlingCommand : ICommand
    {
        private Func<Task> targetExecuteMethod;
        public event EventHandler CanExecuteChanged;
        private bool enabled;


        public StartCrawlingCommand(Func<Task> executeMethod)
        {
            targetExecuteMethod = executeMethod;
            enabled = true;
        }

        public bool CanExecute(object parameter)
        {
            return enabled;
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync().ConfigureAwait(false);
        }

        private Task ExecuteAsync()
        {
            return targetExecuteMethod();
        }

        public void Disable()
        {
            enabled = false;
        }

        public void Enable()
        {
            enabled = true;
        }

        private void NotifyObservers()
        {
            if(CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
        
    }
}
