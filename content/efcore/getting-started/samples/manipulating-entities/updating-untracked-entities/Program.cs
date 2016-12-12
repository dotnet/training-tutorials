using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var untrackedBook = new Book()
            {
                BookId = 1,
                Title = "Frankenstein: or, The Modern Prometheus",
                Genre = "Science Fiction",
                PublicationYear = 1818
            };

            context.Books.Update(untrackedBook);
            context.SaveChanges();

            var updatedBook = context.Books
                .Single(b => b.BookId == 1);

            Console.WriteLine("{0}: {1}, {2} {3}", updatedBook.BookId, updatedBook.Title, updatedBook.Genre, updatedBook.PublicationYear);
        }
    }
}