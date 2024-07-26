using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Contracts;
using Trivi.WebGui.Filters;

namespace Trivi.WebGui.Controllers.Api;

[ApiController]
[Route("api/games")]
[ServiceFilter<InternalApiAuthFilter>]
public class ApiGamesController(IGameService gameService) : InternalApiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix(nameof(ApiGamesController));

    private readonly IGameService _gameService = gameService;

    [HttpPost]
    [ActionName(nameof(PostGameAsync))]
    [ServiceFilter<PostGameFilter>]
    public async Task<IActionResult> PostGameAsync([FromBody] NewGameForm newGameForm)
    {
        var game = Game.FromNewGameForm(newGameForm);

        var createGame = await _gameService.CreateGameAsync(game);

        if (!createGame.Successful)
        {
            return BadRequest(createGame);
        }

        var uri = $"/api/games/{game.Id}";

        return Created(uri, createGame);
    }

    [HttpGet("{gameId:gameId}")]
    [ActionName(nameof(GetGameAsync))]
    public async Task<IActionResult> GetGameAsync([FromRoute] string gameId)
    {
        var getGame = await _gameService.GetGameAsync(gameId);

        if (!getGame.Successful)
        {
            return BadRequest(getGame);
        }

        return Ok(getGame);
    }

}
