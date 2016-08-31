# Injecting Dependencies into Controllers
by [James Chambers](http://jameschambers.com)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Completed Version (Pending)] - Includes the completed versions of all MVC tutorials

## Using Dependencies in Your Controllers

## The Explicit Dependencies Principle 
Show how Controllers can follow Explicit Dependencies Principle and request their dependencies via their constructor.

## Configuring Your Application
Show how to configure services in ConfigureServices (going beyond what's in lesson 8).

## Using a Service In Your Controllers
Demonstrate how to use a configured service to perform some work for a view-based controller...

## Using a Service In Your API
...and for an API method.

## Next Steps
Note that dependencies should be small and focused on only UI concerns. Business logic should go into other classes and should be injected. This makes both controllers and business classes easier to test (see next lesson).

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.