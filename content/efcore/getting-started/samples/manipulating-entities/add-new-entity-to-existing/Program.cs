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
                .Single(a => a.LastName == "Crane");

            var book = new Book
            {
                Title = "The Red Badge of Courage",
                Genre = "War Novel",
                PublicationYear = 1871
            };

            author.Books.Add(book);
            context.SaveChanges();

            var addedBook = context.Books
                .Include(b => b.Author)
                .Single(b => b.Title.Contains("Badge"));

            Console.WriteLine("-- Added new book to existing author --");
            Console.WriteLine("Book: {0}", addedBook.Title);
            Console.WriteLine("Author: {0} {1}", author.FirstName, author.LastName);
        }
    }
}