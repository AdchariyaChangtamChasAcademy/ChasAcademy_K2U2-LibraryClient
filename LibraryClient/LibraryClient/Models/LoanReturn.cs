using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryClient.Models;

[Keyless]
[Index("FkloanId", Name = "IX_Returns_LoadID")]
[Index("FkloanId", Name = "UQ__LoanRetu__759C88ABE7E29DF2", IsUnique = true)]
public partial class LoanReturn
{
    [Column("LoanReturnID")]
    public int LoanReturnId { get; set; }

    public DateOnly ReturnDate { get; set; }

    public bool? IsLate { get; set; }

    [Column("FKLoanID")]
    public int FkloanId { get; set; }

    [ForeignKey("FkloanId")]
    public virtual Loan Fkloan { get; set; } = null!;
}
