<!-- Begin header -->

<p align="center">
   <img src="https://raw.githubusercontent.com/codidact/landing-page/master/source-assets/img/logo-codidact.png" width="500" alt="Codidact Logo">
</p>

<h3 align="center">
   <a href="https://codidact.org">The core Codidact Q&amp;A software implementation.</a>
</h3>

<p align="center">
   <a href="https://forum.codidact.org/"><img src="https://img.shields.io/badge/forum-discourse-brightgreen" alt="Website"></a>
   <a href="https://discord.gg/7jf8UzS/"><img src="https://img.shields.io/badge/chat-on%20discord-7289da.svg" alt="Chat"></a>
   <a href="https://github.com/codidact/core/blob/develop/CONTRIBUTING.md"><img src="https://img.shields.io/badge/contributions-welcome-brightgreen.svg" alt="Contributing"></a>
</p>

<!-- End header; begin body -->

## Huh?
Codidact is community-driven Q&A software: created, maintained, used, and run by the community, for the community. There's no
money-making company behind this.

We're currently in the early stages of development, but once we have a working MVP, we'll be hosting a public Codidact instance
for everyone to use. The software will also be available to download and run yourself to host your instance under your own
rules.

## Installation
These instructions are for setting up and running a local development instance of Codidact.

#### Technology Stack
A list of the current tech stack is [here](https://github.com/codidact/docs/wiki/Technology-Stack).
These items will be installed in the steps below, but if all of these are already installed on your
system you can skip straight to running the project.

#### Windows
We'll be using Visual Studio for this setup.

1. [Download Visual Studio 2019 Community](https://visualstudio.microsoft.com/downloads/) and start up the installer. If you
   already have VS2019 installed, run the Visual Studio Installer instead.
2. When prompted to select workloads, select the "ASP.NET and Web Development" workload, and then install it.
3. Once the installation is complete, clone/download this repository and extract it somewhere.
4. In the root of the repository, open the `Codidact.sln` file with visual studio to get started.
5. Visual Studio will attempt to install any missing packages. This usually requires no additional action, *but if it fails to do so, 
   [perform a package restore](https://docs.microsoft.com/en-US/nuget/consume-packages/package-restore).*
6. Once Visual Studio has finished loading, you can run Codidact by setting the WebUI project as the startup project. You'll
   know you're ready to run the project when a Start IIS Express button appears.
7. See the Database Setup instructions below

Alternatively, if you don't want to run Visual Studio just to start the project, you can navigate to the src/WebUI folder
(so that `WebUI.csproj` is in your working directory) and issue the command `dotnet run`.

#### Linux
1. [Download .NET Core SDK](https://docs.microsoft.com/en-us/dotnet/core/install/linux-package-manager-ubuntu-1904). Specifically,
   from this page, follow the sections labeled "Register Microsoft Key and feed" and "Install the .NET Core SDK"
2. Once the installation is complete, clone/download the repository and extract it somewhere.
3. You can run Codidact by navigating to the src/WebUI folder (so that `WebUI.csproj` is in your working directory) and issuing
   the command `dotnet run`.

#### Database Setup
These instructions assume that you don't have a PostgreSQL DB server installed at the moment
and would like to run one locally. These instructions may change heavily depending on your circumstances.

1. Install PostgreSQL:
    * [Windows](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads). This will also install pgAdmin.
    * Linux users will most likely want to use the PostgreSQL version provided by their package manager.
2. Create a new database and name it whatever you like. Windows users can do this using pgAdmin.
3. Open `appsettings.Development.json` in the WebUI project and add a new connection string for your database. It will look
   something like this, along with any relevant connection information:
```
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=YOUR_DATABASE;"
}
```
4. Open a terminal at the Codidact solution folder. Run the command `dotnet tool restore`.
5. Navigate to the WebUI project folder and run `dotnet ef database update` to apply the project migrations to the database.
6. Verify that your database is now populated with new tables.


## License
[AGPL v3.0](https://github.com/codidact/core/blob/develop/LICENSE).

## Contributions
Very welcome! Please read our [contributing guidelines](https://github.com/codidact/core/blob/develop/CONTRIBUTING.md) before
you start writing code.
