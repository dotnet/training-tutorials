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

            Console.WriteLine("-- Included checkout record(s) and associated reader with book --");
            Console.WriteLine("Title: {0}", book.Title);
            
            Console.WriteLine("Checkout Records:", book.Title);		
            foreach (CheckoutRecord checkoutRecord in book.CheckoutRecords)
            {
                Console.WriteLine("Checked out by {0} {1} on {2}", checkoutRecord.Reader.FirstName, checkoutRecord.Reader.LastName, checkoutRecord.CheckoutDate.ToString("MMMM dd, yyyy"));
            }
        }
    }
}