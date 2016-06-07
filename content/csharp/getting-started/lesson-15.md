# Encapsulation and Object-Oriented Design
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## What is Encapsulation? 

*Encapsulation* is a fundamental concept in computer science and programming. At its core, encapsulation is simply "information hiding", but that doesn't convey the reasoning behind the practice. By hiding information about the inner workings of a software construct, you force collaborators to work only with the construct's exposed interface. How work is done withing the construct is a "black box", and as a result, the inner workings are free to change without disrupting collaborators provided the external interface (and associated behavior) are not changed.

You achieve encapsulation in your object-oriented programs primarily through the use of object design and accessibility modifiers, which you learned about in the [previous lession](lesson-14.md). Your program would have no encapsulation if the entire thing resided in a single method. By breaking functionality out into separate, focused methods and classes, and controlling how these methods access their classes' state through accessibility modifiers, you can achieve good encapsulation in your program's design.

A very common example of poor encapsulation is the overuse of properties, especially for collection types. Usually, these types expose a great deal more functionality than any client code should be able to access, which can result in program bugs. Consider the following program, which prints customers and their orders:

```c#
public class Program
{
    public static void Main(string[] args)
    {
        var customer1 = new Customer() { Name = "Steve"};
        customer1.Orders.Add(new Order("123"));
        customer1.Orders.Add(new Order("234"));
        customer1.Orders.Add(new Order("345"));

        var customer2= new Customer() { Name = "Eric"};
        customer2.Orders.Add(new Order("100"));
        customer2.Orders.Add(new Order("200"));
        customer2.Orders.Add(new Order("300"));

        var customers = new List<Customer>() { customer1, customer2};

        // print customers
        var orders = new List<Order>();
        foreach (var customer in customers)
        {
            Console.WriteLine(customer.Name);
            Console.WriteLine("Orders:");
            orders = customer.Orders;
            while(orders.Count > 0)
            {
                Console.WriteLine(orders[0].OrderNumber);
                orders.RemoveAt(0);
            }
        }
        Console.WriteLine($"Customer 1 Order Count: {customer1.Orders.Count}");
        Console.WriteLine($"Customer 2 Order Count: {customer2.Orders.Count}");
    }
}

public class Customer
{
    public string Name { get; set; }
    public List<Order> Orders { get; set;} = new List<Order>();
}

public class Order 
{
    public Order(string orderNumber)
    {
        OrderNumber = orderNumber;
    }
    public string OrderNumber {get; set;}
}
```

Notice that in this example, the technique used to print the orders is a ``while`` loop that throws away each record as it prints it. This is an implementation detail, and if the collection this loop was working with were properly encapsulated, it wouldn't cause any issues. Unfortunately, even though a locally scoped ``orders`` variable is used to represent the collection, the calls to ``RemoveAt`` are actually removing records from the underlying ``Customer`` object. At the end of the program, both customers have 0 orders.

There are a variety of ways this can be addressed, the simplest of which is to change the ``while`` loop to a ``foreach``, but the underlying problem is that ``Customer`` isn't encapsulating its ``Orders`` property in any way. Even if it didn't allow other classes to set the property, the ``List<Order>`` type it exposes is itself breaking encapsulation, and allowing collaborators to arbitrarily ``Remove`` or even ``Clear`` the contents of the collection.

## Using Encapsulation To Constrain Operations

The classes you create to model the problem you're trying to solve should be designed so that they can collaborate with one another without relying on implementation details. This produces a loosely-coupled, modular design that is easier to maintain and less likely to have bugs. Each class can be responsible for its own state, and can control how that state is changed so that it can maintain certain business rules. In the example above, there are no business rules protecting the ``Customer`` class against inadvertent changes to its ``Order`` history. Presumably, once a customer's order has been completed, the history of that order should never change. Certainly, it should be trivial for the application to wipe out the history completely.

When you create a software model, which is what your classes are, you should constrain the ways in which the components of that model can interact. By default, certain programming constructs may offer a wealth of functionality, but part of designing your program is restricting those operations to just the ones that should be available within a given context. In the case of printing out customers and their orders, there's no reason why that should include the ability to remove orders, for example. Look at the example program above and consider what operations are actually needed for the program to print the necessary information before continuing.

### Read Only Properties

One way in which we can improve encapsulation is through the use of read-only properties. These help to protect primitive types (int, string, DateTime, etc) from unwanted direct manipulation by collaborators. Unforunately, collection types frequently expose methods for manipulating their contents even if the collection type itself is read only.

Updating ``Customer`` and ``Order`` to use read only properties for their string properties improves their encapsulation, though, and the ``set`` can safely be removed from the ``Orders`` property, as well:

```c#
public class Customer
{
    public Customer(string name)
    {
        Name = name;
    }
    public string Name { get; }
    public List<Order> Orders { get;} = new List<Order>();
}

public class Order 
{
    public Order(string orderNumber)
    {
        OrderNumber = orderNumber;
    }
    public string OrderNumber {get; }
}
```

With this change, customers must have a name when they are created, which is a reasonable expectation in most cases.

### Encapsulating Collections

When it comes to collections, sometimes the best way to protect them is to only expose a copy of the collection's contents. This can sometimes have performance implications, so you must be careful with such design decisions. Using this approach a ``private`` collection is used by the object internally, and the property it exposes simply is a copy of this private collection:

```c#
private List<Order> _orders = new List<Order>();
public List<Order> Orders { 
    get
    {
        return new List<Order>(_orders);
    }
}
```

The [``ReadOnlyCollection``](https://msdn.microsoft.com/en-us/library/ms132474(v=vs.110).aspx) type can be used as well, though it adds some additional complexity. This helps ensure collaborators have only read only access to the collection:

```c#
private List<Order> _orders = new List<Order>();
private ReadOnlyCollection<Order> _ordersView;
public ReadOnlyCollection<Order> Orders { 
    get
    {
        if(_ordersView == null)
        {
            _ordersView = new ReadOnlyCollection<Order>(_orders);
        }
        return _ordersView;
    }
}
```

Another approach is to expose the collection as a type with limited capabilities, such as ``IEnumerable``.

```c#
private List<Order> _orders = new List<Order>();
public IEnumerable<Order> Orders2 {
    get
    {
        return _orders.AsEnumerable(); // in System.Linq namespace
    }
}
```

This approach is simple and effective, though it can be circumvented if the property is cast back to a list type. For example, using this approach, if the original program were to add a cast, it would still result in the underlying *private* collection being modified:

```c#
orders = (List<Order>)customer.Orders; // cast from IEnumerable to List
```

The ``ReadOnlyCollection`` approach is the safest one to use if you have collections you need to protect.

In all of these cases, in order for the original program code that adds orders to customers to work, the ``Customer`` type must expose an ``AddOrder`` method:

```c#
public void AddOrder(Order order)
{
    _orders.Add(order);
}
```

### Encapsulating Infrastructure

## Single Responsibility

## Tell, Don't Ask

## Composition over Inheritance

## Explicit Dependencies

## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.