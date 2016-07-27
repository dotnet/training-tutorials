# Bindings and Values

by [Microsoft Research](https://www.microsoft.com/en-us/research/)

With Try F#, writing your first line of F# code is much easier than it is with many other languages. To get started, try typing the following code into the Script Window and pressing the *run* button or, *ctrl+enter*. If you don't feel like typing, you can also press the *load* and *run* button to execute the code.

```{.snippet}
let lucky = 3 + 4
```
```{.REPL}
let lucky = 3 + 4
```

Nice job! You have just executed your first line of F# code. If everything went smoothly, the Output Window should have printed `val lucky : int = 7`. Behind the scenes, Try F# executed your simple program using what is known as a Read Evaluate Print Loop (REPL). If you've done any programming in a language with a REPL, you know that they are handy tools to have for exploratory programming. The REPL will remember the result of each command that you give it, so you can build up programs incrementally instead of having to run them all at once. Execute the following code to see how it works. You can execute just one line of code by highlighting it in the Script Window and clicking the *run* button or pressing *Ctrl+Enter*.

```{.snippet}
let unlucky = lucky + 6
```
```{.REPL}
let lucky = 3 + 4

let unlucky = lucky + 6
```

You should see `val unlucky : int = 13` in the Output Window. The REPL used the value of lucky from the first statement that you entered to calculate your new result.

Don't be afraid to use the REPL to execute code that you're curious about as you go through this tutorial. The samples are designed to guide you through the basics of the language, but you can learn much more by exploring on your own. If you ever get yourself into a state that you want to get out of, you can reset the session by clicking in the Script Window and pressing *Ctrl+Shift+R* on Windows, *Command+R* on OS X. You can also reload your browser to restart your session. Note that reloading will also clear the Script Window, so be careful to copy your script somewhere else if you don't want to lose your work.

## Values and Bindings

Now that you're a master of executing F# code, let's take a closer look at the results of the statements that you executed. You can intuitively observe that the REPL allowed you to do basic math, but there are also some less obvious things to note. First and foremost is the `let` keyword. `let` is used to bind a name to a value in F#. If you've programmed in other languages, you might expect that you can change the value of this binding, but you'd be wrong. Execute the following code to see what happens when you try to modify a `let` binding.

```{.snippet}
let duplicated = "original value"
let duplicated = "new value"
```
```{.REPL}
let duplicated = "original value"
let duplicated = "new value"
```

You get a duplicate definition error because `let` bindings are *immutable* in F#. It is similar to declaring a variable `const` or `final` in other languages. Immutable bindings make your code simpler and easier to reason about, and they should be natural to you if you're coming from a math background. However, if you've programmed in other languages, you might be wondering if you can create a binding that you can modify. F# does allow for mutable bindings, but you must be explicit about declaring them.

```{.snippet}
let mutable modifiable = "original value"
modifiable <- "new value"
```
```{.REPL}
let mutable modifiable = "original value"
modifiable <- "new value"
```

The `mutable` keyword denotes that `modifiable` is a mutable binding, and the assignment operator `(<-)` is used to update its value to `"new value"`. The mutable keyword can come in handy from time to time, but you should use normal `let` bindings when possible. Not only are immutable bindings idiomatic in F#, they also make your code easier to read, write, and maintain.
Shadowing
As you build up a program in the REPL, you may want to overwrite a let binding that you previously made. You can do that using a technique called shadowing. Execute the following two code snippets separately for an example of how to do that.

```{.snippet}
let shadowed = "original value"
```
```{.REPL}
let shadowed = "original value"
```

```{.snippet}
let shadowed = "new value"
```
```{.REPL}
let shadowed = "new value"
```

By running the two snippets separately, you avoided the duplicate definition error. It might look like the second snippet changed the original shadowed binding, but that's not quite what happened. Instead, the second statement created a new binding that shadowed, or covered up, the original. It's a subtle difference, so don't worry if you don't fully understand it at this point. If you'd like, you can try updating the shadowed variable with the assignment operator to see that it is still an immutable binding.

## Types

Between the REPL and the lightweight let syntax, it's easy to mistake F# for a dynamically typed language like Ruby or Python. However, F# is actually a statically typed language like C#, Java, and C++. Look closely at the output of the following snippet.

```{.snippet}
let anInt = 10
let aFloat = 20.0
let aString = "I'm a string!"
```
```{.REPL}
let anInt = 10
let aFloat = 20.0
let aString = "I'm a string!"
```

The Output Window shows the types of each binding along with its value. For example, `anInt : int` shows that `anInt` is of type `int`. As you might expect, ints represent integer values, floats represent floating point numbers, and strings represent text. F#'s type system helps you to avoid errors as your programs grow in complexity by making sure that you don't try to perform operations on values that don't support them. However, unlike most statically typed languages, F# goes through great lengths to infer the types of values for you. With F#, you get the readability benefits of a dynamic language with the robustness of a statically typed language.

## Printing Messages

Sometimes you may want to print a message to users of your programs. In F#, you can do that with `printfn`.

```{.snippet}
printfn "hello world from Try F#!"
```
```{.REPL}
printfn "hello world from Try F#!"
```

The above code snippet prints a beautiful message to the Output Window. You can also use `printfn` statements to debug your programs by printing values. You do that using special formatting characters.

```{.snippet}
printfn "The answer is %d" 42
```
```{.snippet}
printfn "The answer is %d" 42
```

`%d` tells `printfn` to insert an integer value into the output string, so F# will print "The answer is 42" in the Output Window.
Console logging is common to almost every programming language, but F# has a feature that you might not expect. In F#, your formatting strings are type checked against their arguments to avoid programming mistakes. For example, the following will result in an error.

```{.snippet}
printfn "The answer is %d" "not an integer!"
```
```{.snippet}
printfn "The answer is %d" "not an integer!"
```

"not an integer" violates the formatting string, so F# helpfully warns you of your mistake. For a full list of formatting options, see [this page](https://msdn.microsoft.com/visualfsharpdocs/conceptual/core.printf-module-%5bfsharp%5d).

## Summary

Good work, you've completed your first foray into the world of F#! Now you can create basic bindings and execute F# code with ease. You know about F#'s REPL, and you've seen how easy it is to execute F# directly in your browser. It may not seem like much yet, but these fundamentals form the foundation that you will build on in the next few lessons.
