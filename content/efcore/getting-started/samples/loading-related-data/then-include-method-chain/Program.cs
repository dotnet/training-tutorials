using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var author = context.Authors
                .Include(a => a.Books)
                    .ThenInclude(b => b.Editions)
                        .ThenInclude(e => e.Publisher)
                .Single(a => a.LastName == "Douglass");

            foreach (Book book in author.Books)
            {
                foreach (Edition edition in book.Editions)
                {
                    Console.WriteLine("{0}: {1} - {2}", book.Title, edition.Year, edition.Publisher.Name);
                }
            }
        }
    }
}