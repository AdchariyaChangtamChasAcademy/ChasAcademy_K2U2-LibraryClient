using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryClient.Models;

[Index("Email", Name = "UQ__Members__A9D10534066722A3", IsUnique = true)]
public partial class Member
{
    [Key]
    [Column("MemberID")]
    public int MemberId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string? Phone { get; set; }

    public DateOnly RegistrationDate { get; set; }

    [InverseProperty("Fkmember")]
    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
