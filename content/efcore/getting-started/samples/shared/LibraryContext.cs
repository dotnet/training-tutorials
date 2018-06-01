using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using REPLHelper;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(DBHelper.GetReadonlyDbConnectionString());
    }
}