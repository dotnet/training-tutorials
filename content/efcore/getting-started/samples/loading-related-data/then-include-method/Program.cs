using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var book = context.Books
                .Include(b => b.Editions)
                    .ThenInclude(e => e.Publisher)
                .Single(b => b.BookId == 1);

            foreach (Edition edition in book.Editions)
            {
                Console.WriteLine("{0} - {1}", edition.Book.Title, edition.Publisher.Name);
            }
        }
    }
}