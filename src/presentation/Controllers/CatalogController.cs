using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SapB1.Service;

namespace SapB1.Api.Controllers;

[Authorize(Policy = "sap-b1.catalog.read")]
[Route("api/v1/catalog")]
public sealed class CatalogController(SapTableService service) : ApiController
{
    [HttpGet("domains")]
    public Task Domains() => OkOrFailAsync(service.DomainsAsync());
}
