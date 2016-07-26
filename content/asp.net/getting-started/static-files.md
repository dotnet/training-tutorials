# Serving Static Files
by [James Chambers](http://jameschambers.com)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples


## Basics of Static Files

You might be wondering why one would want to use static files in an app that dynamically generates its output. Static files are the assets in your project that do not typically change from request to request. In fact, these won't likely change between releases. While a dashboard view, a user's profile or the current state of a shopping cart might require constant processing and rendering, other items in a typical app will not.

Your company's logo, the styles that are applied to elements on your page and scripts that are used in your views are all examples of these types of assets. These are files that can be returned directly to the client to assist the browser in rendering the page and they do not require additional processing from your application.

Furthermore, a request for these types of applications would not end up with the response you might expect. You learned in a [previous lesson](middleware-basic.md) about the basic middleware that is used to produce an execution pipeline for your MVC app; that pipeline uses concepts like [routing](routing.md) and [filters](filters.md) to process the request to determine the appropriate controller and action to generate a result. CSS, JavaScript and other static file types require no such processing and therefore generate no such response when handled by the MVC middleware.


## Adding Static Files Middleware to Your App

## Implications of the Static Files Middleware 

Remember from the [middleware tutorial](middleware-basic.md) that the order in which you compose your execution pipeline is significant. The MVC middleware is "greedy" in that it captures the request and does not invoke futher middleware: it is the end of the chain. For this reason, you must put the static file middleware before MVC or other middleware that is request-terminating.

When you start to add in things like identity or other middleware you may be introducing additional load on your server as the request must be evaluated at each step. By optimizing the order of your pipeline you can remove that load and improve the performance of your application.

The static file middleware will pass off any request that does not match a file path on disk to the next component in the pipeline very quickly, so it is advised to put it fairy early in the chain. Not all static files should



## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.