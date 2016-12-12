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
                .Include(b => b.Author)
                .Single(b => b.BookId == 1);

            foreach (Edition edition in book.Editions)
            {
                Console.WriteLine("{0}: {1} {2}, {3}", edition.Book.Title, book.Author.FirstName, book.Author.LastName, edition.Year);
            }
        }
    }
}