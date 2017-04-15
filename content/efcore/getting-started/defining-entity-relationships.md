# Defining Entity Relationships 
 
In this lesson, we are going to learn what relationships are and how to define them in EF Core. 
 
## What is a relationship? 
 
A relationship represents how two entities within a model associate with each other. There are three types of relationships: 
 
  * A **one-to-one** relationship exists when two entities are related to at most one instance of the other entity. In our library example, a reader is related to one address, and an address is related to one reader.  
  * A **one-to-many** relationship exists when one entity is related to at most one instance of another entity, but the second entity can be related to multiple instances of the first entity. For example, a book has a single author, but an author can have multiple books.  
  * A **many-to-many** relationship exists when two entities can both be related to multiple instances of the other entity. For example, a reader can check out multiple books, and a book can be checked out by multiple readers over time. 
 
## Defining Relationships 
 
Let's learn how to define each type of relationship in EF Core. 
 
### One-to-One Relationships 
 
 In the following example, we define a one-to-one relationship between `Reader` and `Address`. 
 
```{.snippet} 
public class Reader 
{ 
    public int Id { get; set; } 
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
 
    public Address Address { get; set; } 
} 
 
public class Address  
{ 
    public int Id { get; set; } 
    public string City { get; set; } 
    public string State { get; set; } 
 
    public int ReaderId { get; set; } 
    public Reader Reader { get; set; } 
}  
``` 
:::repl{data-name=one-to-one-relationship}   
:::  
 
As you can see, we added a property with an `Address` data type to `Reader` and a property with a `Reader` data type to `Address`. Adding a property with another entity class as the data type like this signifies a relationship. The property itself is known as a **navigation property**.  
 
Defining a navigation property for both of the entities involved in a relationship is known as a **fully defined relationship**. This is generally considered good practice, but it is not necessary. Defining a navigation property for just one of the entities is enough for EF Core to discover the relationship by convention. 
 
In addition to the navigation properties, we also added a `ReaderId` property to `Address` to track the primary key of the related `Reader`. This is referred to as a **foreign key** property, and it lets EF Core know that the "Addresses" database table should have a foreign key that points to the "Readers" database table. Because `Address` has the foreign key property and is thereby dependent on `Reader`, it is referred to as the **dependent entity** in this relationship, and `Reader` is referred to as the **principal entity**. 
 
In cases where you don't follow the convention, you will need to manually define which entity is the principal entity and which is the dependent entity using Fluent API. Some specific cases include using a foreign key that doesn't match the principal entity's primary key name, using a foreign key that points to a composite primary key, or including a foreign key in both of the entities involved in the one-to-one relationship. In the following example, both entities include a foreign key, so we define the one-to-one relationship using Fluent API's `HasOne` and `WithOne` methods and then define `Address` as the dependent entity by specifying its foreign key using the `HasForeignKey` method. 
 
```{.snippet}    
public class LibraryContext : DbContext  
{  
    public DbSet<Address> Addresses { get; set; } 
    public DbSet<Reader> Readers { get; set; }  
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)  
    {  
        modelBuilder.Entity<Reader>()  
            .HasOne(r => r.Address) 
            .WithOne(a => a.Reader)  
            .HasForeignKey<Address>(a => a.ReaderId); 
    }  
}  
```   
:::repl{data-name=one-to-one-manual-foreign-key}   
:::  
 
### One-to-Many Relationships 
 
 In this example, we define a one-to-many relationship between `Author` and `Book`. 
 
```{.snippet} 
public class Author  
{ 
    public int Id { get; set; } 
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
 
    public List<Book> Books { get; set; } 
} 
 
public class Book  
{ 
    public int Id { get; set; } 
    public string Title { get; set; } 
    public string Genre { get; set; } 
    public int PublicationYear { get; set; } 
 
    public Author Author { get; set; } 
} 
``` 
:::repl{data-name=one-to-many-relationship}   
:::  
 
This time we added a navigation property with an `Author` data type to `Book` and a navigation property with a `List<Book>` data type to `Author`.  When the navigation property is represented as a collection, such as a list, as opposed to a single entity, the navigation property is referred to as a **collection navigation property**. Unlike one-to-one relationships, EF Core is easily able to identify which entity is the primary entity and which is the dependent entity because it assumes the entity that represents the 'many' side of the relationship is the dependent entity. 
 
### Many-to-Many Relationships 
 
In this example, we define a many-to-many relationship between `Reader` and `Book`. 
 
```{.snippet} 
public class Book  
{ 
    public int Id { get; set; } 
    public string Title { get; set; } 
    public string Genre { get; set; } 
    public int PublicationYear { get; set; } 
 
    public List<CheckoutRecord> CheckoutRecords { get; set; } 
} 

public class Reader  
{ 
    public int Id { get; set; } 
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
 
    public List<CheckoutRecord> CheckoutRecords { get; set; } 
} 
 
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
:::repl{data-name=many-to-many-relationship}   
:::  
 
Above, we created a one-to-many relationship between `Reader` and `CheckoutRecord` and another one-to-many relationship between `Book` and `CheckoutRecord`. By creating these two one-to-many relationships, we create a many-to-many relationship between `Book` and `Reader` through the `CheckoutRecord` entity. We must do this because EF Core does not support direct many-to-many relationships; instead, it relies on **joining tables** like `CheckoutRecord` in our example. Typically, joining tables only contain foreign keys, but in our example, we also keep track of some other checkout-related data. 
 
 