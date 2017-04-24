using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var author = context.Authors
                .Single(a => a.LastName == "Cather");

            var numBooks = context.Entry(author)
                .Collection(a => a.Books)
                .Query()
                .Count();

            Console.WriteLine("Number of books by {0} {1}: {2}", author.FirstName, author.LastName, numBooks);
        }
    }
}