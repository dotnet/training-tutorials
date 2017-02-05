# Configuring and Requesting App Services
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## Introduction

Your ASP.NET Core app will likely depend on a number of services. These services perform important functionality for the app. Almost anything your app does could be encapsulated in a service, but the most important operations to isolate are those that work with infrastructure outside the scope of your app. For instance, if your app stores data in a database, or reads data in from an online API, these operations would benefit from being encapsulated in services rather than being performed directly from the app. ASP.NET Core is built on the premise of modularity, and includes built-in functionality for registering services your app requires and making these services available to classes within your app. You register services your app will need in the `ConfigureServices` method in *Startup.cs*. Your classes that depend on these services then request them by declaring parameters in their constructors. Writing classes such that they explicitly request their dependencies is said to follow the [Explicit Dependencies Principle](http://deviq.com/explicit-dependencies-principle/), which is closely related to the [Dependency Inversion Principle](http://deviq.com/dependency-inversion-principle/).

The current sample app you've seen uses a static `QuotationStore` to access quotes from a fixed collection. Working with this static class tightly couples your app to this implementation. If you want to easily test your code, or swap in a different implementation for how quotes are accessed, you would need to edit all of the code that directly references `QuotationStore` (this violates the [Open/Closed Principle](http://deviq.com/open-closed-principle/)). To improve this design, making it more loosely coupled and closer to how ASP.NET Core expects apps to be written, you can [refactor](http://deviq.com/refactoring/) the `QuotationStore` into a non-static service that implements an interface.

## Creating a Service

The current `Configure` method works directly with `QuotationStore` to set its contents from [configuration](configuration.md) and use these contents in [middleware](middleware-basic.md):

```c#
    public void Configure(IApplicationBuilder app, 
        IOptions<List<Quotation>> quoteOptions,
        ILoggerFactory loggerFactory)
    {
        loggerFactory.AddConsole(Configuration.GetSection("Logging"));
        app.UseStatusCodePagesWithRedirects("~/{0}.html");
        app.UseDeveloperExceptionPage();
        app.UseStaticFiles();

        var quotes = quoteOptions.Value;
        if(quotes != null) 
        {
            QuotationStore.Quotations = quotes;
        }

        app.Map("/quote", builder => builder.Run(async context =>
        {
            var id = int.Parse(context.Request.Path.ToString().Split('/')[1]);
            var quote = QuotationStore.Quotations[id];
            await context.Response.WriteAsync(quote.ToString());
        }));

        app.Map("/all", builder => builder.Run(async context =>
        {
            foreach(var quote in QuotationStore.Quotations)
                {
                    await context.Response.WriteAsync("<div>");
                    await context.Response.WriteAsync(quote.ToString());
                    await context.Response.WriteAsync("</div>");
                }
        }));

        app.Map("/random", builder => builder.Run(async context =>
        {
            await context.Response.WriteAsync(QuotationStore.RandomQuotation().ToString());
        }));
    }
```

Now is a good time to clean up this code a bit. Initializing the quotation store is something that could be left to the store itself. Other than that, the middleware needs access to either a list of quotations, or a random quotation. Thus, you can represent this using a simple interface:

```c#
    public interface IQuotationStore
    {
        IEnumerable<Quotation> List();
        Quotation RandomQuotation();
    }
```

With an interface extracted, you can refactor the static `QuotationStore` to implement the interface. You can pull the initialization logic into `QuotationStore`'s constructor.

```c#
    public class QuotationStore : IQuotationStore
    {
        private static List<Quotation> _quotations {get; set;}

        public QuotationStore(IOptions<List<Quotation>> quoteOptions)
        {
            _quotations = quoteOptions.Value;
            if(_quotations == null)
            {
                // if nothing configured, use default in code
                _quotations = DefaultQuotations();
            }
        }

        private List<Quotation> DefaultQuotations()
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

        public IEnumerable<Quotation> List()
        {
            return _quotations.AsEnumerable();
        }

        public Quotation RandomQuotation()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            return _quotations[rnd.Next(0,Quotations.Count)];
        }
    }
```

## Using the Service

After making this change, you can modify the `Configure` method to request an instance of `IQuotationStore` as a parameter. ASP.NET Core will provide the instance when the method is executed (if it's been registered, which you'll do in a moment). The refactored `Configure` method no longer requires `quoteOptions`, since the initialization code for the quotes has been encapsulated in the service itself.

// show refactored Configure method

// build and run - you'll get an error

## Registering the Service





## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.