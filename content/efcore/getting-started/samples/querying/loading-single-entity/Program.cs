using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var book = context.Books
                .Single(b => b.Id == 1);
            Console.WriteLine(book.Title);
        }
    }
}