# Setting up a Development Environment on Windows

## Setting up the Database

 1. Install Postgres, the recommended way is to use [the EnterpriseDB installer][5]. You need version
    12 or newer.

 2. Start "pgAdmin", a web interface should open.

 3. Create a new user, this can be done by right-clicking on `Servers > PostgreSQL > Login/Group Roles`
    and selecting `Create > Login/Group Role...`. You can choose any name, the instructions assume that
    you choose "codidact". Make sure to select "Can login?" in the "Privileges" tab. It is necessary to
    add a password to this account, do this in the "Definition" tab, the instructions assume that you choose
    "codidact". *(Providing a password isn't necessary on Linux, but on Windows it is.
    If you find a way to bypass this requirement, please create an issue.)*

 4. Create a new database, this can be done by right-clicking on `Servers > PostgreSQL > Databases` and
    selecting `Create > Database...`. You can choose any name, the instructions assume that you choose
    "codidact". Make sure that the owner is set to the user that you created before.

 5. Prepare a connection string for later use, the format is `Host=localhost;Database=X;Username=X;Password=X`.
    If you honored the suggestions, you would have the connection string
    `Host=localhost;Database=codidact;Username=codidact;Password=codidact`.

## Setting up Secrets

 1. Run the following command inside of 'src/WebUI' to install the connection string

  ```
  dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING"
  ```


## Installation using Visual Studio

 1. Install [Visual Studio][1] if you haven't already, the "Community" edition is sufficent.

 2. Ensure that the "ASP.NET and Web Development" workload is installed, this can be done during
    installation or later by starting the "Visual Studio Installer" application and selecting
    "Modify".

 3. Next, clone the repository. This can be done using Visual Studio by selecting "Clone or check out code"
    in the launcher. Alternativly, you can clone the repository manually with [Git for Windows][3]. The
    URL is `https://github.com/codidact/core`.

 4. Create a new branch, note that there is a [naming convention for branches][4]. This can be done at the bottom right of
    the screen, by clicking on the current branch and selecting `New Branch...`.

 5. For some reason Visual Studio opens the repository as folder and not as a solution. It's not possible
    to launch the application in this state. There should be a popup message in the solution explorer
    to "Switch Views", do that.

 6. You can launch the application by clicking `Debug > Start Debugging`.

 7. You can run the integration tests by clicking `Test > Run all Tests`.

## Installation without Visual Studio

 1. Install the [.NET Core SDK][2] this is not necessary if you have Visual Studio with the
    required workload installed. You need version 3.1 or newer.

 2. Install [Git for Windows][3] and clone the repository somewhere, the URL is `https://github.com/codidact/core`.

 3. Create a new branch, notice that there is a [naming convention for branches][4].

 4. Setup the `appsettings.Development.json` file in `src/WebUI`, refer to the instructions for Visual Studio.

 5. You can launch the appliation by executing `dotnet run` in the `src/WebUI` directory.

 6. You can run the integration tests using `dotnet test` in the root directory.
 
  
 ## Setting up the auth provider
 
 You'll also need to set up the auth provider. See [this instructions](https://github.com/codidact/authentication/blob/develop/docs/development.md) for more information.

  [1]: https://visualstudio.microsoft.com/downloads/
  [2]: https://dotnet.microsoft.com/download
  [3]: https://git-scm.com/download/win
  [4]: https://github.com/codidact/core/blob/develop/CONTRIBUTING.md#whats-the-workflow
  [5]: https://www.enterprisedb.com/downloads/postgres-postgresql-downloads
