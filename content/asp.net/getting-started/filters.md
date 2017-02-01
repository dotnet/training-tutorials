# Understanding ASP.NET Core MVC Filters
by [James Chambers](http://deviq.com/me/james-chambers)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Completed Version (Pending)] - Includes the completed versions of all MVC tutorials

Sometimes you will see an opportunity for a cross-cutting concern in your code that either requires an awful lot of duplication of code, or some way to facilitate code reuse to help address the concern.  Filters in ASP.NET Core MVC are one way developers can address aspects of their application without having to resort to copy-and-pasting similar blocks of code throughout their project.

## Filters in ASP.NET Core MVC

Imagine that you wanted to ensure that a user was logged in before you allowed them to view a particular page on your site. In your controller's action you might do something like this:

```c#
    if(!User.Identity.IsAuthenticated)
        return Unauthorized();

    // todo: proceed with authenticated user's processing...
}
```

This could work, but consider that you might want this to work on several different operations, perhaps entire controllers or even entire areas within your application.  What if you needed to also test for specific roles?  It's easy to see how this code could spiral out of control and lead to errors and [repeating yourself](http://deviq.com/don-t-repeat-yourself/) throughout the application.

This is where filters come in, allowing you to execute code before and after your action to simplify the exercise of reusing your code.

## Using the Built-In Filters

Filters are similar in concept to [middleware](middleware-basic.md) in that you have the option to modify part of the requested operation. Where they differ is in location in the pipeline and their intent: middleware sits outside of the MVC pipeline and is meant more to control application flow whereas filters allow you to deal with a request in the context of access to a specific resource or endpoint. 

These two features can easily be imagined by using authentication and authorization. Authentication can be inspected before a request even enters the MVC pipeline, ensuring that the user has the correct cookies, tokens or whatever is configured in order to represent identity. The components of the MVC pipeline are not required. Authorization, on the other hand, is more concerned about properties of the route, if the user has a particular set of claims to allow them to access a controller or a particular action.  And whereas filters have access to aspects of the request, such as identity, that are built up before the MVC pipeline, middleware does not have access to MVC concepts like routing or model binding.

You will typically see that a filter will be implemented as an attribute, making it pretty straightforward to use them.  Here is an example of using an action filter on an action through an attribute:

```c#
[Authorize]
public IActionResult About()
{
    ViewData["Message"] = "Your application description page.";
    return View();
}
```

The filter affects only the action that is decorated, however you can also use a filter attribute at a class level. The controller will first be instantiated, the attribute code will execute, then your action will be called. You also have the option to participate in the pipeline after the action completes.

## Building Your Own Filters

Filters come in a variety of flavors for different purposes and can be created by implementing one of a number of interfaces:
 - 'IActionFilter' - called before and after model binding, but before the action is executed
 - 'IExceptionFilter' - called only after an exception has been thrown from the action
 - 'IResultFilter' - called before and after the action result is executed (typically a view in MVC, or the result of a Web API call)
 - 'IAuthorizationFilter' - called earlier in the request; used to confirm authorization of access
 - 'IResourceFilter' - executed after authorization filters; used to modify the flow of execution in the pipeline

Note that many of these have asynchronous counterparts as well. In these synchronous versions you implement the before and after calls like so:

```c#
public class SampleActionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // before
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // after
    }
}
```

In the asynchronous version, the corresponding code would be contained in the same method, using this pattern:

```c#
public class SampleAsyncActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // before
        await next(); // <== other filters and the action
        // after            
    }
}
```

If you want to use your filter as an attribute, you can instead use the `ActionFilterAttribute` as a base class and override the appropriate method.  

```c#
public class SampleActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // before
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        // after
    }
}
```

You should only implement the synchronous or the asynchronous override, but not both as only the async version would be called by the framework.  

## Dependencies in Filters

You can register your filter in the [dependency injection](controller-dependencies.md) container to take advantage of other services in your application.  Here, an asynchronous implementation of an action filter has an `ILoggerFactory` passed in so that it can use the logging services:

```c#
public class SampleAsyncActionFilter : IAsyncActionFilter
{
    private readonly ILogger<SampleAsyncActionFilter> _logger;

    public SampleAsyncActionFilter(ILogger<SampleAsyncActionFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _logger.LogInformation("[AsyncFilter - BEFORE]");
            
        await next(); // <== other filters

        _logger.LogInformation("[AsyncFilter - AFTER]");
    }
}
```

To use this filter we lean on a different attribute which can create an instance of the filter for us:

```c#
[ServiceFilter(typeof(SampleAsyncActionFilter))]
public IActionResult Index()
{
    _logger.LogInformation("{{ Controller Action Executing }}");
    return View();
}
```

To get that to work, we have to register our filter in our start up routine, likely in your `ConfigureServices` method as so:

```c#
services.AddScoped<SampleAsyncActionFilter>();
```

## Next Steps
To build on what you've learned here, why not check out a few of the following ideas?  Write your own filter using an attribute approach. Some ideas here might be to alter or add to the headers based on content of your result, or checking to see if a model is in a valid state.

If you're wanting to give those a try be sure to check out these resources:
 - Read Steve Smith's [Real-World ASP.NET Core MVC Filters](https://msdn.microsoft.com/en-us/magazine/mt767699.aspx) article in MSDN
 - The ASP.NET Core MVC [Official Documentation]
(https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters)