using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Contracts;

namespace Trivi.WebGui.Controllers.Api;

[ApiController]
[Route("api/auth")]
public class ApiAuthController(IAuthService authService) : InternalApiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveControllerSuffix(nameof(ApiAuthController));

    private readonly IAuthService _authService = authService;


    [HttpPost("login")]
    [ActionName(nameof(PostLoginAsync))]
    public async Task<IActionResult> PostLoginAsync([FromBody] LoginForm form)
    {
        var loginResult = await _authService.LoginUserAsync(form);

        ServiceResponse result = new(loginResult);

        if (!result.Successful)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("login")]
    public async Task<IActionResult> GetTest()
    {
        SessionManager manager = new(HttpContext.Session);

        return Ok(manager);
    }

}
