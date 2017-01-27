# Adding Logging to the App
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## Configuring Logging

Logging provides information about a running app that may be useful to a developer or system administrator. By default, ASP.NET Core logs a great deal of information about individual requests. You can configure a logger to allow you to view this information, as well as add your own logging of events of interest to you regarding your app.

To get started with logging, the first thing you should do is add the required package(s) to your project. For this example, you're only going to be logging to the console, so you need to add this package: `Microsoft.Extensions.Logging.Console`. Once you've added the package to your project (and restored your project's dependencies), you can modify *Startup.cs* to include logging support. Update your `Configure` method as follows:

```c#
    public void Configure(IApplicationBuilder app, 
        IOptions<List<Quotation>> quoteOptions,
        ILoggerFactory loggerFactory)
    {
        loggerFactory.AddConsole();
        ...
```



## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.