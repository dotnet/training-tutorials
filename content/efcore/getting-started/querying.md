# Querying Data 
 
In this lesson, you'll learn how to use querying to fetch one or more items from your database. EF Core uses LINQ to query data, so it is important that you understand LINQ before going through this lesson. Check out the [LINQ lesson](../../csharp/getting-started/linq.md) in the C# Interactive Tutorial if you need a refresher. 
 
> **Note** {.note}  
> If you want to see the contents of the database or how our entities relate to each other, please refer to the [Database Reference](tutorial-database-reference.md) and [Model Reference](tutorial-model-reference.md) pages.
 
## Loading All Entities 
 
Let's say we want to get all of the books from our database in a C# application. Normally, we would have to write a database query in a domain-specific language, such as SQL, and then we would have to manually map the results of this query to C# objects. With EF Core, this process is much easier because it takes care of the data access code for us. 
 
```{.snippet}
using (var context = new LibraryContext()) 
{ 
    var books = context.Books.ToList(); 
} 
```
:::repl{data-name=loading-all-entities}
:::

In order to interact with the database via EF Core, we must first create an instance of our context (we learned about contexts in [Understanding Data Models](understanding-data-models.md)). Notice that we instantiate the context with the `using` keyword. This automatically disposes the context after the using block has finished executing. Alternatively, we could manually dispose  `LibraryContext` with the `dispose` method, but the `using` method is more convenient and readable. It is imperative that we dispose of the context after we are finished using it because it holds an open connection to the database.
 
Once we have an instance of the context, we can use it to interact with the database. To access the books in the database, we reference the relevant `DbSet` within the context (`Books` in our case) and call the `ToList` method to convert the `DbSet` to a `List`. The resulting list will contain all of the books within the database. 
 
## Filtering Entities 
 
Loading all of the entities from a database is useful, but there are many cases where we only want to load a subset of the entities from the database. For example, we may want to filter books by author or genre. EF Core allows us to filter entities via the `Where` extension method. Let's look at an example where we retrieve all Historical books from the database. 
 
```{.snippet} 
using (var context = new LibraryContext()) 
{ 
    var books = context.Books 
        .Where(b => b.Genre == "Historical") 
        .ToList(); 
} 
```
:::repl{data-name=filtering-entities}
:::
 
We use a lambda expression within the `Where` method to detect if the `Genre` property of each book is equal to "Historical". Books that meet the criteria of the lambda expression will be included in the final result, while books that do not will be excluded. 
 
We could retrieve all of the books like in the previous example and then filter them in our application; however, this would require us to load all of the books into memory, and it does not allow us to take advantage of our database's optimized querying functionality. Allowing the database to perform the filtering for us results in a significant performance increase. Thus, it is important that we filter the `DbSet` with the `Where` method before calling `ToList`. 
 
## Loading a Single Entity 
 
Both of our examples so far have shown how to retrieve a collection of entities. Let's look at how to retrieve a single entity based on a unique identifier. 
 
```{.snippet}
using (var context = new LibraryContext()) 
{ 
    var book = context.Books 
        .Single(b => b.Id == 1); 
} 
```
:::repl{data-name=loading-single-entity}
:::
 
In this example, we use the `Single` extension method to find the book with an `Id` of 1. Note that we do not need to call `ToList` because `Single` returns a single entity. It is important to only use `Single` with unique identifiers because if multiple entities meet the success criteria a `System.InvalidOperationException` will be thrown. 
