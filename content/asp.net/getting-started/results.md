# Understanding Controllers and Actions
by [James Chambers](http://jameschambers.com)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Completed Version (Pending)] - Includes the completed versions of all MVC tutorials

## Returning Views in Your App
We can have an action in `PersonController` return a view with the following code:
```c#
public IActionResult Index()
{
    return View();
}
``` 

As [previously discussed](controllers-actions.md), the call to `View()` signals the framework to continue processing the request, and it will do so with a default convention. It expects that a view with a name corresponding to the action ("Index") will be found in a folder corresponding to the name of the controller ("Person") in the "Views" folder. In this case, `Views/Person/Index.cshtml`. 

### Note { .note }
> The default convention actually goes further to specify that if a view is not found in the view directory for the controller that it will try to find the view in the `Views/Shared/` folder as a fall-back. This works for both "action name as convention", as we see above, as well as "explicit viewname string parameter" mechanisms of specifying the view name, such as in `View("Index")`.

You can also specify the name of a view explicitly. This is handy when you have two actions that may need to use the same view, for example, a create form and an edit form. Instead of using the name "Edit" to find a view, it will instead look for a filename with a _cshtml_ extension that matches the parameter you provide. 

```c#
public IActionResult Edit()
{
    return View("EditOrCreate");
}
``` 

## Returning Data with WebAPI
The distiction between controllers that are used in HTML-based applications and those used for the purpose of serving data to other software clients has grown quite thin. The same base class can be used for both purposes, but you'll find that organizing your controllers for purpose will help to keep your app easier to maintain. 

Let's start with a simple class for our Person API:

```c#
[Route("api/person/{id?}")]
public class PersonApiController : Controller
{
    // ...
}
``` 

The class has a route on it that has `api` at its base URL and accepts an optional `id` parameter ([more about routing](routing.md)). The framework takes away some of work required to serialize your data that you wish to return, allowing you to return a simple type to your client:

```c#
public Person Get(int id)
{
    // load person, usually from a service
    var person = new Person { Id = id, FirstName = "Johhny", LastName = "B. Goode." };
    return person;                      
}
```

You can try that out in your browser (navigate to `/api/person` ) and you will see the result; however, this is a simple action that creates an object and returns it directly. There is very little chance that something would go wrong, but in a more typical scenario we'd be talking to a service, doing validation on the incoming parameters and such. What happens in the above code if the person is not found in the service? Or if there is an error connecting to the database? If the request doesn't contain a valid ID?

By returning an object like above the framework will kindly serialize our object, return it in JSON format and set the status code to HTTP 200. Should an error occur, an HTTP 500 will be returned. Those are our only options. It is best, then, to leverage the framework and create responses that are more suitable for use by our client.

```c#
public IActionResult Get(int id)
{
    // load person, usually from a service
    var person = new Person { Id = id, FirstName = "Johhny", LastName = "B. Goode." };
    return Ok(person);
}
```

There are only two small changes in the above code, the function's return type and wrapping `person` with the call to `Ok()`, but we've added a lot of flexibility. Let's consider a more complex scenario where a call to find `person` to a service doesn't yield a result.

```c#
public IActionResult Get(int id)
{
    if (id <= 0)
        return BadRequest("You must specify an ID > 0.");

    var person = _personService.GetById(id);
    if (person == null)
        return NotFound();

    return Ok(person);
}
```

Now, by using `IActionResult`, we have the flexibility to introduce alternate paths to respond to the request. We use the helper methods of the base `Controller` class to generate results based on the request. In the event that an invalid ID is presented, we can return an HTTP 400 signaling the client made an error. When we can't locate the record via the service, we return an HTTP 404 saying that the resource cannot be found. Finally, the HTTP 200 status code is explicitly set with with `Ok(person)` invocation. 

You can read in more detail about the use of `IActionResult` in the context of Web API on the [ASP.NET Docs Site](https://docs.asp.net/en/latest/mvc/models/formatting.html).  You can also learn how to make these policies explicit and encapsulate them in filters in [this MSDN article] (https://msdn.microsoft.com/en-us/magazine/mt767699.aspx?f=255&MSPPError=-2147217396).

## Next Steps

 - Now that you know how to push data out as the result of an action, you might want to skip ahead to [views](views.md) to see how to start rendering some HTML for the client.
 - You may have observed the use of the `_personService` suggested in this tutorial. You can learn more about how that object came to be initialized by reading about [controller depencies](controller-dependencies.md).