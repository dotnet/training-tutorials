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
                .Include(b => b.Author)
                .Single(b => b.Id == 1);

            Console.WriteLine("-- Included author and checkout record(s) with book --");
            Console.WriteLine("Title: {0}", book.Title);
            Console.WriteLine("Author: {0} {1}", book.Author.FirstName, book.Author.LastName);
			
            Console.WriteLine("Checkout Records:");
            foreach (CheckoutRecord checkoutRecord in book.CheckoutRecords)
            {
                Console.WriteLine("Checked out by {0} {1} on {2}", checkoutRecord.Reader.FirstName, checkoutRecord.Reader.LastName, checkoutRecord.CheckoutDate.ToString("MMMM dd, yyyy"));
            }
        }
    }
}