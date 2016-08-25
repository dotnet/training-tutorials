# Sending Data to Controllers
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

See the [Issue](https://github.com/dotnet/training-tutorials/issues/62) to claim this lesson and/or view what should be included in it.

## First Header

Getting data to Controllers would be things like DI and configuration. Reference this and mention that DI is coming in next article.

More specifically, this lesson is about getting data to actions within a controller, and as such should focus primarily on Model Binding. Discuss how the action can get access (either through its model bound parameters, or otherwise) to request data from any of these:

Querystring
POST data
URL/routing tokens (e.g. /{id?} )
Also talk about things like [FromBody] attribute and when it's required. Should be scenario-based, not just a repeat of the docs on model binding, though.



## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.