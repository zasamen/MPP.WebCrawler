using System;
using System.Windows;
using System.Windows.Input;

namespace CrawlerTest.Commands
{
    public class Command : ICommand
    {
        private readonly Action callbackAction;
        private bool canExecute = true;
        public event EventHandler CanExecuteChanged;
        public Command(Action callbackAction)
        {
            if (callbackAction == null)
            {
                throw new ArgumentNullException(nameof(callbackAction));
            }

            this.callbackAction = callbackAction;
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

        bool ICommand.CanExecute(object parameter)
        {
            return canExecute;
        }

        public void Execute(object parameter)
        {
            callbackAction();
        }
    }
}
