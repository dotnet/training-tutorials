# Fun with Functional Functions

by [Microsoft Research](https://www.microsoft.com/en-us/research/)

Now that you've taken your first steps in F#, it's time to introduce one of the most basic, yet most powerful building blocks of F#: functions. In the previous lesson, you saw how you can use let to bind a name to a value, but you can also use let to bind a name to a function.

## Bind a name to a function

```{.snippet}
let add x y =
    x + y

add 2 2
```
```{.REPL}
let add x y =
    x + y

add 2 2
```

The above sample creates a simple addition function. The Output Window reveals that F# has inferred the signature of the function to be `val add : x:int -> y:int -> int`. This means that add accepts two `int` arguments, x and y, and returns an `int` result. It was able to infer all of this based on the use of the `+` operator. This small snippet also reveals that F# is a whitespace sensitive language. The body of the add function is denoted by indenting `x + y` four spaces, and the return value is the last line of the function. White space sensitivity can take some getting used to at first, but it's just another way that F# helps you to write concise code that is easy to read.

## Type Annotations

F# was able to infer the types of x and y in the above code, but sometimes you will need to give it a little help. To accomplish this, you use Type Annotations.

```{.snippet}
// This results in an error.
let toHackerTalk phrase =
    phrase.Replace('t', '7').Replace('o', '0')
```
```{.REPL}
// This results in an error.
let toHackerTalk phrase =
    phrase.Replace('t', '7').Replace('o', '0')
```

This snippet defines a simple function to translate a string into fearsome hacker speak. The intent is to use .NET's built in `String.Replace` to replace a couple of characters with their hacker alternatives, but F# doesn't know that phrase is of type String. Replace could be a valid call for many types, so F# gives you the following error: "Lookup on object of indeterminate type based on information prior to this program point. A type annotation may be needed prior to this program point to constrain the type of the object. This may allow the lookup to be resolved." As the error indicates, you can fix this problem by giving F# a little help narrowing down the type.

```{.snippet}
let toHackerTalk (phrase:string) =
    phrase.Replace('t', '7').Replace('o', '0')
```
```{.REPL}
let toHackerTalk (phrase:string) =
    phrase.Replace('t', '7').Replace('o', '0')
```

By adding the (`phrase:string`) type annotation to the method signature, you give F# the context that it needs to be able to resolve the .Replace call. Type annotations are necessary when a function's argument cannot be inferred from its use, but they can also make your code more readable even in cases where they aren't required.

## Functions as Values

Since functions are bound to names via the let keyword, you can treat them just like any other value. That's because functions are "first class citizens" in F#. This allows you to create a helper function within another function.

```{.snippet}
let quadruple x =
    let double x =
        x * 2

    double(double(x))
```
```{.REPL}
let quadruple x =
    let double x =
        x * 2

    double(double(x))
```

`quadruple` calls the `double` function twice to perform an (inefficient) quadrupling.
You can also use a function as an argument to another function to create what's called a *higher order* function.

```{.snippet}
let chrisTest test =
    test "Chris"

let isMe x =
    if x = "Chris" then
        "it is Chris!"
    else
        "it's someone else"

chrisTest isMe
```
```{.REPL}
let chrisTest test =
    test "Chris"

let isMe x =
    if x = "Chris" then
        "it is Chris!"
    else
        "it's someone else"

chrisTest isMe
```

`chrisTest` is a simple higher order function that executes a test against a string value. The `isMe` function checks to see if a string is equal to "Chris". Passing `isChris` to `chrisTest` passes the string "Chris" as an argument to the `isChris` function, and returns the result of "it is Chris!". You'll see some examples of more useful higher ordered functions in the next lesson.

## Lambda Functions

Since it is so common to create small helper functions in F# programming, F# also provides a special syntax for creating in-line functions. These functions are called lambdas, or lambda functions.

```{.snippet}
let add = (fun x y -> x + y)
add 2 2
```
```{.REPL}
let add = (fun x y -> x + y)
add 2 2
```

This add function is the same as the one defined earlier, but the declaration happened in one line. Lambdas are also known as anonymous functions because you aren't required to bind them to a name with `let`.

```{.snippet}
let twoTest test =
    test 2

twoTest (fun x -> x < 0)
```
```{.REPL}
let twoTest test =
    test 2

twoTest (fun x -> x < 0)
```

The above snippet uses an anonymous lambda to pass a negativity check to `twoTest`.

## Summary

In this lesson, you continued your journey in F# by exploring the flexibility of functions. You now know how to create and call functions in F#. You've seen how whitespace sensitivity can cut down on the ceremony of curly braces and return statements. You learned how functions are values that can be nested and passed as arguments. You also created your first lambda function to master F#'s one line function syntax. You've learned enough to be dangerous, and in the next lesson, you'll build on this knowledge to solve a more complicated problem.
