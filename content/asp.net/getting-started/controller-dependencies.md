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

This spiraling effect is why we tend to practice the [Single Responsibility Principle](http://deviq.com/single-responsibility-principle/) and avoid leaking dependencies across and between classes: we want as few reasons as possible to have to change our code. 

Do we care if the database connection is to a real database or not for the purpose of, say, a unit test? Do we want to know in this (and every) class how to setup the database connection? Do we want the responsibility of creating the service? "Probably not," is the likely answer to all of these.

If our controller _needs_ to access the `PersonService` our only other option is to force whichever code is creating an instance of the `PersonController` to provide it to us. This is a pattern known as [Inversion of Control](http://deviq.com/inversion-of-control/), and it allows us to push the creation of dependencies outside of our controller proper.  It also gives us the beneficial side effect of _explicitly_ stating our dependencies, for no code can instantiate our class without providing them to us through our public constructor.  This is known as the [Explicit Dependencies Principle] (http://deviq.com/explicit-dependencies-principle/).

## The Explicit Dependencies Principle 
Let's flip that code on its head now to see what that will look like, requiring our dependencies to be passed in:

```c#
public class PersonController : Controller
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }
}
``` 

That is _much_ cleaner. Now, the only thing our constructor is responsible for is explicitly declaring that it requires an instance of an object that conforms to the public contract of the `IPersonService` interface. This greatly reduces the number of reasons that might come up for us to change our class, lets other callers know what is required to instantiate our controller and paves the way for simplified testing mechanisms.

But, in the context of ASP.NET Core MVC, how are these constructor parameters provided? This is where we answer the call of the _Inversion of Control_ pattern with another: _Dependency Injection_.

## Configuring Your Application
[Dependency Injection](http://deviq.com/dependency-injection/) provides a mechanism for an application to resolve dependencies at runtime without requiring knowledge of how to do so when the application is being coded or tested. It typically requires a container of some sort - a bucket that contains all the mappings - so that it has somewhere to look as objects are being created. ASP.NET Core provides such a container, and any services you need throughout your application should be configured there during your startup process.

Now that our `PersonController` needs nothing but an `IPersonService` to be created, we have to either register an instance of that service or instruct the framework on how to instantiate one on its own. To do this we have to return to the `Startup` class and edit the the `ConfigureServices` method (which is included as part of the application template) so that it knows how to build the dependencies for us.  

```c#
public void ConfigureServices(IServiceCollection services)
{
    // Add framework services.
    services.AddSingleton<IAddressVerificationService, AddressVerificationService>();
    services.AddScoped<IPersonService, PersonService>();
    services.AddMvc();
}
```

One of the interesting things to note here is that the `IAddressVerificationService` dependency for the `PersonService` is also registered. At runtime, as the framework creates our controller, it will ask the dependency injection container for an `IPersonService`, which in turn requires an  `IAddressVerificationService`. The entire chain of dependencies is resolved for us automatically.

For each service we add to the container we have to make a decision about the lifetime of that object.  There are three types of supported lifetimes:

 - **Transient** - created every time a dependency request for this type is made. It has the implication that if a single incoming HTTP request asks for multiple instances of this type, there will be one of these objects created for each dependency.
 - **Scoped** - dependencies which can be shared throughout the HTTP request lifetime. The first dependency resolution for this type will result in an instance being created that is shared for all dependencies throughout the execution of the HTTP request.
 - **Singleton** - only one instance is created for use throughout the lifetime of the application. All resolutions of the type will be served with the same instance.

Thus, in our above `ConfigureServices` method, there will be one instance of the `AddressVerificationService` created for use throughout our entire application, and the `PersonService` will be created once for each incoming HTTP request, as required.

## Using a Service In Your Controllers
Returning to our `PersonController` we can now make use of our service in one of our actions and pass the data it provides on to our view. Remember that we get a different kind of [data injected](sending-data.md) into our actions through model binding, and here we'll take advantage of that from a route token:

```c#
public IActionResult Index(int id)
{
    var person = _personService.GetById(id);
    return View(person);
}
```

Here, the `id` comes from the route token, the `_personService` was injected into our controller, and we pass the data to the view through a controller helper method called `View`.

## Using a Service In Your API
Likewise, when we strip away the concerns we need not know about from our API controllers, we get the same benefit of being able to construct a simple response after invoking our service method.

```c#
[Route("api/person")]
public IActionResult Get()
{
    var people = _personService.GetAllPeople();
    return Ok(people);
}
```

The call to `Ok` in this case will take the collection we get from the service, serialize it as JSON and wrap it with an HTTP 200 response. Our API is able to return the anticipated text result without much code at all.

## Next Steps
This lesson built on top of these other concepts which you may wish to review:
 - [Sending Data to Controllers](sending-data.md) 
 - [Routing](routing.md)
 - [Controller Actions](controller-actions.md)

Most of what we've learned so far will prepare the response for the client as requested. Before we dive into how to [render an HTML result](views.md) as part of our processing, let's first look at how to ensure our controllers are doing what we expect of them via [unit testing](testing-aspnetcore.md).