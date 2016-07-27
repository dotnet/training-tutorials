# Chaining Functions with the Forward Pipe Operator

by [Microsoft Research](https://www.microsoft.com/en-us/research/)

In the last lesson, you learned to create simple functions. You can create fairly sophisticated procedural programs with just this knowledge, but F# becomes truly powerful when you combine simple functions to build more complex computations. In this lesson, you'll learn to go beyond the fundamentals of F# to solve more interesting problems.

## F# Lists

One of the best ways to experience the power of combining functions is to perform computations across a set of data. In F#, one of the ways to store a data set is to use a List. F# supports a lightweight list creation syntax.

```fsharp
let evens = [2; 4; 6; 8]
```

The above snippet creates a small list, and fills it with four even numbers. The output window reveals the type of evens as int list, meaning that evens is a list of integers. You can specify the elements of a list by using the semicolon separated syntax above, but you can also create an arbitrarily large list by using the `..` syntax.

```fsharp
let firstHundred = [0..100]
```

As you probably expected, the Output Window reveals that firstHundred is a list containing the numbers zero through one hundred. If you have a Computer Science background, note that F# lists are singly linked lists.

## Working with Lists

F# provides a suite of functions for working with data in lists. You can see all of these methods by typing List followed by a . to bring up intellisense. The List module provides some powerful methods for operation on elements in a list.

```fsharp
let firstHundred = [0..100]
let doubled = List.map (fun x -> x * 2) firstHundred
```

List.map executes a mapping function on all of the values in a list and projects a new list from the results of the mapping function. In the above example, List.map builds the list doubled by performing a double function on each value in the firstHundred list. As you learned in the last lesson, List.map is a higher ordered function because it takes another function as an argument. List.map is One of the most useful higher-ordered functions in the List module on its own, but it becomes even more useful when you combine it with other functions.

## Combining Functions

```fsharp
let firstHundred = [0..100]
List.map (fun x -> x * 2)
    (List.filter (fun x -> x % 2 = 0) firstHundred)
```

In this example, the List.filter excludes all of the odd numbers from firstHundred before List.map doubles them. The result is a new list containing the first 50 even numbers doubled.

## Introducing the Forward Pipe Operator

Chaining together the higher ordered functions in the List module is powerful, but it quickly becomes difficult to read if you have to nest multiple functions on the same line.

```fsharp
List.sum (List.map (fun x -> x * 2)
    (List.filter (fun x -> x % 2 = 0) [0..100]))
```

This one line of code takes the first 100 integers, filters out the odd numbers, doubles them, and then adds them together. That's a lot of power for one line of code. Unfortunately, it is also difficult to read because you have to start from the inside at [0..100] and work your way out to List.sum. This is where the forward pipe operator comes in handy.

```fsharp
[0..100]
|> List.filter (fun x -> x % 2 = 0)
|> List.map (fun x -> x * 2)
|> List.sum
```

The |> operator allows you to reorder your code by specifying the last argument of a function before you call it. This example is functionally equivalent to the previous code, but it reads much more cleanly. First, it creates a list of numbers. Then, it pipes that list of numbers to filter out the odds. Next, it pipes that result to List.map to double it. Finally, it pipes the doubled numbers to List.sum to add them together. The Forward Pipe Operator reorganizes the function chain so that your code reads the way you think about the problem instead of forcing you to think inside out.

Believe it or not, you now possess enough F# skills to solve some fairly complicated problems. For example, below is a list of comma separated stock data. Let's walk through the process of finding the day with the greatest variance between the opening and closing price. First, take a look at the full solution.

```fsharp
//"Date,Open,High,Low,Close,Volume,Adj Close"
let stockData =
    [
      "2012-03-30,32.40,32.41,32.04,32.26,31749400,32.26";
      "2012-03-29,32.06,32.19,31.81,32.12,37038500,32.12";
      "2012-03-28,32.52,32.70,32.04,32.19,41344800,32.19";
      "2012-03-27,32.65,32.70,32.40,32.52,36274900,32.52";
      "2012-03-26,32.19,32.61,32.15,32.59,36758300,32.59";
      "2012-03-23,32.10,32.11,31.72,32.01,35912200,32.01";
      "2012-03-22,31.81,32.09,31.79,32.00,31749500,32.00";
      "2012-03-21,31.96,32.15,31.82,31.91,37928600,31.91";
      "2012-03-20,32.10,32.15,31.74,31.99,41566800,31.99";
      "2012-03-19,32.54,32.61,32.15,32.20,44789200,32.20";
      "2012-03-16,32.91,32.95,32.50,32.60,65626400,32.60";
      "2012-03-15,32.79,32.94,32.58,32.85,49068300,32.85";
      "2012-03-14,32.53,32.88,32.49,32.77,41986900,32.77";
      "2012-03-13,32.24,32.69,32.15,32.67,48951700,32.67";
      "2012-03-12,31.97,32.20,31.82,32.04,34073600,32.04";
      "2012-03-09,32.10,32.16,31.92,31.99,34628400,31.99";
      "2012-03-08,32.04,32.21,31.90,32.01,36747400,32.01";
      "2012-03-07,31.67,31.92,31.53,31.84,34340400,31.84";
      "2012-03-06,31.54,31.98,31.49,31.56,51932900,31.56";
      "2012-03-05,32.01,32.05,31.62,31.80,45240000,31.80";
      "2012-03-02,32.31,32.44,32.00,32.08,47314200,32.08";
      "2012-03-01,31.93,32.39,31.85,32.29,77344100,32.29";
      "2012-02-29,31.89,32.00,31.61,31.74,59323600,31.74"; ]

let splitCommas (x:string) =
    x.Split([|','|])

stockData
|> List.map splitCommas
|> List.maxBy (fun x -> abs(float x.[1] - float x.[4]))
|> (fun x -> x.[0])
```

The whole computation occurs in just six lines of code, but there is a lot of power in those simple six lines. Let's review each part in detail.

First, you need to break apart the list into its comma separated parts. You accomplish that by defining a helper function.

```fsharp
let splitCommas (x:string) =
    x.Split([|','|])
```

`splitCommas` takes a string and breaks it into pieces whenever it finds a comma. String.Split is a function that splits a string based on takes an array of separator characters. Arrays are another basic container type in F# that allow you to index stored elements. You create arrays using a [| ... |] which is similar to the [ .. ] syntax used for lists. Here,[|','|] creates a single element array containing a comma. The result of the splitCommas is also an array. This array contains the individual string parts. For example, parsing the last line of the input yields the array [| 2012-02-29; 31.89; 32.00; 31.61; 31.74; 59323600; 31.74 |].

Now that you've separated the data by commas, you need to find the maximum variance between the opening and closing days. You accomplish that using the List.maxBy function, which finds the maximum item in a List based a given comparison function. You use a comparison function of (fun x -> abs(float x.[1] - float x.[4])) to find the maximum variance. Note that abs is a function for calculating absolute value, and float is a function to parse a floating point number from a string. The x.[1] and x.[4] calls get the second and fifth elements in the array, respectively (in F# array indexing is zero-based).

Finally, you project the date from the maximum row using List.map and a projection function of (fun x -> x.[0]).

Putting it all together, you get six simple lines of code that solve a complex problem.

```fsharp
let splitCommas (x:string) =
    x.Split([|','|])

stockData
|> List.map splitCommas
|> List.maxBy (fun x -> abs(float x.[1] - float x.[4]))
|> (fun x -> x.[0])
```

## Summary

In this lesson, you learned to chain functions to perform complicated computations. You explored the power of higher ordered functions in the List module, learned how to better express your program's intent with the forward pipe operator, and built a program to analyze stock data. The basics that you gathered in this lesson will serve you well as you continue in your F# journey. Although these basics may seem simple, mastering the simple will allow you to take on the complex with ease. Give yourself a pat on the back; you've just learned how to express complicated computations in just a few lines of code!