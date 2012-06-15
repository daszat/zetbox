zetbox basic
============

[zetbox basic][zetbox] is a toolkit to build database-oriented applications. The developer specifies the data model and zetbox basic creates the database, schema updates, DTOs and an extensible user interface.

Supported systems
-----------------

The server runs on both Windows and Linux (using [mono 2.10][mono]) and can talk to MS-SQL and [PostgreSQL][pgsql]. The client currently supports Windows with WPF.

Build requirements
------------------

  * On Windows you can build zetbox with Visual Studio 2010 (including the Express versions).
  * On Linux you can build the server components using mono 2.10's xbuild.
  * For build-time code generation, the [Arebis Code Generator][arebis] is required.
  * Git is required for aquiring the source and generating compile-time version information.
    * [TortoiseGit][tgit] works well on Windows.
    * If you already have a GitHub account, you might want to look into [GitHub for Windows][githubwin].
    * Use the git packages from your distribution on Linux.
    * For compile-time version checking install [MSYS git][msysgit] and take care to specify "Run Git and included Unix tools from the Windows Command Prompt".
  * [Gendarme][gendarme] is required to check for common programming errors.
    * Since the Gendarme 2.10 installer does not add itself to the PATH, this has to be done manually: Start -> Computer -> Properties -> Advanced System Settings -> Environment variables -> Add "C:\Program Files (x86)\Gendarme" (or similar) to the PATH variable.
  * A database
    * On Linux install PostgreSQL using your preferred method.
    * On Windows you can use either MS-SQL 2008 (Express or otherwise) or PostgreSQL 8.4.12-9.x.
      * Using the Express edition, you should also install the [Microsoft® SQL Server® 2008 Management Studio Express][mssqls2008mse] (see [this answer][so-install-ssme] for detailled instructions), with the other editions it is already bundled.

Everything else is based on standard tools downloaded with [NuGet][nuget] during the build itself.

Building on Windows
-------------------

  * Have a version of Visual Studio 2010 installed or get the [C# Express edition][vs-csharp-express]
  * Have a version of MS-SQL 2008 installed and/or get [PostgreSQL for Windows][pgwin] (we are currently testing against 8.4.12, but 9.x should work too)
  * Clone the repository to your work area.
  * In the clone, copy Configs\Examples to Configs\Local
  * [MSSQL] Create a database called "zetbox".
  * [MSSQL Express/Compact] When using a DataSet instead of a full database, you need to change the ConnectionString in all MSSQL configurations in Configs\Local.
    * [Tip] When creating the database for development, specify the "Simple" recovery model to reduce the required space on disk.
  * [PG] Create a database called "zetbox" belonging to a user "zetbox" with a password.
  * [PG] Run the uuid-ossp.sql script from PG_INSTALL_DIR\share\contrib in the new database.
  * [PG] Change the database password in all ConnectionStrings in all PostgreSQL configurations in Configs\Local or use the provided default for the login role.
  * Open a Visual Studio Command Prompt and navigate to the checkout
    * Set the desired database and ORM provider with "set zenv=Local\NHibernate\PostgreSQL" or "set zenv=Local\EF\MSSQL"
    * Install our custom nuget source with ".nuget\nuget.exe sources add -name zetbox -source http://office.dasz.at/ngf/nuget"
	* Allow unsigned Powershell scripts to run. Execute "set-executionpolicy -executionPolicy RemoteSigned" in an PowerShell running as Administrator. Take care that you set the policy for both 32- and 64-bit mode.
    * Initialize the database and generated objects by running the "!FullReset.cmd" script.
  * To run the standalone WCF host, you need to set the proper urlacl. Edit and run the "urlreservation.cmd" as administrator
  * Now you can start the server and client with the "zbServer.cmd" and "zbClient.cmd" scripts.

[arebis]: https://github.com/DavidS/ArebisCGen
[gendarme]: https://github.com/spouliot/gendarme/downloads
[githubwin]: http://windows.github.com/
[mono]: https://github.com/mono/mono/tree/mono-2-10
[mssqls2008mse]: http://www.microsoft.com/en-us/download/details.aspx?id=26727
[msysgit]: http://code.google.com/p/msysgit/downloads/list
[nuget]: http://www.nuget.org/
[pgsql]: http://www.postgresql.org/
[pgwin]: http://www.enterprisedb.com/products-services-training/pgdownload#windows
[so-install-ssme]: http://stackoverflow.com/a/6482263/4918
[tgit]: http://code.google.com/p/tortoisegit/downloads/list
[vs-csharp-express]: http://www.microsoft.com/visualstudio/en-us/products/2010-editions/visual-csharp-express
[zetbox]: http://dasz.at/main/zetbox