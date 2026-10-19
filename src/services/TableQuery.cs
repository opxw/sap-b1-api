using System.Linq.Expressions;
using System.Reflection;
using LinqToDB;
using SapB1.Dto;

namespace SapB1.Service;

public sealed record TableReadResult(bool HasMore,
    IReadOnlyList<IReadOnlyDictionary<string, object?>> Rows);

public delegate Task<TableReadResult> TableReader(DataOptions options,
    IReadOnlyList<FieldDto> columns, string orderProperty,
    int skip, int take, CancellationToken cancellationToken);

public static class TableQuery
{
    private static readonly MethodInfo OrderByMethod = typeof(Queryable).GetMethods()
        .Single(method => method.Name == nameof(Queryable.OrderBy) &&
                          method.GetParameters().Length == 2);

    public static async Task<TableReadResult> ReadAsync<T>(ITable<T> table,
        IReadOnlyList<FieldDto> columns, string orderProperty, int skip, int take,
        CancellationToken cancellationToken) where T : class, new()
    {
        var selectedProperties = columns.Select(column =>
            typeof(T).GetProperty(column.Property, BindingFlags.Public | BindingFlags.Instance)
                ?? throw new InvalidOperationException("Configured SAP field is absent from the mapping."))
            .ToArray();
        var order = typeof(T).GetProperty(orderProperty, BindingFlags.Public | BindingFlags.Instance)
            ?? throw new InvalidOperationException("Configured SAP order field is absent from the mapping.");
        var parameter = Expression.Parameter(typeof(T), "row");
        var orderExpression = Expression.Lambda(Expression.Property(parameter, order), parameter);
        var ordered = (IQueryable<T>)OrderByMethod.MakeGenericMethod(typeof(T), order.PropertyType)
            .Invoke(null, [table, orderExpression])!;
        var projection = Expression.Lambda<Func<T, T>>(
            Expression.MemberInit(Expression.New(typeof(T)), selectedProperties.Select(property =>
                Expression.Bind(property, Expression.Property(parameter, property)))), parameter);
        var query = ordered.Select(projection).Skip(skip).Take(take + 1);
        var rows = await LinqToDB.Async.AsyncExtensions.ToListAsync(query, cancellationToken);
        var hasMore = rows.Count > take;
        var result = new List<IReadOnlyDictionary<string, object?>>(Math.Min(rows.Count, take));
        foreach (var row in rows.Take(take))
        {
            var values = new Dictionary<string, object?>(StringComparer.Ordinal);
            for (var i = 0; i < columns.Count; i++)
            {
                var value = selectedProperties[i].GetValue(row);
                values.Add(columns[i].Property, value is Enum option ? option.ToString() : value);
            }
            result.Add(values);
        }
        return new TableReadResult(hasMore, result);
    }
}
