using LibraryClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClient.UI
{
    public class ConsoleMenu
    {
        private readonly LoanServices _loanService;

        public ConsoleMenu(LoanServices loanService)
        {
            _loanService = loanService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Visa aktiva lån");
                Console.WriteLine("2. Låna bok");
                Console.WriteLine("0. Avsluta");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowActiveLoans();
                        break;
                    case "2":
                        CreateLoan();
                        break;
                    case "0":
                        return;
                }
            }
        }

        private void ShowActiveLoans()
        {
            var loans = _loanService.GetActiveLoans();

            foreach (var loan in loans)
            {
                Console.WriteLine(
                    $"- {loan.BookTitle} | {loan.FirstName} {loan.LastName} | Due: {loan.DueDate:d}");
            }

            Console.ReadKey();
        }

        private void CreateLoan()
        {
            Console.Write("Member ID: ");
            int memberId = int.Parse(Console.ReadLine()!);

            Console.Write("Book ID: ");
            int bookId = int.Parse(Console.ReadLine()!);

            Console.Write("Due date (yyyy-mm-dd): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine()!);

            _loanService.CreateLoan(memberId, bookId, dueDate);

            Console.WriteLine("Loan created.");
            Console.ReadKey();
        }
    }
}
