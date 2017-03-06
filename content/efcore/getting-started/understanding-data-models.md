# Understanding Data Models 
 
In this lesson, we are going to learn what a data model is and how to manually define a data model for EF Core. 
 
## What is a Data Model? 
 
A data model is a representation of the different types of information within a system and their relationships. For example, in a relational database, the database schema (tables, columns, etc.) would make up the data model. You sometimes hear these data models simply referred to as "models" in EF Core.  
 
## Defining a Data Model 
 
In EF Core, a data model is defined using "plain-old" C# object (POCO) classes. These are classes that solely consist of getters and setters. In EF Core, these POCO classes are referred to as **entities** or **entity classes**. 
 
Let's look at an example. Imagine we are creating a library application. What types of information would be in such a system? Books and readers are two good examples. To include these types of information in our data model, we first define an entity for each of them like so: 
 
```{C#} 
public class Book 
{ 
    public int Id { get; set; } 
    public string Title { get; set; } 
    public int PublicationYear { get; set; } 
} 
 
public class Reader 
{ 
    public int Id { get; set; } 
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
} 
```
 
> **NOTE** {.note}  
> We will be using this library example throughout the tutorial. It is very simple at the moment, but we will be adding more data members and entities in later lessons to make it more like a full-fledged library application. 
 
After defining our entities, we next need to register them with EF Core. We do this by first extending the abstract class [DbContext](https://msdn.microsoft.com/library/system.data.entity.dbcontext), which serves as the connection between our C# code and database. Then we add our entities as `DbSet` properties to the context as shown below: 
 
```{C#} 
public class LibraryContext : DbContext 
{ 
    public DbSet<Book> Books { get; set; } 
    public DbSet<Reader> Readers { get; set; } 
} 
``` 
 
Now that we have defined our data model, we can use EF Core to interact with a matching database. It is important that the data model and database schema match because otherwise EF Core won't be able to translate between the two, and it will just throw an error instead.  
 
When mapping the data model to the database, EF Core makes some assumptions which allow it to perform the mapping automatically. For example, it expects each entity in the data model to correspond to a database table that shares the same name as the `DbSet<>` property in the `DbContext`. In our library example, a matching database would have a "Books" table and a "Readers" table. Likewise, it expects each property of an entity to correspond to a table column that shares the same name and datatype. Thus, the "Books" table in our example would have an "Id" column with a `INT` datatype, a "Title" column with a `VARCHAR` datatype, and a "PublicationYear" column with an `INT` datatype. These assumptions made by EF Core are referred to as **conventions**, and they are simply its default behavior. 
 
In cases where you don't want the default behavior, conventions can be overridden through two different methods. **Data Annotations** involves annotating your entity classes to specify desired behavior.  **Fluent API** is a programmatic method of configuring EF Core, and it overrides data annotations. This tutorial focuses on Fluent API because it supports some features that are not available with data annotations. We will learn more about using Fluent API to configure EF Core in upcoming lessons. 