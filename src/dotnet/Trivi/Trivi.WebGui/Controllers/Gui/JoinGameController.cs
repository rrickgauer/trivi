using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.ViewModels.Gui;
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
    public ActionResult JoinGamePage([FromQuery] string? gameId)
    {
        JoinGameViewModel viewModel = new()
        {
            GameId = gameId,
        };

        return View(GuiPages.GameJoin, viewModel);
    }
}
