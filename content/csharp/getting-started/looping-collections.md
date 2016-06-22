# Looping Through Members of a Collection
by [Steve Smith](http://deviq.com/me/steve-smith)

## Using the ``foreach`` Loop

In earlier lessons, you learned about ``while`` loops and ``for`` loops. Although you can use these loop statements to loop through the contents of an array or other collection, the ``foreach`` loop statment is designed specifically for this purpose. The ``foreach`` loop uses the following syntax:

```c#
var myList = new List<string>(){ "one", "two", "three" };
foreach(var item in myList)
{
    Console.WriteLine(item);
}
```

The ``foreach`` statement begins with the ``foreach`` keyword, followed by an expression in parantheses. This expression includes the declaration of a local variable that will have scope within the loop block (``var item``). Then, the ``in`` keyword is specified, followed by the collection to be iterated over (``myList``). Like other loop statements, it's acceptable to follow the loop with a single statement (without curly braces), but it's recommended to always use the braces for greater clarity.

You may recall from the [previous lesson](arrays-collections.md) that the ``List`` type includes a similarly-named method, ``ForEach``, that works very similarly to this loop. If you do find yourself working with a ``List`` and needing to only execute a single statement per item in the list, the ``ForEach`` method may be a good choice. However, the ``foreach`` loop statement works on other collection types, not justs lists. In fact, you can use the ``foreach`` loop on any type that implements ``IEnumerable`` (or its generic equivalent, or, more accurately, has a ``GetEnumerator()`` method), which includes a wide variety of collection types (and arrays).

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

## Other looping techniques

The ``for`` loop can be used to iterate over an array or list, using its length or count property to determine when the loop should end:

```c#
// List<T>
var myList = new List<int>() { 43, 55, 100 };
for(int i = 0; i < myList.Count; i++)
{
    // access current element of the list with index of i
    Console.WriteLine(myList[i]);
}

// array
int[] numbers = new[] { 2, 3, 5, 7 };
for(int i = 0; i < numbers.Length; i++)
{
    // access current element of the array with index of i
    Console.WriteLine(numbers[i]);
}
```

You can also use a ``while`` loop, and your own indexer variable. The example below shows a list but the same approach works with arrays.

```c#
var myList = new List<int>() { 10, 20, 30 };
int index = 0;
while(index < myList.Count)
{
    Console.WriteLine(myList[index]);
    index++;
}
```
**Tip:** Be careful with this approach. If you forget to increment ``index``, you'll end up with an infinite loop.

## Next Steps

Write a program that allows the user to pass in a series of integer numbers as arguments, and will print the sum of the numbers. For example:

```
> dotnet run 1 2 3

6
```
