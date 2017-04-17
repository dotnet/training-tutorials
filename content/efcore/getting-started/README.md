# Getting Started using Entity Framework Core
by [The Raikes School](http://cse.unl.edu/raikes-school)

## What is Entity Framework Core?

Entity Framework Core (EF Core) is a lightweight, extensible, and cross-platform version of Entity Framework. Entity Framework is an object-relational mapper (O/RM) that enables .NET developers to work with a database using .NET objects. This allows developers to create and manage databases without most of the data-access code that developers usually need to write. 

As a lightweight version of the original Entity Framework, EF Core does not have all the features available in the latest version of Entity Framework (EF6.x). However, EF Core does offer a few features EF6.x does not. For more on these differences, check out [the docs](https://docs.efproject.net/en/latest/efcore-vs-ef6/features.html). If you have further questions about which version is best for your project, visit ["Which One Is Right for You?"](https://docs.efproject.net/en/latest/efcore-vs-ef6/choosing.html).

## Who is this tutorial meant for?

This tutorial is meant for those new or unfamiliar to EF Core, but if you already have an understanding of EF Core, consider this a refresher course. This tutorial will also help you navigate EF Core if you are familiar with previous versions of EF. For those more experienced EF Core programmers, [the docs](https://docs.efproject.net/en/latest/) may be of more use to you. The docs feature higher level EF Core concepts that this tutorial does not.

If you know what you're looking for, feel free to browse by topic on the side. If not, welcome! Let's get started together!

## Prerequisites for this tutorial

This tutorial assumes a knowledge of other programming concepts, including: 

* Basic programming concepts 
* C# (see the [interactive C# tutorial](https://www.microsoft.com/net/tutorials/csharp/getting-started))
* Relational database concepts
	- SQL-type databases will be the focus of this tutorial, though other databases function with EF Core
* LINQ statements (see this [interactive tutorial](https://www.microsoft.com/net/tutorials/csharp/getting-started/linq) for an introduction to LINQ) 

If you're a new to these topics, you can check out their tutorials. If you're only a little rusty, we'll remind you of the relevant aspects along the way.

Now, let's get started!