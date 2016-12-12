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
                .Single(a => a.LastName == "Twain");

            context.Entry(author)
                .Collection(a => a.Books)
                .Query()
                .Where(b => b.Title.Contains("Huck"))
                .Load();

            foreach (var book in author.Books)
            {
                Console.WriteLine(book.Title);
            }
        }
    }
}