using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryClient.Models;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<LoanReturn> LoanReturns { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C2271B770B86");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__Loans__4F5AD437682A7232");

            entity.Property(e => e.LoanDate).HasDefaultValueSql("(CONVERT([date],getdate()))");

            entity.HasOne(d => d.Fkbook).WithMany(p => p.Loans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loans__FKBookID__5441852A");

            entity.HasOne(d => d.Fkmember).WithMany(p => p.Loans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loans__FKMemberI__534D60F1");
        });

        modelBuilder.Entity<LoanReturn>(entity =>
        {
            entity.Property(e => e.LoanReturnId).ValueGeneratedOnAdd();
            entity.Property(e => e.ReturnDate).HasDefaultValueSql("(CONVERT([date],getdate()))");

            entity.HasOne(d => d.Fkloan).WithOne()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanRetur__FKLoa__59063A47");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B38E00AC9D8");

            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(CONVERT([date],getdate()))");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
