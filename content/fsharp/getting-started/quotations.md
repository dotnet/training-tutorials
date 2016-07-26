# Quotations: Leveraging the Language Parser to Define Your Own Language
by [Microsoft Research](https://www.microsoft.com/en-us/research/)

In a programmer's life (perhaps also in yours) the opportunity arises to develop a program that manipulates another program. Usually this is done for the purpose of automating some tedious task at hand in the normal development lifecycle. Eventually you'll end up writing an interpreter of some language where the ability to define a special syntax provides more freedom and flexibility than a library from an existing programming language.
F# allows you to quote a fragment of program that returns a value representing it:
Example of Quotation
let q = <@ 2 + 2 * 3 @>
Load
F# interactive shows a tree representing the simple expression enclosed between <@ and @>, moreover the type of the whole expression is Quotation.Expr<int> which of course represents an expression whose returned value is of type int. Here are a few more examples:
More Quotation Examples
let w = <@ while true do printfn "It never ends..." @>
let f = <@ fun x y -> x + y @>
Load
Go ahead and try it for yourself to see what you can quote and what you cannot. Quotations render a very valuable service for a number of occasions, though there is a significant limitation on this mechanism: there is no evaluation function for quotations. Using F# quotations allows you to piggyback on the F# compiler to get a code fragment and type checked; it is up to you to decide what to do with the final quotation tree representing the code.
With F# 3.0 there is a notable exception to this fact: you can transform quotations into executable code within a type provider, an F# module capable of interacting with the compiler at compile time to generate specialized code. Type providers are not in the scope of this lesson, but you can think of them as a restricted form of quotations evaluation.
You may wonder why the mechanism has been introduced before type providers, but many applications are made possible thanks to this ability. A significant example of this is a cross-compiler (F# to JavaScript) used by Websharper to allow the development of web applications using F# to describe both server side and client side computations. Moreover, F# PowerPack comes with a quotation evaluator that allows you to evaluate these expressions.
In this lesson we'll learn how to navigate a quotation tree and synthesize a new quotation tree by combining its fragments. Let's start with simple example that evaluates quotations tree made only of additions and multiplications.
Simple Interpreter of Expressions
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns
open Microsoft.FSharp.Quotations.DerivedPatterns

let rec evalExpr q =
    match q with
    | SpecificCall <@ (+) @> (_, _, [a; b]) -> 
        let l, r = evalExpr a, evalExpr b
        l + r
    | SpecificCall <@ (*) @> (_, _, [a; b]) -> 
        let l, r = evalExpr a, evalExpr b
        l * r
    | Value (v, t) ->
        if t = typeof<int> then v :?> int 
            else failwith "Only integer values supported"
    | _  -> failwith "Unsupported expression"

evalExpr <@ 22 + 2 * 22 + 45 @>
Load
The F# libraries provide a set of active patterns that can help to effectively match quotation tree nodes. As you can see from the output of evaluating an expression involving additions and multiplications, operands map to function applications with special names such as op_Addition.
Our simple evaluation function recursively traverse the tree by recognizing additions and multiplications and propagates values from laves to the root obtaining the result. One of the useful things of quotations is that you can splice a quotation inside another one as is in the following example:
Splicing Example
let e = if System.DateTime.Today.Day % 2 = 0 then 
            <@ + 1 @> 
        else 
            <@ - 1 @>
let resf = <@ fun x -> %e + x @>
Load
The resulting function resf is the increment or decrement function depending on the day of the month.
Splicing is quite natural to get, imagine inserting a quoted expression in the placeholder removing the quotes. F# type checks splicing, if you try to splice an expression with a type incompatible with the splicing site an error is risen. As a final example we present a classic in program specialization literature, how to specialize the pow function if the exponent is known. Here are two possible algorithms for computing the integer power function:
Different Algorithms for Computing the Integer Power Function
let rec simplepow x y =
    if y = 0 then 1
    else x * (simplepow x (y - 1))

let rec pow x y =
    if y = 0 then 1
    elif y % 2 = 0 then
        let v = pow x (y / 2)
        v * v
    else
        x * (pow x (y - 1))

seq { 1..16 } |> Seq.iter 
    (fun v -> printfn "2^%d = %d" v (pow 2 v))
Load
The simplepow function simply performs the iterative multiplication y times of x to compute xy. pow performs a smarter computation that consists reduces to square and multiplication operations; the idea is exemplified by the following expansion:
x10 = (x5)2 = (x*(x4))2 = (x*(x2)2)2
In practice, you factorize the exponent to get a sequence of multiplication and square operations. This algorithm requires the number of steps equal to the number of bits in the exponent, and is much more efficient than simplepow. However, if you always need to compute pow with the given exponent y, you may want to generate the actual multiplication sequence and square for it.
The following mimics the pow algorithm for generating the exact sequence of square and multiplication operations for a given exponent:
Generating the Specialized pow for a Given Exponent
open Microsoft.FSharp.Quotations

let sqr e = <@ let v = %e in v * v @>
let mul x e = <@ %x * %e @>

let rec genpow y x =
    if y = 0 then <@ 1 @>
    elif y % 2 = 0 then sqr (genpow (y / 2) x)
    else mul x (genpow (y - 1) x)

let x = new Var("x", typeof<int>)

let specialpow y = 
  Expr.Lambda(x, genpow y (Expr.Cast<int>((Expr.Var(x)))))
Load
In this example we generated the body using splicing and a recursive function genpow mimicking the pow function of the previous example. The x argument of genpow is of type Expr<int> but we would like to generate a function that raises its argument to y.
To perform this final task we need a little bit of explicit manipulation because we need to introduce a name used as an argument of our generated function and in the body. To do this we will create a value of type Var representing our variable and then we explicitly build a Lambda node (corresponding to fun x -> ...) indicating the same variable we pass to genpow.
One final but important consideration should be made about the last argument of the genpow call which may look magic. F# quotations come in two flavors, typed and untyped. So far we have used the first kind and every node is tagged with an argument type (our expressions were of type Expr<int> not just Expr). It is possible (and sometimes useful) to avoid types by using <@@ ... @@> quotes to get untyped expressions. You can also splice using %%e instead of %e.
In our example, Expr.Var(x) returns an untyped expression (with type Expr) but our genpow needs an Expr<int>, which explains the cast using the Cast function before passing the variable to the function.
Quotations are generally intuitive, as proven by countless frameworks such as PHP, ASP, and many others; in F# you can leverage the compiler to obtain type-checked syntax trees that you can use for many applications. Now with type providers these are the core mechanism for generating code at compile time and extending the compiler itself!