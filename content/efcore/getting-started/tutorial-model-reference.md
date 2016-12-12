# Entity Classes 
 
In code, our entity classes look like: 
 
## Author 
 
```{.snippet} 
public class Author 
{ 
public int AuthorId { get; set; } 
public string FirstName { get; set; } 
public string LastName { get; set; } 
 
public List<Book> Books { get; set; } 
} 
``` 
 
## Book 
 
```{.snippet} 
public class Book 
{ 
public int BookId { get; set; } 
public string Title { get; set; } 
public string Genre { get; set; } 
public int PublicationYear { get; set; } 
 
public Author Author { get; set; } 
} 
``` 
 
## Edition 
 
```{.snippet} 
public class Edition 
{ 
public int EditionId { get; set; } 
public int Year { get; set; } 
 
public Book Book { get; set; } 
public Publisher Publisher {get; set;} 
} 
``` 
 
## Publisher 
 
```{.snippet} 
public class Publisher 
{ 
public int PublisherId { get; set; } 
public string Name { get; set; } 
 
public List <Edition> Editions { get; set; } 
} 
```