namespace SapB1.Service;

public static class DomainRoutes
{
    public static readonly IReadOnlyDictionary<string, string> ByName =
        new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["Administration"] = "administration",
            ["Banking"] = "banking",
            ["Business Partners"] = "business-partners",
            ["Finance"] = "finance",
            ["Human Resources"] = "human-resources",
            ["Inventory and Production"] = "inventory-and-production",
            ["Marketing Documents and Receipts"] = "marketing-documents-and-receipts",
            ["MRP"] = "mrp",
            ["Reports"] = "reports",
            ["Sales Opportunities"] = "sales-opportunities",
            ["Service"] = "service",
            ["Others"] = "others"
        };
}
