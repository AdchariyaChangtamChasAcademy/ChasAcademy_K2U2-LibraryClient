using K2_EducationProgramClient.Models.UI;
using LibraryClient.Data;
using LibraryClient.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClient.UI
{
    public class ConsoleMenu
    {
        private readonly LibraryServices _libraryService;

        public ConsoleMenu(LibraryServices loanService)
        {
            _libraryService = loanService;
        }

        public void Show()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ConsolePrintHelper.AdminTitle("LIBRARY");
                ConsolePrintHelper.AdminMenu("EDIT MENU", new List<string>
                {
                    "Search book",         // Sök efter böcker
                    "Borrow book",         // Registrera lån
                    "Return book",         // Registrera återlämningar
                    "Show active loans",   // Visa alla aktiva lån
                    "Register new member", // Registrera nya medlemmar
                    "Register new book"    // Registrera nya böcker
                });
                var choice = ConsolePrintHelper.AdminAskChoice("Choose:");

                switch (choice)
                {
                    case "1":
                        SearchBook();
                        break;
                    case "2":
                        CreateLoan();
                        break;
                    case "3":
                        CreateLoanReturn();
                        break;
                    case "4":
                        ShowActiveLoans();
                        break;
                    case "5":
                        CreateMember();
                        break;
                    case "6":
                        CreateBook();
                        break;
                    case "0":
                        return;
                    default: ConsolePrintHelper.FaultyMenuChoice(); break;
                }
            }
        }

        private void SearchBook()
        {
            string? inSearchString = ConsolePrintHelper.AdminAskChoice("Search by Title or Author:");

            if (inSearchString != null)
                ConsolePrintHelper.AdminList("BOOKS", _libraryService.SearchBook(inSearchString));
            else
                Console.WriteLine(" Input cannot be empty!");

            ConsolePrintHelper.Pause();
        }

        private void CreateLoan()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("LIBRARY");

            ConsolePrintHelper.AdminList("MEMBERS", _libraryService.GetMembersAsStringList());
            int memberId = -1;
            while (!int.TryParse(ConsolePrintHelper.AdminAskChoice("Member ID: "), out memberId))
            {
                Console.WriteLine(" Invalid input. Please enter a valid integer.");
                Console.Write(" Enter valid ID number: ");
            }

            ConsolePrintHelper.AdminList("BOOKS AVAILABLE", _libraryService.GetBooksAsStringList());
            int bookId = -1;
            while (!int.TryParse(ConsolePrintHelper.AdminAskChoice("Book ID: "), out bookId))
            {
                Console.WriteLine(" Invalid input. Please enter a valid integer.");
                Console.Write(" Enter valid ID number: ");
            }
            string? dueDate = ConsolePrintHelper.AdminAskChoice("Due date (yyyy-mm-dd): ");
            if (ConsolePrintHelper.NullInputWarning(dueDate)) return;
            
            if (!DateTime.TryParse(dueDate, out DateTime validDueTime))
            {
                Console.WriteLine(" Invalid time format. Please use YYYY-MM-DD.");
                ConsolePrintHelper.Pause();
                return;
            }

            _libraryService.CreateLoan(memberId, bookId, validDueTime);

            ConsolePrintHelper.AdminSubTitle(" Loan process done");
            ConsolePrintHelper.Pause();
        }

        private void CreateLoanReturn()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("LIBRARY");

            ConsolePrintHelper.AdminList("MEMBERS", _libraryService.GetMembersAsStringList());
            int memberId = -1;
            while (!int.TryParse(ConsolePrintHelper.AdminAskChoice("Member ID: "), out memberId))
            {
                Console.WriteLine(" Invalid input. Please enter a valid integer.");
                Console.Write(" Enter valid ID number: ");
            }

            ConsolePrintHelper.AdminList("LOANS", _libraryService.GetMemberLoans(memberId));

            int loanId = -1;
            while (!int.TryParse(ConsolePrintHelper.AdminAskChoice("Loan ID: "), out loanId))
            {
                Console.WriteLine(" Invalid input. Please enter a valid integer.");
                Console.Write(" Enter valid ID number: ");
            }
            var loanList = _libraryService.GetActiveLoans();
            var loan = loanList.FirstOrDefault(l => l.LoanID == loanId);

            if (loan == null)
            {
                Console.WriteLine("Loan not found.");
                ConsolePrintHelper.Pause();
                return;
            }
            var isLate = DateTime.Now > loan.DueDate;

            _libraryService.CreateLoanReturn(DateOnly.FromDateTime(DateTime.Today), isLate, loan.LoanID);

            Console.WriteLine(" Return process done");

            ConsolePrintHelper.Pause();
        }

        private void ShowActiveLoans()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("LIBRARY");

            var loans = _libraryService.GetActiveLoans();

            List<string> strList = loans
                .GroupBy(l => new { l.FirstName, l.LastName })
                .OrderBy(g => g.Key.LastName)
                .ThenBy(g => g.Key.FirstName)
                .Select(g =>
                {
                    var sb = new StringBuilder();

                    sb.AppendLine($"[{g.Key.FirstName} {g.Key.LastName}]");

                    foreach (var loan in g.OrderBy(l => l.DueDate))
                    {
                        sb.AppendLine(
                            $"  - {loan.BookTitle} | Due: {loan.DueDate:d}"
                        );
                    }
                    return sb.ToString().TrimEnd();
                })
                .ToList();

            ConsolePrintHelper.AdminList("Loaned Books", strList);
            ConsolePrintHelper.Pause();
        }

        private void CreateMember()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("LIBRARY");
            ConsolePrintHelper.AdminSubTitle("New Member");

            string? firstName = ConsolePrintHelper.AdminAskChoice("Enter First Name: ");
            if (ConsolePrintHelper.NullInputWarning(firstName)) return;
            string? lastName = ConsolePrintHelper.AdminAskChoice("Enter Last Name: ");
            if (ConsolePrintHelper.NullInputWarning(lastName)) return;
            string? email = ConsolePrintHelper.AdminAskChoice("Enter Email: ");
            if (ConsolePrintHelper.NullInputWarning(email)) return;
            string? phone = ConsolePrintHelper.AdminAskChoice("Enter Phone Number: ");
            if (ConsolePrintHelper.NullInputWarning(phone)) return;

            _libraryService.CreateMember(firstName, lastName, email, phone);

            ConsolePrintHelper.AdminSubTitle(" Member creation done");
            ConsolePrintHelper.Pause();
        }

        private void CreateBook()
        {
            Console.Clear();
            ConsolePrintHelper.AdminTitle("LIBRARY");
            ConsolePrintHelper.AdminSubTitle("New Member");

            string? title = ConsolePrintHelper.AdminAskChoice("Enter Title: ");
            if (ConsolePrintHelper.NullInputWarning(title)) return;
            string? author = ConsolePrintHelper.AdminAskChoice("Enter Author: ");
            if (ConsolePrintHelper.NullInputWarning(author)) return;
            string? isbn = ConsolePrintHelper.AdminAskChoice("Enter ISBN: ");
            if (ConsolePrintHelper.NullInputWarning(isbn)) return;
            string? publicationDate = ConsolePrintHelper.AdminAskChoice("Due publication date (yyyy-mm-dd): ");
            if (ConsolePrintHelper.NullInputWarning(publicationDate)) return;
            if (!DateOnly.TryParse(publicationDate, out DateOnly validPublicationDate))
            {
                Console.WriteLine(" Invalid format. Please use YYYY-MM-DD.");
                ConsolePrintHelper.Pause();
                return;
            }
            int quantity = -1;
            while (!int.TryParse(ConsolePrintHelper.AdminAskChoice("Book quantity: "), out quantity))
            {
                Console.WriteLine(" Invalid input. Please enter a valid integer.");
                Console.Write(" Enter valid quantity number: ");
            }

            _libraryService.CreateBook(title, author, isbn, validPublicationDate, quantity);

            ConsolePrintHelper.AdminSubTitle(" Book creation done");
            ConsolePrintHelper.Pause();
        }
    }
}
