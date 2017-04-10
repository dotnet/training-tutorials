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
dotnet new console
```

## Add EF Core to Project

To add EF Core to your project, you need to list it as a dependency in your `csproj` file:

```{xml}
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite"
    Version="1.0.0" />
```

You also must add the appropriate package for your database provider as a dependency. See [Database Providers](https://docs.microsoft.com/en-us/ef/core/providers/) in the docs for a full listing of available database providers. In this example, we use SQLite:

```{xml}
<PackageReference Include="Microsoft.NETCore.App" Version="1.0.0" />
```

To add EF Core functionality to the [.NET Core Command-Line Interface](https://docs.microsoft.com/en-us/dotnet/articles/core/tools/), you need to add the EF Core tools package to the tools section of your `csproj`.

```{xml}
<DotNetCliToolReference
  Include="Microsoft.EntityFrameworkCore.Tools.DotNet"
  Version="1.0.0" />
```

After adding these packages, your `csproj` should look similar to this example:

```{xml}
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp1.1</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite"
    Version="1.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design"
    Version="1.1.1" PrivateAssets="All" />
    <PackageReference Include="Microsoft.NETCore.App" Version="1.0.0" />
  </ItemGroup>
  <ItemGroup> 
    <DotNetCliToolReference
    Include="Microsoft.EntityFrameworkCore.Tools.DotNet"
    Version="1.0.0" />
  </ItemGroup>
</Project>
```

Once you have updated the `csproj` file, run the following command in the project directory to install your project's tools and dependencies:

```
dotnet restore
```

After running this command, EF Core should be installed on your machine. You can run the following command to ensure it installed correctly:

```
dotnet ef --help
```