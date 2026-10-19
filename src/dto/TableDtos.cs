namespace SapB1.Dto;

public sealed record DomainDto(string Name, string Route, string Description,
    int CatalogTables, int MappedTables);

public sealed record EnumOptionDto(string Code, string Label);

public sealed record FieldDto(string Column, string Property, string Description,
    string SourceType, string Size, IReadOnlyList<string> Related,
    IReadOnlyList<EnumOptionDto> EnumOptions);

public sealed record TableDto(string Code, string Category, string Description,
    string Status, int FieldCount, IReadOnlyList<FieldDto> Fields);

public sealed record TableSummaryDto(string Code, string Description,
    string Status, int FieldCount);

public sealed record TablePageDto(string Code, string Category, int Skip, int Take,
    bool HasMore, IReadOnlyList<IReadOnlyDictionary<string, object?>> Rows);
