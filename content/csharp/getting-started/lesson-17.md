# Introducing LINQ
by [Eric Fleming](http://deviq.com/me/eric-fleming)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## What is LINQ?

Language Integrated Query, or LINQ, is a way to query a set of data with the use of extension methods. In order to provide you with some examples of these LINQ extension methods, I'll be using a list of people. This is combining ideas from (lesson with lists and arrays) and (lesson about classes and objects). Here is the list of people I'll be using along with the type they are modeled after.

Person class

```c#
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Occupation { get; set; }
    public int Age { get; set; }
}
```

List of People we will use throughout this lesson.

```c#
var people = new List<Person>();

people.Add(new Person { FirstName = "Eric", LastName = "Fleming", Occupation = "Dev", Age = 24 });
people.Add(new Person { FirstName = "Steve", LastName = "Smith", Occupation = "Manager", Age = 40 });
people.Add(new Person { FirstName = "Brendan", LastName = "Enrick", Occupation = "Dev", Age = 30 });
people.Add(new Person { FirstName = "Jane", LastName = "Doe", Occupation = "Dev", Age = 35 });
people.Add(new Person { FirstName = "Samantha", LastName = "Jones", Occupation = "Dev", Age = 24 });
```

###Where

The LINQ `Where` extension method is probably the most commonly used. This can be used to filter elements from a collection based on certain criteria. For example, say we wanted to filter our list of people based on whether or not they are above the age of 30, it would look something like this:

```c#
var peopleOverTheAgeOf30 = people.Where(x => x.Age > 30);
``` 

As you can see, the Where method takes in a lamda expression which is performed on each item in the list of people. In this scenario, we check every Person's Age property, to see if it is greater than 30. If the result of the expression comes back as true, the current item is added to an `IEnumerable<T>`. This new `IEnumerable<T>` will be an `IEnumerable` of people,

```c#
new IEnumerable<Person>();
```

###Select
The LINQ `Select` extension method will select certain properties on an object from the list in which you're interating over. In the example below, we are going to "select" the first names of all the people in the list. In using the `Select` extension method, an `IEnumerable<T>` will be the result.

```c#
IEnumerable<string> listOfFirstNames = people.Select(x => x.FirstName);
```

The contents of listOfFirstNames will now hold the strings "Eric", "Steve", "Brendan", "Jane", "Samantha".

###FirstOrDefault
The LINQ `FirstOrDefault` extension method will take in an expression to query the data by, and then return the **first** element of the list. In our example, calling `FirstOrDefault` will return the first the `Person` in the list.

```c#
Person firstOrDefault = people.FirstOrDefault();
```

This will result in returning `Person { FirstName = "Eric", LastName = "Fleming", Occupation = "Dev", Age = 24 });`

One item to note about FirstOrDefault is, if there is not an item that matches the criteria for FirstOrDefault, whether the list was empty, or the expression has no results then the item returned will be null. 

```c#
List<Person> emptyList = new List<Person>(); //Creating an empty list
Person willBeNull = emptyList.FirstOrDefault(); //The result of the FirstOrDefault call will be null

Person willAlsoBeNull = people.FirstOrDefault(x => x.FirstName == "John"); //The result of the FirstOfDefault call will be null

Console.WriteLine(willBeNull.FirstName); //This will cause an exception
Console.WriteLine(willAlsoBeNull.FirstName); //This will also cause an exception
```

###Any
The LINQ `Any` extension method will query a set of data and return a boolean value based on whether or not your criteria was met. One common way this is used is when checking if a list has any elements in it before performing some other action on the list.

```c#
bool thereArePeople = people.Any(); //This will return true
```

```c#
bool thereArePeople = emptyList.Any(); //This will return false
```

The above query is a better way of checking if elements exist rather than checking if the count of a list is greater than 0 like this:

```c#
if (people.Count() > 0) //Not the best way to check if a data set has items
    //perform some action(s)
	
if (people.Any()) //The better way
    //perform some action(s)
```

###ToList
The LINQ `ToList` extension method allows you to convert a result of another extension method to a list of the same type. When you filter a list by using the `Where` extension method we talked about above, the result is an `IEnumerable<T>` which is readonly and  cannot be modified. So, what if you need to modify the results of your query? This is where the `ToList` extension method comes in to play.

```c#
List<Person> listOfDevs = people.Where(x => x.Occupation == "Dev").ToList(); //This will return a List<Person>
```

###ToArray
WIP

## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.
