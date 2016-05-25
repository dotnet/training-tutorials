# Writing Your First C# Program
by [Steve Smith](http://deviq.com/me/steve-smith)

## Getting and Installing .NET

If you haven't already installed .NET, you'll need to do so. .NET runs on Windows, Linux, and Mac and can be used to build apps on mobile platforms including iOS and Android. To get started, navigate to http://dot.net and download the appropriate .NET Core SDK Installer for your system.

You can also [download the tools](https://www.microsoft.com/net/download#tools) needed to get started, including free tools like Visual Studio Community, Visual Studio Code, and Xamarin Studio.

## Creating an Application Using the Command Line Interface (CLI)

Open a console or terminal window and verify that you have access to the .NET Command Line Interface (CLI) by typing

> dotnet

You should see something like the following:

> Usage: dotnet [--help | app.dll]

If instead you see a message like:

> 'dotnet' is not recognized as an internal or external command, operable program, or batch file.

Then you should verify that you have installed the .NET Core SDK on your system, and confirm that the ``dotnet`` application is in your PATH.

Assuming you were successful, navigate to a new folder where you could like to work, such as ``dev``, and create a new directory, ``hello-world``. Change to that directory. Now run:

> dotnet new

You should see output like the following:

> Created new C# project in {your folder}.




## Creating an Application Using Visual Studio

## Creating an Application Using Visual Studio Code

