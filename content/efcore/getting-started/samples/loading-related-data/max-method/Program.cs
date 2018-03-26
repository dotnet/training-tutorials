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
                .Single(b => b.Title.Contains("Orient Express")); 
 
            var mostRecentCheckout = context.Entry(book) 
                .Collection(b => b.CheckoutRecords) 
                .Query() 
                .Max(c => c.CheckoutDate); 

            Console.WriteLine("Most Recent Checkout: {0}", mostRecentCheckout.ToString("MMMM dd, yyyy"));
        }
    }
}