# Working with the dotnet Command Line Interface (CLI)
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## dotnet

In the [previous lesson](lesson-01.md), you used the ``dotnet`` command line interface (CLI) to create, build, and run your first ASP.NET Core app. In this lesson, you'll learn a little more about this tool.

To get started, open a new console window and type:

    > dotnet --help

You should see something like the following:

    > dotnet --help
    .NET Command Line Tools (1.0.0-preview1-002702)
    Usage: dotnet [common-options] [command] [arguments]

    Arguments:
    [command]     The command to execute
    [arguments]   Arguments to pass to the command

    Common Options (passed before the command):
    -v|--verbose  Enable verbose output
    --version     Display .NET CLI Version Number
    --info        Display .NET CLI Info

    Common Commands:
    new           Initialize a basic .NET project
    restore       Restore dependencies specified in the .NET project
    build         Builds a .NET project
    publish       Publishes a .NET project for deployment (including the runtime)
    run           Compiles and immediately executes a .NET project
    test          Runs unit tests using the test runner specified in the project
    pack          Creates a NuGet package

As you can see, the ``dotnet`` tool supports a number of commands. You've worked with several of these already. Each of these commands can also be called with the ``--help`` argument.

### dotnet new

The ``new`` command is used to create new .NET projects or applications:

    > dotnet new --help
    .NET Initializer

    Usage: dotnet new [options]

    Options:
    -h|--help             Show help information
    -l|--lang <LANGUAGE>  Language of project [C#|F#]
    -t|--type <TYPE>      Type of project

As you can see, you can use this command to create a number of different kinds of projects, using either C# or F#. Valid C# project types include:

- Console
- Web
- Library
- xUnit Test

**Note**: The current version of the ``dotnet`` CLI only supports console applications.

### dotnet restore

The ``restore`` command uses NuGet to restore dependencies and project-specific tools that are defined in the project file.

    > dotnet restore --help

    Usage: nuget3 restore [arguments] [options]

    Arguments:
    [root]  List of projects and project folders to restore. Each value can be: a path to a project.json or global.json file, or a folder to recursively search for project.json files.

    Options:
    -h|--help                       Show help information
    --force-english-output          Forces the application to run using an invariant, English-based culture.
    -s|--source <source>            Specifies a NuGet package source to use during the restore.
    --packages <packagesDirectory>  Directory to install packages in.
    --disable-parallel              Disables restoring multiple projects in parallel.
    -f|--fallbacksource <FEED>      A list of packages sources to use as a fallback.
    --configfile <file>             The NuGet configuration file to use.
    --no-cache                      Do not cache packages and http requests.
    --infer-runtimes                Temporary option to allow NuGet to infer RIDs for legacy repositories
    -v|--verbosity <verbosity>      The verbosity of logging to use. Allowed values: Debug, Verbose, Information, Minimal,
    Warning, Error.
    --ignore-failed-sources         Only warning failed sources if there are packages meeting version requirement

As you can see from the "Usage:" line above, the ``restore`` command is simply calling out to the ``nuget`` executable, which has the options shown here. Typically, you won't need to modify these options. However, if you're using a custom package source, either because you're working with pre-release versions of Microsoft libraries or your organization is using its own package source, you may need to specify a package source using the ``-s`` argument.

Running ``dotnet restore`` produces a lock file (*project.json.lock*) which includes detailed information about all of the packages that were restored.

### dotnet build

The ``build`` command will build a project and all of its dependencies into a binary. By default, the binary will be in Intermediate Language (IL) and will have a ``.dll`` file extension. Building requires the existence of a lock file (produced by the ``restore`` command, above).

In order to build an executable application, you need to ensure the project is configured with ``buildOptions`` set to emit an entry point:

```json
  "buildOptions": {
    "emitEntryPoint": true
  },
```

Running help on the ``build`` command yields:

    > dotnet build --help
    .NET Builder

    Usage: dotnet build [arguments] [options]

    Arguments:
    <PROJECT>  The project to compile, defaults to the current directory. Can be one or multiple paths to project.json, project directory or globbing patter that matches project.json files

    Options:
    -h|--help                           Show help information
    -o|--output <OUTPUT_DIR>            Directory in which to place outputs
    -b|--build-base-path <OUTPUT_DIR>   Directory in which to place temporary outputs
    -f|--framework <FRAMEWORK>          Compile a specific framework
    -r|--runtime <RUNTIME_IDENTIFIER>   Produce runtime-specific assets for the specified runtime
    -c|--configuration <CONFIGURATION>  Configuration under which to build
    --version-suffix <VERSION_SUFFIX>   Defines what `*` should be replaced with in version field in project.json
    --build-profile                     Set this flag to print the incremental safety checks that prevent incremental compilation
    --no-incremental                    Set this flag to turn off incremental build
    --no-dependencies                   Set this flag to ignore project to project references and only build the root project

You can use the ``-f`` option to specify a particular framework you want to compile for. This framework must be defined in the project file. 

The ``-c`` option lets you specify a configuration to use. It defaults to ``Debug`` but you can specify ``Release``.

### dotnet run

Most of the time, you can skip explicitly building your application, and just use ``dotnet run`` to run it. This will build the application if changes have occurred since it was last built, and either way will run the application. Note that ``dotnet run`` can only be used in the context of projects, not assemblies.

    > dotnet run --help
    .NET Run Command

    Usage: dotnet run [options] [[--] <arg>...]]

    Options:
    -h|--help           Show help information
    -f|--framework      Compile a specific framework
    -c|--configuration  Configuration under which to build
    -p|--project        The path to the project to run (defaults to the current directory). Can be a path to a project.json or a project directory

### dotnet [assemblyname]

You can run a compiled assembly by simply running ``dotnet path/to/assemblyname.dll`` (no need for the ``run`` command).

    > dotnet .\bin\Debug\netcoreapp1.0\lesson2.dll
    Hello World!

If you have compiled binaries you want to run, this is a quick way to execute them.


## Next Steps

Give the reader some additional exercises/tasks they can perform to try out what they've just learned.