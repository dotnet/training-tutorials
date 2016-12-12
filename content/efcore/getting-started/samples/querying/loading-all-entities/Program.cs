using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var books = context.Books.ToList();
            foreach (Book book in books)
            {
                Console.WriteLine(book.Title);
            }
        }
    }
}