# Understanding C# Files and Projects
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## The Hello World Project

In [lesson one](lesson-01.md), you created a "Hello World" console application. In this lesson, you're going to learn about the different parts of that program, so that you understand how they work together. You'll also learn what happens when you build and run the application, and you'll learn about some common errors you may encounter and how to correct them.

To review, the project you created has the following files:

- Program.cs
- project.json
- project.lock.json

If you're just starting from this lesson, you may not have a *project.lock.json* file; it's created when you first restore or build the project. Let's look at each of these files individually.

### Program.cs

*Program.cs* is a small text file. Its file extension is "cs" because it contains C# source code. When you build the program, the ``dotnet build`` tool will build all of the files that end in ".cs" using the C# compiler. Although it's a small program, there are a number of important bits of syntax in it that you should understand. First, remember that C# is case-sensitive, so keywords won't work unless they're lowercase, and other named elements within the source code must exactly match the case of the element being referenced.

The first line of the program is 
```c#
using System;
```

The ``using`` statement is a programmer convenience. It allows us to refer to elements that exist within the listed *namespace* (in this case, ``System``) without prefixing them with the namespace name. What's a namespace? A namespace is a way of organizing programming constructs. They're similar to folders or directories in your file system. You don't have to use them, but they make it much easier to find and organize things. The reason this program includes the ``System`` namespace is that the ``Console`` type (used to print "Hello World!") is in that namespace. If the ``using`` statement were removed, the ``Console.WriteLine`` statement would need to include the namespace, becoming ``System.Console.WriteLine". Using statements must end with a semicolon (``;``). In C#, most statements that aren't defining a scope end with a semicolon.

After the ``using`` statements, the code declares its namespace:
```c#
namespace ConsoleApplication
```

Again, it's a good idea to use namespaces to keep your code organized. ``namespace`` is a language keyword; *ConsoleApplication* is an identifier. In this case, the ``ConsoleApplication`` namespace has only one element in it (the ``Program`` class), but this would  grow as the program grew in complexity. Namespaces use curly braces (``{`` and ``}``) to denote which types belong within the namespace.

Inside the namespace's scope (defined by its curly braces), a ``class`` called "Program" is created:
```c#
public class Program
```

This line includes two keywords and one identifier. The ``public`` keyword describes the class's [accessibility level](https://msdn.microsoft.com/en-us/library/ba0a1yw2.aspx). This defines how the class may be accessed by other parts of the program, and ``public`` means there are no restrictions to its access. The ``class`` keyword is used to define classes in C#, one of the primary constructs used to define *types* you will work with. C# is a *strongly typed* language, meaning that most of the time you'll need to explicitly define a type in your source code before it can be referenced from a program.

Inside the class's scope, a *method* called "Main" is defined:
```c#
public static void Main(string[] args)
```

The "Main" method is this program's entry point - the first code that runs when the application is run. Like classes, methods can have accessibility modifiers, too. In this case, ``public`` means there are no limitations on access to this method. 

Next, the ``static`` keyword marks this method as global and associated with the type it's defined on, not a particular *instance* of that type. You'll learn more about this distinction in later lessons. 

The ``void`` keyword indicates that this method doesn't return a value. The method is named *Main*. 

Finally, inside of parentheses (``(`` and ``)``, the method defines any *parameters* it requires. In this case, the method has one parameter of type *string array*, defined by ``string[]``, named *args*. Args in this case is short for *arguments*. Arguments correspond to parameters. A method defines the parameters it requires; when calling a method, the values passed to its parameters are referred to as arguments. Like namespaces and classes, methods have scope defined by curly braces.

A class can contain many methods, which are one kind of *member* of that class.

Within the method's scope, there is one line:
```c#
Console.WriteLine("Hello World!");
```

You've already learned that ``Console`` is a type inside of the ``System`` namespace. It's worth noting that this code does not create an *instance* of the ``Console`` type - it is simply calling the ``WriteLine`` method on the type directly. This tells you that ``WriteLine``, like the ``Main`` method in this program, is declared as a ``static`` method. This means that any part of the application that calls this method will be calling the same method, doing the same thing. The program won't, for instance, open several different console windows and write to them separately. Every call to ``Console.WriteLine`` is going to write to the same console window. 

Inside of the parentheses, the program is passing in "Hello World!" to the method. This is an *argument*, and will be used by the ``WriteLine`` method internally. C# defines a number of built-in types, one of which is a *string*. A string is a series of text characters. In this case, the program is passing the string ``"Hello World!"`` as an argument to the ``WriteLine`` method, which has defined a string parameter type. At the end of the line, the statement ends with a semicolon.

After the ``Console.WriteLine`` statement, there are three closing curly braces ``}``. These close the scopes for the ``Main`` method, the ``Program`` class, and the ``ConsoleApplication`` namespace, respectively. Note that the program uses indentation to make it easy to see which elements of the code belong to which scope. This is a good practice to follow, and will make it much easier for you (or others) to quickly read and understand the code you write.

### project.json

The *project.json* file includes information about your program, which is used to build and run the program correctly. A basic *project.json* file is shown here:

```json
{
  "version": "1.0.0-*",
  "buildOptions": {
    "emitEntryPoint": true
  },
  "dependencies": {
    "Microsoft.NETCore.App": {
      "type": "platform",
      "version": "1.0.0-rc2-3002702"
    }
  },
  "frameworks": {
    "netcoreapp1.0": {
      "imports": "dnxcore50"
    }
  }
}```

This file is formatted using [JavaScript Object Notation (JSON)](http://www.json.org/). It includes four root-level properties, *version*, *buildOptions*, *dependencies*, and *frameworks*. 

The *version* simply specifies the current version of this project. It defaults to "1.0.0-*", and you can manually update it any time you want, or there are tools you can use to automatically increment it as you make updates to the program.

The *buildOptions* property is used to configure how the application is built, or compiled. Since this is a program that should be executable on its own, it needs an *entry point*, where execution begins. Thus, the *emitEntryPoint* property is set to true.

The *dependencies* section lists any dependencies the program has. These dependencies refer to [NuGet packages](https://docs.nuget.org/consume/overview) and are pulled down into the project folder when you run `dotnet restore`. In this case, the program doesn't depend on functionality from any library packages, but it does depend on the "Microsoft.NETCore.App" platform package with the particular version specified. You'll learn how you can leverage existing packages to easily pull rich functionality into your programs.

### project.lock.json

This file is created each time you restore (``dotnet restore``) your application. You can delete it any time, and the next time you run ``dotnet restore`` it will be re-created. The *project.json* file includes a set of dependencies your program needs to compile, and these dependencies are often vague in terms of the version of the dependency they need. Thus, when you do a restore, you won't always get exactly the same packages, since different versions of the dependencies may be available at that time. The *project.lock.json* file includes detailed information about the specific versions of every package that was restored.

## Troubleshooting

In software development, attention to detail is critical. Small mistakes can cause an otherwise correct program to fail to build, much less run. The rules of a programming language like: the keywords it uses, the order in which they can appear, whether or not they're case sensitive, and how to define scopes and statements are collectively referred to as the language's *syntax*. Syntax errors are usually caught when you build the program, and will result in errors that you'll need to understand in order to correct. To demonstrate some of these errors, so you will know how to address them when you see them in later programs you write, you can intentionally introduce problems in your *Program.cs* file.

### No closing }

Delete the last ``}`` in the program and run ``dotnet build``. You should see an error like this one:
> Program.cs(11,6): error CS1513: } expected

The error lists file (*Program.cs*), the line of code (11), and how far into the line (the 6th character) the problem occurred at. In this case it makes it fairly easy to find the issue. Realize, though, that the error would not be very different if, instead of deleting the last curly brace, you instead deleted the one closing the *Main* method. In that case the error would still occur on the line of the last curly brace in the file, because the compiler would simply match the other curly braces in the order it found them.

### using system;

What if you didn't capitalize the *System* namespace? You might see an error like:
> Program.cs(1,7): error CS0246: The type or namespace 'system' could not be found (are you missing a using directive or an assembly reference?)

Remember, C# is case sensitive. Your application could refer to both a ``system`` namespace and a ``System`` namespace at the same time. Both of these would refer to completely different namespaces.

### Using System;

What if you accidentally capitalize the ``using`` keyword? In that case you'll get two errors:
> Program.cs(1,7): error CS0116: A namespace cannot directly contain members such as fields or methods
> Program.cs(1,7): error CS0246: The type or namespace 'Using' could not be found (are you missing a using directive or an assembly reference?)

The first error occurs because the compiler doesn't recognize ``Using`` as a keyword, so it treats it as the name of a member you're providing (like a method). Members, however, must be defined inside of classes or other types, not directly within namespaces, which is why this error occurs.

The second error is the same one we saw when we tried to use the ``system`` namespace above. The compiler simply doesn't recognize was ``Using`` is, because it's not a known keyword or a name that our program has defined.

You'll often find that fixing one compiler error will fix others, so it's a good idea when faced with many errors to just start with the first one before worrying too much about the others (since they may go away once the first one is fixed).

### public void Main(string[] args)

What if you forget the ``static`` keyword for the ``Main`` method? You'll get an error like:
> error CS5001: Program does not contain a static 'Main' method suitable for an entry point

This error should hopefully tell you what you need to know to add the ``static`` keyword to the ``Main`` method.

### public static void main(string[] args)

If you name the method ``main`` instead of ``Main`` you will get the same error as above. The method must be named exactly ``Main`` with a capital ``M``.

Note: It is acceptable to omit the ``string[] args`` parameter, as well as the ``public`` access modifier. ``static void Main()`` will compile and run just fine.

### console.WriteLine("Hello World!");

If you call ``console`` instead of ``Console`` you'll get an error:
> Program.cs(9,13): error CS0103: The name 'console' does not exist in the current context

This error should point out to you where the problem lies, at which point you will simply need to remember that ``Console`` starts with a capital ``C`` to correct the issue.

### Console.Writeline("Hello World!");

If you try to call the method ``Writeline`` instead of ``WriteLine`` (note capital ``L``), you will get an error:
> Program.cs(9,21): error CS0117: 'Console' does not contain a definition for 'Writeline'

This error should provide you with the context you need to realize you've misspelled (or capitalized) the name of the method you're trying to call.

## Next Steps

Modify your console application to display a different message. Go ahead and intentionally add some mistakes to your program, so you can see what kinds of error messages you get from the compiler. The more familiar you are with these messages, and what causes them, the better you'll be at diagnosing problems in your programs that you *didn't* intend to add!