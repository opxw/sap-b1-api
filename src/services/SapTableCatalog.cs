using System.Text.Json;
using SapB1.Dto;

namespace SapB1.Service;

public sealed class SapTableCatalog
{
    private readonly IReadOnlyDictionary<string, TableDto> _tables;
    private readonly IReadOnlyDictionary<string, IReadOnlyList<TableDto>> _byCategory;

    public SapTableCatalog(string path)
    {
        using var document = JsonDocument.Parse(File.ReadAllBytes(path));
        var root = document.RootElement;
        Domains = root.GetProperty("categories").EnumerateArray()
            .Select(c => new DomainDto(
                c.GetProperty("name").GetString()!,
                DomainRoutes.ByName[c.GetProperty("name").GetString()!],
                c.GetProperty("description").GetString()!,
                c.GetProperty("catalogTables").GetInt32(),
                c.GetProperty("mappedTables").GetInt32()))
            .ToArray();
        var tables = root.GetProperty("tables").EnumerateArray()
            .Select(ParseTable).ToArray();
        _tables = tables.ToDictionary(table => table.Code, StringComparer.Ordinal);
        _byCategory = tables.GroupBy(table => table.Category)
            .ToDictionary(group => group.Key,
                group => (IReadOnlyList<TableDto>)group.ToArray(), StringComparer.Ordinal);
        if (Domains.Count != 12 || tables.Length != 2709 ||
            tables.Count(table => table.Status == "mapped") != 2547)
        {
            throw new InvalidDataException("SAP table catalog coverage differs from the local source snapshot.");
        }
    }

    public IReadOnlyList<DomainDto> Domains { get; }

    public IReadOnlyList<TableDto> Tables(string category) =>
        _byCategory.TryGetValue(category, out var tables) ? tables : [];

    public bool TryGet(string code, out TableDto table) => _tables.TryGetValue(code, out table!);

    private static TableDto ParseTable(JsonElement table)
    {
        var fields = table.TryGetProperty("fields", out var fieldArray)
            ? fieldArray.EnumerateArray().Select(field => new FieldDto(
                field.GetProperty("column").GetString()!,
                field.GetProperty("property").GetString()!,
                field.GetProperty("description").GetString()!,
                field.GetProperty("sourceType").GetString()!,
                field.GetProperty("size").GetString()!,
                field.GetProperty("related").EnumerateArray().Select(x => x.GetString()!).ToArray(),
                field.GetProperty("enumOptions").EnumerateArray()
                    .Select(x => new EnumOptionDto(x.GetProperty("code").GetString()!,
                                                   x.GetProperty("label").GetString()!)).ToArray()))
                .ToArray()
            : [];
        return new TableDto(
            table.GetProperty("code").GetString()!,
            table.GetProperty("category").GetString()!,
            table.GetProperty("description").GetString()!,
            table.GetProperty("status").GetString()!,
            table.GetProperty("fieldCount").GetInt32(), fields);
    }
}
