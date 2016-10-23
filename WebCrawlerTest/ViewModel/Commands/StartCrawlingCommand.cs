using System;
using System.Windows.Input;

namespace WebCrawlerTest.ViewModel
{
    internal class StartCrawlingCommand : ICommand
    {
        private Action TargetExecuteMethod;

        public event EventHandler CanExecuteChanged;

        public StartCrawlingCommand(Action executeMethod)
        {
            TargetExecuteMethod = executeMethod;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(TargetExecuteMethod != null)
            {
                TargetExecuteMethod();
            }
        }
    }
}
