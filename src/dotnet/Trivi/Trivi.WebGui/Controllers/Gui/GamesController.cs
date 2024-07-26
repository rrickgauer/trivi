using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Contracts;
using Trivi.WebGui.Filters;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("games")]
public class GamesController(IPlayerService playerService) : GuiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix(nameof(GamesController));

    private readonly IPlayerService _playerService = playerService;

    [HttpGet("{gameId:gameId}/admin")]
    [ActionName(nameof(GameAdminPage))]
    [ServiceFilter<LoginFirstRedirectFilter>]
    [ServiceFilter<ModifyGameFilter>]
    public async Task<IActionResult> GameAdminPage([FromRoute] string gameId)
    {
        return Ok(new
        {
            t = "admin page",
        });
    }



    /// <summary>
    /// GET: /games
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName(nameof(GamesPageAsync))]
    public async Task<IActionResult> GamesPageAsync()
    {
        return View(GuiPages.GameJoin);
    }
}
