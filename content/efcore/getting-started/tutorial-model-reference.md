# Entity Classes 
This document contains the entity class definitions for the example data model used throughout this tutorial.

## Author.cs
 
```c#
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
 
```c# 
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
 
```c#
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
 
```c#
public class Reader
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Address Address { get; set; }
}
```

## Address.cs 
 
```c#
public class Address
{
    public int Id { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public int ReaderId { get; set; }
    public Reader Reader { get; set; }
}
```