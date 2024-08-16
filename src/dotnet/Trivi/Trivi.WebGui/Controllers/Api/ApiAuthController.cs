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
    public static string ControllerRedirectName => IControllerName.RemoveSuffix<ApiAuthController>();

    private readonly IAuthService _authService = authService;

    /// <summary>
    /// POST: /api/auth/login
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [ActionName(nameof(PostLoginAsync))]
    public async Task<ActionResult<ServiceResponse>> PostLoginAsync([FromBody] LoginForm form)
    {
        var loginResult = await _authService.LoginUserAsync(form);

        ServiceResponse result = new(loginResult);

        return result.ToAction();
    }

    /// <summary>
    /// POST: /api/auth/signup
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("signup")]
    public async Task<ActionResult<ServiceResponse>> PostSignupAsync([FromBody] SignupForm form)
    {
        var signupResult = await _authService.SignupUserAsync(form);

        return signupResult.ToAction();
    }



}
