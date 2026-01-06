using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace LibraryClient.Dtos
{
    [Keyless]
    public class ActiveLoanDto
    {
        public int LoanID { get; set; }
        public string BookTitle { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DueDate { get; set; }
    }
}
