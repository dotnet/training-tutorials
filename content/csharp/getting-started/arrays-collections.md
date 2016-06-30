# Working with Arrays and Collections
by [Steve Smith](http://deviq.com/me/steve-smith)

## Arrays

You can store multiple variables of a particular type in an *array* data structure. 

### Creating Arrays

Arrays must be declared by specifying the type of its elements. For example:

```c#
int[] someIntegers;
```

When declared, arrays do not need to specify a size, but when assigned, they must have a fixed size. You can instantiate an array using a variety of techniques:

```{.snippet}
int[] someIntegers = new int[3]; // holds 3 elements, with indexes of 0, 1, and 2.
// uninitialized elements of a declared array hold the default value for the type (in this case 0).

int[] moreIntegers = new int[] { 1, 2, 3, 4, 5 }; // initializes the values of the array

int[] otherIntegers = new [] { 1, 3, 5, 7, 9 }; // you can omit `int` and just specify []; type is inferred
```
```{.REPL}
using System;

class Program
{
    static void Main()
    {
        int[] someIntegers = new int[3]; // holds 3 elements, with indexes of 0, 1, and 2.
        // uninitialized elements of a declared array hold the default value for the type (in this case 0).
        Console.WriteLine(someIntegers[0]);
        Console.WriteLine(someIntegers[1]);
        Console.WriteLine(someIntegers[2]);

        int[] moreIntegers = new int[] { 1, 2, 3, 4, 5 }; // initializes the values of the array
        Console.WriteLine(moreIntegers[0]);
        Console.WriteLine(moreIntegers[1]);
        Console.WriteLine(moreIntegers[2]);
        // write out more if you like

        int[] otherIntegers = new [] { 1, 3, 5, 7, 9 }; // you can omit `int` and just specify []; type is inferred
        Console.WriteLine(otherIntegers[0]);
        Console.WriteLine(otherIntegers[1]);
        Console.WriteLine(otherIntegers[2]);
        // write out more if you like
    }
}
```

> **Tip** {.tip .newLanguage }    
> You can think of an array as being like an egg carton or pill organizer. A weekly pill organizer typically has 7 spaces of equal size, while an egg carton has two rows of 6 spaces each. Array locations are often referred to as *cells*.

> **Tip** {.tip .cpp}    
> Unlike in c++, arrays in C# aren't just pointers to an initial location in memory. These arrays are full objects in their own right, inheriting from System.Object and having members like `Length`. C# keeps handles those details behind the scenes.

Array values are referenced by an *index*. C# array indexes start with 0, and values are accessed by passing the index to the array using square braces (``[`` ``]``).

> **Tip** {.tip .vb }    
> In VB, parentheses are used for indexes as well as method invocation, and indexes start at 1. In C#, indexers always use square braces, and indexes start at 0.

Arrays can have more than one *dimension*. All of the arrays declared above are single-dimensional. A multi-dimensional array can store multiple values for each element. Two-dimensional arrays are often thought of like a grid; three-dimensional arrays like a cube.

```{.snippet}
int[,] eggCarton = new int[2,6]; // a typical egg carton can be thought of as a 2x6 array

int[,] someTable = { { 1, 2, 3 }, { 4, 5, 6 } }; // you can fill a multi-dimensional array on assignment as well
```
```{.REPL}
using System;

class Program
{
    static void Main()
    {
        int[,] eggCarton = new int[2,6]; // a typical egg carton can be thought of as a 2x6 array
        Console.WriteLine(eggCarton[0,0]); // one "corner" of the array
        Console.WriteLine(eggCarton[1,5]); // the opposite "corner"

        int[,] someTable = { { 1, 2, 3 }, { 4, 5, 6 } }; // you can fill a multi-dimensional array on assignment as well
        Console.WriteLine(someTable[0,0]); // one "corner" of the array
        Console.WriteLine(someTable[1,2]); // the opposite "corner"
    }
}
```

C# also supports *jagged* arrays, which are multi-dimensional arrays in which each element is itself an array of variable length.

```{.snippet}
int[][] jaggedArray = new int[4][]; // define first dimension
jaggedArray[0] = new int[2] { 1, 2 }; // set values of first array
```
```{.REPL}
using System;

class Program
{
    static void Main()
    {
        int[][] jaggedArray = new int[4][]; // define first dimension
        jaggedArray[0] = new int[2] { 1, 2 }; // set values of first array
        Console.WriteLine(jaggedArray[0][0]); // first element in first row
        Console.WriteLine(jaggedArray[0][1]); // second element in first row

        // additional rows haven't yet been created/assigned
        Console.WriteLine(jaggedArray[1]); // value is null
    }
}
```

> **Tip** {.tip .java }    
> Java has support for arrays of arrays, which are equivalent to C# jagged arrays, but doesn't have direct support for C#'s multi-dimensional array syntax. [Learn more](https://msdn.microsoft.com/en-us/library/ms228389(v=vs.90).aspx)

Unlike some other collection types you'll learn about, the number of dimensions and size of an array are fixed for the lifetime of the instance. All uninitialized elements of arrays are set to the default value for the type: 0 for numeric types, null for reference types, etc.

### Working with Arrays

As noted above, C# arrays are zero indexed, meaning the first element has an index of 0. In an array of *n* elements, the last element's index will be *n-1*. Arrays implement the ``IEnumerable`` interface, which you'll learn is important in the next lesson. To access the value of an array element, whether to read it or set it, you refer to the array by name and specify the index. For example:

```{.snippet}
int[] someIntegers = { 1, 2, 3 };

int x = 1 + someIntegers[0]; // x = 2
int y = 2 * someIntegers[2]; // y = 6
someIntegers[2] = y; // someIntegers now contains { 1, 2, 6 }
someIntegers[3] = 3; // EXCEPTION
```
```{.REPL}
using System;

class Program
{
    static void Main()
    {
        int[] someIntegers = { 1, 2, 3 };

        int x = 1 + someIntegers[0]; // x = 2
        Console.WriteLine(x);

        int y = 2 * someIntegers[2]; // y = 6
        Console.WriteLine(y);

        someIntegers[2] = y; // someIntegers now contains { 1, 2, 6 }
        Console.WriteLine(someIntegers[0]);
        Console.WriteLine(someIntegers[1]);
        Console.WriteLine(someIntegers[2]);

        // following lines produces an exception
        someIntegers[3] = 3; // EXCEPTION
    }
}
```

If you try to access an array element outside of the size of the array, as the last line in the example above does, an ``IndexOutOfRangeException`` will be thrown. If you are working with an array and need it to store more values than it currently has space for, you must declare a new array with enough space and populate it from the old array. You can check the array's ``Length`` property to see its size. Remember its maximum index will always be one less than its length.

### Arrays and Strings

You can quickly create arrays from strings using the ``String.Split`` method. This method will take a string and turn it into an array of strings. You provide the method with a *delimiter*, or separator, that is used to determine where to split apart the original string. For example, comma-separated values, or CSV, is a common data transfer format. You can easily convert a string of comma-separated values into an array of values:

```{.snippet}
string input = "red,blue,yellow,green";
string[] colors = input.Split(','); // note single quotes, which are used to define literal character (``char``) values
```
```{.REPL}
using System;

class Program
{
    static void Main()
    {
        string input = "red,blue,yellow,green";
        string[] colors = input.Split(','); // note single quotes, which are used to define literal character (``char``) values

        Console.WriteLine(colors[0]);
        Console.WriteLine(colors[1]);
    }
}
```

The ``colors`` array will include 4 elements, "red", "blue", "yellow", and "green". The reverse of this operation is the ``Join`` method, which you can see here applied to the ``colors`` array from the sample above.

```{.snippet}
string output = String.Join(" | ", colors);
Console.WriteLine(output);
```
```{.REPL}
using System;

class Program
{
    static void Main()
    {
        string input = "red,blue,yellow,green";
        string[] colors = input.Split(','); // note single quotes, which are used to define literal character (``char``) values

        string output = String.Join(" | ", colors);
        Console.WriteLine(output);
    }
}
```

When run, this sample displays:
    red | blue | yellow | green

Arrays are a built-in C# type with a lot of utility, but with certain limitations. Often, you won't know how many elements you'll need to store when you first create a collection, or there may be other behavior available to other collection types that you would like to take advantage of. One last very common extension method you can apply to arrays is ``ToList``. This method does just what you would expect - it creates a new ``List`` type and initializes it with the current contents of the array.

## Lists

.NET includes support for a variety of collection types, including the most commonly used ``List`` type. Typically, you'll want to work with a list of a particular type of item, so you'll declare a generic ``List<T>``, where ``T`` in this case is the type of objects the list will hold. When you refer to a ``List<T>`` verbally, you'll say "a list of T", which makes sense since that really is what it represents. A ``List<int>`` is a list of ints. A ``List<Customer>`` is a list of customers. When working with a ``List<T>``, your program will raise an exception if you try to add an element to the list that isn't of the declared type.

> **Tip** {.tip .java}    
> Unlike in Java, behind the scenes, C# creates custom code for generics you use. This means that there is no behind-the-scenes casting like in Java. There is a custom list type create for your type `T`. In Java, it is just a wrapper around the `ArrayList` and casting the objects for you.

> **Tip** {.tip .cpp}    
> Don't let the name confuse you, the C# `List<T>` is more closely related to the `vector<T>` from c++. The C# equivalent of `list<T>` is called `LinkedList<T>`. 

> **Tip** {.tip .cpp}    
> Like c++ does for templates, behind the scenes, C# creates custom code for generics you use. Unlike c++ templates, C# is very strict about the types, so using any members of a `T` requires telling the compiler something about the type. 

Lists do not have a fixed size. Unless the computer running your code runs out of memory, you can always add another element to a ``List``. This makes them more flexible than arrays. Generic lists are defined in the ``System.Collections.Generic`` namespace; you may need to add a ``using`` statement for the samples below to work in your application.

### Declaring Lists

```{.snippet}
List<int> someInts = new List<int>(); // declares an empty list
someInts.Add(2);  // the list now has one item in it: {2}

List<int> moreInts = new List<int>() { 2, 3, 4 }; // you can initialize a list when you create it

string[] colors = "red,blue,yellow,green".Split(','); // an array of 4 strings
List<string> colorList = new List<string>(colors); // initialize the list from an array
```
```{.REPL}
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> someInts = new List<int>(); // declares an empty list
        someInts.Add(2);  // the list now has one item in it: {2}
        Console.WriteLine($"someInts[0]: {someInts[0]}");

        List<int> moreInts = new List<int>() { 2, 3, 4 }; // you can initialize a list when you create it
        Console.WriteLine($"moreInts[0]: {moreInts[0]}");

        string[] colors = "red,blue,yellow,green".Split(','); // an array of 4 strings
        List<string> colorList = new List<string>(colors); // initialize the list from an array
        Console.WriteLine($"colorList[0]: {colorList[0]}");
    }
}
```

### Working with Lists

You can add items to an existing list using the ``Add`` method. You can add a group of items all at once by using the ``AddRange`` method, which you could use to add an array or another list to an existing list. You can also remove items from a list using a variety of methods, and insert items anywhere within the current list. See below for some more examples:

```{.snippet}
List<string> colors = new List<string>() { "black", "white", "gray" };
colors.Remove("black");
colors.Insert(0, "orange"); // orange is the new black
colors.RemoveAll(c => c.Contains("t")); // removes all elements matching condition (containing a "t")
// colors currently: orange, gray
colors.RemoveAt(0); // removes the first element (orange)
int numColors = colors.Count; // Count currently is 1
colors.Clear(); // colors is now an empty list
```
```{.REPL}
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> colors = new List<string>() { "black", "white", "gray" };
        colors.Remove("black");
        colors.Insert(0, "orange"); // orange is the new black
        Console.WriteLine($"colors[0] {colors[0]}");

        colors.RemoveAll(c => c.Contains("t")); // removes all elements matching condition (containing a "t")
        // colors currently: orange, gray
        Console.WriteLine($"colors[1] {colors[1]}");

        colors.RemoveAt(0); // removes the first element (orange)
        Console.WriteLine($"colors[0] {colors[0]}");

        int numColors = colors.Count; // Count currently is 1
        Console.WriteLine($"numColors: {numColors}");

        colors.Clear(); // colors is now an empty list
        Console.WriteLine($"colors.Count: {colors.Count}");
    }
}
```

If you need to perform a simple operation on every element in a list, you can use its ``ForEach`` method:

```{.snippet}
var colors = new List<string>() { "blue", "green", "yellow" };
colors.ForEach(Console.WriteLine); // equivalent to ForEach(c => Console.WriteLine(c)) 
```
```{.REPL}
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var colors = new List<string>() { "blue", "green", "yellow" };
        colors.ForEach(Console.WriteLine); // equivalent to ForEach(c => Console.WriteLine(c)) 
    }
}
```

The above sample will loop through the entire list and execute the expression provided once for each element. In this case, it will output to the console:

    blue
    green
    yellow

To quickly create a string from a list of values, you can use the ``String.Join`` method:

```{.snippet}
var colors = new List<string>() { "blue", "green", "yellow" };
Console.WriteLine(String.Join(",", colors));
```
```{.REPL}
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var colors = new List<string>() { "blue", "green", "yellow" };
        Console.WriteLine(String.Join(",", colors));
    }
}
```

This will display:
    blue,green,yellow

Finally, if you need to create an array from a list, you can call the ``ToArray`` method. This produces an array with a length equal to the list's ``Count``, containing the list elements in the same order. It can sometimes be useful when you need to provide an array as an argument to another method. If you don't know up front how many elements you need, you can first create and populate a list, and then call ``ToArray`` to produce the array.

## Next Steps

Write a simple program that lets the user manage a list of elements. It can be a grocery list, "to do" list, etc. Refer to [Looping Based on a Logical Expression](looping-logical-expression) if necessary to see how to implement an infinite loop. Each time through the loop, ask the user to perform an operation, and then show the current contents of their list. The operations available should be Add, Remove, and Clear. The syntax should be as follows:

    + some item
    - some item
    --

Your program should read in the user's input and determine if it begins with a "+" or "-", or if it is simply "--". In the first two cases, your program should add or remove the string given ("some item" in the example). If the user enters just "--" then the program should clear the current list. Your program can start each iteration through its loop with the following instruction:
``Console.WriteLine("Enter command (+ item, - item, or -- to clear)):");``

### Note { .note }
> You can get the contents of a string, minus the first 2 characters, by using ``Substring(2)`` on the string.
