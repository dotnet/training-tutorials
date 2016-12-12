using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var book = context.Books.First();

            Console.WriteLine(String.Format("Original Book - {0}: {1}", book.BookId, book.Title));

            book.Title = "Frankenstein: or, The Modern Prometheus";
            context.SaveChanges();
        }

        using (var context = new LibraryContext()) {
            var updatedBook = context.Books
                .Single(b => b.Title.Contains("Frankenstein"));

            Console.WriteLine(String.Format("Updated Book - {0}: {1}", updatedBook.BookId, updatedBook.Title));
        }
    }
}