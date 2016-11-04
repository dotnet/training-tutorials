# Sending Data to Controllers
by [James Chambers](http://jameschambers.com)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Completed Version (Pending)] - Includes the completed versions of all MVC tutorials

## Sources of Data in Your Application
There are different data sources that your app will rely on throughout the course of execution. There is application-level data which is typically stored in configuration. We also have services, often injected by the framework, which can be called with or without parameters to fetch data. We've talked about [configuration](configuration.md) and will cover [dependency injection](controller-dependencies.md) in a future lesson, but here we will focus on the parameters that are passed into our Actions and how those values are populated.  

## Understanding the Data Sources
In a simplified view, you are likely familiar with a few parts to a typical HTTP request. We have the query string, which is everything that comes after the `?` character in a URL. You might see it in in a request where a parameter is named and its value is inline, something like:

    http://localhost:5000/user/view?id=100

The URL is not limited to the query string, however; you can have parameters as part of the URL that are extracted via your configured [routing](routing.md) rules. In that case, your URL might look something more like the following:

    http://localhost:5000/user/view/100

A body of the request may optionally contain a `form-data` section of the request. This allows for another key-value transport mechanism for the client, typically the method of choice for default HTML form submission.

Form aside, the `BODY` of the request may come more into play specifically when you're working with HTTP verbs like `POST` or `PUT` and using alternate formats. The body can be more expressive than just the `form-data` as it can contain payload of your design, and it can follow standard object notation such as XML or JSON.

By nature and by virtue HTTP and HTML do not provide a strongly-typed context that a language can make use of. These data sources are simple, text-based inputs to our application and are typically thought of as key-value pairs. Without some kind of support, you would need to manually inspect all of the sources, extract the values and then cast them to the appropriate object types in your application. This is where "model binding" comes into play, and makes light work of the task of working with the key-value pairs. 

## Model Binding in ASP.NET Core MVC
**Model binding** is the process through which those key-value pairs get evaluated for appropriate mapping to parameters on our controller actions. Depending on where those values are located and which types you are using as parameters, you need to be aware of how MVC will try to make those mappings for you. Consider the following signature of an action on our `PersonController`:

```c#
public IActionResult Edit(int id, Person person)
{
    // ...
}
```

We have two different types of parameters here: a simple type for the ID and a complex type for the person. Here's how these will be populated:

 1. Any values that can be extracted as tokens from the route will be, and those values will be assigned to the parameters on the action, including properties on complex types (like `Person.Id`) by walking the graph and comparing property names to the token values. 
 2. Next, the query string will be evaluated to find properties that match the parameters on the action and those will be set accordingly.  Even if only part of the properties are present, those will be filled in.
 3. Any values that are present in the `form-data` will be used to fill in properties on the action in the same manner.
 4. If the request instead contains a specific content type header and the `BODY` contains corresponding data in the correct format, that data will be used to fill in the blanks on the action parameters. As an example of this, an HTTP header of `Content-Type` set to `application/JSON` and a body containing a `Person` object serialized as JSON would meet this criteria.

If there are errors in the model binding process, you may or may not be made aware of them. No exceptions will be thrown if an error occurs, but setting the property values will fail.  It is important to validate your incoming parameters by inspecting the `ModelState.IsValid` property of the controller for any framework-provided errors and taking appropriate action.

> Model validation is an important step in processing a request. You may be interested in further reading on the `DataAnnotations` namespace, which contains a number of attributes you can decorate your model class with to denote requirements for parameters. These can further enhance and simplify your validation process. 

## Some Tips on Model Binding

Values that are set earlier (such as those that match a route token) will be overwritten by those that may be evaluated later (such as those that appear in part of the `BODY`), but you can instruct the default model binder to source its data from an explicit location simply by adding parameter attributes to the signature. For instance, if you want only to populate the `Person` object from the request `BODY` you can use an attribute like so:

```c#
public IActionResult Edit(int id, [FromBody] Person person)
{
    // ...
}
```

Likewise, if you wanted to strictly allow the ID of an object to be set by a routing token, and not overwritten by a later binding operation, you would need to be explicit about that as well. If your incoming URL looked like the following:

    http://www.alpineskihouse.com/peeps/edit/21

...you could then update your signature to include the `[FromRoute]` parameter attribute to have the `id` populated through the route token, and not overwritten by other values.  The end result could end up looking like the following:

```c#
public IActionResult Edit([FromRoute] int id, [FromBody] Person person)
{
    // ...
}
```

The default model binder is pretty good at figuring out things like lists as well. For example, we could be asking the user who their closest friends are. Imagine a list of checkboxes on a page that had the following markup:

```html
<input type="checkbox" name="friend" value="Stephen">Steve
<input type="checkbox" name="friend" value="Peter">Peter
<input type="checkbox" name="friend" value="Mark">Mark
<input type="checkbox" name="friend" value="James">Jimmy
```

With those fields wrapped in a form element and submitted to an action, we could have the following code in place for the model binder to give us the data:

```c#
public IActionResult SetClosestFriends(List<string> friends)
{
    // ...
}
```

In the above example, each friend selected in a checkbox and submitted to the action will be in the `friends` parameter.

Another point to make with the binding process is that you essentially get one opportunity to bind parameter values using any part of the `BODY`. The `BODY` is a stream that can be read from, but you cannot rely on being able to rewind the stream and re-read it. For this reason, a maximum of one parameter may contain a binding attribute that reads from the `BODY` of the request.

## Next Steps

 - Review the [routing](routing.md) concepts to understand how to extract tokens that can be used in the model binding process.
 - Have a look again at [configuration](configuration.md) or read ahead into [dependency injection](controller-dependencies.md) if you are interested in getting environment- or application-level data into your controller.
 - Experiment with the sample project to see the different model binding in action. Be sure to set breakpoints so that you can inspect the values of the parameters as your action is entered.



