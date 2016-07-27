# Explore F#

by [Microsoft Research](https://www.microsoft.com/en-us/research/)


"***F# developers regularly solve problems in days that would take weeks using more traditional languages...***" Finance Trading Company

F# is a strongly-typed, functional-first, open-source, cross-platform programming language for writing simple code to solve complex problems. From the business perspective, the primary role of F# is to reduce the time-to-deployment for analytical software components in the modern enterprise. Its interoperability with other languages and libraries and its ability to tackle the complexity of components such as calculation engines and data-rich analytical services offer a compelling story for businesses.

F# is both open-source under the OSI-approved Apache 2.0 license and a first-class language in Visual Studio. It is cross-platform and can be used on Mac OS X, Linux, Android, HTML5 and other platforms.

## Why F#?

**Simple Code for Complex Problems**
F# is expressive and concise, which allows developers to implement their algorithms more directly. This means less code to read and maintain.

**Rapid Prototyping**
Using F# Interactive, code can be executed immediately without first compiling, which enables fluid problem exploration. Developers can use F# Interactive to iteratively refine algorithms to production quality.

**Fewer Bugs**
Case studies and user reports consistently show that F#'s strong type system reduces software bugs. Units of Measure further increase these benefits by preventing code from accidentally combining such elements as inches and centimeters, dollars and euros, or any custom units. 

**Seamless Interoperability**
F# interoperates seamlessly with C#, and F# can be used with HTML5, JavaScript and other web standards. F# type providers can be used to integrate data sources and thousands of statistical libraries from packages such as R. The NuGet package environment provides over 8,000 new packages. Enterprises can use F# effectively without having to use different libraries and frameworks, and can leverage their existing assets and domain knowledge.

**Efficient Execution**
F# features modern, high-performance just-in-time (JIT) compilation to native code. F# code runs unchanged on both 32-bit and 64-bit systems by using the instructions available on the target architecture. The resulting code runs at the speeds much faster than languages such as Python, JavaScript or R, and in some cases significantly faster than C#.

**Reduced Complexity**
F# makes it easier to write functional programs, which eliminates complex time and state dependencies. This helps prevent bugs, makes unit testing more straightforward, simplifies refactoring, and promotes code reuse.

**Information-Rich Programming with F# 3.0**
"*The idea of integrating internet-scale information services directly into the program's variable and type space that is in F# 3.0 is probably the most innovative programming language idea I have heard of in a decade.*", -Dr. James R. Cordy, Queen's University, Canada

A growing trend in the theory and practice of programming is the interaction between programming and rich information spaces. From databases and web services, to the semantic web and cloud-based data, the need to integrate programming with heterogeneous, connected, richly structured, streaming, and evolving information sources is ever-increasing.

The new type provider mechanism in F# 3.0 makes F# a uniquely extensible and adaptable data-rich language. Using type providers, F# programmers can integrate a wide range of information spaces and emerging industry data standards. Over time, we expect to see a number of new type provider projects samples, community projects, internal enterprise utilities, or products which each configure F# for use in a particular domain. So, not only would you have a near-seamless programming experience using SQL, OData and WSDL sources (to name a few), but anyone could create type providers that work against NoSQL data, semantic web data stores, social network graphs, scientific data repositories, data markets, SharePoint, WMI, streaming data, and high-performance cloud data stores. To learn more, [read the F# 3.0 white paper from Microsoft Research](research.microsoft.com/pubs/173076/information-rich-themes-v4.pdf).

## Learning F#

### F# Books & Tutorials
To learn more about F# or to teach it, you can find more information in one of the existing F# [books](https://www.amazon.com/s/ref=sr_nr_scat_5_ln?h=965d0cc01d9489d2e79c265a798062a0729bd03d&ie=UTF8&keywords=f%23&qid=1345236584&rh=n%3A5%2Ck%3Af%23&scn=5) or other available online tutorials. There is an increasing selection of F# books becoming available all the time.
There are also many F# tutorials available online that complement the interactive Try F# tutorials available on this web site. Find out more on the Learn F# page at F# Developer Center .

### F# Screencasts & Talks
Here are a number of talks and screencasts that will help you learn more about F#.

[Webinar: Try F# for Big and Broad Data](https://www.microsoft.com/en-us/research/video/webinar-try-f-for-big-and-broad-data/)

*Christophe Poulain, Don Syme, Evelyne Viegas and Kenji Takeda, Microsoft Research*

This webinar shows how to use the Try F# tutorials to solve real-world scenarios, including analytical programming, and information-rich programming that is encountered in finance and data science. At the end of the webinar, viewers will be able to bring the web of data to their fingertips through type providers, write code in the browser, and share it with the rest of the community.

[Functional first programming in an information-rich world](https://www.microsoft.com/en-us/research/video/phd-functional-first-programming-in-an-information-rich-world/)

*Kenji Takeda, Microsoft Research*

In this talk we explore how the design and features of the F# language provides developers with ways to elegantly make use of the world of typed data and services on the web, cloud and in the enterprise.

[Teaching F#: From numerical expressions to 3D graphics](http://www.viddler.com/v/aca9839f)

*Tomas Petricek, University of Cambridge*

The talk discusses how to teach the essential functional concept - composability. It starts with an easy to understand example, numerical expressions, and then moves to more exciting examples using a library for composing 3D graphics.

[Teaching programming language concepts with F#](https://channel9.msdn.com/Tags/peter-sestoft)

*Peter Sestoft, ITU Copenhagen, Denmark*

In the lecture, Peter introduces the curriculum, lecture plan and lecture notes for the course "Programs as data" that uses the functional programming concepts in F# to teach students programming language concepts and implementation details.

[F# 3.0 - Information Rich Programming](https://channel9.msdn.com/Blogs/Charles/C9-Lectures-Donna-Malayeri-F-30-Information-Rich-Programming-1-of-1)

*Donna Malayeri, F# Program Manager*

F# 3.0 is the first language to bring integrated support for Information Rich Programming. F# Type Providers and F# Queries greatly simplify data-rich analytical programming, allowing programmers to easily access and manipulate a variety of data sources. This lecture introduces these exciting new features and how they can be used to leverage technologies such as OData, WSDL services, and Windows Azure Marketplace.

[Introduction to F# (C9 Lectures)](https://channel9.msdn.com/Blogs/David+Gristwood/An-Introduction-to-F-with-Don-Syme-1-of-4)

*Don Syme, MSR Cambridge, UK*

Three part series of introductory video lectures by Don Syme, a leading contributor to the F# language. Don introduces functional concepts such as functional data structures and pattern matching, imperative features of F#, and the F# object model.

## Installing F#

To use F# outside of this learning environment choose from the following options:

### On Windows 7, Windows 8 and Azure
F# is a first-class language in Visual Studio, a powerful integrated development environment that supports many languages, including C# and JavaScript. You can download a free Express edition of Visual Studio and add F# to it. You can also download a full free trial of Visual Studio to get started. If you are a student or academic, then you can also get Visual Studio via the Dreamspark program.

### On Mac OS X, Linux, Android, iOS and GPGPU
For using F# on these platforms, see the instructions provided by The F# Open Source Group.

Contributing to F#
------

F# has a vibrant and active community who contribute to many aspects of the language and who actively develop its cross-platform capabilities, tools and libraries. From local meetups, user groups, discussion forums and code sharing sites, it's easy to get involved.

You can actively contribute to F# by creating F# code on this site and sharing it with your friends. You can also contribute to F# as a cross-platform language by joining The F# Open Source Group and helping to develop the cross-platform capabilities and tools for the language. Further F# communities are:

* The F# Software Foundation
* The F# Open Source Group
* Microsoft F# forum
* F# on Stack Overflow
* #fsharp on Twitter
* F# on DeveloperFusion
* F# Community Samples
* F# Snippets
* NuGet packages related to F#
* CodePlex packages related to F#
* GitHub packages related to F#
