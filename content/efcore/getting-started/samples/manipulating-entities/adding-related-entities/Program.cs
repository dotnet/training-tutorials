using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var author = new Author
            {
                FirstName = "Mary",
                LastName = "Shelley",
                Books = new List<Book> {
                    new Book
                    {
                        Title = "Frankenstein: or, The Modern Prometheus",
                        Genre = "Science Fiction",
                        PublicationYear = 1818
                    }
                }
            };
            context.Authors.Add(author);
            context.SaveChanges();

            var addedAuthor = context.Authors
                .Include(a => a.Books)
                .Single(a => a.LastName.Contains("Shelley"));

            foreach (Book book in addedAuthor.Books) {
                Console.WriteLine("{0} - {1}, {2}", book.Title, book.Genre, book.PublicationYear);
            }
        }
    }
}