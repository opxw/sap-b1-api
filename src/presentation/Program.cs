using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Opx.Api.Web;
using SapB1.Service;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var authority = configuration["Authentication:Authority"];
var audience = configuration["Authentication:Audience"];
var companyId = configuration["SapB1:CompanyId"];
if (string.IsNullOrWhiteSpace(authority) || string.IsNullOrWhiteSpace(audience) ||
    string.IsNullOrWhiteSpace(companyId))
{
    throw new InvalidOperationException("Authentication authority, audience, and SAP company ID are required.");
}

builder.Services.AddControllers();
builder.Services.AddOpxApiWeb(configuration);
builder.Services.AddOpenApi();
builder.Services.Configure<SapReadOptions>(configuration.GetSection("SapB1"));
builder.Services.AddSingleton(new SapTableCatalog(
    Path.Combine(AppContext.BaseDirectory, "catalog.generated.json")));
builder.Services.AddScoped<SapConnectionFactory>();
builder.Services.AddScoped<SapTableService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = authority;
        options.Audience = audience;
        options.RequireHttpsMetadata = true;
    });
builder.Services.AddAuthorization(options =>
{
    AddReadPolicy(options, "sap-b1.catalog.read", companyId);
    foreach (var route in DomainRoutes.ByName.Values)
    {
        AddReadPolicy(options, $"sap-b1.{route}.read", companyId);
    }
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseOpxApiProtection();
app.UseAuthentication();
app.UseAuthorization();
app.UseOpxWebApiHandler();
app.UseOpxWebApiStatusCodePages();
app.MapControllers();
app.GenerateOpxApiDocs();
app.Run();

static void AddReadPolicy(AuthorizationOptions options, string permission, string companyId)
{
    options.AddPolicy(permission, policy => policy.RequireAuthenticatedUser()
        .RequireAssertion(context =>
            context.User.HasClaim("company", companyId) &&
            (context.User.HasClaim("permission", permission) ||
             context.User.FindAll("scope").Any(claim =>
                 claim.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Contains(permission,
                     StringComparer.Ordinal)))));
}

public partial class Program;
