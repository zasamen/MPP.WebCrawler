using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfWebCrawler.Commands
{
    internal class AsyncCommand : IAsyncCommand
    {

        #region Private Members

        private readonly Func<Task> _command;
        private bool _canExecute = true;

        #endregion

        #region Public Members

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Ctor

        public AsyncCommand(Func<Task> command)
        {
            _command = command;
        }

        #endregion


        #region Public Methods

        public async void Execute(object parameter)
        {
            _canExecute = false;
            await ExecuteAsync(parameter);
            _canExecute = true;
        }

        public Task ExecuteAsync(object parameter)
        {
            return _command ();
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        #endregion
    }
}
