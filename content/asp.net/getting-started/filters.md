# Understanding ASP.NET Core MVC Filters
by [James Chambers](http://deviq.com/me/james-chambers)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Completed Version (Pending)] - Includes the completed versions of all MVC tutorials

Sometimes you will see an opportunity for a cross-cutting concern in your code that either requires an awful lot of duplication of code, or some way to facilitate code reuse to help address the concern.  Filters in ASP.NET Core MVC are one way developers can address aspects of their application without having to resort to copy-and-pasting similar blocks of code throughout their project.

## Filters in ASP.NET Core MVC

Imagine that you wanted to you ensure that a user was logged in before you allowed them to view a particular page on your site. In your controller's action you might do something like this:

```c#
    if(!User.Identity.IsAuthenticated)
        return Unauthorized();

    // todo: proceed with authenticated user's processing...
}
```

This could work, but consider that you might want this to work on several different operations, perhaps entire controllers or even entire areas within your application.  What if you needed to also test for specific roles?  It's easy to see how this code could spiral out of control and lead to errors and repeating yourself throughout the application.

This is where filters come in, allowing you to execute code before and after your action to simplify the exercise of reusing your code.

## Using the Built-In Filters

Filters are similar in concept to [middleware](middleware-basic.md) in that you have the option to modify part of the requested operation. Where they differ is in location in the pipeline and their intent: middleware sits outside of the MVC pipeline and is meant more to control application flow whereas filters allow you to deal with a request in the context of access to a specific 

TODO: example of differences, use authorize filter


## Building Your Own Filters


Filters come in a variety of flavors for different purposes and can be created by implementing one of a number of interfaces:
 - 'IActionFilter' - called before and after model binding, but before the action is executed
 - 'IExceptionFilter' - called only after an exception has been thrown from the action
 - 'IResultFilter' - called before and after the action result is executed (typically a view in MVC)
 - 'IAuthorizationFilter' - called earlier the request, used to confirm authorization of access
 - 'IResourceFilter' - executed after authorization filters, used to modify the flow of execution in the pipeline

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

In the asynchronous version, the corresponding code using this pattern:

```c#
public class SampleAsyncActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // before
        await next(); // <== other filters
        // after            
    }
}
```
TODO: Sample here
TODO: Wire it up

## Next Steps
TODO: Some exercises
 - Check out the [official documentation](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters)