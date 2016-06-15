# Your First ASP.NET Core App
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## Install .NET Core

If you don't already have it installed, make sure you have .NET Core installed along with whatever tooling you'd like to use (for instance, Visual Studio Code or Visual Studio). You can download everything you need from [here](https://microsoft.com/net/core).

## Create a New Console Application

Open a command prompt and create a new folder for your first ASP.NET Core application. Navigate to that folder and type:

    > dotnet new
    Created new C# project in (path)

This command produces a simple "Hello World" console application, which produces two files:

- Program.cs: Your program
- project.json: Project information, including dependencies on other packages

You can run the application immediately to confirm it works. First, run the ``dotnet restore`` command to download the project's dependencies, and then use ``dotnet run`` to execute it.

    > dotnet restore
    > dotnet run
    Hello World!

## Convert the Console App into an ASP.NET Core App

To modify this program to be an ASP.NET Core app, you need to add a dependency. Open *project.json* in your editor and add a dependency on "Microsoft.AspNetCore.Server.Kestrel", as shown:

```json
{
  "version": "1.0.0-*",
  "buildOptions": {
    "emitEntryPoint": true
  },
  "dependencies": {
    "Microsoft.NETCore.App": {
      "type": "platform",
      "version": "1.0.0-rc2-3002702"
    },
    "Microsoft.AspNetCore.Server.Kestrel":"1.0.0-rc2-final"
  },
  "frameworks": {
    "netcoreapp1.0": {
      "imports": "dnxcore50"
    }
  }
}
```

One more, restore the project's dependencies (so that this new dependency is downloaded):

    > dotnet restore

Add a new *Startup.cs* file that will define how the ASP.NET Core app handles requests:

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

Finally, modify the *Program.cs* file to configure a *host* for ASP.NET, telling it to use the ``Startup`` class you just created, and then running the host.

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

Run the app with ``dotnet run``:

    > dotnet run
    Now listening on: http://localhost:5000
    Application started. Press Ctrl+C to shut down.

Browse to [http://localhost:5000]. You should see:

![Hello from ASP.NET Core](images/hello-world.png)

## Next Steps

Right now, this web application will respond to every request the same way. You can modify this response by changing what happens inside of the ``app.Run`` block. Change the content type to HTML and have the app return a more nicely formatted response.

You can modify the response to be HTML with the following:

```c#
context.Response.ContentType="text/html";
``` 