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
                Id = 1, 
                Title = "To the Lighthouse",
                Genre = "Literary",
                PublicationYear = 1927 
            };
			
            context.Books.Update(untrackedBook); 
            context.SaveChanges();

            var updatedBook = context.Books
                .Single(b => b.Id == 1);

            Console.WriteLine("-- Updated Book --");
            Console.WriteLine("Id: {0}", updatedBook.Id);
            Console.WriteLine("Title: {0}", updatedBook.Title);
            Console.WriteLine("Genre: {0}", updatedBook.Genre);
            Console.WriteLine("Publication Year: {0}", updatedBook.PublicationYear);
        }
    }
}