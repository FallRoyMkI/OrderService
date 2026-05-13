using Microsoft.AspNetCore.Mvc;

namespace OrderService.ApiHost.Controllers.Common;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController
    : ControllerBase;
