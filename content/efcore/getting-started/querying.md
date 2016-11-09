# Querying Data 
 
In this lesson, you'll learn how to use querying to fetch one or more items from your database. EF Core uses LINQ to query data, so it is important that you understand LINQ before going through this lesson. Check out the [LINQ lesson](../../csharp/getting-started/linq.md) in the C# Interactive Tutorial if you need a refresher. 
 
## Example database 
 
For this lesson, we will use a small database to allow you to try querying on your own. The database has two tables, Books and Authors, and the data is as follows: 
 
### Books 
| BookId | AuthorId | Title                           | Genre            | PublicationYear | 
|--------|----------|---------------------------------|------------------|-----------------| 
| 1      | 9        | Mrs. Dalloway                   | Literary         | 1925            | 
| 2      | 6        | The Mysterious Island           | Science Fiction  | 1874            | 
| 3      | 7        | The Blazing World               | Science Fiction  | 1666            | 
| 4      | 1        | The Scarlet Plague              | Science Fiction  | 1912            | 
| 5      | 8        | The Secret Adversary            | Mystery          | 1922            | 
| 6      | 6        | An Antarctic Mystery            | Mystery          | 1897            | 
| 7      | 5        | My Bondage and My Freedom       | Narrative        | 1855            | 
| 8      | 3        | The Count of Monte Cristo       | Adventure        | 1845            | 
| 9      | 10       | Minnie's Sacrifice              | Historical       | 1869            | 
| 10     | 4        | My Antonia                      | Historical       | 1918            | 
| 11     | 4        | O Pioneers!                     | Historical       | 1913            | 
| 12     | 2        | Adventures of Huckleberry Finn  | Satire           | 1884            | 
| 13     | 2        | The Adventures of Tom Sawyer    | Satire           | 1876            | 
| 14     | 10       | Iola Leroy                      | Historical       | 1892            | 
| 15     | 8        | Murder on the Orient Express    | Mystery          | 1934            | 
| 16     | 1        | The Call of the Wild            | Adventure        | 1903            | 
| 17     | 4        | Death Comes for the Archbishop  | Historical       | 1927            | 
    
### Authors 
| AuthorId | FirstName  | LastName  | 
|----------|------------|-----------| 
| 1        | Jack       | London    | 
| 2        | Mark       | Twain     | 
| 3        | Alexandre  | Dumas     | 
| 4        | Willa      | Cather    |  
| 5        | Frederick  | Douglass  | 
| 6        | Jules      | Vern      | 
| 7        | Margaret   | Cavendish | 
| 8        | Agatha     | Christie  | 
| 9        | Virginia   | Woolf     | 
| 10       | Frances    | Harper    | 
| 11       | Stephen    | Crane     | 
 
## Loading All Entities 
 
Let's say we want to get all of the books from our database in a C# application. Normally, we would have to write a database query in a domain-specific language, such as SQL, and then we would have to manually map the results of this query to C# objects. With EF Core, this process is much easier because it takes care of the data access code for us. 
 
```c# 
using (var context = new LibraryContext()) 
{ 
    var books = context.Books.ToList(); 
} 
``` 
 
In order to interact with the database via EF Core, we must first create an instance of our context (`LibraryContext`). Notice that we create the context with the `using` keyword. This automatically disposes the context after the using block has finished executing. Alternatively, we could manually call `LibraryContext.dispose()`, but the `using` method is more convenient and readable. It is imperative that we dispose of the context after we are finished using it because it holds an open connection to the database. 
 
Once we have an instance of the context, we can use it to interact with the database. To access the books in the database, we reference the relevant `DbSet` within the context - `Books` in our case - and call the `ToList` method to convert the `DbSet` to a `List`. The resulting list will contain all of the books within the database. 
 
## Filtering Entities 
 
Loading all of the entities from a database is useful, but there are many use cases where we only want to load a subset of the entities from the database. For example, we may want to filter books by author or genre. EF Core allows us to filter entities via the `Where` extension method. Let's look at an example where we retrieve all science fiction books from the database. 
 
```c# 
using (var context = new LibraryContext()) 
{ 
    var books = context.Books 
        .Where(b => b.Genre == "Science Fiction") 
        .ToList(); 
} 
``` 
 
We use a lambda expression within the `Where` method to detect if the `Genre` property of each book is equal to "Science Fiction". Books that meet the criteria of the lambda expression will be included in the final result, while books that do not will be excluded. 
 
We could retrieve all of the books like in the previous example and then filter them in our application; however, this would require us to load all of the books into memory, and it does not allow us to take advantage of our database's optimized querying functionality. Allowing the database to do what it does best and perform the filtering for us results in a significant performance increase. Thus, it is important that we filter the `DbSet` with the `Where` method before calling `ToList`. 
 
## Loading a Single Entity 
 
Both of our examples so far have shown how to retrieve a collection of entities. Let's look at how to retrieve a single entity based on a unique identifier. 
 
```c# 
using (var context = new LibraryContext()) 
{ 
    var book = context.Books 
        .Single(b => b.Id == 1); 
} 
``` 
 
In this example, we use the `Single` extension method to find the book with an `Id` of 1. Note that we do not need to call `ToList()` because `Single` returns a single entity. It is important to only use `Single` with unique identifiers because if multiple entities meet the success criteria a `System.InvalidOperationException` will be thrown. 