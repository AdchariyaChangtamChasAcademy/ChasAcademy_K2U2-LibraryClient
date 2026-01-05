using LibraryClient.Data;
using LibraryClient.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibraryClient.Services
{
    public class LoanServices
    {
        private readonly LibraryContext _context;

        public LoanServices(LibraryContext context)
        {
            _context = context;
        }

        public List<ActiveLoanDto> GetActiveLoans()
        {
            using var context = new LibraryContext();

            var activeLoans = context
                .Set<ActiveLoanDto>()
                .ToList();

            return activeLoans;
        }

        public void CreateLoan(int memberId, int bookId, DateTime dueDate)
        {
            _context.Database.ExecuteSqlRaw(
                "EXEC dbo.sp_CreateLoan @MemberID, @BookID, @DueDate",
                new SqlParameter("@MemberID", memberId),
                new SqlParameter("@BookID", bookId),
                new SqlParameter("@DueDate", dueDate)
            );
        }
    }
}
