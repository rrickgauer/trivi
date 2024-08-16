using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Contracts;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("auth")]
public class AuthController(IAuthService authService) : GuiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix<AuthController>();

    private readonly IAuthService _authService = authService;

    /// <summary>
    /// /auth/login
    /// </summary>
    /// <param name="destination"></param>
    /// <returns></returns>
    [HttpGet("login")]
    [ActionName(nameof(LoginPage))]
    public ActionResult<ViewResult> LoginPage([FromQuery] string? destination)
    {
        AuthPageVM viewModel = new()
        {
            Destination = destination,
        };

        return View(GuiPages.Login, viewModel); 
    }

    /// <summary>
    /// /auth/signup
    /// </summary>
    /// <param name="destination"></param>
    /// <returns></returns>
    [HttpGet("signup")]
    [ActionName(nameof(SignupPage))]
    public ActionResult<ViewResult> SignupPage([FromQuery] string? destination)
    {
        AuthPageVM viewModel = new()
        {
            Destination = destination,
        };

        return View(GuiPages.Signup, viewModel);
    }

    /// <summary>
    /// /auth/logout
    /// </summary>
    /// <returns></returns>
    [HttpGet("logout")]
    public ActionResult<ViewResult> LogoutPage()
    {
        _authService.Logout();

        // return to the home page
        return LocalRedirect("/");
    }
}
