using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var book = context.Books
				.Single(b => b.Title == "Mrs Dalloway"); 

            Console.WriteLine("-- Original Book --");
            Console.WriteLine("Id: {0}", book.Id);
            Console.WriteLine("Title: {0}", book.Title);
            Console.WriteLine("Genre: {0}", book.Genre);
            Console.WriteLine("Publication Year: {0}", book.PublicationYear);

            book.Title = "To the Lighthouse";
            book.PublicationYear = 1927;
            context.SaveChanges(); 
			
            Console.WriteLine("\n-- Updated Book --");
            Console.WriteLine("Id: {0}", book.Id);
            Console.WriteLine("Title: {0}", book.Title);
            Console.WriteLine("Genre: {0}", book.Genre);
            Console.WriteLine("Publication Year: {0}", book.PublicationYear);
        }
    }
}