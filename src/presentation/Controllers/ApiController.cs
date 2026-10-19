using Microsoft.AspNetCore.Mvc;
using Opx.Api.Web.Common;
using Opx.Api.Web.Handlers;

namespace SapB1.Api.Controllers;

[ApiController]
[OpxApiExceptionHandler]
[OpxApiFilterHandler]
public abstract class ApiController : OpxApiController;
