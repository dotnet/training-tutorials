using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using REPLHelper;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<CheckoutRecord> CheckoutRecords { get; set; }
    public DbSet<Reader> Readers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(DBHelper.GetMigrationDbConnectionString());
    }
}