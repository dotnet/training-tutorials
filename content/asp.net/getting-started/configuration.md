# Adding Basic Configuration Support
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## Adding Support for Configuration

So far the Quotes app you're building hasn't needed any configuration values. However, most real apps need to store some values related to their configuration somewhere, typically in a file that's deployed with the app. ASP.NET Core has a very extensible configuration system, allowing you to choose from a variety of built-in configuration sources or to customize your own. You're not limited to a particular file or file format, and you can even add configuration values directly using code.

Typically in ASP.NET Core apps, configuration is set up in the ``Startup`` class. An instance of the ``IConfigurationRoot`` type is created using a ``ConfigurationBuilder``. This can be done in your ``Startup`` class's constructor:

```c#
public Startup(IHostingEnvironment env)
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("quotes.json", optional: false, reloadOnChange: true);

    Configuration = builder.Build();
}

public IConfigurationRoot Configuration { get; set;}
```

In this code, the constructor uses an ``IHostingEnvironment`` instance to set the base configuration path to the content root path (the root of the project). Then, it specifies that configuration will come from a required JSON file, ``quotes.json``. The result of the ``Build`` method is stored in the ``Configuration`` property, which is accessible from elsewhere in ``Startup``.

Note that for the above code to compile, you'll need to be sure to include these namespaces:

```c#
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
```

You'll also need to reference these packages:

* Microsoft.Extensions.Configuration.Json
* Microsoft.Extensions.Options.ConfigurationExtensions


## Accessing Configuration Values as Options

ASP.NET Core uses the [options pattern](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration#options-config-objects) to access configuration values in a strongly-typed manner. Once configured, strongly typed values may be requested from any type or method that supports [dependency injection](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection) (which includes most ASP.NET Core types, as well as the ``Configure`` method in ``Startup``, as you'll see in a moment). To use the pattern, simply request an instance of ``IOptions<T>`` where ``T`` is the type you're trying to access from configuration. Currently, the quotes app stores its data in a hard-coded ``List<Quotation>`` in ``QuotationStore``. To load these quotes from a configuration source, an instance of ``IOptions<List<Quotation>>`` is used. To access the strongly-typed value, use the ``Value`` property.

```c#
public void Configure(IApplicationBuilder app, 
    IOptions<List<Quotation>> quoteOptions)
{
    // other code omitted
    var quotes = quoteOptions.Value;
    if(quotes != null) 
    {
        QuotationStore.Quotations = quotes;
    }
}
```

Ultimately the app can be refactored so that the ``QuotationStore`` isn't static, and can accept the configured quotes through dependency injection itself. The current design works, but isn't ideal since any code can manipulate the ``Quotations`` property, violating the principle of [encapsulation](http://deviq.com/encapsulation/).

## Setting up Options

In order for the options pattern to work, you first need to add options support in ``ConfigureServices``. Then, you need to add the options value to the services container. You can specify the value of the options directly, or you can specify that it should be loaded from configuration, as the following code demonstrates:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddOptions();
    services.Configure<List<Quotation>>(Configuration.GetSection("Quotes"));
}
```

It's important when using a file for your configuration that you format it appropriately. In this example, the ``quotes.json`` file needs to container a ``quotes`` section, which in turn should contain a collection of ``Quotation`` instances. An example of ``quotes.json``:

```json
{
    "Quotes": [
        { "author": "Ralph Johnson", "quote": "Before software can be reusable it first has to be usable." },
        { "author": "Albert Einstein", "quote": "Make everything as simple as possible, but not simpler." },
        { "author": "Dwight Eisenhower", "quote": "I have always found that plans are useless, but planning is indispensable." }
    ]
}
```

## Next Steps

Since the configuration options needed in this lesson were inside of the ``Startup`` class, the method could have directly accessed the settings from the ``Configuration`` property. Modify the code to use this approach, and consider whether you prefer it to the use of the options pattern in this instance. 

Note that in most situations, such as using configuration options from controllers, middleware, or filters, your code won't have direct access to a configuration instance.