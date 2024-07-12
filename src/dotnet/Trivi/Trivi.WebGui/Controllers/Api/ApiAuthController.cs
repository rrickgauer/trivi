using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Contracts;

namespace Trivi.WebGui.Controllers.Api;

[ApiController]
[Route("api/auth")]
public class ApiAuthController(IAuthService authService) : InternalApiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix(nameof(ApiAuthController));

    private readonly IAuthService _authService = authService;

    /// <summary>
    /// POST: /api/auth/login
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
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

    [HttpPost("signup")]
    public async Task<IActionResult> PostSignupAsync([FromBody] SignupForm form)
    {
        var signupResult = await _authService.SignupUserAsync(form);

        if (!signupResult.Successful)
        {
            return BadRequest(signupResult);
        }

        return Ok(signupResult);
    }



}
