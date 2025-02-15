Repository
==========

[![build status](https://img.shields.io/azure-devops/build/dnceng/public/51/master)](https://dev.azure.com/dnceng/public/_build?definitionId=51) [![test results](https://img.shields.io/azure-devops/tests/dnceng/public/51/master)](https://dev.azure.com/dnceng/public/_build?definitionId=51)

This repository is home to the following [.NET Foundation](https://dotnetfoundation.org/) projects. These projects are maintained by [Microsoft](https://github.com/microsoft) and licensed under the [Apache License, Version 2.0](LICENSE.txt).

* [Entity Framework Core](#entity-framework-core)
* [Microsoft.Data.Sqlite](#microsoftdatasqlite)

Entity Framework Core
--------------------

[![latest version](https://img.shields.io/nuget/v/Microsoft.EntityFrameworkCore)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore) [![preview version](https://img.shields.io/nuget/vpre/Microsoft.EntityFrameworkCore)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/absoluteLatest) [![downloads](https://img.shields.io/nuget/dt/Microsoft.EntityFrameworkCore)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore)

EF Core is a modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations. EF Core works with SQL Server, Azure SQL Database, SQLite, Azure Cosmos DB, MySQL, PostgreSQL, and other databases through a provider plugin API.

### Installation

EF Core is available on [NuGet](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore). Install the provider package corresponding to your target database. See the [list of providers](https://docs.microsoft.com/ef/core/providers/) in the docs for additional databases.

```sh
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Cosmos
```

Use the `--version` option to specify a [preview version](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/absoluteLatest) to install.

Use the [daily builds](https://github.com/aspnet/AspNetCore/blob/master/docs/DailyBuilds.md) to verify bug fixes and provide early feedback.

### Usage

The following code demonstrates basic usage of EF Core. For a full tutorial configuring the `DbContext`, defining the model, and creating the database, see [getting started](https://docs.microsoft.com/ef/core/get-started/) in the docs.

```cs
using (var db = new BloggingContext())
{
    // Inserting data into the database
    db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
    db.SaveChanges();

    // Querying
    var blog = db.Blogs
        .OrderBy(b => b.BlogId)
        .First();

    // Updating
    blog.Url = "https://devblogs.microsoft.com/dotnet";
    blog.Posts.Add(
        new Post
        {
            Title = "Hello World",
            Content = "I wrote an app using EF Core!"
        });
    db.SaveChanges();

    // Deleting
    db.Remove(blog);
    db.SaveChanges();
}
```

Microsoft.Data.Sqlite
--------------------

[![latest version](https://img.shields.io/nuget/v/Microsoft.Data.Sqlite)](https://www.nuget.org/packages/Microsoft.Data.Sqlite) [![preview version](https://img.shields.io/nuget/vpre/Microsoft.Data.Sqlite)](https://www.nuget.org/packages/Microsoft.Data.Sqlite/absoluteLatest) [![downloads](https://img.shields.io/nuget/dt/Microsoft.Data.Sqlite.Core)](https://www.nuget.org/packages/Microsoft.Data.Sqlite)

Microsoft.Data.Sqlite is a lightweight ADO.NET provider for SQLite. The EF Core provider for SQLite is built on top of this library. However, it can also be used independently or with other data access libraries.

### Installation

The latest stable version is available on [NuGet](https://www.nuget.org/packages/Microsoft.Data.Sqlite).

```sh
dotnet add package Microsoft.Data.Sqlite
```

Use the `--version` option to specify a [preview version](https://www.nuget.org/packages/Microsoft.Data.Sqlite/absoluteLatest) to install.

Use the [daily builds](https://github.com/aspnet/AspNetCore/blob/master/docs/DailyBuilds.md) to verify bug fixes and provide early feedback.

### Usage

This library implements the common [ADO.NET](https://docs.microsoft.com/dotnet/framework/data/adonet/) abstractions for connections, commands, data readers, etc. See the [samples](samples/Microsoft.Data.Sqlite) directory for additional examples.

```cs
using (var connection = new SqliteConnection("Data Source=Blogs.db"))
{
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText = "SELECT Url FROM Blogs";

    using (var reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            var url = reader.GetString(0);
        }
    }
}
```

Getting support
---------------

If you have a specific question about using these projects, we encourage you to [ask it on Stack Overflow](https://stackoverflow.com/questions/tagged/entity-framework-core*?tab=Votes). If you encounter a bug or would like to request a feature, [submit an issue](https://github.com/aspnet/EntityFrameworkCore/issues/new/choose). For more details, see [getting support](.github/SUPPORT.md).

Contributing
------------

If you're interested in contributing to these projects, see [contributing](.github/CONTRIBUTING.md).

See also
--------

* [Documentation](https://docs.microsoft.com/ef/core/)
* [Weekly status updates](https://github.com/aspnet/EntityFrameworkCore/issues/15403)
* [Release planning process](https://docs.microsoft.com/ef/core/what-is-new/#release-planning-process)
* [Code of conduct](.github/CODE_OF_CONDUCT.md)
