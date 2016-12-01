# Chaining Functions with the Forward Pipe Operator

by [Microsoft Research](https://www.microsoft.com/en-us/research/)

Now that you know how to perform some interesting computations in F#, you need a more sophisticated way to store the results of those computations to pass from function to function. F# provides many different ways to represent data including a full object oriented type system, but in this lesson, you'll learn about the most lightweight options.

## Record Types

One of the most convenient ways to represent data in F# is to use a record type. Record types allow you to group pieces of data together and to give each piece a name. For example, here is how you might represent a book.

```fsharp
type Book =
  { Name: string;
    AuthorName: string;
    Rating: int;
    ISBN: string }
```

This defines a data type representing a book. Instances of type Book will always have a name, author name, rating and ISBN.
Now that you've defined the structure of a book, you can create one using a lightweight syntax.

```fsharp
let expertFSharp =
  { Name = "Expert F#";
    AuthorName = "Don Syme, Adam Granicz, Antonio Cisternino";
    Rating = 5;
    ISBN = "1590598504" }
```

F# infers the type of `expertFSharp` to be `Book` based on the record fields that you use. Now that you have created the book, you can easily access information about it.

```fsharp
printfn "I give this book %d stars out of 5!"
    expertFSharp.Rating
```

Note that record bindings are immutable just like normal let bindings. In other words, the following will give you an error.

```fsharp
expertFSharp.AuthorName <- "Chris Marinos"
```

However, F# provides a nice syntax to create a new book with an updated value for one of the fields.

```fsharp
let partDeux = { expertFSharp with Name = "Expert F# 2.0" }
```

## Duplicate Label Names

Problems can arise if duplicate label names are used. For example, say a new record type VHS is introduced.

```fsharp
type VHS =
  { Name: string;
    AuthorName: string;
    Rating: string; // Videos use a different rating system.
    ISBN: string }
```

Since VHS is the last record type to be defined, subsequent record type instances which share one or more label names will be inferred as the VHS type. This will pose a problem when we attempt to define a new book.

```fsharp
let theFSharpQuizBook =
  { Name = "The F# Quiz Book";
    AuthorName = "William Flash";
    Rating = 5;
    ISBN = "1234123412" }
```

As you can see the book we just defined has been inferred to be of type VHS. Because the type of the Rating field is different for VHS, this results in an error. The problem can easily be remedied by explicitly specifying the appropriate record type. In the definition for theFSharpQuizBook, update the Rating label with an explicit definition as shown here and click run to see the change.
Book.Rating = 5;
Because we used Book.Rating instead of just Rating, F# now correctly infers the record as the Book type. Only a single label needs to be explicitly specified for F# to infer the correct type although for clarity you may consider using explicit references for all labels.

## Option Types

Record types are great for grouping structured data, but structured data doesn't always come complete with every field. That's where option types are useful. Option types represent data that may or may not exist. They come in two flavors, Some and None. Some is used when the data does exist and None is used when it doesn't. Let's update the Book type to have an optional rating.

```fsharp
type Book =
  { Name: string;
    AuthorName: string;
    Rating: int option;
    ISBN: string }
```

Book is the same as it was before, but now you aren't required to specify a rating.

```fsharp
let unratedEdition =
  { Name = "Expert F#";
    AuthorName = "Don Syme, Adam Granicz, Antonio Cisternino";
    Rating = None;
    ISBN = "1590598504" }
```

Instead of creating a Book with a value for rating, you communicated that this Book doesn't have a rating by setting it to None. If you want to specify a rating, use the Some option type.

```fsharp
let stingyReview =
  { Name = "Expert F#";
    AuthorName = "Don Syme, Adam Granicz, Antonio Cisternino";
    Rating = Some 1;
    ISBN = "1590598504" }
```

This Book has a rating and its value is 1.
Now that you've set up Book to have an optional rating, you need to check for the presence of a rating before you use it. You can do that with a pattern match statement.

```fsharp
let printRating book =
    match book.Rating with
    | Some rating ->
      printfn "I give this book %d star(s) out of 5!" rating
    | None -> printfn "I didn't review this book"
```

The match checks for the presence of the book's rating. If it is Some, it binds the value to rating and uses printfn to output the rating. If the book's rating is None, the match will fall to the second case and print a different message. Pattern matching is a powerful technique that you'll see in many places in your F# journey. This example just scratches the surface of what pattern matching can do.

## Discriminated Unions

Sometimes, your computations will yield results that don't have a uniform structure. Instead, they may contain a few different flavors of results. You can use a discriminated union to help capture this type of data. Discriminated unions represent data that can take on one of a few different types of results. The Option type itself is actually just a simple discriminated union. However, you can define your own discriminated unions to capture more complicated scenarios.

```fsharp
type PowerUp =
| FireFlower
| Mushroom
| Star
```

This discriminated union models a video game power up. You can use pattern matching to create different outcomes based on the value of a discriminated union.

```fsharp
let powerUp = FireFlower

match powerUp with
| FireFlower -> printfn "Ouch, that's hot!"
| Mushroom -> printfn "Please don't step on me..."
| Star -> printfn "Let me play some special music for you."
```

This code will print a different message depending on the value of powerUp. Go ahead and change this value to get the different messages.
You can also bundle data with each discriminated union case.

```fsharp
type MushroomColor =
| Red
| Green
| Purple

type PowerUp =
| FireFlower
| Mushroom of MushroomColor
| Star of int
```

This adds data to Mushroom and Star to capture additional information. Now, Mushroom has a color associated with it and Star has an int to reflect a duration of time. You can use this information to make a more sophisticated function to handle power ups.

```fsharp
let handlePowerUp powerUp =
    match powerUp with
    | FireFlower -> printfn "Ouch, that's hot!"
    | Mushroom color -> match color with
                        | Red -> printfn "Please don't step on me..."
                        | Green -> printfn "1UP!!!"
                        | Purple -> printfn "Sorry, about that!"
    | Star duration -> printfn "Let me play some special music for you
        for %d seconds." duration

// Test handlePowerUp.
let powerUp = Star 14
handlePowerUp powerUp
```

The new handlePowerUp method uses the additional information bundled with each discriminated union case to give a more specialized message depending on the power up.

## Summary

As your programs grow in complexity, you need better ways to model data and pass information between functions. In this lesson, you learned how to create records types, option types and discriminated unions to solve those problems. You learned that record types are great at grouping data and giving meaningful names to data members. You used option types to capture data that might not exist. Finally, you modeled data that can take one of many different structures by using a discriminated union. Each of these data types is useful on its own, but like many things in F#, their true power comes by combining them. Mastering the use of F#'s data structures empowers you to write simple code to model complicated data structures.