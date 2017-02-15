# Testing ASP.NET Core Apps
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## Installing a Test Framework

The first thing you need to do to start testing your ASP.NET Core apps is to install a test framework. If you're using the dotnet CLI, you can create a new test project like this:

```
dotnet add xunittest
dotnet add mstest
```

These two commands will create an xunit or mstest project, respectively. You can then edit this project to reference your ASP.NET Core application. In this lesson, you'll learn how to write tests for your ASP.NET Core app using [xUnit](https://xunit.github.io/), which is the same test framework used on ASP.NET Core itself. If you already have a project configured that you'd like to use for your tests, you should add the *xunit* and *xunit.runner.visualstudio* packages. The latter test runner package will also work from the dotnet command line interface (using `dotnet test`).

> **Note** {.note}    
> You don't have to put your tests in a separate project, but that's usually how you'll want to organize your code in production apps. While you're learning, if you put tests in your actual web application, it should still work fine.

Once you have a test framework intalled, write your first test. The `dotnet new xunittest` command generates a very simple test to get you started:

```c#
using System;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
    }
}
```

By default, this test passes because it has no failing assertions and it doesn't throw an exception. Thus, if you run `dotnet test` on a project with this test in it (and the xunit packages mentioned above), you should see output like this:

```
Microsoft (R) Test Execution Command Line Tool Version 15.0.0.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
Passed   Tests.UnitTest1.Test1

Total tests: 1. Passed: 1. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 2.3188 Seconds
```

## Testing Business Services

One of the benefits of following the [Dependency Inversion Principle](http://deviq.com/dependency-inversion-principle/) (and the [Explicit Dependencies Principle](http://deviq.com/explicit-dependencies-principle/)) is that your services should be fairly easy to unit test. In the [previous lesson](controller-dependencies.md) the sample used a service, ``AddressVerificationService``. You can write unit tests for this service independent of how you're calling it from your ASP.NET Core app. These are *unit* tests because they do not depend on any code other than the code in the method being tested. Infrastructure concerns, like data access or file systems, are specifically excluded from unit tests (but may be valid as part of *integration* tests).

TODO: unit test the AddressVerificationService

## Unit Testing a Controller Action

You can write unit tests for controller actions, just like for other classes. However, unit tests that simply instantiate a controller and call its action will not run through the MVC pipeline. Such tests will not be able to verify routing is correct, or test the presence of middleware or filters. Since most controller actions should have a bare minimum of logic in them, with much of the cross-cutting concerns extracted and any business logic delegated to services, there should be little need to *unit* test controller actions. They benefit most from integration tests that run through the full MVC stack. Fortunately, ASP.NET Core ships a test server that makes hosting the full ASP.NET Core MVC stack for test purposes very easy.

TODO: show a unit test on an action method, and how it doesn't test routes or attributes/filters

## Configuring a Test Server

A Tes

## Testing Simple Middleware-Based ASP.NET Core Apps

TODO: Write an integration test for a simple hello world ASP.NET Core app

## Testing a Controller Action

TODO: Write an integration test for an API action method

## Next Steps

Write some additional tests to confirm the behavior of the applications shown is what you expect it to be.