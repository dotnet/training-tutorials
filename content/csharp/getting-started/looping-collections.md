# Looping Through Members of a Collection
by [Steve Smith](http://deviq.com/me/steve-smith)

## Using the ``foreach`` Loop

In earlier lessons, you learned about ``while`` loops and ``for`` loops. Although you can use these loop statements to loop through the contents of an array or other collection, the ``foreach`` loop statment is designed specifically for this purpose. The ``foreach`` loop uses the following syntax:

```{.snippet}
var myList = new List<string>(){ "one", "two", "three" };
foreach (var item in myList)
{
    Console.WriteLine(item);
}
```
```{.REPL}
using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main()
        {
            var myList = new List<string>(){ "one", "two", "three" };
            foreach (var item in myList)
            {
                Console.WriteLine(item);
            }
        }
    }
}
```

The ``foreach`` statement begins with the ``foreach`` keyword, followed by an expression in parentheses. This expression includes the declaration of a local variable that will have scope within the loop block (``var item``). Then, the ``in`` keyword is specified, followed by the collection to be iterated over (``myList``). Like other loop statements, it's acceptable to follow the loop with a single statement (without curly braces), but it's recommended to always use the braces for greater clarity.

> **Tip** {.tip .java}    
> Unlike the `for` loop in Java, C# does not support iterating over collections in the `for` loop. You will need to use the `foreach` loop to achieve this functionality.

You may recall from the [previous lesson](arrays-collections) that the ``List`` type includes a similarly-named method, ``ForEach``, that works very similarly to this loop (hence the name). If you do find yourself working with a ``List`` and needing to only execute a single statement per item in the list, the ``ForEach`` method may be a good choice. However, the ``foreach`` loop statement works on other collection types, not justs lists. In fact, you can use the ``foreach`` loop on any type that implements ``IEnumerable`` (or its generic equivalent, or, more accurately, any type that has a ``GetEnumerator()`` method), which includes a wide variety of collection types (and arrays).

For example, to echo back to the user the arguments they passed to a console application, you could use ``foreach`` over the ``args`` array:

```c#
public static void Main(string[] args)
{
    foreach (var arg in args)
    {
        Console.WriteLine(arg);
    }
}
```

The ``foreach`` loop is a very simple way to work with every element in a collection. It doesn't require access to an indexer for the collection, making it applicable to many different collection types. However, in many cases you may want indexer-access to the collection within the loop. In that case, you should consider other techniques for looping over collections.

> **Tip** {.tip .newLanguage}    
> If all you need to do is perform some operation on each item in a collection, the ``foreach`` loop should be your default choice.

## Other looping techniques

The ``for`` loop can be used to iterate over an array or list, using its length or count property to determine when the loop should end:

```{.snippet}
// List<T>
var myList = new List<int>() { 43, 55, 100 };
for (int i = 0; i < myList.Count; i++)
{
    // access current element of the list with index of i
    Console.WriteLine(myList[i]);
}

// array
int[] numbers = new[] { 2, 3, 5, 7 };
for (int i = 0; i < numbers.Length; i++)
{
    // access current element of the array with index of i
    Console.WriteLine(numbers[i]);
}
```
```{.REPL}
using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main()
        {
            // List<T>
            var myList = new List<int>() { 43, 55, 100 };
            for (int i = 0; i < myList.Count; i++)
            {
                // access current element of the list with index of i
                Console.WriteLine(myList[i]);
            }

            // array
            int[] numbers = new[] { 2, 3, 5, 7 };
            for (int i = 0; i < numbers.Length; i++)
            {
                // access current element of the array with index of i
                Console.WriteLine(numbers[i]);
            }
        }
    }
}
```

You can also use a ``while`` loop, and your own indexer variable. The example below shows a list but the same approach works with arrays.

```{.snippet}
var myList = new List<int>() { 10, 20, 30 };
int index = 0;
while (index < myList.Count)
{
    Console.WriteLine(myList[index]);
    index++;
}
```
```{.REPL}
using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main()
        {
            var myList = new List<int>() { 10, 20, 30 };
            int index = 0;
            while (index < myList.Count)
            {
                Console.WriteLine(myList[index]);
                index++;
            }
        }
    }
}
```

> **Tip** {.tip .newLanguage}    
> Be careful with this approach. If you forget to increment ``index``, you'll end up with an infinite loop.

## Next Steps

Write a program that initializes a list integer numbers, and then prints the numbers out along with their sum. Sample output:

```
Numbers: 2 4 6
Sum: 12
```
