using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using REPLHelper;

public class LibraryContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Reader> Readers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(DBHelper.GetMigrationDbConnectionString());
    }
}