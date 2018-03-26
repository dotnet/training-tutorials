using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var author = context.Authors
                .Single(a => a.LastName == "Twain");
			
            Console.WriteLine("-- Eagerly Loaded Author --");
            Console.WriteLine("{0} {1}", author.FirstName, author.LastName);
			
            context.Entry(author)
                .Collection(a => a.Books)
                .Load();
			
            Console.WriteLine("-- Explicitly loaded the author's books --");
            foreach (Book book in author.Books)
            {
                Console.WriteLine(book.Title);
            }
        }
    }
}