# Building Your Own Middleware
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## Middleware in Configure

In many cases, especially for simple middleware, you can start by just adding the code directly in the ``Configure`` method, as you saw in the [basic middleware](middleware-basic.md) lesson. For example, to add some middleware that performs some simple timing, you could do something like this:

```c#
// simple timing middleware
app.Use(async (context, next) =>
{
    Stopwatch stopWatch = Stopwatch.StartNew();
    await next.Invoke();
    var logger = loggerFactory.CreateLogger<Startup>();
    stopWatch.Stop();
    logger.LogInformation($"Execution time: {stopWatch.ElapsedMilliseconds} ms");
});

app.Run(async (context) =>
{
    // add a sleep so the time isn't negligible
    System.Threading.Thread.Sleep(25);
    await context.Response.WriteAsync("Hello World!");
});
```

There's really no limit to what you can add in this fashion. However, it does tend to bloat the ``Configure`` method, and it's best if you can move middleware that has a specific focus into its own class, so that you can better test it and perhaps reuse it (and keep ``Configure`` from getting too big).

## Moving Middleware to its Own class

The next logical step is to move this middleware implementation to its own class. If you're using Visual Studio, you can add a New Item and choose the Middleware template. It will generate code like this:

```c#
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApplication3
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SimpleTimingMiddleware
    {
        private readonly RequestDelegate _next;

        public SimpleTimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SimpleTimingMiddlewareExtensions
    {
        public static IApplicationBuilder UseSimpleTimingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SimpleTimingMiddleware>();
        }
    }
}
```

Note that by default the middleware generated is not async, and it does nothing. The template does also include an ``Extensions`` class that includes an extension method to make adding the middleware in ``Configure`` easy to do. This is a convention you should follow as you create your own middleware classes. To adjust the above template to support the functionality of the simple timing middleware, you need to inject a ``LoggerFactory`` in order to get a logger instance (which in this case will be of type ``Logger<SimpleTimingMiddleware>`` to match this new class):

```c#
public class SimpleTimingMiddleware
{
    private readonly RequestDelegate _next;
    private ILogger<SimpleTimingMiddleware> _logger;
    public SimpleTimingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<SimpleTimingMiddleware>();
    }
// ...
```

Next, update the ``Invoke`` method so that it is async and awaits the call to ``_next`` just as the original code in ``Configure`` did:

```c#
public async Task Invoke(HttpContext httpContext)
{
    Stopwatch stopWatch = Stopwatch.StartNew();

    await _next(httpContext);
    stopWatch.Stop();
    _logger.LogInformation($"Execution time: {stopWatch.ElapsedMilliseconds} ms");

}
```

In this case, rather than creating a logger in-line, the middleware uses the logger created in its constructor. This is slightly more efficient since the same logger will be used for the lifetime of the application, rather than being created on every request.

Now all that remains is to wire up the middleware in ``Configure``. It can actually be used in conjunction with the ``app.Use`` middleware, as shown:

```c#
    app.UseSimpleTimingMiddleware();

    app.Use(async (context, next) =>
    {
        Stopwatch stopWatch = Stopwatch.StartNew();
        await next.Invoke();
        var logger = loggerFactory.CreateLogger<Startup>();
        stopWatch.Stop();
        logger.LogInformation($"Execution time: {stopWatch.ElapsedMilliseconds} ms");
    });

    app.Run(async (context) =>
    {
        // add a sleep so the time isn't negligible
        System.Threading.Thread.Sleep(25);
        await context.Response.WriteAsync("Hello World!");
    });
```

When run in this fashion, both sets of middleware execute, resulting in multiple log messages:

```
info: WebApplication1.Startup[0]
      Execution time: 25 ms
info: WebApplication1.SimpleTimingMiddleware[0]
      Execution time: 26 ms
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
      Request finished in 28.161ms 200
```

> **Note** {.note}    
> Notice in the log output which message appears first. You might expect that since the ``SimpleTimingMiddleware`` middleware is first in the pipeline, that its log message would appear first, but this isn't the case. The reason is that the log messages occur at the end of the middleware's invocation, so the ``app.Use`` version completes before the ``SimpleTimingMiddleware`` class method completes.

## Next Steps

In this example, a service required by the middleware is provided in its constructor. The ``Invoke`` method can also accept services through dependency injection. This can be useful if you have a service you need to pass between multiple sets of middleware, or between middleware and other parts of your app. Create a service that includes a counter, and increment the counter whenever your middleware is invoked. Configure multiple middleware to use the counter, passing it as a parameter via ``Invoke``, and confirm the count is accurate for a given request.

> ** Note** {.note}
> You should register the counter class in ``ConfigureServices`` as ``Scoped``. You'll probably also want your default middleware to be in its own class as well - see below for an example.

```c#
    public class HelloWorldMiddleware
    {
        private readonly RequestDelegate _next;

        public HelloWorldMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, CounterService counter)
        {
            counter.Increment();
            // add a sleep so the time isn't negligible
            System.Threading.Thread.Sleep(25);
            await httpContext.Response.WriteAsync($"Hello World! Count: {counter.Count}");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class Middleware1Extensions
    {
        public static IApplicationBuilder UseHelloWorldMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloWorldMiddleware>();
        }
    }
```