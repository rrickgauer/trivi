using Microsoft.AspNetCore.Mvc;
using Trivi.WebGui.Controllers.Contracts;


namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("games")]
public class JoinGameController : GuiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix<JoinGameController>();

    /// <summary>
    /// GET: /games
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName(nameof(JoinGamePage))]
    public async Task<ActionResult<ViewResult>> JoinGamePage()
    {
        return View(GuiPages.GameJoin);
    }
}
