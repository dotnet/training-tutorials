# Common Patterns and Anti-Patterns
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## Common C# Patterns and Anti-Patterns

Common approaches to solving similar problems are referred to as *design patterns*. Common approaches to solving similar problems that often end up causing more problems than they solve are called *anti-patterns* (or *antipatterns*). As you continue learning C#, there are a few of each to keep in mind.

**Note:** Learn more about [design patterns](http://deviq.com/design-patterns/) and [antipatterns](http://deviq.com/antipatterns/).

In this lesson, you're going to learn about *adapter*, *factory*, *repository*, and *strategy* design patterns. You'll also learn about two related antipatterns: *Singleton* and *Static Cling*, which also relate [new is glue](http://ardalis.com/new-is-glue), which you learned about in a previous lesson.

### The Adapter Design Pattern

The goal of the Adapter design pattern is to convert one interface to another. Frequently this is to allow multiple different systems or objects to interact with one another. You may have code that needs to work simultaneously with many different systems, or needs to be able to switch between them easily (either at runtime or with a simple change in configuration).

As an example, imagine you're working on a system that can integrate with multiple payment providers. Based on the preferred provider, the system will accept payments. However, each provider uses its own API to accept payments. Let's look at the two providers you're going to start with, *Stwipe* and *PaySal*:

```c#
public class StwipeProvider
{
    public StwipeProvider(string merchantKey)
    {}

    // returns false if payment is rejected
    public bool Pay(string cardNumber, string expiration, decimal amount)
    {}
}

public class PaySalProvider
{
    // throws exception if payment is rejected
    public void ProcessPayment(string merchantId, CreditCardDetails cardInfo, decimal amount)
    {}
}
```

You expect that you're going to support additional payment providers in the future, and it's also likely that at some point these providers may change their APIs. Thus, you want to shield your code from potential breaking changes in the future. The way to achieve this is to create your own Adapter interface that you will work with, and write your own Adapter implementations for each provider. These should be the only classes in your application that reference the provider-specific code - the rest of your application should work only with your adapters.

When creating your adapter interface, you're free to make whatever design decisions will make it easiest for your application to work with it. Of course, the closer you make it to at least one of the interfaces it will be adapting, the less work you'll need to do when writing (at least one of) the adapter implementations. Note that the ``PaySalProvider`` interface accepts a ``CreditCardDetails`` type. This is a custom type defined specifically in the ``PaySalProvider`` package. Your adapter will need to avoid using any provider-specific types in its interface.

```c#
public interface IPaymentProcessorAdapter
{
    // returns false if payment is rejected
    bool ProcessPayment(string merchantId, string cardNumber, string expiration, 
                        decimal amount);
}

public StwipeAdapter : IPaymentProcessorAdapter
{
    public void ProcessPayment(string merchantId, string cardNumber, string expiration, 
                        decimal amount)
    {
        var provider = new StwipeProvider(merchantId);
        return provider.Pay(cardNumber, expiration, amount);
    }
}

public PaySalAdapter : IPaymentProcessorAdapter
{
    public void ProcessPayment(string merchantId, 
        string cardNumber, string expiration, 
        decimal amount)
    {
        var provider = new PaySalProvider();
        try
        {
            var cardInfo = new CreditCardDetails(cardNumber, expiration);
            provider.Pay(cardNumber, expiration, amount);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
```

With this design in place, you could accept an IPaymentProcessorAdapter anywhere you needed to process a payment. You could set it as a property on a user or storefront object to specify its payment preference. And you could add many additional adapters as your support for other payment providers continued to grow, all without any of the rest of your application having to deal with these updates directly.

### The Factory Design Pattern

The factory design pattern is actually a combination of a few different patterns, but with the same intent: to simplify construction of object instances and encapsulate decisions about which specific type to instantiate. For building on the example above, let's say the payment providers are used by different stores that all run on your platform. Each store must choose a particular payment provider they're going to use from the ones the platform supports. This is stored as part of the ``Store``'s properties:

```c#
public class Store
{
    public string Name { get; set; }
    public string PaymentProvider { get; set; }
    public string MerchantId { get; set; }
}
```

When someone buys something from the store, there's a method somewhere that is responsible for processing the payment. It might belong to the ``Store`` class, or it might live somewhere else, but regardless it's going to need to create the appropriate adapter class in order to make the payment. The method might look something like this:

```c#
public void ProcessCard(string cardNumber, string expiration, decimal amount)
{
    IPaymentProcessorAdapter adapter = null;
    if(PaymentProvider = "Stwipe")
    {
        adapter = new StwipeAdapter();
    }
    else if(PaymentProvider = "PaySal")
    {
        adapter = new PaySalAdapter();
    } else {
        throw new InvalidPaymentProviderException(PaymentProvider);
    }
    adapter.Pay(MerchantId, cardNumber, expiration, amount);
}
```

As you can see, the bulk of this method is concerned with determining which specific adapter to instantiate, not the logic of processing the card. This construction logic can be moved into its own *factory method*:

```c#
public void ProcessCard(string cardNumber, string expiration, decimal amount)
{
    IPaymentProcessorAdapter adapter = GetPaymentAdapter();

    adapter.Pay(MerchantId, cardNumber, expiration, amount);
}

private IPaymentProcessorAdapter GetPaymentAdapter()
{
    if(PaymentProvider = "Stwipe")
    {
        adapter = new StwipeAdapter();
    }
    else if(PaymentProvider = "PaySal")
    {
        adapter = new PaySalAdapter();
    } else {
        throw new InvalidPaymentProviderException(PaymentProvider);
    }
}
```

You can take this further, and move the logic into its own type, so that the class doing the payment processing doesn't have the added responsibility of determining how to create the adapter. Typically this type will use the "Factory" suffix in its name:

```c#
public interface IPaymentProcessorAdapterFactory
{
    IPaymentProcessorAdapter Create(string providerName);
}

public PaymentProcessorAdapterFactory : IPaymentProcessorAdapterFactory
{
    public IPaymentProcessorAdapter Create(string providerName)
    {
        if(PaymentProvider = "Stwipe")
        {
            adapter = new StwipeAdapter();
        }
        else if(PaymentProvider = "PaySal")
        {
            adapter = new PaySalAdapter();
        } else {
            throw new InvalidPaymentProviderException(PaymentProvider);
        }
    }
}
```

Now the responsibility for creating the correct adapter (which could require much more complexity than we see here, and which might have far more than two valid options) has been moved into its own class. As it stands now, the ``ProcessCard`` method would probably need to directly instantiate the factory class in order to use it, but you'll see below how the *Strategy* pattern can address this.

### The Repository Design Pattern

Of course, when an order is placed and payment succeed, the order needs to be stored somewhere. The ``Store`` itself could include logic for connecting to a database and executing commands against it to perform this logic, but once more that's an additional responsibility the store shouldn't take on for itself. Rather, some other class should have the responsibility of persistence (of ``Orders`` in this case). There is a design pattern for encapsulating persistence operations behind a class with a collection-like interface, and it is called the *Repository pattern*.

The goal of this pattern is to make working with external persistence mechanisms, like databases, as simple for the application code as working with a built-in collection would be. Thus, when you define an interface for a repository, it will typically accept parameters like *Add* and *Remove* and *Get*, but usually this similarity stops short of having the repository type implement *ICollection* or *IEnumerable* directly.

Connecting to different data stores is outside the scope of this tutorial, but the details aren't important. Consider the following block of code:

```c#
public void CompleteOrder()
{
    // verify order
    
    // process card
    
    // create order object
    
    // connect to database
    // convert order object into database statement(s)
    // execute database commands
    
    // send customer confirmation
}
```

You don't want the low-level details of connecting to a database to be in the middle of a high level business method on completing an order. At the very least, those details should be extracted into their own method. However, there are likely to be many similar such methods, all concerned with the specifics of data access. It's far more cohesive to put these methods on classes whose specific responsibility is persistence. These are called repositories.

```c#
public interface IOrderRepository 
{
    void Add(Order order);
}
public class DbOrderRepository : IOrderRepository
{
    public void Add(Order order);
}
```

Now, the ``CompleteOrder`` method can simply refer to an instance of ``IOrderRepository``:

```c#
public void CompleteOrder()
{
    // other logic omitted
    orderRepository.Add(order);
}
```

Or course, in addition to adding records, your applications will probably need to read them, update them, and delete them. These operations (Create, Read, Update, Delete) are often referred to by the acronym *CRUD*. Repositories are where you should typically implement your CRUD logic in your applications. Add additional methods to the initial interface as your requirements demand them. A more complete ``IOrderRepository`` interface might look like this:

```c#
public interface IOrderRepository
{
    Order GetById(int id);
    List<Order> List();
    void Add(Order order);
    void Update (Order order);
    void Delete (Order order);
}
```

### The Strategy Design Pattern

### The Singleton Antipattern

### The Static Cling Antipattern

## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.