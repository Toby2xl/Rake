using System.Data;
using Identity.Abstractions;
using Npgsql;

namespace Identity.Infrastructure.Data;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;
    public SqlConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("RakeDbConn") ??
        throw new ArgumentException("Connection String is missing");
    }
    public IDbConnection Create()
    {
        return new NpgsqlConnection(_connectionString);
    }
}