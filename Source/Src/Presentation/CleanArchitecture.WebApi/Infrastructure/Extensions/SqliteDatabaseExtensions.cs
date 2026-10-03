using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CleanArchitecture.WebApi.Infrastructure.Extensions;

public static class SqliteDatabaseExtensions
{
    /// <summary>
    /// SQLite creates the database file itself on the first connection, but it does not create the
    /// directory that holds the file. A path such as "App_Data/CleanArchitecture.db" therefore fails
    /// with "unable to open database file" on a fresh clone, on the first run, or in a container
    /// started without the volume mounted. Creating the directories up front makes the first run work.
    /// Connection strings that are not SQLite are ignored.
    /// </summary>
    /// <param name="configuration">The configuration holding the connection strings.</param>
    public static void EnsureDatabaseDirectoriesExist(this IConfiguration configuration)
    {
        var connectionStrings = configuration.GetSection("ConnectionStrings");

        foreach (var connectionString in connectionStrings.GetChildren())
        {
            if (!TryGetDataSource(connectionString.Value, out var dataSource))
                continue;

            // In-memory and file: URI sources are not backed by a directory we can create.
            if (string.IsNullOrWhiteSpace(dataSource)
                || dataSource.Equals(":memory:", StringComparison.OrdinalIgnoreCase)
                || dataSource.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
                continue;

            var directory = Path.GetDirectoryName(Path.GetFullPath(dataSource));

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);
        }
    }

    private static bool TryGetDataSource(string connectionString, out string dataSource)
    {
        dataSource = null;

        if (string.IsNullOrWhiteSpace(connectionString))
            return false;

        try
        {
            // Anything that is not a SQLite connection string throws on the unsupported keywords.
            dataSource = new SqliteConnectionStringBuilder(connectionString).DataSource;
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}
