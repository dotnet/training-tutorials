# Troubleshooting
by [Steve Smith](http://deviq.com/me/steve-smith)

## Common Errors

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
