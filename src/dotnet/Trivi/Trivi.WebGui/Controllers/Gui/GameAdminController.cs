using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Filters;
using Trivi.WebGui.Controllers.Contracts;
using Trivi.WebGui.Filters;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("games/admin/{gameId:gameId}")]
[ServiceFilter<LoginFirstRedirectFilter>]
public class GameAdminController(RequestItems requestItems) : GuiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix(nameof(GameAdminController));

    private readonly RequestItems _requestItems = requestItems;

    [HttpGet]
    [HttpGet("lobby")]
    [ActionName(nameof(GameAdminPage))]
    [ServiceFilter<LoginFirstRedirectFilter>]
    [ServiceFilter<ModifyGameFilter>]
    public async Task<IActionResult> GameAdminPage([FromRoute] string gameId)
    {
        return View(GuiPages.AdminLobby, new AdminLobbyViewModel()
        {
            Game = _requestItems.Game,
        });
    }

}
