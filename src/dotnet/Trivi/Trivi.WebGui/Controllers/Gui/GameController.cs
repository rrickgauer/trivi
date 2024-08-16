using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.RequestArgs;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Implementations;
using Trivi.WebGui.Controllers.Contracts;
using Trivi.WebGui.Filters;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("games/{gameId:gameId}")]
[ServiceFilter<PlayGameFilter>]
public class GameController : GuiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix<GameController>();

    private readonly RequestItems _requestItems;
    private readonly ShortAnswerGameQuestionVMService _shortAnswerVMService;
    private readonly IResponseService _responseService;
    private readonly TrueFalseGameQuestionVMService _trueFalseGameQuestionVMService;
    private readonly MulitpleChoiceGameQuestionVMService _mulitpleChoiceGameQuestionVMService;
    private readonly IGamePageService _gamePageService;

    public GameController(RequestItems requestItems, ShortAnswerGameQuestionVMService shortAnswerVMService, IResponseService responseService, TrueFalseGameQuestionVMService trueFalseGameQuestionVMService, MulitpleChoiceGameQuestionVMService mulitpleChoiceGameQuestionVMService, IGamePageService gamePageService)
    {
        _requestItems = requestItems;
        _shortAnswerVMService = shortAnswerVMService;
        _responseService = responseService;
        _trueFalseGameQuestionVMService = trueFalseGameQuestionVMService;
        _mulitpleChoiceGameQuestionVMService = mulitpleChoiceGameQuestionVMService;
        _gamePageService = gamePageService;
    }

    /// <summary>
    /// /games/:gameId
    /// </summary>
    /// <param name="gameRequest"></param>
    /// <returns></returns>
    [HttpGet]
    [ActionName(nameof(GamePageAsync))]
    public async Task<ActionResult<ViewResult>> GamePageAsync(PlayGameGuiRequest gameRequest)
    {
        var game = _requestItems.Game;

        if (game.Status == GameStatus.Open)
        {
            return RedirectToAction(nameof(LobbyPageAsync), gameRequest.GetRedirectRouteValues());
        }

        else if (game.Status == GameStatus.Running && game.ActiveQuestionId is QuestionId activeQuestionId)
        {
            return await HandleRunning(gameRequest, activeQuestionId);
        }

        else
        {
            return BadRequest("Unknown next step");
        }
    }

    private async Task<ActionResult<ViewResult>> HandleRunning(PlayGameGuiRequest gameRequest, QuestionId activeQuestionId)
    {
        // check if player has already responded to question
        var getQuestion = await _responseService.GetResponseAsync(new()
        {
            PlayerId = gameRequest.PlayerId,
            QuestionId = activeQuestionId
        });

        if (getQuestion.Data is not null)
        {
            return RedirectToAction(nameof(GetWaitingPageAsync), gameRequest.GetRedirectRouteValues());
        }

        return activeQuestionId.QuestionType switch
        {
            QuestionType.TrueFalse => RedirectToAction(nameof(TrueFalseGameQuestionPageAsync), gameRequest.GetRedirectRouteValues(activeQuestionId)),
            QuestionType.ShortAnswer => RedirectToAction(nameof(ShortAnswerGameQuestionPageAsync), gameRequest.GetRedirectRouteValues(activeQuestionId)),
            QuestionType.MultipleChoice => RedirectToAction(nameof(MultipleChoiceGameQuestionPageAsync), gameRequest.GetRedirectRouteValues(activeQuestionId)),
            _ => throw new NotImplementedException(),
        };
    }

    /// <summary>
    /// /games/:gameId/waiting
    /// </summary>
    /// <param name="gameRequest"></param>
    /// <returns></returns>
    [HttpGet("waiting")]
    [ActionName(nameof(GetWaitingPageAsync))]   
    public async Task<ActionResult<ViewResult>> GetWaitingPageAsync(PlayGameGuiRequest gameRequest)
    {
        return View(GuiPages.GameWaiting);
    }

    /// <summary>
    /// /games/:gameId/lobby
    /// </summary>
    /// <param name="gameRequest"></param>
    /// <returns></returns>
    [HttpGet("lobby")]
    [ActionName(nameof(LobbyPageAsync))]
    public async Task<ActionResult<ViewResult>> LobbyPageAsync(PlayGameGuiRequest gameRequest)
    {
        return View(GuiPages.GameLobby, new GameLobbyViewModel()
        {
            Game = _requestItems.Game,
            Player = _requestItems.Player,
        });
    }

    /// <summary>
    /// /games/:gameId/questions/:sa_questionId
    /// </summary>
    /// <param name="gameRequest"></param>
    /// <param name="questionId"></param>
    /// <returns></returns>
    [HttpGet("questions/{questionId:shortAnswerQuestion}")]
    [ActionName(nameof(ShortAnswerGameQuestionPageAsync))]
    [ServiceFilter<ViewPlayerGameQuestionPageFilter>]
    public async Task<ActionResult<ViewResult>> ShortAnswerGameQuestionPageAsync(PlayGameGuiRequest gameRequest, [FromRoute] QuestionId questionId)
    {
        var getVM = await _shortAnswerVMService.GetViewModelAsync(new()
        {
            GameId = gameRequest.GameId,
            QuestionId = questionId,
        });

        if (!getVM.Successful)
        {
            return BadRequest(getVM);
        }

        return View(GuiPages.GameQuestionShortAnswer, getVM.Data);
    }


    /// <summary>
    /// /games/:gameId/questions/:tf_questionId
    /// </summary>
    /// <param name="gameRequest"></param>
    /// <param name="questionId"></param>
    /// <returns></returns>
    [HttpGet("questions/{questionId:trueFalseQuestion}")]
    [ActionName(nameof(TrueFalseGameQuestionPageAsync))]
    [ServiceFilter<ViewPlayerGameQuestionPageFilter>]
    public async Task<ActionResult<ViewResult>> TrueFalseGameQuestionPageAsync(PlayGameGuiRequest gameRequest, [FromRoute] QuestionId questionId)
    {
        var getVm = await _trueFalseGameQuestionVMService.GetViewModelAsync(new()
        {
            GameId = gameRequest.GameId,
            QuestionId = questionId,
        });

        if (!getVm.Successful)
        {
            return BadRequest(getVm);
        }

        return View(GuiPages.GameQuestionTrueFalse, getVm.Data);
    }

    /// <summary>
    /// /games/:gameId/questions/:tf_questionId
    /// </summary>
    /// <param name="gameRequest"></param>
    /// <param name="questionId"></param>
    /// <returns></returns>
    [HttpGet("questions/{questionId:multipleChoiceQuestion}")]
    [ActionName(nameof(MultipleChoiceGameQuestionPageAsync))]
    [ServiceFilter<ViewPlayerGameQuestionPageFilter>]
    public async Task<ActionResult<ViewResult>> MultipleChoiceGameQuestionPageAsync(PlayGameGuiRequest gameRequest, [FromRoute] QuestionId questionId)
    {
        var getVM = await _mulitpleChoiceGameQuestionVMService.GetViewModelAsync(new()
        {
            GameId = gameRequest.GameId,
            QuestionId = questionId,
        });

        if (!getVM.Successful)
        {
            return BadRequest(getVM);
        }

        return View(GuiPages.GameQuestionMulitpleChoice, getVM.Data);
    }

}
