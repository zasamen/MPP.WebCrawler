using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrawlerTest.Commands
{
    public class AsyncCommand : IAsyncCommand
    {
        
        public event EventHandler CanExecuteChanged;

        private readonly Func<Task> command;
        private bool canExecute = true;

        public AsyncCommand(Func<Task> command)
        {
            this.command = command;
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }
        
        bool ICommand.CanExecute(object parameter)
        {
            return canExecute;
        }
        public Task ExecuteAsync(object parameter)
        {
            return command();
        }

        public bool CanExecute
        {
            get { return canExecute; }
            set
            {
                if (canExecute != value)
                {
                    canExecute = value;
                    EventHandler canExecuteChanged = CanExecuteChanged;
                    if (canExecuteChanged != null)
                    {
                        canExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}
