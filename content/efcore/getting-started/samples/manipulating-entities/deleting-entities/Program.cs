using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        using (LibraryContext context = new LibraryContext()) {
            var authorToDelete = context.Authors
                .Single(a => a.LastName == "Crane");
            context.Authors.Remove(authorToDelete);
            context.SaveChanges();

            try
            {
                var author = context.Authors
                    .Single(a => a.LastName == "Crane");
            }
            catch
            {
                Console.WriteLine("Author not found!");
            }
        }
    }
}