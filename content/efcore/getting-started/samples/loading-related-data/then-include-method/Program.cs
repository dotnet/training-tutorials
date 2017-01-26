using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var book = context.Books
                .Include(b => b.CheckoutRecords)
                    .ThenInclude(c => c.Reader)
                .Single(b => b.Id == 1); 

            foreach (CheckoutRecord checkoutRecord in book.CheckoutRecords)
            {
                Console.WriteLine("{0} - {1} {2}", checkoutRecord.Book.Title, checkoutRecord.Reader.FirstName, checkoutRecord.Reader.LastName);
            }
        }
    }
}