using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;

namespace Blazor8Test.Controllers;

[ApiController]
[AllowAnonymous]
[Route("fluxor")]
public class FluxorStateController(IStateService stateSvc) : ControllerBase
{
    [HttpGet("set")]
    public async Task<IActionResult> SetStateAsync([FromQuery(Name="state")]string state)
    {
        await stateSvc.PersistSerializedStateAsync(state);
        return new OkResult();
    }

    [HttpGet("get")]
    public async Task<ActionResult<string>> GetStateAsync()
    {
        var result = await stateSvc.RetrieveSerializedStateAsync();
        return result is not null ? new OkObjectResult(result) : new NotFoundResult();
    }
}