# Making Decisions in Your Program
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## If This Then That

Programs that always do the same thing aren't nearly as powerful or interesting as those that can make decisions. These decisions can be based on any number of things, from user input to the current state of the program. Based on some kind of *condition*, the program can choose a different set of instructions to run. The most common C# keyword used to make decisions like this is the ``if`` statement. Because the ``if`` statement depends on a condition, the statement is sometimes referred to as a *conditional* statement.

You can use ``if`` statements for all kinds of things in your programs. You saw a common use for these statements, validating user input, in the (third lesson)[lesson-03.md], when you checked to see if any arguments had been passed to the program. When you write an ``if`` statement, it consists of the ``if`` keyword, a logical expression inside of parentheses, and then one or more statements. It's generally considered a best practice to always wrap the statement(s) after an ``if`` statement in curly braces (``{`` ``}``), unless it's a very short statement that can fit on the same line as the ``if`` statement. For example, you can exit a console application by *returning* from the ``Main`` method. For a program that requires arguments, you could immediately return if no arguments are supplied, using an ``if`` statement like this one:

```c#
if(args.Length == 0) return;
```

However, it can cause confusion if you don't use curly braces around the statements that go with your ``if`` statement, especially if your indentation is off:

```c#
var sum = 10;
if(isTrue)
    sum = sum + 5;
    sum = sum * 2;

Console.WriteLine(sum);
```

In this example, when ``isTrue`` is true, the value of sum will be 30. Otherwise, it will be 20. However, based on the indentation, the reader might think that the ``sum = sum * 2`` line only occurs when the if condition is true, which isn't the case. If that's the correct behavior, the code should be rewritten as:

```c#
var sum = 10;
if(isTrue)
{
    sum = sum + 5;
    sum = sum * 2;
}

Console.WriteLine(sum);
```

Now the above code will print a sum of 30 when the if condition evaluates to true, or the original sum value of 10 otherwise.

## Or Else!

Often, you'll want to have your program do one thing if a condition is true, and another, different thing if the condition is false. Either way, the code should execute only one of these two different paths. This is accomplished using an ``if-else`` statement. To create this statement, simply start with any ``if`` statement, and follow it with the ``else`` keyword and a statement block. Your ``else`` block can consist of a single statement without curly braces, but again it's generally much clearer, especially with ``else`` statements, to always wrap the statement(s) in curly braces.

For example, let's say you're writing a simple guessing game. You ask the user a question, and they answer it. Either they're correct, or they're not, and in either case you display a message telling them so. You could implement this as follows:

```c#
Console.WriteLine("What is the capital of Ohio?");
var answer = Console.ReadLine();
if(answer.ToLower() == "columbus")
{
    Console.WriteLine("Correct!");
}
else
{
    Console.WriteLine("Sorry, that's not the right answer.");
}
```

Of course, sometimes the decisions your program needs to make are a little more complicated than just "this or that". You can *nest* conditional statements within other conditional statements, creating complex trees of conditions. It's generally a good idea to try to limit how deeply you do such nesting, as complex hierarchies of conditional logic are common sources of bugs in software. However, often they're necessary. For example, sorting algorithms often use comparison logic to determine which of two values is greater, or if they're equal, like this:

```c#
// two values, x and y, are being compared
if(x < y)
{
    return -1;
}
else
{
    if(x == y)
    {
        return 0;
    }
    else // x must be greater than y
    {
        return 1;
    }
}
```

As you can see, these kinds of nested conditionals can get quite long to write out. One way these can be shortened is by using the ``else if`` construct. If your ``else`` block is going to contain another ``if`` statement, use ``else if`` instead, which eliminates the need for one set of curly braces and a level of indentation. Here's the above example, rewritten to use ``else if``:

```c#
// two values, x and y, are being compared
if(x < y)
{
    return -1;
}
else if (x == y) // == checks for equality between two values
{
    return 0;
}
else // x must be greater than y
{
    return 1;
}
```

If you find that you have more than 3 independent options in your conditional, you should consider using a different conditional structure: the *switch* statement.

## Switch Statements

The ``switch`` keyword is used to have your program execute a different *section* of code for potentially many different cases. The ``switch`` statement relies on the value of a single statement. You can use a ``switch`` statement to achieve the same behavior as you can with an ``if-else`` statement in many cases, but generally ``switch`` is preferred when you have more than a few options to deal with. Within a ``switch`` block, you define separate ``case`` labels, with code to execute. You can have more than one label for a given section of code. Each section must not allow execution to continue to the next section. This is usually achieved by using the ``break`` keyword to break out of the ``switch`` statement, but you can use other statements (like ``return``) as well.

The following example shows a simple ``switch`` statement based on an integer variable:

```c#
int guessedNumber = 1;
switch (guessedNumber)
{
    case 0:
        Console.WriteLine("Sorry, 0 is not a valid guess.");
        break;
    case 1:
    case 2:
    case 3:
        Console.WriteLine("You guessed low.");
        break;
    case 4:
        Console.WriteLine("You guessed the right number!");
        break;
    default:
        Console.WriteLine("You guessed high.");
        break;
}
```

In the example above, you can see how multiple different ``case`` labels can be applied to a single switch section.

## Next Steps

Your program can create a random number between 1 and 3 with the following code:
```c#
int correctNumber = new Random().Next(4);
```

Write a program that generates a random number between 1 and 3 and asks the user to guess what the number is. Tell the user if they guess low, high, or get the correct answer. Also, tell the user if their answer is outside of the range of numbers that are valid guesses (less than 1 or more than 3). You can convert the user's typed answer from a string to an int using this code:

```c#
int guessedNumber = int.Parse(Console.ReadLine());
```

Note that the above code will crash the program if the user doesn't type an integer value. For this exercise, assume the user will only enter valid guesses.