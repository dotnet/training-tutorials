# A Little Bit of Currying

by [Microsoft Research](https://www.microsoft.com/en-us/research/)

If you have a background in any of the popular programming languages, it may seem strange that function arguments in F# are passed without parentheses, and are separated by spaces rather than commas. Let's explore the essence of this notation to fully understand the implications, and the options you have when programming your F# abstractions. Consider the following function:

## The Add Function

```fsharp
let add x y = x + y
```

This function evaluates to `val add : x:int -> y:int -> int`, which can be read as "add is a function that, given an integer, returns a function which given an integer returns an integer". In other words, function application associates to the left and the application `(add 1 2)` can be read (and written) as `((add 1) 2)`. Another useful way to look at this ability is that you can fix arguments of a function from left to right in order to specialize a more general function to perform a specific task. Consider for instance the following definition:

## Specialization of Add

```fsharp
let inc = add 1
let anotherInc x = add 1 x
printfn "%d is the same as %d" (inc 1) (anotherInc 1)
```

The two functions are equivalent though the inc function is obtained by currying and fixing the first argument of add to 1. Function specialization happens all the time during programming and it is usually expressed as it is in the anotherInc function definition.
The important aspect to be considered here is that the order of arguments matters in F#. It affects the ability to use currying to specialize a function. The functions included in the collection modules (i.e. Seq, List, etc.) have the collection as the last argument exactly for this reason: you can "configure" the function to indicate which collection to operate on.

## Using Specialized Filters

```fsharp
let searchEven = Seq.filter (fun v -> v % 2 = 0)
printfn "%d even numbers in [1, 100]"
    ([ 1 .. 100] |> searchEven |> Seq.length)
printfn "%d even numbers in [1, 1000]"
    ([ 1 .. 1000] |> searchEven |> Seq.length)
```

Sometimes arguments cannot be easily swapped, consider for instance the following function:

## The sub function

```fsharp
let sub x y = x - y
```

In this case we have a problem in that subtraction is not commutative, and usually it is more interesting to fix the subtracted value. For instance, we cannot specialize sub as we did with inc since we would fix the second argument. Of course we can still define a new function that uses sub inside, but one may wonder if this is an intrinsic limitation of F#.
It would be nice to be able to swap the arguments of sub without changing its definition so that we can use currying and fix the subtracted value (now the first argument). The following function can help us to do this:

## Swapping Arguments

```fsharp
let swapargs f x y = f y x
```

It's interesting to have a look at the inferred type of `swapargs`:
`val swapargs : f:('a -> 'b -> 'c) -> x:'b -> y:'a -> 'c`
F# acknowledges the general form of the operation by generalizing the types of arguments that allow using swapargs with any function with two curried arguments. Now we can derive the dec function using currying:

## The Dec Function

```fsharp
let dec = swapargs sub 1
printfn "Before 10 there is %d" (dec 10)
```

The F# compiler ensures that using swapargs will not affect performance since it is possible to reorder arguments appropriately during compile time.
In conclusion, currying is an important tool in the hands of F# programmers, particularly those interested in building abstractions that allow expressing recurring patterns. Remember that defining a function as a first-class value may affect its usage, so choose wisely when you decide which arguments to put first (i.e. put the filter function before the collection to filter).