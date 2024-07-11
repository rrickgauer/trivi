using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Constants;
using Trivi.WebGui.Controllers.Contracts;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("auth")]
public class AuthController : GuiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveControllerSuffix(nameof(AuthController));


    [HttpGet("login")]
    public IActionResult GetLoginPage()
    {
        return View(GuiPages.Login); 


    }
}
