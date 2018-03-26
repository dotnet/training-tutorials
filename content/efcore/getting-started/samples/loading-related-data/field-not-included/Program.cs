using System;
using System.Linq;
using Newtonsoft.Json;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var book = context.Books
                .Single(b => b.Id == 1);
            
            Console.WriteLine(JsonConvert.SerializeObject(book, Formatting.Indented));
        }
    }
}