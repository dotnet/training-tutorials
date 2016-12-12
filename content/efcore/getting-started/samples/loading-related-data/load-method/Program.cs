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

            context.Entry(author)
                .Collection(a => a.Books)
                .Load();
				
			foreach (Book book in author.Books)
			{
				Console.WriteLine(book.Title);
			}
        }
    }
}