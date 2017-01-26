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

            foreach (CheckoutRecord checkoutRecord in book.CheckoutRecords)
            {
                Console.WriteLine("{0}: {1} {2}, Due {3}", checkoutRecord.Book.Title, book.Author.FirstName, book.Author.LastName, checkoutRecord.DueDate.ToString("MMMM dd, yyyy"));
            }
        }
    }
}