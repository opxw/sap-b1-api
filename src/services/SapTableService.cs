using Microsoft.Extensions.Options;
using Opx.Api.Infrastructure.Service;
using SapB1.Dto;

namespace SapB1.Service;

public sealed class SapTableService(SapTableCatalog catalog, SapConnectionFactory connections,
    IOptions<SapReadOptions> options) : AppService
{
    public Task<AppResult> DomainsAsync() => ExecuteAsync(() =>
        Task.FromResult(Success(catalog.Domains)));

    public Task<AppResult> TablesAsync(string category) => ExecuteAsync(() =>
        Task.FromResult(Success(catalog.Tables(category).Select(table =>
            new TableSummaryDto(table.Code, table.Description, table.Status, table.FieldCount)).ToArray())));

    public Task<AppResult> TableAsync(string category, string code) => ExecuteAsync(() =>
    {
        if (!catalog.TryGet(code.ToUpperInvariant(), out var table) || table.Category != category)
        {
            return Task.FromResult(ValidationError("Table is not in this category."));
        }
        return Task.FromResult(Success(table));
    });

    public Task<AppResult> RowsAsync(string category, string code, int skip, int take,
        CancellationToken cancellationToken) => ExecuteAsync(async ct =>
    {
        code = code.ToUpperInvariant();
        if (!catalog.TryGet(code, out var table) || table.Category != category || table.Status != "mapped" ||
            !GeneratedTableReaders.Readers.TryGetValue(code, out var reader))
        {
            return ValidationError("Mapped table is not available in this category.");
        }
        if (skip < 0 || skip > 10000 || take < 1 || take > 100)
        {
            return ValidationError("Use skip 0..10000 and take 1..100.");
        }
        if (!options.Value.Tables.TryGetValue(code, out var selection) ||
            selection.Columns is not { Length: > 0 and <= 512 } ||
            selection.Columns.Distinct(StringComparer.Ordinal).Count() != selection.Columns.Length)
        {
            return ValidationError("Table read is not enabled with an explicit column allowlist.");
        }
        var fields = selection.Columns.Select(column => table.Fields.FirstOrDefault(field =>
            field.Column == column)).ToArray();
        if (fields.Any(field => field is null) ||
            !selection.Columns.Contains(selection.OrderBy, StringComparer.Ordinal))
        {
            return ValidationError("Configured columns or order field are not in the table mapping.");
        }
        var chosen = fields.Select(field => field!).ToArray();
        var order = chosen.Single(field => field.Column == selection.OrderBy).Property;
        var result = await reader(connections.Create(), chosen, order, skip, take, ct);
        return Success(new TablePageDto(code, category, skip, take, result.HasMore, result.Rows));
    }, cancellationToken);
}
