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
    public void ProcessPayment(string merchantId, string cardNumber, string expiration, 
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

### The Repository Design Pattern

### The Strategy Design Pattern

### The Singleton Antipattern

### The Static Cling Antipattern

## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.