using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var books = context.Books
                .Where(s => s.Genre.Contains("Science Fiction"))
                .ToList();

            foreach (Book book in books)
            {
                Console.WriteLine(String.Format("{0} - {1}", book.Title, book.Genre));
            }
        }
    }
}