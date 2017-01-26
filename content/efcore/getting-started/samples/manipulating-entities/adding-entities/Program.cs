using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var author = new Author
            {
                FirstName = "Mary",
                LastName = "Shelley"
            };
            context.Authors.Add(author);
            context.SaveChanges();

            var addedAuthor = context.Authors
                .Single(a => a.LastName.Contains("Shelley"));

            Console.WriteLine("{0}: {1} {2}", addedAuthor.Id, addedAuthor.FirstName, addedAuthor.LastName);
        }
    }
}