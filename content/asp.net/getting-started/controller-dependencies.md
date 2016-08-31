# Injecting Dependencies into Controllers
by [James Chambers](http://jameschambers.com)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Completed Version (Pending)] - Includes the completed versions of all MVC tutorials

## Using Dependencies in Your Controllers
Through the various lessons in this tutorial series we have talked about the responsibilities of the different types of components of - and the role that the MVC framework has in - your app. While taking a pragmatic approach will pay dividends and help you deliver software, it's also important to consider the things that will make you successful down the road in managing, maintaining and adding features to your software.

A good example of a place for this is in your controllers. When a controller is created, you may be tempted to add in the code that initializes the dependencies in the controller's constructor, which would look something like the following:

```c#

public class PersonController : Controller
{
    private PersonService _personService;

    public PersonController()
    {
        _personService = new PersonService();
    }
}
``` 

This seems harmless enough; the `PersonController` needs access to the facilities of `PersonService` and so it creates an instance when it is being initialized. However there are a couple of caveats to this approach.  What if `PersonService` now needs access to the database, and you'll need to provide it an instace of a `DbContext`?  One approach might now be to do the following:

```c#
public class PersonController : Controller
{
    private PersonService _personService;

    public PersonController()
    {
        var dbContext = new ApplicationDbContext();
        _personService = new PersonService(dbContext);
    }
}
```  

Okay so far, right? Except, now how will you test this? The controller is now essentially responsible for providing a way to connect to the database, so any unit test is now going to be difficult to implement without talking to a database. What if we need to know an environment-specific connection string?  Furthermore, if the `PersonService` is  refined to also require an `AddressVerificationService` you'll now need to update your `PersonController` constructor to properly configure an instance of it when you adjust your `PersonService`. It doesn't take long before we end up with a constructor that looks like this:

```c#
public class PersonController : Controller
{
    private PersonService _personService;

    public PersonController()
    {
        var connString = Settings.ConnectionStrings["application"];
        var endpoint = Settings.AddressVerificationServiceEndpoint;
        var dbContext = new ApplicationDbContext(connString);
        var addressService = new AddressVerificationService(endpoint);
        
        // ... and on ...
        
        _personService = new PersonService(dbContext, addressService);
    }
}
```  

This spiralling effect is why we tend to practice the [Single Responsibility Principle](https://en.wikipedia.org/wiki/Single_responsibility_principle) and avoid leaking dependencies across multiple classes: we want as few reasons as possible to have to change our code. 

Do we care if the database connection is real or not? Do we want to know in this (and every) class how to setup the database connection? Do we want the responsibility of creating the service? "Probably not," is the likely answer to all of these.

If our controller _needs_ to access the `PersonService` our only other option is to force whichever code is creating an instace of the `PersonController` to provide it to us. This is a pattern known as [Inversion of Control](https://en.wikipedia.org/wiki/Inversion_of_control), and it allows us to push the creation of dependencies outside of our controller proper.  It also gives us the beneficial side effect of _explicitly_ stating our dependencies, for no code can instantiate our class without providing them to us through our public constructor.  

## The Explicit Dependencies Principle 
Let's flip that code on its head now to see what that will look like:



Show how Controllers can follow Explicit Dependencies Principle and request their dependencies via their constructor.

## Configuring Your Application
Show how to configure services in ConfigureServices (going beyond what's in lesson 8).

## Using a Service In Your Controllers
Demonstrate how to use a configured service to perform some work for a view-based controller...

## Using a Service In Your API
...and for an API method.

## Next Steps
Note that dependencies should be small and focused on only UI concerns. Business logic should go into other classes and should be injected. This makes both controllers and business classes easier to test (see next lesson).

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.