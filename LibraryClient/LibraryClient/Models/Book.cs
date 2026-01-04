using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryClient.Models;

[Index("Isbn", Name = "UQ__Books__447D36EAD0C011A6", IsUnique = true)]
public partial class Book
{
    [Key]
    [Column("BookID")]
    public int BookId { get; set; }

    [StringLength(100)]
    public string Title { get; set; } = null!;

    [StringLength(100)]
    public string Author { get; set; } = null!;

    [Column("ISBN")]
    [StringLength(13)]
    [Unicode(false)]
    public string Isbn { get; set; } = null!;

    public DateOnly PublicationDate { get; set; }

    public int Quantity { get; set; }

    [InverseProperty("Fkbook")]
    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
