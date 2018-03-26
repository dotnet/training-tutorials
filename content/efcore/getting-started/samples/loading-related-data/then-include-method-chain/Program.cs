using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            var author = context.Authors 
                .Include(a => a.Books) 
			        .ThenInclude(b => b.CheckoutRecords) 
				        .ThenInclude(c => c.Reader)
                .Single(a => a.LastName == "Douglass");

            foreach (Book book in author.Books)
            {
                Console.WriteLine("Book: {0}", book.Title);
                Console.WriteLine("Checkout Records:");
                foreach (CheckoutRecord checkoutRecord in book.CheckoutRecords)
                {
                    Console.WriteLine("Checked out by {0} {1} on {2}", checkoutRecord.Reader.FirstName, checkoutRecord.Reader.LastName, checkoutRecord.CheckoutDate.ToString("MMMM dd, yyyy"));
                }
            }
        }
    }
}