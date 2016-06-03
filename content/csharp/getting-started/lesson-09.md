# Looping a Known Number of Times
by [Brendan Enrick](http://deviq.com/me/brendan-enrick)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

See the [Issue](https://github.com/dotnet/training-tutorials/issues/7) to claim this lesson and/or view what should be included in it.

## For Loops
When you know the number of times you want to execute the code inside of your loop, or it is easily calculated, the ``for`` loop in C# is often the best available option. It looks and works like the ``while`` loop you used in the previous lesson, however, it has some additional features that we'll use in this lesson. For the simple case of looping over an operation a known-in-advance number of times, the ``for`` loop is the preferred way of doing this.

Using a ``for`` loop, you can easily write a loop that will print a list of numbers to the screen:

```c#
public static void Main(string[] args)
{
    for (int i = 0; i < 6; i++)
    {
        Console.WriteLine(i);
    }
}
```

Your ``for`` loop will execute multiple times just like the ``while`` loop did, however, there are a two additional *expressions* in its declaration. The loop in the example has a variable, ``i``, declared within the ``for`` loop's declaration. This variable is being used inside the loop, and its value increases by 1 each time through the loop. Because of the ``int i = 0;``, the first time through the loop, the value of ``i`` is 0. The last time the loop executes, the value of ``i`` is 5, because the conditional expression ``i < 6`` says to continue only if the value of ``i`` is less than 6.

Note: The *loop control variable* used most often is and integer variable with the name ``i``. This is a common convention used across languages in programming.

## Starting from different values
In the previous example, the first value in the loop is 0. If you care about the number of times a loop executes, but not about the value, that might be fine. If you do care about the value of the variable, you can start it wherever you like. If you adjust the ``for`` loop to count like most people do, you will want to have it start at one. And when you change it, you will have code like this:

```c#
public static void Main(string[] args)
{
    for (int i = 1; i < 6; i++)
    {
        Console.WriteLine(i);
    }
}
```

This code will count from 1 to 5 instead of starting at 0. In fact, you can put in any values you like into that loop. Go ahead and give it a try.

You can use variables to set the initial value of ``i`` or in your *conditional*, so your loop becomes dynamic:

```c#
public static void Main(string[] args)
{
    Console.WriteLine("Enter a starting number: ");
    int startingNumber = int.Parse(Console.ReadLine());
    Console.WriteLine("Enter an ending number: ");
    int endingNumber = int.Parse(Console.ReadLine());
    for (int i = startingNumber; i <= endingNumber; i++)
    {
        Console.WriteLine(i);
    }
}
```

## Counting up by different increments


## Counting down

## Nested Loops


## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.
