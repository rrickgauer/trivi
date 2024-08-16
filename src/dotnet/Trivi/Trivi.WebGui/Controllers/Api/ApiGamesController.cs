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

    /// <summary>
    /// POST: /api/games
    /// </summary>
    /// <param name="newGameForm"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName(nameof(PostGameAsync))]
    [ServiceFilter<PostGameFilter>]
    public async Task<ActionResult<ServiceResponse<ViewGame>>> PostGameAsync([FromBody] NewGameForm newGameForm)
    {
        var game = Game.FromNewGameForm(newGameForm);

        var createGame = await _gameService.CreateGameAsync(game);

        return createGame.ToActionCreated($"/api/games/{game.Id}");
    }

    /// <summary>
    /// GET: /api/games/:gameId
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    [HttpGet("{gameId:gameId}")]
    [ActionName(nameof(GetGameAsync))]
    public async Task<ActionResult<ServiceResponse<ViewGame>>> GetGameAsync([FromRoute] string gameId)
    {
        var getGame = await _gameService.GetGameAsync(gameId);

        return getGame.ToAction();
    }

    /// <summary>
    /// POST: /api/games/:gameId/start
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    [HttpPost("{gameId:gameId}/start")]
    [ActionName(nameof(PostGameStartAsync))]
    [ServiceFilter<StartGameFilter>]
    public async Task<ActionResult<ServiceResponse<ViewGame>>> PostGameStartAsync([FromRoute] string gameId)
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


    /// <summary>
    /// POST: /api/games/:gameId/close
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="questionId"></param>
    /// <returns></returns>
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
