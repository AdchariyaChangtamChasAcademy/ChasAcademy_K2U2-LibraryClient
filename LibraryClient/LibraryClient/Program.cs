using LibraryClient.Models;
using LibraryClient.Services;
using LibraryClient.UI;

namespace LibraryClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new Data.LibraryContext();
            var loanService = new LoanServices(context);
            var menu = new ConsoleMenu(loanService);

            menu.Show();
        }
    }
}
