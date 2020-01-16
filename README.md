# Codidact/Core
The core Codidact Q&amp;A software implementation.

## Huh?
Codidact is community-driven Q&A software: created, maintained, used, and run by the community, for the community. There's no
money-making company behind this.

We're currently in the early stages of development, but once we have a working MVP, we'll be hosting a public Codidact instance
for everyone to use. The software will also be available to download and run yourself to host your own instance under your own
rules.

## Installation
#### Windows
We'll be using Visual Studio for this setup.

1. [Download Visual Studio 2019 Community](https://visualstudio.microsoft.com/downloads/) and start up the installer. If you
   already have VS2019 installed, run the Visual Studio Installer instead.
2. When prompted to select workloads, select the "ASP.NET and Web Development" workload, and then install it.
3. Once installation is complete, clone/download this repository and extract it somewhere.
4. In the root of the repository, open the `Codidact.sln` file with visual studio to get started.
5. Once Visual Studio has finished loading, you can run Codidact by setting the WebUI project as the startup project.

Alternatively, if you don't want to run Visual Studio just to start the project, you can navigate to the src/WebUI folder
(so that `WebUI.csproj` is in your working directory) and issue the command `dotnet run`.

## License
[AGPL v3.0](https://github.com/codidact/core/blob/develop/LICENSE).

## Contributions
Very welcome! Please read our [contributing guidelines](https://github.com/codidact/core/blob/develop/CONTRIBUTING.md) before
you start writing code.
