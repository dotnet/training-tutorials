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
                .Single(b => b.Title == "The Scarlet Plague");

            var year = context.Entry(book)
                .Collection(b => b.Editions)
                .Query()
                .Min(e => e.Year);

            Console.WriteLine("Minimum Year: " + year);
        }
    }
}