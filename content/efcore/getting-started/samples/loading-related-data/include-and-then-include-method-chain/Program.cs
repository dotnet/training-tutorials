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
                .Include(b => b.Author)
                .Single(b => b.Title.Contains("Orient Express"));

            Console.WriteLine(String.Format("{0}", book.Title));
            Console.WriteLine("Checkout Records:");
            foreach (CheckoutRecord checkoutRecord in book.CheckoutRecords)
            {
                Console.WriteLine("{0} {1} - Due {2}", checkoutRecord.Reader.FirstName, checkoutRecord.Reader.LastName, checkoutRecord.DueDate.ToString("MMMM dd, yyyy"));
            }
        }
    }
}