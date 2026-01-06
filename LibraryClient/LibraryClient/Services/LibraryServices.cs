using K2_EducationProgramClient.Models.UI;
using LibraryClient.Data;
using LibraryClient.Dtos;
using LibraryClient.Models;
using LibraryClient.UI;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory; 


namespace LibraryClient.Services
{
    public class LibraryServices
    {
        private readonly LibraryContext _context;

        public LibraryServices(LibraryContext context)
        {
            _context = context;
        }

        public List<String> SearchBook(string searchString)
        {
            using var context = new LibraryContext();

            var books = context.Books
                .Where(b => b.Author.ToLower().Contains(searchString.ToLower()) || b.Title.ToLower().Contains(searchString.ToLower()))
                .Select(b => $"- [{b.Title}] by {b.Author} | Quantity: {b.Quantity}")
                .ToList();

            return books;
        }
        public void CreateLoan(int memberId, int bookId, DateTime dueDate)
        {
            // Using a SQL Procedure in LibraryDB
            _context.Database.ExecuteSqlRaw(
                "EXEC dbo.sp_CreateLoan @MemberID, @BookID, @DueDate",
                new SqlParameter("@MemberID", memberId),
                new SqlParameter("@BookID", bookId),
                new SqlParameter("@DueDate", dueDate)
            );
        }

        public void CreateLoanReturn(DateOnly returnDate, bool isLate, int loanId)
        {
            // Find the loan
            var loan = _context.Loans.FirstOrDefault(l => l.LoanId == loanId);
            if (loan == null)
                throw new InvalidOperationException($"Loan with ID {loanId} not found.");

            // Mark the loan as returned
            loan.IsReturned = true;

            // Insert LoanReturn using SQL Interpolated (Bypasses trigger issues)
            _context.Database.ExecuteSqlInterpolated($@"
                INSERT INTO LoanReturns (ReturnDate, IsLate, FKLoanID)
                VALUES ({returnDate}, {isLate}, {loanId})
            ");

            // Save the loan update
            _context.SaveChanges();
        }

        public List<ActiveLoanDto> GetActiveLoans()
        {
            using var context = new LibraryContext();

            var activeLoans = context
                .Set<ActiveLoanDto>()
                .ToList();

            return activeLoans;
        }
        public void CreateMember(string firstName, string lastName, string email, string phone)
        {
            // Using Entity Framework Core
            var member = new Member
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                RegistrationDate = DateOnly.FromDateTime(DateTime.Today)
            };

            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public List<String> GetMembersAsStringList()
        {
            using var context = new LibraryContext();

            var membersList = context.Members
                .Select(m => $" [ID: {m.MemberId}] | NAME: {m.FirstName} {m.LastName}")
                .ToList();

            return membersList;

        }

        public List<String> GetMemberLoans(int memberID)
        {
            using var context = new LibraryContext();

            var booksList = context.Loans
                .Where(l => l.FkmemberId == memberID && l.IsReturned != true)
                .Select(l =>
                    $" [ID: {l.LoanId}] | TITLE: {l.Fkbook.Title} | LoanDate: {l.LoanDate} | DueDate: {l.DueDate}]"
                )
                .ToList();

            return booksList;
        }

        public void CreateBook(string title, string author, string isbn, DateOnly publicationDate, int quantity)
        {
            // Using Entity Framework Core
            var book = new Book
            {
                Title = title,
                Author = author,
                Isbn = isbn,
                Quantity = quantity
            };

            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public List<String> GetBooksAsStringList()
        {
            using var context = new LibraryContext();

            var booksList = context.Books
                .Where(b => b.Quantity > 0)
                .Select(b => $" [ID: {b.BookId}] QTY: {b.Quantity} | TITLE: {b.Title} | AUTHOR: {b.Author} | PUBLICATION DATE: {b.PublicationDate}")
                .ToList();

            return booksList;

        }
    }
}
