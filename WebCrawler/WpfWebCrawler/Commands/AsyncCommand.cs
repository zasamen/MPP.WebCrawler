using System;
using System.Threading.Tasks;

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
            await ExecuteAsync(parameter);
        }

        public Task ExecuteAsync(object parameter)
        {
            return _command ();
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void SetFalse()
        {
            _canExecute = false;
        }

        public void SetCanExecuteStatus(bool status)
        {
            _canExecute = status;
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
