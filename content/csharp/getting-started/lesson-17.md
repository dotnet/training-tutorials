# Introducing LINQ
by [Eric Fleming](http://deviq.com/me/eric-fleming)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## What is LINQ?
Language-Integrated Query, or LINQ, is a way to query a set of data with the use of *extension methods*. These extension methods can only be accessed by adding the `using System.Linq;` statement. In the following examples, you'll see how to use LINQ on a ``List`` of `Person` objects. The following material builds upon the [Working with Arrays and Collections](lesson-10.md) lesson and the [Extension Methods](lesson-12.md#extension-methods) section of the [Defining and Calling Methods](lesson-12.md) lesson. 

As you follow along in these examples, use this `List<Person>` collection and `Person` class:

```c#
public class Program
{
    public static void Main(string[] args)
    {
        var people = new List<Person>();

        people.Add(new Person { FirstName = "Eric", LastName = "Fleming", Occupation = "Dev", Age = 24 });
        people.Add(new Person { FirstName = "Steve", LastName = "Smith", Occupation = "Manager", Age = 40 });
        people.Add(new Person { FirstName = "Brendan", LastName = "Enrick", Occupation = "Dev", Age = 30 });
        people.Add(new Person { FirstName = "Jane", LastName = "Doe", Occupation = "Dev", Age = 35 });
        people.Add(new Person { FirstName = "Samantha", LastName = "Jones", Occupation = "Dev", Age = 24 });
        
        // Write your code here
    }
}
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Occupation { get; set; }
    public int Age { get; set; }
}
```

## Finding Items in Collections
At some point, you're going to need to find items in a collection that meet specific criteria. LINQ provides you with extension methods like `Where`, `Skip`, and `Take` to make this easy.

###Where
The `Where` extension method is the most commonly used. It can be used to filter elements from a collection based on certain criteria. For example, say you wanted to filter the list of people based on whether or not they are above the age of 30, it would look something like this:

```c#
var peopleOverTheAgeOf30 = people.Where(x => x.Age > 30); //There will be two Persons in this variable: the "Steve" Person and the "Jane" Person
``` 

As you can see, the `Where` method takes in a lambda expression as a *predicate* (a statement evaluating to either `true` or `false`) to be applied to each item in the list of people. In this scenario, every person's `Age` property is checked to see if it is greater than 30. If the result of the expression is `true`, the current item is added to an an object of type `IEnumerable<T>`. This new `IEnumerable<T>` will be an "IEnumerable of Person" `IEnumerable<Person>`, because the `List` it came from was made of `Person` objects.

###Skip
Sometimes you will want to ignore the first items in a collection and include only what remains. You can do this by using the aptly-named `Skip` method.

```c#
IEnumerable<Person> afterTwo = people.Skip(2); //Will ignore Eric and Steve in the list of people
```

###Take
The opposite of the `Skip` extension method is the `Take` extension method. `Take` will return the first items in the collection including a number of items equal to the number passed as an argument to the method.

```c#
IEnumerable<Person> takeTwo = people.Take(2); //Will only return Eric and Steve from the list of people 
```

## Changing Objects in Collections
You may want to retrieve specific properties of an object, and either return an `IEnumerable<T>` where T is the source type or create and return an entirely new object based on multiple properties. LINQ provides you with the `Select` extension method to make this easy. In the below example, we are going to return the first names of all the people in the `people` list as an `IEnumerable<string>`.

```c#
IEnumerable<string> listOfFirstNames = people.Select(x => x.FirstName);
```

The contents of `listOfFirstNames` will now contain the strings `"Eric", "Steve", "Brendan", "Jane", "Samantha"`.

You can also create a brand new object based on properties you select. For this example, I'll create another model `Name` and set the `First` and `Last` properties based on the `FirstName` and `LastName` properties of the `Person` model.

```c#
public class Name
{
    public string First { get; set; }
    public string Last { get; set; }
}
```

The below line is how we can create a new `Name` object for each `Person` in the `people` list.
```c#
IEnumerable<Name> listOfFirstAndLastNames = people.Select(x => new Name { First = x.FirstName, Last = x.LastName });
```

## Finding One Item in Collections

###FirstOrDefault
The `FirstOrDefault` extension method will return the first element of a set of data. If there are no elements that match your criteria, the result will be the default value type for the object. 

```c#
Person firstOrDefault = people.FirstOrDefault();
Console.WriteLine(firstOrDefault.FirstName); //Will output "Eric"
```

The `FirstOrDefault` method can also be used to filter the list to return the first element that matches your expression's criteria.

```c#
var firstThirtyYearOld1 = people.FirstOrDefault(x => x.Age == 30);
var firstThirtyYearOld2 = people.Where(x => x.Age == 30).FirstOrDefault();
Console.WriteLine(firstThirtyYearOld1.FirstName); //Will output "Brendan"
Console.WriteLine(firstThirtyYearOld2.FirstName); //Will also output "Brendan"
```

The two expressions above return the same item whether the predicate is in the `Where` or the `FirstOrDefault`. Skipping the `Where` is more concise, and can improve readability of your code.

**Note:** The "OrDefault" means, if no elements in the queried data match the expression passed into the method, the returning object will be `null`.

```c#
List<Person> emptyList = new List<Person>(); //Creating an empty list
Person willBeNull = emptyList.FirstOrDefault(); //The result of the FirstOrDefault call will be null

Person willAlsoBeNull = people.FirstOrDefault(x => x.FirstName == "John"); //The result of the FirstOfDefault call will be null

Console.WriteLine(willBeNull.FirstName); //This will cause an exception
Console.WriteLine(willAlsoBeNull.FirstName); //This will also cause an exception
```

Similarly, there is a `First` extension method that will function the same as `FirstOrDefault` but will return a `System.InvalidOperationException` if there are no elements that match your criteria.

###SingleOrDefault
The `SingleOrDefault` extension method will result in the one and only occurance of your expression. If no elements match your criteria, the default value of your type will be returned. `SingleOrDefault` functions much like `FirstOrDefault`, but if there are more than one occurance of your expression a `System.InvalidOperationException` will be thrown.

```c#
Person single = people.SingleOrDefault(x => x.FirstName == "Eric"); //Will return the Eric Person obejct
Person singleDev = people.SingleOrDefault(x => x.Occupation == "Dev"); //Will throw the System.InvalidOperationException
```

The `Single` extension method will function the same as `SingleOrDefault` but if there are no elements or a list, or there are more than one occurance of your expression, a `System.InvalidOperationException` will be thrown.

## Finding Data About Collections
There are extension methods that will allow you to determine if or how many items will satisfy an expression. 

###Count
The `Count` extension method will return the number of items in the data over which you're iterating as an `int`.

```c#
int numberOfPeopleInList = people.Count(); //Will return 5
```

This method will also allow you to pass in a predicate expression, and return the number of results as an `int`.

```c#
int peopleOverTwentyFive = people.Count(x => x.Age > 25); //Will return 3
```

###Any
The `Any` extension method will query a set of data and return a boolean value based on whether or not your criteria was met. One common way this is used is when checking if a list has any elements before performing some other action on the list.

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

## Converting Results to Collections
LINQ provides you with extension methods that will allow you to convert readonly Enumerable collections to other collection types (Lists, Arrays, Dictionarys).

### ToList
The `ToList` extension method allows you to convert an `IEnumerable<T>` to a `List<T>`, where `T` will be the same type received, and this includes the results of most other LINQ Extension Methods. When you filter a list by using the `Where` extension method, the result is an `IEnumerable<T>` which is readonly and  cannot be modified. What if you need to modify the results of your query? This is where the `ToList` extension method comes in to play.

```c#
List<Person> listOfDevs = people.Where(x => x.Occupation == "Dev").ToList(); //This will return a List<Person>
```

### ToArray
Similar to `ToList`, there is also a `ToArray` extension method that will result in an array rather than a list.

```c#
Person[] arrayOfDevs = people.Where(x => x.Occupation == "Dev").ToArray(); //This will return a Person[] array
```

Along with `ToList` and `ToArray`, a less commonly used extension method is `ToDictionary`, which will result in a `Dictionary<TKey, TValue>`.

## Next Steps
For the next steps I would encourage you to first create a model, like our `Person` class we used in this lesson. Then, try some of the extension methods we described above. Query the results and output the results to the console so you can confirm everything is as expected.

```c#
var devs = people.Where(x => x.Occupation == "Dev");
foreach(var dev in devs)
{
    Console.WriteLine(dev.FirstName);
}
```
