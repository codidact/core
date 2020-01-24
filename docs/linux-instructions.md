# Setup a Development Environment on Linux

## Requisites

 1. Install the [.NET Core 3.1][2] framework, make sure to check if your package manager ships this version.

 2. Install [Postgres 12 or newer][4].

 3. (optional) Install [Visual Studio Code][3], this is a text editor which works really well with C#
    on Linux.

## Setup the Database

 1. Login as `postgres` user, this is usally done by executing `sudo -iu postgres`.

 2. Create a new user by executing `createuser codidact` you can choose any name here.

 3. Create a new database by executing `createdb --owner codidact codidact` you can choose any name here.

 4. Perpare a connection string by inserting your parameters into the format
    `Host=localhost;Database=<database>;Username=<username>`, this is needed later. In the example above,
    the connection string would be `Host=localhost;Database=codidact;Username=codidact`.

 5. You can logout using `logout`.

## Setting up the Repository

The following instructions include a few optional steps, they are by no means neccessary, or even
recommended in some cases.

 1. [Fork the repository on GitHub][5] and clone it into a local directory. The following instructions
    asume that you are in that directory.

 2. (optional) Add an upstream for the canonical repository this can be done using
    `git remote add upstream git://github.com/codidact/core`. This can be very helpful for rebasing.

## (optional) Setting up Visual Studio Code

The following instructions are made very explicit for clarity, many of the steps can be done using
shortcuts or by running a command directly.

 1. Go to `View > Extensions` and install the `ms-vscode.csharp` extension.

 2. Go to `File > Open Folder...` and open the directory into which you cloned your fork. There might
    be some popups messages, ignore them and use the instructions below, this will ensure that we are
    all on the same page.

 3. Go to `View > Command Palette...` and execute the `.NET: Generate Assets for Build and Debug` command.
    You will be prompted to select a project, select `Codidact.WebUI`.

 4. New files should appear now, namely `.vscode/launch.json` and `.vscode/tasks.json`. These determine how
    the project is build and launched. The default options are really good, however, I would recommend tinkering
    until they match your personal preferences. Especially the option `logging.moduleLoad` is worth looking into,
    refer to this [Stack Overflow question][1].

 5. More configuration can be done in `src/WebUI/Properties/launchSettings.json`, the defaults work though.

 6. Add your connection string to `src/WebUI/appsettings.Development.json`. This is how it could look like:

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

 7. You can run and debug the project by going to `Debug > Start Debugging`; a web browser should automatically open.

 8. In order to get IntelliSense (that is code completion as you type), go to `View > Command Palette...` and execute
    `.NET: Restore All Projects`.

 9. It's currently not possible to run the tests from the editor directly, refer to the instructions for the command line.

## (optional) Running the Project from the Command Line

The following instructions assume that you are in the directory into which you cloned your fork.

 1. Add your connection string to `src/WebUI/appsettings.Development.json`, refer to the instructions for Visual Studio
    Code.

 2. Change the configuration options in `src/WebUI/Properties/launchSettings.json` if you like, the defaults work though.

 3. You can launch the application by executing `dotnet run` in the `src/WebUI` directory.

 4. You can run the tests by executing `dotnet test` in the root directory.

  [1]: https://stackoverflow.com/q/55683834/8746648
  [2]: https://dotnet.microsoft.com/download/dotnet-core/3.1
  [3]: https://code.visualstudio.com/
  [4]: https://www.postgresql.org/download/
  [5]: https://github.com/codidact/core
