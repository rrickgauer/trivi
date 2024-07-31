using Microsoft.AspNetCore.Mvc;
using Trivi.WebGui.Controllers.Contracts;


namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("games")]
public class JoinGameController : GuiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix(nameof(JoinGameController));

    /// <summary>
    /// GET: /games
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName(nameof(JoinGamePage))]
    public async Task<IActionResult> JoinGamePage()
    {
        return View(GuiPages.GameJoin);
    }
}
