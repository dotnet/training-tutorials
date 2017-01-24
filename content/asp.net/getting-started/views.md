# Working with ASP.NET Core MVC Views
by [James Chambers](http://deviq.com/me/james-chambers)

#### Sample Files

Download a ZIP containing this tutorial's sample files:
- [Completed Version (Pending)] - Includes the completed versions of all MVC tutorials

## Rendering Web Pages With the Razor View Engine

By default in ASP.NET Core MVC view rendering is the job of the Razor view engine.  Razor views are typically composed of interleaved HTML and C# code, the lines of which are run through a parser to generate a class at run time.  If we think of our view as a class that has properties and methods on it, we can start to see how some of the elements of the view work. Even our file extension - .cshtml - makes a little more sense as it represents what's in the file - a combination of C# and HTML.

The intent of the Razor view engine is to make switching between HTML and C# as seamless as possible in order to render output that is useful to the client. We can create our document tags as we would in a normal HTML document and use the `@` character to indicate that we are making use of C#. As our view is essentially a class, it's also easy to understand how Razor can expose data and keywords as though they were properties and methods. Here is an example taken from the sample project in `Views\Person\Index.cshtml`:

```html
@model MvcBasics.Models.Person

<h1>Person Details</h1>
<p>@Model.FirstName @Model.LastName</p>
```

To better understand how the above code works, let's take a step back to see how the runtime arrives at our view equipped with the knowledge that is needed in order to render something useful from that code. We'll come back to the above syntax shortly.

## The Mechanics of a View

The primary function of the `ViewImports.cshtml` file is to provide a mechanism for us to pull in any namespaces that we want available to all our views. This facilitates bringing in a namespace that contains your view models, rather than having to declare them in each of your views (or reference those classes explicitly by their fully qualified name). The other thing you can do here is to manage the Tag Helpers that  Razor is aware of either individually or by namespace. 

`ViewStart.cshtml` is semantically different from `ViewImports.cshtml` in that it can be used as a starting point for view-related logic. You typically use it to set a default layout for your views, but you can also use it to change the layout based on a user's role or to inspect and manipulate the request. A word of caution: it is as easy to over-reach inside of the `ViewImports` as it is in a view and to start writing business logic in the wrong places.  `ViewImports` is likely not the best place to call out to services or enforce business rules.

Finally, the `_Layout.cshtml` is used as a template for rendering your web pages and is located in the `Views\Shared` folder of your project.  Layouts allow you to define mandatory and optional sections of what will become your rendered page that can be later filled in by your views. A default layout is created in the project template complete with common styles and page head information, a navigation bar, a footer, a section stubbed in for the body of the page as well as an optional section to add scripts to the final output in your views.  You can have as many layouts as are needed, perhaps to enable specific functionality for a subset of users or to provide a different experience in an administration area in your site. 

> Read more about layouts in ASP.NET Core MVC in the the [documentation](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/layout).

## Using Razor View Engine Features

When you employ these features in unison it is important to remember a few of the rules that govern their use by the view engine. As Razor navigates your folder structure looking for your view, it also looks for direction on which layout to use and which namespaces to import at each level in the directory tree. `Using` statements that bring in a namespace at the root level will be available in all subfolders. Likewise, Tag Helpers that are brought in at a subfolder level will not be available to other "sibling" folders unless you explicitly declare them there as well. Finally, `ViewImports` are additive and allow you to add or remove namespaces and Tag Helpers, but declaring imports at a subfolder level will not override or clear out previously imported components.

In the project template we see a very basic set of statements in our `ViewImports.cshtml` document that expose the root project namespace and add in the Microsoft Tag Helpers library. This is the perfect place to add additional Tag Helpers that you may write yourself or pull in from other third-party libraries. Defining them here makes them available throughout all views in the project (due to the inheritance model used by Razor).

``` csharp
@using MvcBasics
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

The `ViewStart` is also pretty straightforward in the template, but its use helps us to understand a little better what is going on. You can think of this code as the code that will be appended to the start of each class that is generated when our views are compiled. In this case, the layout that will be used for all views is assigned to the `Layout` property of the class. This can be overridden in views if needed. This would also be a good place to customize the view's layout, perhaps based on properties of the request, such as a user claim or the host name.

``` csharp
@{
    Layout = "_Layout";
    if (User.HasClaim("Role", "Admin"))
    {
        Layout = "_AdminLayout";
    }
}
```

The `_Layout.cshtml` file is much more opinionated. It essentially creates the HTML page structure that your views will use including the containers and styles with which they are decorated. The template has to take on some dependencies on JavaScript and CSS libraries in order for this to be of any use to someone creating a web site, and you will see evidence of Bootstrap and jQuery here. We will leave full exploration of the HTML, CSS and JavaScript incorporated in this page to the reader, but there are a few important lines to point out in `_Layout.cshtml`. First is how the page head information is being set. 

``` csharp
<title>@ViewData["Title"] - MvcBasics</title>
```

Here you see how the layout can interact with the view through the use of shared properties. When rendered, the page title will be set to the value of `@ViewData["Title"]`.

Scanning the layout you'll also come across the following line of code:
``` csharp
@RenderBody()
```

The call to `RenderBody()` is where the bulk of a view's output will be rendered. This is the signal to Razor to inject the parts of a view that are not contained in a section.  Sections are set aside and rendered in a call such as `@RenderSection("scripts", required: false)`. Your views then define corresponding sections and put in the requisite code in order for the page to be output as desired. For example, you might find the following in a view to add JavaScript to your page:

``` csharp
@section scripts{
    <!-- add your JavaScript sources here -->
}
```

## Using the Data Passed to Our Views

With a few of the basics of how the views come together in place, the next step is to incorporate application data into the view.

In the sample project we have a controller called `PersonController` with an action method on it called `Index`. Index accepts an integer called `id` as a parameter, retrieves the requested person from the service and returns with a call to the `View` helper method, passing in the `Person` that was resolved from the service. 

``` csharp
public IActionResult Index(int id)
{
    var person = _personService.GetById(id);
    return View(person);
}
```

You can use the above code with the view we discussed at the start of this document, which is repeated here:

```html
@model MvcBasics.Models.Person

<h1>Person Details</h1>
<p>@Model.FirstName @Model.LastName</p>
```

In the first line of our document the view engine is instructed to use the `Person` class from the project. The person is set in the `Model` property for us at runtime by virtue of us having passed an instance of a person to the `View` helper method in the controller.  To access the properties of the `Person` object, you use the `@Model.FirstName` notation.

There is much more to take advantage of in the Razor view engine and further reading is available at the official [documentation](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor).

## Incorporating Partial Views

Partial views are incomplete on their own, but can be used like components to help build a page. You might break out your site's menu or part of your footer so that it can be used conditionally depending on other runtime factors. 

You can inject the results of a rendered partial view into your page by using the HTML helper for partials.

``` csharp
@Html.Partial("_CopyrightStamp")
```

A partial view has no layout applied to it, so HTML elements in it should be crafted in a way that respects the UI framework of the pages they will appear in. 

## Next Steps
You now know the basics of create Views that correspond to your Actions, incorporating data from your application. Here are a few ideas to explore these concepts on your own:
 - Create a new model class in your project to represent the properties of a vehicle
 - Add a [Controller with an Action](controllers-actions.md) that accepts a parameter to specify a vehicle ID
 - Add a service that is [injected into your controller](controller-dependencies.md) and returns sample data
 - Build a view that is typed to the model and displays the vehicle information

For more information on working with Razor views check out the official [ASP.NET documentation](https://docs.asp.net/en/latest/mvc/views/index.html), or these related resources in this tutorial:
 - [Sending Data to Controllers](sending-data.md)
 - [Partial Views](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/partial)
 - [Working With Data](data.md) 
 - [Razor Syntax](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor)