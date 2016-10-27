using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrawlerApp.ViewModel
{
    internal class AsyncCommand : IAsyncCommand
    {
        private bool _canExecute = true;
        private readonly Func<Task> _command;

        public event EventHandler CanExecuteChanged;

        public AsyncCommand(Func<Task> command)
        {
            _command = command;
        }

        public Task ExecuteAsync(object parameter)
        {
            return _command();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public bool CanExecute
        {
            get
            {
                return _canExecute;
            }
            set
            {
                if (_canExecute != value)
                {
                    _canExecute = value;
                    EventHandler canExecuteChanged = CanExecuteChanged;
                    canExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
