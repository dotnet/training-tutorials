# .NET Training and Tutorials

This repository holds the content used to build the .NET getting started tutorials hosted at http://dot.net

## Organization

The content is broken up into high level groups by topic, initially covering
- [C#](content/csharp/README.md)
- [ASP.NET](content/asp.net/README.md)
- [F#](content/fsharp/README.md)

Within each content area, there are one or more *tutorials*.

Each tutorial consists of *lessons*. Each lesson should be a page detailing the concept being taught, along with sample code. Lesson and page may be used interchangeably when describing the tutorials. Lessons may be broken up into several *steps*. Each step and lesson should end with working code the user can run using a REPL, Visual Studio, or Visual Studio Code.

You can view the C# content as a model for other content areas. Note that URLs/links should use 'csharp' in place of C# because GitHub will convert the latter to C%23 resulting in broken links. Also note, links to README.md files are case-sensitive within GitHub's web view.

Lessons will include *tips* that will be tagged according to reader background, and eventually these will be displayed to readers who have indicated they have that background. For example:

> **Tip** {.tip .vb}
> The *static* keyword in C# is equivalent to *Shared* in Visual Basic .NET

Lessons will also include *notes*, which will receive special formatting when displayed to readers. For example:

> **Note** {.note}
> It is common to log exceptions that occur, so that you can review them later and improve the program to avoid them, if possible.

Lessons will be written using [markdown (Github variant)](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet). Lessons should link to other documents for specific tasks and API references. These should focus on helping the user follow through and understand the tutorial.

## Code Examples

There are three types of code examples used throughout the tutorials:

  * Non-runnable
  * Single-file runnable
  * Multi-file runnable

Runnable code examples are preferred over non-runnable code examples.

### Non-runnable

Non-runnable code examples should be formatted like so:

    ```{C#}
    Console.WriteLine("Hello World");
    ```

### Single-file Runnable

Single-file runnable code examples should be formatted like so:

    ```{.snippet}
    Console.WriteLine("Hello World");
    ```
    ```{.REPL}
    using System;
    
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello World");
        }
    }
    ```

The `.snippet` code block will be displayed in the lesson content with a "Show Me" button that, when clicked, will open up the console editor with the content of the `.REPL` block, which will not be displayed in the lesson content. Only code relevant to what is being discussed in the lesson should be placed in the `.snippet` block, and all of the code necessary to run the example should be included in the `.REPL` block.

### Multi-file Runnable

Multi-file runnable code examples should be formatted like so:

    ```{.snippet}
    var greeter = new Greeter();
    greeter.greet();
    ```
    :::repl{data-name=greeter}
    :::

The `.snippet` code block will be displayed in the lesson content with a "Show Me" button that, when clicked, will open up the console editor with the content of the multi-file code example that has the name specified by "data-name". The code files associated with the example should be located within the `training-tutorials` repo in `content/{topic}/{tutorial}/samples/{lesson}/{example}`. This directory should also contain a `README.md` that contains a description of the code example, as well as a list of the files necessary for that example.

For example, the loading-all-entities code example for the querying lesson in the efcore getting-started tutorial is organized like so:

```
content
|
|--efcore
   |
   |--getting-started
      |
      |--samples
         |
         |--querying
         |  |
         |  |--loading-all-entities
         |     |
         |     |--Program.cs
         |     |--README.md
         |
         |--shared
            |
            |--Address.cs
            |--Author.cs
            |--Book.cs
            |--CheckoutRecord.cs
            |--LibraryContext.cs
            |--Reader.cs
```

The associated `README.md` file is formatted like so:

```{Markdown}
# Loading All Entities

This code example demonstrates how to use EF Core to query all entities from the database.

## Code Files:
- [Program.cs](Program.cs)
- [Book.cs](../../shared/Book.cs)
- [LibraryContext.cs](../../shared/LibraryContext.cs)
- [Author.cs](../../shared/Author.cs)
- [CheckoutRecord.cs](../../shared/CheckoutRecord.cs)
- [Reader.cs](../../shared/Reader.cs)
- [Address.cs](../../shared/Address.cs)
```

It is important that the `Code Files` section be formatted exactly as shown here because `dotnet-core-website` parses this list to find the files necessary for the code example.

## Includes / Shared Elements

The ultimate publishing platform capabilities and requirements aren't yet known. Until they are, we don't have a system in place for includes or for sharing common elements across tutorials/pages. For now, we'll need to copy/paste.

## Branching Strategy

The *master* branch will contain the latest approved content. In the short term, approved content authors will be permitted to push directly to master. Once there is a significant amount of content, however, authors will be expected to work in their own feature branch before submitting a pull request and awaiting a :shipit: from another team member.

## Contributing
We welcome contributions and corrections to these tutorials. We will build out a roadmap shortly on the areas that we are looking to address next but send us your ideas. When contributing the steps are:

 1. Fork this repo and clone your fork locally
 2. Create a new branch for your contribution
 3. Do your awesomeness, commit and push to your fork
 4. Create a pull request from it to the master branch of https://github.com/dotnet/training-tutorials
 5. Leave some handy comments

We ask that authors of significant changes sign the [.NET Foundation Contribution License Agreement](https://cla.dotnetfoundation.org). This project has adopted the code of conduct defined by the [Contributor Covenant](http://contributor-covenant.org/)
to clarify expected behavior in our community.
For more information see the [.NET Foundation Code of Conduct](http://www.dotnetfoundation.org/code-of-conduct).

### .NET Foundation
This project is supported by the [.NET Foundation](http://www.dotnetfoundation.org).




