# Understanding Required ASP.NET Core Files
by [Steve Smith](http://deviq.com/me/steve-smith)

You can run most of the examples in this tutorial in your browser. However, if you want to install and run ASP.NET Core apps on your own machine, refer to the appendix lessons on [installing and getting started with ASP.NET Core](your-first.md) and [working with the dotnet command line interface (or CLI)](dotnet-cli.md).

## Program.cs


ASP.NET Core apps require a *host*, which is built using a ``WebHostBuilder``. This is typically done in the program's entry point, which is typically located in the ``Main`` method in *Program.cs* file. Once configured, you won't typically need to update this file very often going forward. A very simple, minimal example would be:

```c#
using System;
using Microsoft.AspNetCore.Hosting;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
```

### Note { .note }
> The above class references the ``Startup`` type in the call to ``.UseStartup<T>()``, which is where you configure your individual app.

> **Tip** {.tip .newLanguage }    
> The ``UseStartup<T>`` method is a generic method which accepts a type in place of T when called. [Learn more about generics in C#](https://msdn.microsoft.com/en-us/library/512aeb7t.aspx).

## Startup.cs

The *Startup.cs* file contains the application's ``Startup`` class. This class must include certain methods that will be called by the host (that you just saw was configured in *Program.cs*). Specifically, the class must define a ``Configure`` method. It can optionally include a ``ConfigureServices`` method and/or a constructor as well.

### Configure

The ``Configure`` method is used to configure how your app will handle requests. This is sometimes referred to as the app's *request pipeline*, and involves configuring one or more pieces of *middleware*. At its most basic, an ASP.NET Core app can configure a single handler that will respond to every request, like this one:

```c#
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ConsoleApplication
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello from ASP.NET Core!");
            });
        }
    }
}
```

The ``app.Run`` method configures *middleware*, which you'll learn more about in the next lesson. In addition to configuring your app's request pipeline, the ``Configure`` method is also used to (surprise!) configure other aspects of the app. It's here that you'll add support for diagnostics, logging, error handling, routing, and frameworks like MVC. These packages often have helper methods that follow a naming pattern of ``Use[Package]``, which is used to configure them within the ``Configure`` method. For example, configuring MVC is done with the ``app.UseMvc();`` statement. You'll see examples of these packages in later lessons in this tutorial. 

### ConfigureServices

The ``ConfigureServices`` method in the ``Startup`` class is optional. You use it to populate the services *container* used by ASP.NET Core. This container provides requested services to objects within your app through a process called [dependency injection](http://deviq.com/dependency-injection/). The container is represented by the ``IServiceCollection`` parameter that is passed into the ``ConfigureServices`` method. ASP.NET Core provides and requests many services itself, and uses the same infrastructure you will use in your app. Many built-in as well as third-party features require the addition and sometimes configuration of certain services to work properly. You'll often see a particular package (for instance, MVC) adding services in ``ConfigureServices`` and then also being configured in ``Configure``.

Some packages will require many different services. Rather than requiring many statements to register each service individually, helper methods are added. By convention, these helper methods follow a naming pattern of ``Add[Package]``. So, for example, to add the services needed by MVC, you would add the statement ``services.AddMvc();`` to ``ConfigureServices``.

## Other files

Your ASP.NET Core app also requires a project file. However, most of the time you won't need to work with this file directly, and in any case its structure will be changing before .NET Core's tooling (which includes the command line interface as well as editors like VS Code and Visual Studio) are finalized. For now, you'll notice a *project.json* and probably a *project.json.lock* file in your application folder.

To include support for certain features in your ASP.NET Core app you may need to add additional packages. This is currently done by modifying the *dependencies* section of the *project.json* file. When this is required, the tutorial instructions will walk you through what's required.

## Next Steps

In the next lesson, you'll learn how to customize responses from your ASP.NET Core app based on the client's request, so that your app can do something more interesting than just always return the same message regardless of which path is requested.
