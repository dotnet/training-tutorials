# Understanding Controllers and Actions
by [James Chambers](http://jameschambers.com)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Completed Version (Pending)] - Includes the completed versions of all MVC tutorials

## Controllers and Actions in ASP.NET Core
As we talked about in the [last tutorial](mvc.md), a _controller_ is where we put the logic needed to respond to incoming requests. Each incoming request is evaluated and mapped into a specific action on a controller by virtue of its HTTP verb, path and query string through a process called [routing](routing.md).

Controllers are classes that typically inherit from `Microsoft.AspNetCore.Mvc.Controller` and reside in a folder called `Controllers` in the root of your project.  Controllers are used to group together a set of related concerns, usually associated to each other by a business concept or entity.  These concerns might be related to displaying or updating data and are implemented in methods called _actions_.  

Actions are responsible for ensuring that incoming requests are valid and that appropriate responses are returned to the client, typically building up and sending a model to a [view](views.md) for rendering. You can put whatever logic is required in an action to respond accordingly to requests, but the purpose of controllers and actions is to help separate view rendering from business logic, not to provide a place to put your business logic. Business logic should be located in services that are passed into your controller using [dependency injection](controller-dependencies.md). 

Each action has a role in constructing a proper HTTP response to the client, but you don't have to do all this work on your own; the Framework has a number of features that will help you along the way. You can start to explore those features by creating a new class in the controllers folder called `PersonController`. Update the class so that it appears as below:

```c#
using Microsoft.AspNetCore.Mvc;

namespace MvcBasics.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
``` 
There's only a few lines of code, but what's there is pretty powerful. First off, we inherit `Controller`, which gives us the ability to access the context of the request, information about the current user, and a way to leverage validation.  `Controller` also gives us a suite of helper methods that we can use to help generate responses.

Secondly, we use one of the most common controller helpers in the action when we make a call to `View()`, which triggers the rendering of the view given the current state of our controller.

Next, we have a return type of `IActionResult` giving us a flexible way to generate all different types of HTTP responses, from the typical `200 OK` to more complex responses like `302 Permanent Redirect` and a whole host of others.  These responses can be created using controller helpers, which we'll examine in greater detail later in this lesson.

Finally, we have a method named `Index` and, along with our class named `PersonController`, users are now able to request the URL `person/`. This works because of the conventions and defaults of the framework and project template.

> Note that you can actually visit the `PersonController` if you run your app at this time and navigate to `/Person` in the browser, but you will see an error because we haven't yet added a [view](views.md). You can, however, create a breakpoint in your controller on the `return View();` statement, which would allow you see execution flow through to that point.

## Routing for Controllers and Actions
When configured, a route is a pattern that is matched against an incoming request that helps the framework determine which controller and action should be invoked to respond to the caller. The [routing](routing.md) configured in the [project template](mvc.md) faciliates this, and you don't need to wire up anything custom to make your project work provided you follow the default convention. Here are some examples of how your application will respond:

| Route           | Controller | Action  | Reason                                                                                                                                    |
|-----------------|------------|---------|-------------------------------------------------------------------------------------------------------------------------------------------|
| /               | Home       | Index   | There is nothing specified in the route that can be mapped. The defaults are used: `Home` for the controller and `Index` for the action.  |
| /Home/Contact   | Home       | Contact | Both the controller and action are specified in the route.                                                                                |
| /Person         | Person     | Index   | While the controller is specified, the action is not; thus the default `Index` will be used.                                                |
| /Person/Index/6 | Person     | Index   | The `Index` action on the `Person` controller will be invoked and the parameter `id`, if present, will be set to 6.                         |

## Attribute routes
When the globally-configured routes do not suit your needs you can override the defaults on a per-controller or per-action basis using routing attributes. For instance, you could decorate your `PersonController` with the following attribute to change the route from `/person` to `/peeps`:

```c#
[Route("peeps")]
public class PersonController : Controller
{
    // ...
}
```

Likewise, an action can be setup to use a different route if you apply an attribute to it, for instance, with the above `peeps` route set on the controller, you can have `peeps/favs` as the route for the action below with the following route applied:

```c#
[Route("peeps")]
public class PersonController : Controller
{
    [Route("favs")]
    public IActionResult Favorites()
    {
        //...
    }
}
```

In the above scenario, the action assumes the controller's route and builds upon it. You can also override the base for that specific action if you prepend the route with a forward slash:

```c#
    [Route("peeps")]
    public class PersonController : Controller
    {
        [Route("favs")]
        public IActionResult Favorites()
        {
            //...
        }

        [Route("/people/home")]
        public IActionResult Index()
        {
            //...
        }
    }
```

Routes can get tricky to debug, so use them judiciously and know how they affect incoming requests in your app.  For example, someone looking at the above controller might make assumptions about the `Index` action, given the default route configuration on the project; however, with the attributes applied as above, a request to `/person/index` would give them a `404 Not Found`.  Read more about routes [here](routing.md). 

## Controller Helpers and Result Objects 
In addition to the actions that you write yourself, you're also privy to the host of helper methods that are built into the base controller that you inherit from. These methods help to formulate a response after you've completed any validation, service access or processing required. We've already looked at one as part of the samples above, but let's have another quick look:

```c#
public IActionResult Index()
{
    return View();
}
```        

The call to the helper method `View` above gives you an easy way to say, "I'm done processing here, and now I would like the user to see the page related to this action."  The framework takes over, maintains the state of any properties you've set on the controller and renders a view following the default naming and location conventions we covered earlier.

Just like `View`, there are other helpers that will help you return appropriate and meaningful responses from your actions.

 - **Redirection**- You can use `Redirect`, `LocalRedirect`, `RedirectToRoute` and other similar methods in your actions for the purpose of forwarding users on to another location, for example, after a login.   
 - **Standard Response Types**- When you are working at more of an API level or need more granular control over building the response, you can use methods like `Ok` or `NotFound` to convey the results of your processing.   
 - **Json**- If you want to control the way your data is returned, perhaps decorating an object in order to better interoperate with another service or client, you can manually build JSON responses. 
 - **PartialView**- When you only need to render part of a page without the overhead of a layout, typically used in AJAX scenarios where only a portion of the page is fetched from the server.   
 - **ViewBag, ViewData**- These are containers that are backed by the same key/value pairs in a dictionary. `ViewData` uses a string for a key, whereas `ViewBag` is a `dynamic` that allows you to the dictionary as though the keys were property names.  While these are not explicitly methods or result objects, they can help you in the presentation of data in your views.

## Next Steps

 - You'll get an error if you try to get to an action that doesn't have a corresponding view. Views will be covered in the [next lesson](views.md).
 - Actions can return different types of results, especially when using the helper methods. Try using the browser developer tools (usually accessed by hitting F12) and experiment with different result types.
 - Try using different route attribute settings and read more about [routing](routing.md).   
