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

    [HttpGet("login")]
    [ActionName(nameof(LoginPage))]
    public IActionResult LoginPage([FromQuery] string? destination)
    {
        AuthPageVM viewModel = new()
        {
            Destination = destination,
        };

        return View(GuiPages.Login, viewModel); 
    }

    [HttpGet("signup")]
    [ActionName(nameof(SignupPage))]
    public IActionResult SignupPage([FromQuery] string? destination)
    {
        AuthPageVM viewModel = new()
        {
            Destination = destination,
        };

        return View(GuiPages.Signup, viewModel);
    }

    [HttpGet("logout")]
    public IActionResult LogoutPage()
    {
        _authService.Logout();

        // return to the home page
        return LocalRedirect("/");
    }
}
