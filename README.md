# .NET Training and Tutorials

This repository holds the content used to build the .NET getting started tutorials hosted at http://dot.net

## Organization

The content is broken up into high level groups by topic, initially covering
- [C#](content/csharp/README.md)
- [ASP.NET](content/asp.net/README.md)
- [F#](content/fsharp/README.md)

Within each content area, there are one or more *tutorials*.

Each tutorial consists of *lessons*. Each lesson should be a page detailing the concept being taught, along with sample code. Lesson and page may be used interchangeably when describing the tutorials. Lessons may be broken up into several *steps*. Each step and lesson should end with working code the user can run using a REPL, Visual Studio, or Visual Studio Code.

You can view the C# content as a model for other content areas. Note that URLs/links should use 'csharp' in place of C# because GitHub will convert the latter to C%23 resulting in broken links. Also note, links to README.md files are case-sensitive within GitHub's web view.

Lessons will include *tips* that will be tagged according to reader background, and eventually these will be displayed to readers who have indicated they have that background. For example:

> **Tip** {.tip .vb}
> The *static* keyword in C# is equivalent to *Shared* in Visual Basic .NET

Lessons will also include *notes*, which will receive special formatting when displayed to readers. For example:

### Note {.note}
> It is common to log exceptions that occur, so that you can review them later and improve the program to avoid them, if possible.

Lessons will be written using [markdown (Github variant)](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet). Lessons should link to other documents for specific tasks and API references. These should focus on helping the user follow through and understand the tutorial.

## Includes / Shared Elements

The ultimately publishing platform capabilities and requirements aren't yet known. Until they are, we don't have a system in place for includes or for sharing common elements across tutorials/pages. For now, we'll need to copy/paste.

## Branching Strategy

The *master* branch will contain the latest approved content. In the short term, approved content authors will be permitted to push directly to master. Once there is a significant amount of content, however, authors will be expected to work in their own feature branch before submitting a pull request and awaiting a :shipit: from another team member.

## Contributing
We welcome contributions and corrections to these tutorials. We will build out a roadmap shortly on the areas that we are looking to address next but send us your ideas. When contributing the steps are:

 1. Fork this repo and clone your fork locally
 2. Create a new branch for your contribution
 3. Do your awesomeness, commit and push to your fork
 4. Create a pull request from it to the master branch of https://github.com/dotnet/training-tutorials
 5. Leave some handy comments

We ask that authors of significant changes sign the [.NET Foundation Contribution License Agreement](https://cla.dotnetfoundation.org). This project has adopted the code of conduct defined by the [Contributor Covenant](http://contributor-covenant.org/)
to clarify expected behavior in our community.
For more information see the [.NET Foundation Code of Conduct](http://www.dotnetfoundation.org/code-of-conduct).

### .NET Foundation
This project is supported by the [.NET Foundation](http://www.dotnetfoundation.org).




