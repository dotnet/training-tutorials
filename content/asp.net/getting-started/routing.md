# Introducing Routing
by [Lauren Miller](https://github.com/PentaBismuth)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## Adding Routing Dependency and Middleware

You may have noticed so far that to map a particular URL to an action has required the use of ``app.Map``. This maps the URL based on what it starts with. But what if we want to perform more advanced matching? This is where ``routing`` comes in to play.

Fundamentally, ``routing`` is the process of matching incoming requests to handlers for those requests. To get started with ``routing``, we need to add the proper dependency to our project. So simply add a ``using Microsoft.AspNetCore.Routing`` to the top of your ``Startup.cs`` file. And now that we have the needed middleware ``RouterMiddleware``, we are all set on dependencies.

## Configuring Routing with Existing Middleware

Next, we need to add the proper entry to the ``ConfigureServices`` method as follows:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddRouting();
}
```

In order to add routed, we first need a ``RouteHandler`` to tell the router what to do when a matching route is found. In the ``Configure`` method, we can replace this code...

```c#
app.Map("/quote", builder => builder.Run(async context =>
{
    var id = int.Parse(context.Request.Path.ToString().Split('/')[1]);
    var quote = quotationStore.List().ToList()[id];
    await context.Response.WriteAsync(quote.ToString());
}));
```

...with an equivalent ``RouteHandler``

```c#
var quoteRouteHandler(context =>
{
    var id = context.GetRouteData().Values.Item["id"];
    var quote = quotationStore.List().ToList()[id];
    return context.Response.WriteAsync(quote.ToString());
}));
```

## Accessing Route Variables from Middleware

To actually use this handler and show the power of routing when it comes to breaking out a URL, we need only a few more lines of code:

```c#
var routeBuilder = new RouteBuilder(app, quoteRouteHandler);

routeBuilder.MapRoute(
    "Find Quote Route",
    "quote/{id:int}");

var routes = routeBuilder.Build();
app.UseRouter(routes);
```
Most of this code is pretty straight forward, with the exception of the ``routeBuilder.MapRoute`` line. The curly braces in the string ``"quote/{id:int}"`` allow us to extract the parameters we need from the URL and pass them by name to the handler. Here, we are expecting an int within the URL and passing it to ``quoteRouteHandler`` by the name id. This is extendable to quite a bit more advanced matching and extracting of values.

## Next Steps

There are many more routing operations available. You can limit routing based on request type and other parameters.
For more information, see the [Microsoft docs on routing](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing).
You can also reference the methods from the [RouteBuilder class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.routing.routebuilder).