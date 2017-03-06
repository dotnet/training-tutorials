# Defining Unique Identifiers 
 
In this lesson we will learn about unique identifiers, how they are configured by default in EF Core, and how to manually configure them when we don't follow the convention. 
 
## Primary Keys 
 
When defining our data model, we need to ensure that each entity class includes a property that can be used to uniquely identify instances of that entity. These unique identifiers are referred to as **primary keys**, and they map to the concept of primary keys in a relational database.  
 
By convention, EF Core assumes that a property named `Id` or `<type name>Id` is the primary key for an entity. For example, in our `Book` class below, the property named `Id` is assumed to be the primary key. 
 
```{.snippet}  
public class Book 
{ 
    public int Id { get; set; }  // You could also use 'BookId' here 
    public int ISBN { get; set; }
    public string Title { get; set; } 
    public string Genre { get; set; } 
    public int PublicationYear { get; set; } 
} 
```  
:::repl{data-name=primary-key}  
::: 
 
## Unconventionally Named Keys 
 
In some cases, you may not want to use the conventional naming scheme for your primary key. For example, all commercially-sold books have a unique ISBN, so we could use that as a unique identifier instead of adding an extra `Id` property (assuming our library doesn't contain multiple copies of the same book). To configure an unconventionally named key like `ISBN`, use the Fluent API `HasKey` method, as shown below: 
 
```{.snippet}  
     
public class LibraryContext : DbContext 
{ 
    public DbSet<Book> Books { get; set; } 
 
    protected override void OnModelCreating(ModelBuilder modelBuilder) { 
        modelBuilder.Entity<Book>() 
            .HasKey(b => b.ISBN); 
    }
}
 
```  
:::repl{data-name=unconventional-key-name}  
::: 
 
## Composite Keys 
 
Instead of adding a property to an entity solely for the purpose of uniquely identifying it, you may be able to use a combination of its existing properties. For example, in our library data model we added an `Id` property to `CheckoutRecord`, but a book can't be checked out by multiple readers at the same time, so we could use its `ReaderId`, `BookId`, and `CheckoutDate` properties to uniquely identify it instead. Combining multiple properties as a unique identifier like this is known as a **composite key**.  
 
To configure a composite key, we use Fluent API's `HasKey` method. In the following example, we configure a composite key for `CheckoutRecord` based on its `CheckoutDate` and its foreign keys `ReaderId` and `BookId`. 
 
```{.snippet}  
public class LibraryContext : DbContext 
{ 
    public DbSet<CheckoutRecord> CheckoutRecords { get; set; } 
 
    protected override void OnModelCreating(ModelBuilder modelBuilder) { 
        modelBuilder.Entity<CheckoutRecord>() 
            .HasKey(b => new { cr.ReaderId, cr.BookId, cr.CheckoutDate }); 
    }
}
 
```  
:::repl{data-name=composite-key} 
::: 