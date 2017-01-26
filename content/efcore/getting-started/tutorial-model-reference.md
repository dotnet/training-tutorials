# Entity Classes 
 
Here is what the entity classes that make up our example data model look like:
 
## Author.cs
 
```{.snippet} 
using System.Collections.Generic;

public class Author 
{ 
    public int Id { get; set; } 
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
 
    public List<Book> Books { get; set; } 
} 
``` 
 
## Book.cs
 
```{.snippet} 
using System.Collections.Generic;

public class Book 
{ 
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }
    
    public Author Author { get; set; }
    public List<CheckoutRecord> CheckoutRecords { get; set; }
} 
``` 
 
## CheckoutRecord.cs 
 
```{.snippet} 
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
``` 
 
## Reader.cs
 
```{.snippet} 
public class Reader
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Address Address { get; set; }
}
```

## Address.cs 
 
```{.snippet} 
public class Address
{
    public int Id { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}
```