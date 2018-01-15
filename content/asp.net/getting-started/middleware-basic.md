# Using Basic Middleware to Output Responses
by [Steve Smith](http://deviq.com/me/steve-smith)

## Rendering Quotations

Say you want to create a web site that displays a random quotation each time a request is made to it. For now, that list of quotations can be defined purely in code. Start by creating a class to represent a quotation:

```c#
public class Quotation
{
    public string Quote { get; set; }
    public string Author { get; set;}

    public override string ToString()
    {
        return Quote + " - " + Author;
    }
}
```

Then, add a class to represent the source of the quotations. Specify a few quotes, and provide a way to retrieve a random quotation from the collection.

```c#
public static class QuotationStore
{
    public static IList<Quotation> Quotations {get; private set;}

    static QuotationStore()
    {
        Quotations = new List<Quotation>()
        {
            new Quotation() { 
                Quote="Measuring programming progress by lines of code is like measuring aircraft building progress by weight.", 
                Author="Bill Gates"},
            new Quotation() { 
                Quote="Be kind whenever possible. It is always possible.", 
                Author="Dalai Lama"},
            new Quotation() { 
                Quote="Before software can be reusable it first has to be usable.", 
                Author="Ralph Johnson"}
        };
    }
    
    public static Quotation RandomQuotation()
    {
        Random rnd = new Random(DateTime.Now.Millisecond);
        return Quotations[rnd.Next(0,Quotations.Count)];
    }
}
```

With this in place, you're ready to create some simple ASP.NET middleware to render a random quote for each request. You've already seen middleware in action in the initial "Hello World" app in the last lesson:

```c#
app.Run(context =>
{
    return context.Response.WriteAsync("Hello from ASP.NET Core!");
});
```

Calling ``app.Run`` in your app's ``Configure`` method creates a middleware endpoint for your app. Middleware can be configured into one or more pipelines, with each piece of middleware calling into the next. Middleware configured with ``app.Run`` cannot call any additional middleware, hence it truly represents the endpoint for a given middleware pipeline. Calling ``app.Use`` lets you configure middleware that calls into additional middleware, running logic before and/or after middleware that runs later in the pipeline.

## Random Quote

You can update the "Hello World" app to display a random quote by simply replacing the message string with a call to ``QuotationStore.RandomQuotation()``:

```c#
app.Run(context =>
{
    return context.Response.WriteAsync(QuotationStore.RandomQuotation().ToString());
});
```

## All Quotes

Now let's say you want to let visitors request all quotes. You decide to let request path of ``/all`` represent this option. You update your ``Configure`` method as follows:

```c#
public void Configure(IApplicationBuilder app)
{
    app.Run(context =>
    {
        return context.Response.WriteAsync(QuotationStore.RandomQuotation().ToString());
    });

    app.Run(async context =>
    {
        if(context.Request.Path.StartsWithSegments("/all"))
        {
            foreach(var quote in QuotationStore.Quotations)
            {
                await context.Response.WriteAsync("<div>");
                await context.Response.WriteAsync(quote.ToString());
                await context.Response.WriteAsync("</div>");
            }
        }
        return;
    });
}
```

This code adds another ``app.Run`` statement, which checks the ``Request.Path`` to see if it starts with ``/all``. If so, it lists all of the quotations. Unfortunately, **this code doesn't work**. When you visit the root of the site, you see random quotes, just as before. However, when you visit ``/all``, you still only see the random quote behavior, not the list of quotations. Remember what you learned earlier - ``app.Run`` is an endpoint for your app's request pipeline. When the request pipeline encounters an endpoint and returns, it doesn't enter any other configured pipelines. There are two ways you can address this in this sample. First, you can put all of the logic into a single call to ``app.Run``. Alternately, you can keep the original random quotation ``app.Run`` statement, and configure a new *branch* in your request pipeline by using ``app.Map``.

The ``app.Map`` method takes a path string and an ``Action<IApplicationBuilder>`` function, which you use to configure a new, separate branch of the request pipeline that is only executed if the path matches. It's still important that you order your request delegates properly - if a catch-all ``app.Run`` handles the request before the ``app.Map`` is configured, the latter will never be executed for any requests. The correct order is shown below:

```c#
public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        app.Map("/all", builder => builder.Run(async context =>
        {
            foreach(var quote in QuotationStore.Quotations)
                {
                    await context.Response.WriteAsync("<div>");
                    await context.Response.WriteAsync(quote.ToString());
                    await context.Response.WriteAsync("</div>");
                }
        }));

        app.Run(context =>
        {
            return context.Response.WriteAsync(QuotationStore.RandomQuotation().ToString());
        });
    }
}
```

## Adding to the Pipeline

Even in the case where ``app.Map`` is used to handle requests for all quotations, the request pipelines you've seen so far aren't very deep. Remember that you can add behavior before and/or after any of these endpoints by placing additional middleware ahead of them in the pipeline. For example, if you wanted to specify that both methods return HTML content, you could add middleware to the request pipeline, before the other code in ``Configure``, to achieve this:

```c#
app.Use(async (context, next) => 
{   
    context.Response.ContentType="text/html";
    await next();
});
```

> **Note** {.note}    
> Avoid writing to Response headers in middleware if you're not certain that other middleware hasn't already begun writing to the response (in which case, the headers will already have been sent to the client). In particular, use caution when writing to the Response after having called other middleware (in the example above, after the ``await next();`` statement).

## Next Steps

See if you can create another request endpoint to display a particular quotation. You can do this by using ``app.Map`` for a particular path, such as ``/quote``. Notice that within a mapped pipeline, the path segment used to match ``app.Map`` is removed from the ``Request.Path``. Thus, if you map ``/quote`` and want to pull the index of the quote from the full path of ``/quote/1``, you'll find that within your ``Map`` function the path is simply ``/1``.

You can fetch a specific quotation by index using, for example, ``QuotationStore.Quotations[0]``.
