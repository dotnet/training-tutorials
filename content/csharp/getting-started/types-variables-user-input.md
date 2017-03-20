# Learning About Built-In Types and Variables
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## Adding Variables

In this lesson, you'll create a simple Hello World program, and then you'll learn how you can customize the behavior of the program by adding variables to it.

This simple program just prints "Hello World!" to the console:

```{class=snippet}
Console.WriteLine("Hello World!");
```
```{class=REPL}
using System;

class Program
{
    static void Main()
    {
		Console.WriteLine("Hello World!");
    }
}
```

> **Tip** {.tip .newLanguage }
> It's programming tradition that the first program one writes in a new language print out the phrase, "Hello World".

You can change this greeting to be more personalized by using a variable. On the line above this one, you can add a variable that holds your name, like this:

```{class=snippet}
var name = "Steve"; // use your name here
```
```{class=REPL}
using System;

class Program
{
    static void Main()
    {
		var name = "Steve"; // use your name here
		Console.WriteLine("Hello World!");
    }
}
```

There are a few new elements to this line of code. First, you're using a C# keyword, ``var``, which you can think of as *variable*. The ``var`` keyword is shorthand for whatever the type on the other side of the assignment operator (``=``) might be. In this case, the value in double quotes (``"Steve"`` in the example above) is a *string*. Strings are one of the built-in types in C#, and are used to represent text values. You can also declare a variable by specifying its type explicitly. In this example, the equivalent statement would be ``string name = "Steve";``.

The ``//`` on the line represents a single-line comment. Everything on the line that follows these two characters is ignored by the compiler. You can use these comments on a line all by themselves, or following other code as in this case. Comments are useful for explaining why you're doing something a certain way in your application, but avoid the temptation to overuse them or to use them to explain complicated code. A better solution is to make the code less complicated.

Now that you have a variable representing your name, you can use it in the next line so that the program greets you, rather than the world. To do that, remove the word *World* and replace it with ``{name}``. Note that these are curly braces around the name of the variable. By using this convention, you're letting C# know that you want it to substitute the value of the variable ``name`` in that location. The last thing you need to do for this convention to work is prefix the string with a ``$`` sign. When completed, the two lines of code should look like this:

```{.snippet}
var name = "Steve"; // use your name here
Console.WriteLine($"Hello {name}!");
```
```{.REPL}
using System;

class Program
{
    static void Main()
    {
		var name = "Steve"; // use your name here
		Console.WriteLine($"Hello {name}!");
    }
}
```

Run the program (from a command prompt in the project folder, type ``dotnet run``). You should see the output that includes your name (or "Steve" if you decided to just copy the code above).

This is somewhat useful, in that you've extracted out an important part of the output into a variable, but currently there is no way for this value to ever change. Whatever it's set to in the code when it's declared is what will be displayed. What would be even more powerful would be if you could let the end user of the program specify *their* name, and then use that in the output. You'll learn how to do just that in the next section.

## Accepting User Input

Since this is a console application, the easiest way to take in user input is as arguments when they run the application. Recall that the ``Main`` method accepts a parameter of ``string[] args``. You'll modify your program so that if no arguments are passed when the program is run, it prints out "Hello World!", but if they pass an argument, you'll display it instead of "World".

To determine if the user has passed an argument, you need to check and see how many arguments there are in the incoming *array*. Remember, an array is a built-in type for holding a collection of variables of the same type. Arrays have some built-in functionality, like checking how many elements are in the array. For the current requirement, you don't care exactly how many arguments there are; you just care if there are any. You can determine that by seeing if the ``Length`` of the array is greater than zero.

To have your program do one thing under one circumstance, and another otherwise, requires the use of a conditional statement. In this case, you're going to use the ``if`` statement, which is the most common conditional statement. The syntax for an ``if`` statement is the ``if`` keyword, followed by a *boolean* expression, surrounded by parentheses (``(`` ``)`` ). A boolean expression is anything that evaluates to ``true`` or ``false``, which could be a variable or some kind of equality statement. In this case, you're going to test whether ``args.Length > 0``.

When the expression inside of the parentheses is true, the program block immediately following the ``if`` statement will execute. Otherwise, it is skipped (you'll learn about the ``else`` keyword soon, which adds more behavior). To set the value of the ``name`` variable if arguments have been passed to the program, you will need to write the following code:

```c#
var name = "World";
if (args.Length > 0)
{
    name = args[0];
}
Console.WriteLine($"Hello {name}!");
```

This is a common pattern in programming. You're setting the default value of the variable when you declare it (to "World"). Then, you're letting the user override this default value. If they don't, it will remain. To test this program, run it both with and without passing any additional arguments. From the command prompt, run ``dotnet run`` as usual. You should see "Hello World!". Then try adding your name to the end of the command: ``dotnet run Steve``. You should see "Hello Steve!" (where Steve is replaced with your name).

### Reading from the Console

Another way to accept user input is to read it from the console. Just like there is a ``Console.WriteLine`` method, there is also a ``Console.ReadLine`` method. The ``ReadLine`` method returns a string holding whatever the user typed in before pressing *Enter*. You can modify your program to ask the user to enter their name, and then display it based on whatever they typed:

```c#
Console.WriteLine("What is your name?");
var name = Console.ReadLine();
Console.WriteLine($"Hello {name}!");
```

## Next Steps

Using just the ``ReadLine`` and ``WriteLine`` methods and your current knowledge of variables, you can have the user pass in quite a few bits of information. Using this approach, create a console application that asks the user a few questions and then generates some custom output for them. For instance, your program could generate their "hacker name" by asking them their favorite color, their astrology sign, and their street address number. The result might be something like "Your hacker name is RedGemini480."
