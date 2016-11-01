using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfWebCrawler.Commands
{
    internal interface IAsyncCommand: ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
