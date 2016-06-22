# Working with Arrays and Collections
by [Steve Smith](http://deviq.com/me/steve-smith)

## Arrays

You can store multiple variables of a particulare type in an *array* data structure. 

### Creating Arrays

Arrays must be declared by specifying the type of its elements. For example:

```c#
int[] someIntegers;
```

When declared, arrays do not need to specify a size, but when assigned, they must have a fixed size. You can instantiate an array using a variety of techniques:

```c#
int[] someIntegers = new int[3]; // holds 3 elements, with indexes of 0, 1, and 2.
// uninitialized elements of a declared array hold the default value for the type (in this case 0).

int[] moreIntegers = new int[] { 1, 2, 3, 4, 5 }; // initializes the values of the array

int[] otherIntegers = new { 1, 3, 5, 7, 9 }; // you can omit int[]; it will be inferred in this case.
```

Arrays can have more than one *dimension*. All of the arrays declared above are single-dimensional. A multi-dimensional array can store multiple values for each element. Two-dimensional arrays are often thought of like a grid; three-dimensional arrays like a cube.

```c#
int[,] eggCarton = new int[2,6]; // a typical egg carton can be thought of as a 2x6 array

int[,] someTable = { { 1, 2, 3 }, { 4, 5, 6 } }; // you can fill a multi-dimensional array on assignment as well
```

C# also supports *jagged* arrays, which are multi-dimensional arrays in which each element is itself an array of variable length.

```c#
int[][] jaggedArray = new int[4][]; // define first dimension
jaggedArray[0] = new int[2] { 1, 2 }; // set values of first array
```

Unlike some other collection types you'll learn about, the number of dimensions and size of an array are fixed for the lifetime of the instance. All uninitialized elements of arrays are set to the default value for the type: 0 for numeric types, null for reference types, etc.

### Working with Arrays

C# arrays are zero indexed, meaning the first element has an index of 0. In an array of *n* elements, the last element's index will be *n-1*. Arrays implement the ``IEnumerable`` interface, which you'll learn is important in the next lesson. To access the value of an array element, whether to read it or set it, you refer to the array by name and specify the index using square braces (``[`` ``]``). For example:

```c#
int[] someIntegers = { 1, 2, 3 };

int x = 1 + someIntegers[0]; // x = 2
int y = 2 * someIntegers[2]; // y = 6
someIntegers[2] = y; // someIntegers now contains { 1, 2, 6 }
someIntegers[3] = 3; // EXCEPTION
```

If you try to access an array element outside of the size of the array, as the last line in the example above does, an ``IndexOutOfRangeException`` will be thrown. If you are working with an array and need it to store more values than it currently has space for, you must declare a new array with enough space and populate it from the old array. You can check the array's ``Length`` property to see its size. Remember its maximum index will always be one less than its length.

### Arrays and Strings

You can quickly create arrays from strings using the String.Split method. This method will take a string and turn it into an array of strings. You provide the method with a *delimiter*, or separator, that is used to determine where to split apart the original string. For example, comma-separated values, or CSV, is a common data transfer format. You can easily convert a string of comma-separated values into an array of values:

```c#
string input = "red,blue,yellow,green";
string[] colors = input.Split(','); // note single quotes, which are used to define literal character (``char``) values
```

The ``colors`` array will include 4 elements, "red", "blue", "yellow", and "green". The reverse of this operation is the ``Join`` method, which you can see here applied to the ``colors`` array from the sample above.

```c#
string output = String.Join(" | ", colors);
Console.WriteLine(output);
```

When run, this sample displays:
    red | blue | yellow | green

Arrays are a built-in C# type with a lot of utility, but with certain limitations. Often, you won't know how many elements you'll need to store when you first create a collection, or there may be other behavior available to other collection types that you would like to take advantage of. One last very common extension method you can apply to arrays is ``ToList``. This method does just what you would expect - it creates a new ``List`` type and initializes it with the current contents of the array.

## Lists

.NET includes support for a variety of collection types, including the most commonly used ``List`` type. Typically, you'll want to work with a list of a particular type of item, so you'll declare a generic ``List<T>``, where ``T`` in this case is the type of objects the list will hold. When you refer to a ``List<T>`` verbally, you'll say "a list of T", which makes sense since that really is what it represents. A ``List<int>`` is a list of ints. A ``List<Customer>`` is a list of customers. When working with a ``List<T>``, your program will raise an exception if you try to add an element to the list that isn't of the declared type.

Lists do not have a fixed size. Unless the computer running your code runs out of memory, you can always add another element to a ``List``. This makes them more flexible than arrays. Generic lists are defined in the ``System.Collections.Generic`` namespace; you may need to add a ``using`` statement for the samples below to work in your application.

### Declaring Lists

```c#
List<int> someInts = new List<int>(); // declares an empty list
someInts.Add(2);  // the list now has one item in it: {2}

List<int> moreInts = new List<int>() { 2, 3, 4 }; // you can initialize a list when you create it

string[] colors = "red,blue,yellow,green".Split(','); // an array of 4 strings
List<string> colorList = new List<string>(colors); // initialize the list from an array
```

### Working with Lists

You can add items to an existing list using the ``Add`` method. You can add a group of items all at once by using the ``AddRange`` method, which you could use to add an array or another list to an existing list. You can also remove items from a list using a variety of methods, and insert items anywhere within the current list. See below for some more examples:

```c#
List<string> colors = new List<string>() { "black", "white", "gray" };
colors.Remove("black");
colors.Insert(0, "orange"); // orange is the new black
colors.RemoveAll(c => c.Contains("t")); // removes all elements matching condition (containing a "t")
// colors currently: orange, gray
colors.RemoveAt(0); // removes the first element (orange)
int numColors = colors.Count; // Count currently is 1
colors.Clear(); // colors is now an empty list
```

If you need to perform a simple operation on every element in a list, you can use its ``ForEach`` method:

```c#
var colors = new List<string>() { "blue", "green", "yellow" };
colors.ForEach(Console.WriteLine); // equivalent to ForEach(c => Console.WriteLine(c)) 
```

The above sample will loop through the entire list and execute the expression provided once for each element. In this case, it will output to the console:

    blue
    green
    yellow

To quickly create a string from a list of values, you can use the ``String.Join`` method:

```c#
var colors = new List<string>() { "blue", "green", "yellow" };
Console.WriteLine(String.Join(",", colors));
```

This will display:
    blue,green,yellow

Finally, if you need to create an array from a list, you can call the ``ToArray`` method. This produces an array with a length equal to the list's ``Count``, containing the list elements in the same order. It can sometimes be useful when you need to provide an array as an argument to another method. If you don't know up front how many elements you need, you can first create and populate a list, and then call ``ToArray`` to produce the array.

## Next Steps

Write a simple program that lets the user manage a list of elements. It can be a grocery list, "to do" list, etc. Refer to [lesson 8](lesson-08.md) if necessary to see how to implement an infinite loop. Each time through the loop, ask the user to perform an operation, and then show the current contents of their list. The operations available should be Add, Remove, and Clear. The syntax should be as follows:

    + some item
    - some item
    --

Your program should read in the user's input and determine if it begins with a "+" or "-", or if it is simply "--". In the first two cases, your program should add or remove the string given ("some item" in the example). If the user enters just "--" then the program should clear the current list. Your program can start each iteration through its loop with the following instruction:
``Console.WriteLine("Enter command (+ item, - item, or -- to clear)):");``

**Tip:** You can get the contents of a string, minus the first 2 characters, by using ``Substring(2)`` on the string.
