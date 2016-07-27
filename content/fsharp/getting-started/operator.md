# Operator Definition and Overloading

by [Microsoft Research](https://www.microsoft.com/en-us/research/)

Operator overloading is a feature available in many programming languages. In F# not only can you overload existing operators, you can actually define new ones. The ability to define infix operators provides you with a greater degree of control over the syntax of your programs, making it possible to create expressions that are better suited to the problem at hand.
A brilliant example of this is the ubiquitous F# forward pipe operator (indicated as |>), which is not native in the language, but is instead defined as part of the F# library. The forward pipe operator is defined as follows:

## The Forward Pipe Operator Definition

let (|>) x f = f x
Load
The operator is infix and simply indicates that the argument comes before the function that should receive it. Note that in F# all functions take one argument (as discussed in the previous lesson on currying), thus the operator can be applied to all functions.
F# defines a grammar for operators that allows you to combine a sequence of symbols to form a new operator. For instance, + <|> are infix operators and !~ is a prefix operator.
Although the definition of new operators can lead to more concise code, defining new operators should be undertaken with care since this practice has the potential to obfuscate code. An example of this can be seen in regular expressions, which can be effective when paired with a lightweight syntax (as also seen in Perl). Let's see how to define operators to simplify the use of regular expressions in F#:

## Regular Expression Operators

open System.Text.RegularExpressions

//test match
let (^?) a b = Regex.IsMatch(a, b)
// perform a match and returns match info
let (^!) a b = Regex.Match(a, b)
// Query the number of matches
let (!@) (a:Match) = a.Groups.Count - 1
// Access the nth match (1 based, equivalent of $1..$n notation)
let (^@) (a:Match) (b:int) = a.Groups.[b].Value
Load
Now let's test these operators:

## Test of Operators

let input = "F# 3.0 is a very cool programming language"
if input ^? @"F# [\d\.]+" then
  let m = input ^! @"F# ([\d\.]+)"
  printfn "matched %d groups and the F# version is %s"
     !@m (m^@1)
Load
Two things are missing from the typical Perl usage: a syntax for substitution and the ability to specify regex options such as ignore case (typically indicated by letters like i, s, and g). We can easily extend our definition to allow specifying a string with options:
Revised Regular Expression Operators
open System.Text.RegularExpressions

let string2opt (s:string) =
    let mutable ret = RegexOptions.ECMAScript
    for c in s do
        match c with
        | 's' -> ret <- ret ||| RegexOptions.Singleline
        | 'm' -> ret <- ret ||| RegexOptions.Multiline
        | 'i' -> ret <- ret ||| RegexOptions.IgnoreCase
        | _ -> ()
    ret

// test match
let (^?) a (b, c) = Regex.IsMatch(a, b, string2opt c)
// perform a match and returns match info
let (^!) a (b, c) = Regex.Match(a, b, string2opt c)
// Query the number of matches
let (!@) (a:Match) = a.Groups.Count - 1
// Access the nth match (1 based, equivalent of $1..$n notation)
let (^@) (a:Match) (b:int) = a.Groups.[b].Value

let input = "F# 3.0 is a very cool programming language"
if input ^? (@"F# [\d\.]+", "i") then // ignore case
    let m = input ^! (@"F# ([\d\.]+)", "")
    printfn "matched %d groups and the F# version is %s" !@m (m^@1)
Load
The ability to pass a tuple as a single argument allows the introduction of extra arguments, so the substitution operators can be defined as follows:
The Replace Operator Definition
let (^~) a (b, c:string, d) = Regex.Replace(a, b, c, string2opt d)
printfn "%s" (input ^~ ("very", "super", ""))
Load
The syntax we used to introduce new operators can be also be used to redefine the existing ones, although once redefined these cannot be overloaded. Therefore the redefined operator must handle all possible values. For instance, if you develop a new type Point representing a point in the 2D Cartesian space, you cannot simply define the operator as + since it would inhibit the sum among any other type including numbers!
Operator overloading is possible only for some operators (see Operator Overloading (F#) for more information), and it must be done within the type definition. For instance here is an implementation of the Point type which defines the sum operator among points without disrupting any other types:

## Overloading an Operator Within a Type

type Point(x:float, y:float) =
  member this.X = x
  member this.Y = y

  static member (+) (p1:Point, p2:Point) =
      new Point(p1.X + p2.X, p1.Y+p2.Y)

let p1, p2 = new Point(0., 1.), new Point(1.,1.)
p1 + p2
Load
Operator definition and overloading helps you to define programming idioms that allow more concise combinations than those which are possible through the function application. However you must take care not to abuse this mechanism since operators may result in code that is more difficult to read. Therefore you should use it only when you spot a very general pattern that could benefit from concise notation.