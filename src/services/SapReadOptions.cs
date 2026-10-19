namespace SapB1.Service;

public sealed class SapReadOptions
{
    public string CompanyId { get; set; } = "";
    public string ConnectionString { get; set; } = "";
    public string SqlServerVersion { get; set; } = "2019";
    public Dictionary<string, ReadTableOptions> Tables { get; set; } =
        new(StringComparer.Ordinal);
}

public sealed class ReadTableOptions
{
    public string[] Columns { get; set; } = [];
    public string OrderBy { get; set; } = "";
}
