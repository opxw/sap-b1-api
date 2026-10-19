using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using Microsoft.Extensions.Options;

namespace SapB1.Service;

public sealed class SapConnectionFactory(IOptions<SapReadOptions> options)
{
    public DataOptions Create()
    {
        var settings = options.Value;
        if (string.IsNullOrWhiteSpace(settings.CompanyId) ||
            string.IsNullOrWhiteSpace(settings.ConnectionString))
        {
            throw new InvalidOperationException("SAP company connection is not configured.");
        }
        var version = settings.SqlServerVersion switch
        {
            "2019" => SqlServerVersion.v2019,
            "2022" => SqlServerVersion.v2022,
            _ => throw new InvalidOperationException("Unsupported configured SQL Server version.")
        };
        return new DataOptions().UseSqlServer(settings.ConnectionString, version,
            SqlServerProvider.MicrosoftDataSqlClient);
    }
}
