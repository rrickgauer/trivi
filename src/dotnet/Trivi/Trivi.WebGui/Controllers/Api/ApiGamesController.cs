using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Contracts;
using Trivi.WebGui.Filters;

namespace Trivi.WebGui.Controllers.Api;

[ApiController]
[Route("api/games")]
[ServiceFilter<InternalApiAuthFilter>]
public class ApiGamesController(IGameService gameService, IGameLobbyHubService gameHubService, IGameQuestionService gameQuestionService, ResultFilterCache resultFilterCache) : InternalApiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix<ApiGamesController>();

    private readonly IGameService _gameService = gameService;
    private readonly IGameLobbyHubService _gameLobbyHubService = gameHubService;
    private readonly IGameQuestionService _gameQuestionService = gameQuestionService;
    private readonly ResultFilterCache _resultFilterCache = resultFilterCache;

    [HttpPost]
    [ActionName(nameof(PostGameAsync))]
    [ServiceFilter<PostGameFilter>]
    public async Task<ActionResult<ServiceDataResponse<ViewGame>>> PostGameAsync([FromBody] NewGameForm newGameForm)
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
    public async Task<ActionResult<ServiceDataResponse<ViewGame>>> GetGameAsync([FromRoute] string gameId)
    {
        var getGame = await _gameService.GetGameAsync(gameId);

        if (!getGame.Successful)
        {
            return BadRequest(getGame);
        }

        return Ok(getGame);
    }


    [HttpPost("{gameId:gameId}/start")]
    [ActionName(nameof(PostGameStartAsync))]
    [ServiceFilter<StartGameFilter>]
    public async Task<ActionResult<ServiceDataResponse<ViewGame>>> PostGameStartAsync([FromRoute] string gameId)
    {
        var updateGame = await _gameService.StartGameAsync(gameId);

        if (!updateGame.Successful)
        {
            return BadRequest(updateGame);
        }

        if (updateGame.Data?.Questions.First() is ViewGameQuestion firstQuestion)
        {
            // navigate admin
            var adminDestination = $"/games/admin/{gameId}";
            await _gameLobbyHubService.AdminNavigateToAsync(gameId, adminDestination);

            // navigate players
            var playerDestination = $"/games/{gameId}";
            await _gameLobbyHubService.PlayersNavigateToAsync(gameId, playerDestination);
        }

        return Ok(updateGame);
    }


    [HttpPost("{gameId:gameId}/questions/{questionId:questionId}/close")]
    [ActionName(nameof(CloseQuestionAsync))]
    [ServiceFilter<CloseGameQuestionFilter>]
    [ServiceFilter<GameQuestionClosedResultFilter>]
    public async Task<IActionResult> CloseQuestionAsync([FromRoute] string gameId, [FromRoute] QuestionId questionId)
    {
        var activateNextQuestion = await _gameService.ActivateNextGameQuestionAsync(gameId);

        if (!activateNextQuestion.Successful)
        {
            return BadRequest(activateNextQuestion);
        }

        _resultFilterCache.GameId = gameId;

        return Ok(activateNextQuestion);
    }



}
