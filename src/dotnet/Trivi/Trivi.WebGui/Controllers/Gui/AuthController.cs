using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Constants;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.WebGui.Controllers.Contracts;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("auth")]
public class AuthController : GuiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveControllerSuffix(nameof(AuthController));

    [HttpGet("login")]
    [ActionName(nameof(LoginPage))]
    public IActionResult LoginPage([FromQuery] string? destination)
    {
        AuthPageViewModel viewModel = new()
        {
            Destination = destination,
        };

        return View(GuiPages.Login, viewModel); 
    }

    [HttpGet("signup")]
    [ActionName(nameof(SignupPage))]
    public IActionResult SignupPage([FromQuery] string? destination)
    {
        AuthPageViewModel viewModel = new()
        {
            Destination = destination,
        };

        return View(GuiPages.Signup, viewModel);
    }
}
