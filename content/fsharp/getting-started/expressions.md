# Computation Expressions

by [Microsoft Research](https://www.microsoft.com/en-us/research/)

Now that we have learned how to customize language syntax by means of currying, operator definition, and active patterns, we can grasp the basics of a powerful mechanism known as computation expressions. You can use computation expressions to alter the standard evaluation rule of the language and define sub-languages that model certain programming patterns.

The goal of this lesson is to give you an idea of the inner workings of computation expressions.

As we did for operators, let's examine the usage of computation expressions to find a construct that is not native to the language, but instead an application of a mechanism of the language.

Computation expressions are the basis for seq, async and query, three important features of F#. Sequences (seq) generalize the notion of a stream of values and are ubiquitous in F# programming, asynchronous workflows (async) offer support for asynchronous programming patterns, and language-integrated query (query) allow for interaction with data sources.

The seq active workflow is straightforward to use:

## An infinite (up to maxint) list of numbers

```fsharp
let N = seq {
    let n = ref 0
    while true do
        yield !n
        n := !n + 1
}
N |> Seq.take(10) |> Seq.iter
    (fun v -> printfn "%d" v)
```

The sequence N enumerates all integer numbers from 0 up to the maximum integer value for type int; however the program neither hangs nor allocates a large amount of memory since the computation of the sequence is lazy: elements are only computed as they are requested.

The behavior of this code is curious indeed, since if you try to execute the code inside the curly braces (having replaced yield !n with a print statement) the program will print billions of results and wind up in a gigantic loop.

How is it possible for the sequence syntax to alter the semantics of the language so much? This is the core of the computation expressions in F#. The F# compiler rewrites (de-sugars) computation expressions before compiling, transforming the code within curly braces into an expression. For instance, the previous sequence is de-sugared to:

## Example of De-sugaring of a Computation Expression

```fsharp
let n = ref 0
seq.While ((fun () -> true),
    seq.Delay (fun () ->
        seq.Combine(seq.Yield(!n),
            seq.Delay(fun() -> n := !n + 1))))
```

This example, although not meant to run here, gives us an idea of how a computation expression is de-sugared by F#. Now imagine being able to define your own version of seq, an object capable of being called by the compiler to redefine what a particular computational expression does.

Computation expressions are usually puzzling because you need to determine how de-sugared methods interact to obtain the desired result. In general, you need this machinery whenever you have two value domains and you want a nice syntax to jump from one to the other. For sequences the goal is to work with single elements, even if building the sequence is the overarching context.

Let's consider a very simple example where we want to make some projections on the likelihood of our being alive after a number of years. Of course we could do it without computation expressions, but we would like to have a magic construct performing boundary checks for a range of years compatible with a human lifetime. To address this fundamental problem we first define a new computation expression (age) that allows combining age-labeled numbers and automatically checks to see whether boundaries are preserved. The computation expression is introduced by a "builder" type which defines the methods called by the de-sugared form of the expression (a complete list of de-sugared constructs is available [here](https://msdn.microsoft.com/visualfsharpdocs/conceptual/computation-expressions-%5bfsharp%5d)).

At minimum you need to implement `Bind`, `Delay` and `Return`. The following example demonstrates creating an instance of a builder type:

## The Age Computation Expression Builder

```fsharp
type Age =
| PossiblyAlive of int
| NotAlive

type AgeBuilder() =
    member this.Bind(x, f) =
        match x with
        | PossiblyAlive(x) when x >= 0 && x <= 120 -> f(x)
        | _ -> NotAlive
    member this.Delay(f) = f()
    member this.Return(x) = PossiblyAlive x

let age = new AgeBuilder()

let willBeThere a y =
  age {
    let! current = PossiblyAlive a
    let! future = PossiblyAlive (y + a)

    return future
  }
willBeThere 38 150
```

If you play with the `willBeThere` function, you'll find out that you will only be considered `PossiblyAlive` if the total age is between 0 and 120. The de-sugared form of `willBeThere` is shown here:

## De-sugared Form of willBeThere

```fsharp
let willBeThere2 a y =
  age.Delay(fun () ->
    age.Bind(PossiblyAlive a, fun current ->
      age.Bind(PossiblyAlive (y+a), fun future ->
        age.Return(future))))

willBeThere2 38 80
```

By calling the inner function with the integer value of the age (without the label), the Bind function makes it possible to move from the age domain to the int domain to perform calculations.

If you want to dig more into computation expressions, you'll have to get used to the sophisticated architecture of function calls that are generated by de-sugaring. You may also want to study monads, the theory behind computational expressions.

Computational expressions offer a unique opportunity to customize language syntax to introduce particular domains and make them easily programmable. Since it is possible to completely alter the semantics of the language, this construct should be used wisely as it can have the effect of making code more difficult to read.
