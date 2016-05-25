# .NET Training and Tutorials

This repository holds the content used to build the .NET getting started tutorials hosted at http://dot.net

## Organization

The content is broken up into high level groups by topic, initially covering
- [C#](c#/readme.md)
- [ASP.NET](asp.net/readme.md)

Within each content area, there are one or more *tutorials*.

Each tutorial consists of *lessons*. Each lesson should be a page detailing the concept being taught, along with sample code. Lesson and page may be used interchangeably when describing the tutorials. Lessons may be broken up into several *steps*. Each step and lesson should end with working code the user can run using a REPL, Visual Studio, or Visual Studio Code.

Lessons will include *tips* that will be tagged according to reader background, and eventually these will be displayed to readers who have indicated they have that background. For example:

### Tip (tags: vb)
> The *static* keyword in C# is equivalent to *Shared* in Visual Basic .NET

Lessons will be written using [markdown (Github variant)](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet). Lessons should link to other documents for specific tasks and API references. These should focus on helping the user follow through and understand the tutorial.

## Includes / Shared Elements

The ultimately publishing platform capabilities and requirements aren't yet known. Until they are, we don't have a system in place for includes or for sharing common elements across tutorials/pages. For now, we'll need to copy/paste.

## Branching Strategy

The *master* branch will contain the latest approved content. In the short term, approved content authors will be permitted to push directly to master. However, once there is a significant amount of content, authors will be expected to work on their content in their own feature branch, and then submit a pull request and await a :shipit: from another team member.



