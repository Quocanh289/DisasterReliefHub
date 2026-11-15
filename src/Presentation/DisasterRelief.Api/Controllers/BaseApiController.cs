using Microsoft.AspNetCore.Mvc;

namespace DisasterRelief.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase;