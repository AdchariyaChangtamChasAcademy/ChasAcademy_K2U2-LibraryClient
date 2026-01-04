using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryClient.Models;

[Index("FkbookId", Name = "IX_Loans_BookID")]
[Index("FkmemberId", Name = "IX_Loans_MemberID")]
public partial class Loan
{
    [Key]
    [Column("LoanID")]
    public int LoanId { get; set; }

    public DateOnly LoanDate { get; set; }

    public DateOnly DueDate { get; set; }

    [Column("FKMemberID")]
    public int FkmemberId { get; set; }

    [Column("FKBookID")]
    public int FkbookId { get; set; }

    [ForeignKey("FkbookId")]
    [InverseProperty("Loans")]
    public virtual Book Fkbook { get; set; } = null!;

    [ForeignKey("FkmemberId")]
    [InverseProperty("Loans")]
    public virtual Member Fkmember { get; set; } = null!;
}
