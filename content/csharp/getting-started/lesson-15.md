# Encapsulation and Object-Oriented Design
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## What is Encapsulation? 

*Encapsulation* is a fundamental concept in computer science and programming. At its core, encapsulation is simply "information hiding", but that doesn't convey the reasoning behind the practice. By hiding information about the inner workings of a software construct, you force collaborators to work only with the construct's exposed interface. How work is done withing the construct is a "black box", and as a result, the inner workings are free to change without disrupting collaborators provided the external interface (and associated behavior) are not changed.

You achieve encapsulation in your object-oriented programs primarily through the use of object design and accessibility modifiers, which you learned about in the [previous lession](lesson-14.md). Your program would have no encapsulation if the entire thing resided in a single method. By breaking functionality out into separate, focused methods and classes, and controlling how these methods access their classes' state through accessibility modifiers, you can achieve good encapsulation in your program's design.



## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.