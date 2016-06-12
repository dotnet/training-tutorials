# Introducing LINQ
by [Eric Fleming](http://deviq.com/me/eric-fleming)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## What is LINQ?
Language Integrated Query, or LINQ, is a way to query a set of data with the use of extension methods. These extension methods can only be accessed by adding the `System.Linq` using statement. In the following examples, you'll see how to use LINQ on a ``List`` of people. This lesson will combine ideas from [lesson 10](lesson-10.md) and [lesson 13](lesson-13.md). 

As you follow along in these examples, use this ``Person`` class.

```c#
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Occupation { get; set; }
    public int Age { get; set; }
}
```

Below is the list of people you will use throughout this lesson.

```c#
var people = new List<Person>();

people.Add(new Person { FirstName = "Eric", LastName = "Fleming", Occupation = "Dev", Age = 24 });
people.Add(new Person { FirstName = "Steve", LastName = "Smith", Occupation = "Manager", Age = 40 }); //Steve Person - how to refer to these whole objects later in article?
people.Add(new Person { FirstName = "Brendan", LastName = "Enrick", Occupation = "Dev", Age = 30 });
people.Add(new Person { FirstName = "Jane", LastName = "Doe", Occupation = "Dev", Age = 35 }); //Jane Person
people.Add(new Person { FirstName = "Samantha", LastName = "Jones", Occupation = "Dev", Age = 24 });
```

###Where
The LINQ `Where` extension method is the most commonly used. This can be used to filter elements from a collection based on certain criteria. For example, say you wanted to filter the list of people based on whether or not they are above the age of 30, it would look something like this:

```c#
var peopleOverTheAgeOf30 = people.Where(x => x.Age > 30); //There will be two Persons in this variable: the "Steve" Person and the "Jane" Person (need better way to describe this)
``` 

As you can see, the `Where` method takes in a lamda expression which is performed on each item in the list of people. In this scenario, every person's Age property is checked to see if it is greater than 30. If the result of the expression comes back as true, the current item is added to an `IEnumerable<T>`. This new `IEnumerable<T>` will be an `IEnumerable` of Person `(IEnumerable<Person>)`.

###Skip
The `Skip` extension method will return only the set of elements after a ingoring the number of elements specified in the method.

```c#
IEnumerable<Person> skipTwo = people.Skip(2); //Will ignore Eric and Steve in the list of people
```

###Take
The `Take` extension method will returns the sequential number of elements from a set of data specified in the method.

```c#
IEnumerable<Person> takeTwo = people.Take(2); //Will only return Eric and Steve from the list of people 
```

###Select
The LINQ `Select` extension method allows you to project the objects you're iterating over into your required structure, transforming them by selecting from each object. In the example below, we are going to "select" the first names of all people in the list. In using the `Select` extension method, an `IEnumerable<T>` will be the result.

```c#
IEnumerable<string> listOfFirstNames = people.Select(x => x.FirstName);
```

The contents of `listOfFirstNames` will now contain the strings `"Eric", "Steve", "Brendan", "Jane", "Samantha"`.

###FirstOrDefault
The LINQ `FirstOrDefault` extension method will return the first element of a list.

```c#
Person firstOrDefault = people.FirstOrDefault();
Console.WriteLine(firstOrDefault.FirstName); //Will output "Eric"
```

The `FirstOrDefault` method can also be used to filter the list to return the first element that matches your expression's criteria.

```c#
var firstThirtyYearOld = people.FirstOrDefault(x => x.Age == 30);
Console.WriteLine(firstThirtyYearOld.FirstName); //Will output "Brendan"
```

The above expression is the same as chaining together a `Where` and a `FirstOrDefault`. It is much more simple, as well as more performant, to pass your expression straight into the `FirstOrDefault` method from the beginning. 
```c#
var firstThirtyYearOld = people.Where(x => x.Age == 30).FirstOrDefault();
Console.WriteLine(firstThirtyYearOld.FirstName); //Will also output "Brendan"
```

One item to note is, if no elements in the queried data match the expression passed into the method, the returning object will be `null`.

```c#
List<Person> emptyList = new List<Person>(); //Creating an empty list
Person willBeNull = emptyList.FirstOrDefault(); //The result of the FirstOrDefault call will be null

Person willAlsoBeNull = people.FirstOrDefault(x => x.FirstName == "John"); //The result of the FirstOfDefault call will be null

Console.WriteLine(willBeNull.FirstName); //This will cause an exception
Console.WriteLine(willAlsoBeNull.FirstName); //This will also cause an exception
```

###Count
The LINQ `Count` extension method will return the number of items in the data over which you're iterating as an `int`.

```c#
int numberOfPeopleInList = people.Count(); //Will return 5
```

This method will also allow you to pass in an expression, and return the number of results as an `int`.

```c#
int peopleOverTwentyFive = people.Count(x => x.Age > 25); //Will return 3
```

###Any
The LINQ `Any` extension method will query a set of data and return a boolean value based on whether or not your criteria was met. One common way this is used is when checking if a list has any elements before performing some other action on the list.

```c#
bool thereArePeople = people.Any(); //This will return true
bool thereArePeople = emptyList.Any(); //This will return false
```

The above query is a better way of checking if elements exist rather than checking if the count of a list is greater than 0 like this:

```c#
if (people.Count() > 0) //This works
    //perform some action(s)
	
if (people.Any()) //This is better
    //perform some action(s)
```

###All
The `All` extension method will return a boolean based on whether or not all elements in the data satisfy your expression.

```c#
var allDevs = people.All(x => x.Occupation == "Dev"); //Will return false

var everyoneAtLeastTwentyFour = people.All(x => x.Age >= 24); //Will return true
```

###ToList
The LINQ `ToList` extension method allows you to convert an `IEnumerable<T>` to a `List<T>`, where `T` will be the same type received, and this includes the results of most other LINQ Extension Methods. When you filter a list by using the `Where` extension method, the result is an `IEnumerable<T>` which is readonly and  cannot be modified. What if you need to modify the results of your query? This is where the `ToList` extension method comes in to play.

```c#
List<Person> listOfDevs = people.Where(x => x.Occupation == "Dev").ToList(); //This will return a List<Person>
```

Similar to `ToList`, there is also a `ToArray` extension method that will result in an array rather than a list.

```c#
Person[] arrayOfDevs = people.Where(x => x.Occupation == "Dev").ToArray(); //This will return a Person[] array
```

Along with `ToList` and `ToArray`, a less commonly used extension method is `ToDictionary`, which will result in a `Dictionary<TKey, TValue>`.

## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.
