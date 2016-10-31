using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfWebCrawler.AsyncCommands
{
    internal interface IAsyncCommand: ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
