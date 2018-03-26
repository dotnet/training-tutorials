using System;

public class CheckoutRecord
{
    public int Id { get; set; }
    public DateTime CheckoutDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime DueDate { get; set; }
    
    public Book Book { get; set; }
    public Reader Reader { get; set; }
}