using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.RequestArgs;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Filters;
using Trivi.WebGui.Controllers.Contracts;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("games/{gameId:gameId}")]
[ServiceFilter<PlayGameFilter>]
public class GameController(RequestItems requestItems) : GuiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix(nameof(GameController));

    private readonly RequestItems _requestItems = requestItems;

    [HttpGet]
    [ActionName(nameof(GamePageAsync))]
    public async Task<IActionResult> GamePageAsync(PlayGameRequest gameRequest)
    {
        var game = _requestItems.Game;

        switch(game.Status)
        {
            case GameStatus.Open:
                return RedirectToAction(nameof(LobbyPageAsync), gameRequest.GetRedirectRouteValues());
            case GameStatus.Running:
                return Ok("active question page");
            default:
                return BadRequest("Game is closed");
        }
    }


    [HttpGet("lobby")]
    [ActionName(nameof(LobbyPageAsync))]
    public async Task<IActionResult> LobbyPageAsync(PlayGameRequest gameRequest)
    {
        return View(GuiPages.GameLobby, new GameLobbyViewModel()
        {
            Game = _requestItems.Game,
            Player = _requestItems.Player,
        });
    }

}
