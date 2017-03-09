# Generating Databases and Data Models 
 
In this lesson, we will learn how to generate a database from a data model, modify an existing database, and generate a data model from an existing database. 
 
## Generating a Database from a Data Model 
 
We can generate a new database that exactly matches our data model using EF Core **migrations**. Migrations are a feature in the EF Core command line tools which take a data model as input and generate database-creation code as output. The resulting code can then be applied to create a new database. 
 
Let's say we want to generate a database from our library example, we would first navigate to that project's directory from the command line. Then we would run the [dotnet-ef-migrations-add](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet#dotnet-ef-migrations-add) command to generate the database-creation code: 
 
``` 
dotnet ef migrations add [migration name] 
``` 
 
> **Note** {.note}  
> If the above command fails with the error message, `No executable found matching command "dotnet-ef"`, make sure `Microsoft.EntityFrameworkCore.Tools` has been installed for your project, as shown in the [Installing EF Core](installing-ef-core.md) lesson. Also ensure you are running the command from the directory that contains your `project.json` file. 
  
Finally, we would run the [dotnet-ef-database-update](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet#dotnet-ef-database-update) command to execute the database-creation code. 
 
``` 
dotnet ef migrations update  
``` 
  
After running the above command, we will have a new database that exactly matches our data model. You can run the example below to see what the resulting database schema would look like. It runs the migration commands for you in the cloud so you can test your model in-browser.

```{.snippet}   
public class Book {  
    public int Id { get; set; }  
    public string Title { get; set; }  
    public int PublicationYear { get; set; }  
}  
  
public class Reader {  
    public int Id { get; set; }  
    public string FirstName { get; set; }  
    public string LastName { get; set; }  
}  
``` 
:::repl{data-name=generate-database}   
::: 
 
## Modifying an Existing Database 
  
If we ever need to make changes to our data model in the future, migrations can also be used to modify our database. To do this, we first update our entity classes; then, we use the same migration commands we used to originally create the database, but with a new migration name. 
 
``` 
dotnet ef migrations add [migration name]  
dotnet ef migrations update  
``` 
  
Anytime we make changes to the data model, we will need to add and run a new migration for the changes to be reflected in our database. 
 
## Generating a Data Model from an Existing Database 
  
We can also have EF Core automatically generate a data model from an existing database. First, we create an EF Core project by following the steps in the [Installing EF Core](installing-ef-core.md) lesson. Once the project is ready-to-go, we navigate to the directory that contains the `project.json` file in command line. Next, we run the [dotnet-ef-dbcontext-scaffold](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet#dotnet-ef-dbcontext-scaffold) command to create a model from the existing database. 
 
``` 
dotnet ef dbcontext scaffold <DB connection string> <DB provider namespace> 
``` 
 
For example, if we wanted to generate a data model from an existing SQLite database, we would run the following command: 
 
``` 
dotnet ef dbcontext scaffold Filename=MyDatabase.db Microsoft.EntityFramework.Sqlite 
``` 