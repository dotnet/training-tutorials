# Defining and Calling Methods
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## What are Methods?

So far in this tutorial, you've only been working within one method of your own, the ``Main`` method of a console application. However, you've called many other methods in the course of completing the lessons so far and the associated exercises. A method is a function that is associated with a class. A function is a named series of statements in a program. Functions (and therefore methods) can optionally accept parameters, and they can optionally return a result.

## Declaring Methods

There are two main kinds of methods in C#: *static* methods and *instance* methods. A static method is global to the program, and is called off of the type it is associated with. You learned about these kinds of methods in [lesson 2](lesson-02.md), since the ``Main`` method you've seen so much of is static. Instance methods are attached to an object instance. They can only be called if they are invoked from a non-null instance of an object of the appropriate type. Two objects of the same type will run the same code for a given instance method, but each method will have access to that object's internal state, so the results may be very different.

If you're writing a method that will only operate on the parameters being passed into it, you can typically declare it as a ``static`` method. Otherwise, your methods should be instance methods (which is the default if you don't add the ``static`` keyword).

## Naming Methods

## Return Types

## Parameters

## Overloads


## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.