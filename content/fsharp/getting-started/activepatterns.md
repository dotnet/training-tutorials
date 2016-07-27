# Active Patterns

by [Microsoft Research](https://www.microsoft.com/en-us/research/)

Pattern matching is a major feature of F# and once you use it you won't want to live without it. When you have a value, you can deconstruct it and bind with names in such a seamless way that you will forget all the code you would have needed in other programming languages.

Here is a simple example that looks for specific vectors in a list of triples:

## Searching in a List with Pattern Matching

```fsharp
let input = [ (1., 2., 0.); (2., 1., 1.); (3., 0., 1.) ]
let rec search lst =
  match lst with
  | (1., _, z) :: tail ->
      printfn "found x=1. and z=%f" z; search tail
  | (2., _, _) :: tail ->
      printfn "found x=2."; search tail
  | _ :: tail -> search tail
  | [] -> printfn "done."
search input
```

You can try changing the values of the input list in order observe different behaviors of the search function. What we mainly appreciate here is the ability to do four things at once:

* Unpack the list in a head and tail
* Give a sort of template to the head value (a triple whose first argument is a float)
* Bind an element of the head triple to a name (in this case z)
* Check different conditions through a set of rules

Unfortunately not all of these beautiful things can be used straight away with objects. Why? Simply because objects are .NET objects, and they follow rules that depend on the Common Language Runtime (CLR) and not just F#. Moreover the set of predicates you can express using out of the box features is not entirely satisfactory, so the language has a very powerful feature called active patterns.

The active patterns feature offers a way to customize the F# pattern matching syntax in a general way. As with all powerful mechanisms, you should use it wisely (it has been said that with great power comes great responsibility). The core idea is to consent to introduce in the pattern a label (with or without arguments) that cause the invocation of a function defined with a special syntax. This can be used for any kind of computation and to deal with any type including objects.

## The Norm Active Pattern

This example uses an active pattern to find a vector or [versor](https://en.wikipedia.org/wiki/Versor) within a given list.

```fsharp
let (|Norm|) (a:float, b:float, c:float) =
    sqrt(a*a + b*b + c*c)
let v = (1., 0., 0.)
match v with
| Norm(1.) -> printfn "Versor found!"
| Norm(n) -> printfn "Simple vector with norm %f" n
```

Sometimes you may want to classify values, for instance vector vs. versor:

## Classification with Patterns

```fsharp
let (|Vector|Versor|)
    (a:float, b:float, c:float) =
    if sqrt(a*a + b*b + c*c) = 1. then Versor
        else Vector
let v = (1., 0., 0.)
match v with
| Versor -> printfn "Versor found!"
| Vector -> printfn "Is a vector"
```

You can also define incomplete classification patterns. The following pattern matches the string itself to determine whether it is a palindrome (whether the string is the same when read left-to-right and right-to-left):

## Incomplete Classification

```fsharp
let rec isPalindrome (s:string)
    (fromidx:int) (toidx:int) =
  if s = null then false
  elif toidx - fromidx < 2 then true
  elif s.[fromidx] = s.[(toidx - 1)] then
      isPalindrome s (fromidx + 1) (toidx - 1)
  else false
let (|PALINDROME|_|) (s:string) =
    if isPalindrome s 0 s.Length then Some s
        else None
match "aba" with
| PALINDROME(v) -> printfn "The string %s is palindrome" v
| "Antonio" -> printfn "Hello Antonio"
| _ -> printfn "Not a special string!"
```

In this case, the returned value is an option addressing the problem of what should be returned in case of a non-palindrome string.

In this latter example we used an active pattern whose completion time is dependent upon the input string's length. This kind of pattern should be used with care since the user of your pattern has the right to expect that the time necessary for computing a pattern is constant as it is for standard patterns.

Active patterns are ideal if you want to define a rule-based sub-language in F#: you can define patterns to match against any kind of value and use names depending on a particular domain. F# will let you bind the value returned by your pattern as if it were a native F# type preserving the beauty of pattern matching inside the language.
