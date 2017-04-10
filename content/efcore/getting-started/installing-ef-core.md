# Installing EF Core  
 
In this lesson, you will learn how to install Entity Framework Core (EF Core) on your machine.
 
## Install .NET Core 
 
First, you will need to install .NET Core on your machine. Go to the [.NET Core](https://www.microsoft.com/net/core) page of the Microsoft website, and follow the installation instructions for your platform of choice. You can ensure .NET Core is installed by running the following command: 
 
``` 
dotnet --version 
``` 
  
## Create New Project 
 
Create a new project by running the following commands in command line. You can skip these steps if you want to add EF Core to an existing project:  
 
```  
mkdir MyEfCoreProject
cd MyEfCoreProject
dotnet new 
```  
  
## Add EF Core to Project 
 
To add EF Core to your project, you need to list it as a dependency in your `project.json` file. You must add the appropriate package for your database provider as well. See [Database Providers](https://docs.microsoft.com/en-us/ef/core/providers/) in the docs for a full listing of available database providers.

In this example, we use Sqlite:
 
```{json} 
"dependencies": {
    "Microsoft.EntityFrameworkCore.Sqlite": "1.0.0", 
    "Microsoft.EntityFrameworkCore.Design": {       
        "version": "1.0.0-preview2-final", 
        "type": "build"  
    }   
} 
``` 
 
To add EF Core functionality to the [.NET Core Command-Line Interface](https://docs.microsoft.com/en-us/dotnet/articles/core/tools/), you need to add a package to the `tools` section of `project.json`: 
 
``` 
"tools": { 
    "Microsoft.EntityFrameworkCore.Tools": "1.0.0-preview2-final" 
} 
``` 
 
Once you have updated `package.json`, run the following command to install your project's tools and dependencies: 
 
``` 
dotnet restore 
``` 
 
After running this command, EF Core should be installed on your machine. You can run the following command to ensure EF Core was installed correctly: 
 
``` 
dotnet ef --help 
``` 
 
