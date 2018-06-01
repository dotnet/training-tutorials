public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }

    public Author Author { get; set; }
}