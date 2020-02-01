# Setting up a Development Environment on Linux

## Setting up the Database

 1. Install [Postgres][4], the recommended way is to use your package manager.

 2. Login as 'postgres' user, this can be done by executing `sudo -iu postgres`. The name could be
    different for you, but the convention is that the user is called 'postgres'.

 3. Create a new user by executing `createuser codidact`, you can choose any name here.

 4. Create a new database by executing `createdb --owner codidact codidact`, you can choose
    any name here.

 5. Prepare a connection string that will be used later. The format is
    `Host=localhost;Database=X;Username=X`. If you honored the suggestions the connection
    string would be `Host=localhost;Database=codidact;Username=codidact`.

 6. You can logout of the shell using `logout`.

## Setting up the Repository

 1. [Fork the repository on GitHub][5] and clone it into a local directory.

 2. Navigate to that directory.

 2. (optional) Add an upstream for the canonical repository this can be done using
    `git remote add upstream git://github.com/codidact/core`. This can be very helpful for rebasing.

## Installation using Visual Studio Code

 1. Install the [.NET Core SDK][2] you need version 3.1, some package managers have a package for this.
    *Note. On Arch Linux you need to install both the `dotnet-sdk` and `aspnet-runtime` packages,
    other package managers might divide this functionality too.*

 2. Install [Visual Studio Code][3]. You need the official version from Microsoft, the debugger
    for C# isn't included in packages from your package manager.

The following instructions are expressed very verbosely, many things can be done quicker with
shortcuts.

 3. Go to `View > Extensions` and install the `ms-vscode.csharp` extension. The extension will
    automatically start installing dependencies.

 4. Go to `File > Open Folder...` and open the directory where you setup the repository. There might
    be some popups messages, ignore them and use the instructions below, this will ensure that we are
    all on the same page.

 5. Go to `View > Command Palette...` and execute the `.NET: Generate Assets for Build and Debug` command.
    You will be prompted to select a project, select `Codidact.WebUI`.

 6. New files should appear now, namely `.vscode/launch.json` and `.vscode/tasks.json`. These determine
    how the project is built and launched. The default options are really good, however, I would
    recommend tinkering until they match your personal preferences. Especially the option
    `logging.moduleLoad` is worth looking into, refer to this [Stack Overflow question][1].

 7. More configuration can be done in `src/WebUI/Properties/launchSettings.json`, the defaults work though.

 8. Add your connection string to `src/WebUI/appsettings.Development.json`. This is how it could look like:

    ~~~json
    {
        "Logging": {
            "LogLevel": {
                "Default": "Debug",
                "Microsoft": "Warning",
                "Microsoft.Hosting.Lifetime": "Information"
            }
        },
        "ConnectionStrings": {
            "DefaultConnection": "Host=localhost;Database=codidact;Username=codidact"
        }
    }
    ~~~

 9. You can launch the project by going to `Debug > Start Debugging`; a web browser should automatically open.

10. In order to get IntelliSense (that is code completion as you type), go to `View > Command Palette...` and execute
    `.NET: Restore All Projects`.

11. It's currently not possible to run the tests from the editor directly, refer to the instructions without
    Visual Studio Code.


## Installation without Visual Studio Code

 1. Install the [.NET Core SDK][2] you need version 3.1, some package managers have a package for this.

 2. Navigate to the directory where you setup the repository.

 3. Add your connection string to `src/WebUI/appsettings.Development.json`, refer to the instructions
    with Visual Studio Code.

 4. Change the configuration options in `src/WebUI/Properties/launchSettings.json` if you like, the
    defaults work though.

 3. You can launch the application by executing `dotnet run` in the `src/WebUI` directory.

 4. You can run the tests by executing `dotnet test` in the root directory.

  [1]: https://stackoverflow.com/q/55683834/8746648
  [2]: https://dotnet.microsoft.com/download/dotnet-core/3.1
  [3]: https://code.visualstudio.com/
  [4]: https://www.postgresql.org/download/
  [5]: https://github.com/codidact/core
