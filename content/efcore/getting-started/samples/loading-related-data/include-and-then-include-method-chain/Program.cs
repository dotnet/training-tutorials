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
                .Include(b => b.Author)
                .Single(b => b.Title.Contains("Call of the Wild"));

            Console.WriteLine(String.Format("{0}", book.Title));
            Console.WriteLine("Editions:");
            foreach (Edition edition in book.Editions)
            {
                Console.WriteLine("{0} - {1}", edition.Year, edition.Publisher.Name);
            }
        }
    }
}