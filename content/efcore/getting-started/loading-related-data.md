# Loading Related Data  
 
> **Note** {.note} 
> If you want to see the contents of the database or how our entities relate to each other, please refer to the [Database Reference](tutorial-database-reference.md) and [Model Reference](tutorial-model-reference.md) pages.
 
## Single Layer Inclusion 
 
When loading an entity from the database, EF Core will automatically include properties that are primitive types, but non-primitive types (such as other entities) won't be included. For example, if you run the following code snippet, you will see that the `Author` field is `null`: 
 
```{.snippet} 
using (var context = new LibraryContext()) 
{ 
	var book = context.Books 
		.Single(b => b.BookId == 1); 
} 
``` 
:::repl{data-name=field-not-included} 
:::
 
We need to explicitly tell EF Core if we want to load a non-primitive type like the Author field. We do this using the `Include` method. In the example below, the book that is returned will include its author's information. 
 
```{.snippet} 
using (var context = new LibraryContext()) 
{ 
	var book = context.Books  
		.Include(b => b.Author)
		.Single(b => b.BookId == 1); 
} 
``` 
:::repl{data-name=include-method} 
:::
 
You can also include multiple non-primitive type properties at once by calling the `Include` method multiple times. For example, we can include the `Editions` and `Authors` properties of the `Book` entity like so:  
 
```{.snippet} 
using (var context = new LibraryContext()) 
{ 
	var book = context.Books 
		.Include(b => b.Editions) 
		.Include(b => b.Author)
		.Single(b => b.BookId == 1); 
} 
``` 
:::repl{data-name=multiple-include-methods} 
:::
 
## Multi-layer Inclusion 
 
Now, what happens if one of the properties we include also has non-primitive type properties? By default, they won't be loaded from the database, but we can tell EF Core to load them using the `ThenInclude` method. For example, we can load the publisher information for the editions of a book like so: 
 
```{.snippet} 
using (var context = new LibraryContext()) 
{ 
	var book = context.Book 
		.Include(b => b.Editions) 
			.ThenInclude(e => e.Publisher)
		.Single(b => b.BookId == 1) ; 
} 
``` 
:::repl{data-name=then-include-method} 
:::
 
We can also chain `ThenInclude` calls to include deeper layers of related data. For example, if we want to find out the publisher information for the different editions of Fredrick Douglass' books in this library, we will need to chain `ThenInclude` calls to get the book, edition, and publisher information. 
 
```{.snippet} 
using (var context = new LibraryContext()) 
{ 
	var author = context.Authors 
		.Include(a => a.Books) 
			.ThenInclude(b => b.Editions) 
				.ThenInclude(e => e.Publisher)
		.Single(a => a.LastName == "Douglass"); 
} 
``` 
:::repl{data-name=then-include-method-chain} 
:::
 
A mix of `Include` and `ThenInclude` commands can also be chained together to include related data from multiple layers across related entities. In the following example, we are looking for the author, editions, and edition publishers of _The Call of the Wild_. 
 
```{.snippet} 
using (var context = new LibraryContext()) 
{ 
	var book = context.Books 
		.Include(b => b.Editions) 
			.ThenInclude(e => e.Publisher) 
		.Include(b => b.Author)		
		.Single(b => b.Title.Contains("Call of the Wild")); 
} 
``` 
:::repl{data-name=include-and-then-include-method-chain} 
:::
 
## Explicit Loading 
  
> **Note** {.note}
> Explicit Loading support was introduced in EF Core 1.1.0. If you are using an earlier release, the functionality in this section will not be available.

In the previous section, we used *eager loading* which loads the related data from the database as part of the initial database query. Another option is to use *explicit loading* which retrieves the related data separately from the original database query. This allows us to query and filter the related entities before loading them into memory, so we only pull the necessary information from the database. 
 
To use explicit loading, we first use the context's `Entry` method to specify the entity that we want to load related data for. Then, we use the `Collection` and `Reference` methods to specify the related data to load for the entity. `Collection` is used when retrieving multiple items, and `Reference` is used for single items. Finally, we use the `Load` method to actually load the data from the database.  
 
In the following example, we first eagerly load the author, Mark Twain. Then, we explicitly load his books. This means the books will be loaded in a separate database query after the initial author query. 
 
```{.snippet} 
using (var context = new LibraryContext()) 
{ 
    var author = context.Authors 
        .Single(a => a.LastName == "Twain"); 
 
    context.Entry(author) 
        .Collection(a => a.Books)
        .Load(); 
} 
``` 
:::repl{data-name=load-method} 
:::
 
In the above example, we returned all of Twain's books, but what if we only wanted to load a specific book of Twain's, say _Adventures of Huckleberry Finn_? We would use the `Query` command to search for it. 
 
```{.snippet} 
using (var context = new LibraryContext()) 
{ 
    var author = context.Authors 
        .Single(a => a.LastName == "Twain"); 
 
    context.Entry(author) 
        .Collection(a => a.Books) 
        .Query() 
        .Where(b => b.Title.Contains("Huck")) 
        .Load(); 
} 
``` 
:::repl{data-name=query-method} 
:::
 
By using explicit loading, we were able to load only the book we want (_Adventures of Huckleberry Finn_). If we had used eager loading, we would have loaded all of Twain's books. 
 
### Aggregate Functions 

When using explicit loading, we can also use aggregate functions, such as count, max, min, and sum, which allow us to perform calculations on the data without loading it all into memory. The following example counts how many books in the library are by Willa Cather without having to load all of the books into memory: 
 
```{.snippet} 
using (var context = new LibraryContext()) 
{ 
    var author = context.Authors 
        .Single(a => a.LastName == "Cather"); 
 
    var numBooks = context.Entry(author) 
        .Collection(a => a.Books) 
        .Query() 
        .Count(); 
} 
``` 
:::repl{data-name=count-method} 
:::
 
Likewise, we can find the year of the oldest edition of a book using the `Min` method: 
 
```{.snippet} 
using (var context = new LibraryContext()) 
{ 
    var book = context.Books
        .Single(b => b.Title == "The Scarlet Plague"); 
 
    var year = context.Entry(book) 
        .Collection(b => b.Editions) 
        .Query() 
        .Min(e => e.Year); 
} 
``` 
:::repl{data-name=min-method} 
:::
 