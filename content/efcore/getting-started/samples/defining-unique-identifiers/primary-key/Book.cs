using System.Collections.Generic;

public class Book
{
    public int Id { get; set; } // You could also use 'BookId' here
    public int ISBN { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }
}