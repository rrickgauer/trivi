using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Contracts;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("")]
public class LandingController(IUserService userService) : GuiController, IControllerName
{
    private readonly IUserService _userService = userService;

    public static string ControllerRedirectName => IControllerName.RemoveSuffix<LandingController>();

    /// <summary>
    /// /
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ViewResult>> GetLandingPage()
    {
        var getUsers = await _userService.GetUsersAsync();
        return View("Views/Pages/Landing/LandingPage.cshtml");
    }

}
