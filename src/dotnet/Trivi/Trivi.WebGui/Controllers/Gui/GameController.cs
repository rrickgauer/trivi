using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.RequestArgs;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Implementations;
using Trivi.WebGui.Controllers.Contracts;

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

    public GameController(RequestItems requestItems, ShortAnswerGameQuestionVMService shortAnswerVMService, IResponseService responseService, TrueFalseGameQuestionVMService trueFalseGameQuestionVMService, MulitpleChoiceGameQuestionVMService mulitpleChoiceGameQuestionVMService)
    {
        _requestItems = requestItems;
        _shortAnswerVMService = shortAnswerVMService;
        _responseService = responseService;
        _trueFalseGameQuestionVMService = trueFalseGameQuestionVMService;
        _mulitpleChoiceGameQuestionVMService = mulitpleChoiceGameQuestionVMService;
    }

    [HttpGet]
    [ActionName(nameof(GamePageAsync))]
    public async Task<IActionResult> GamePageAsync(PlayGameGuiRequest gameRequest)
    {
        var game = _requestItems.Game;

        return game.Status switch
        {
            GameStatus.Open => RedirectToAction(nameof(LobbyPageAsync), gameRequest.GetRedirectRouteValues()),
            GameStatus.Running => Ok("active question page"),
            _ => BadRequest("Game is closed"),
        };
    }


    [HttpGet("lobby")]
    [ActionName(nameof(LobbyPageAsync))]
    public async Task<IActionResult> LobbyPageAsync(PlayGameGuiRequest gameRequest)
    {
        return View(GuiPages.GameLobby, new GameLobbyViewModel()
        {
            Game = _requestItems.Game,
            Player = _requestItems.Player,
        });
    }


    [HttpGet("questions/{questionId:shortAnswerQuestion}")]
    [ActionName(nameof(ShortAnswerGameQuestionPageAsync))]
    public async Task<IActionResult> ShortAnswerGameQuestionPageAsync(PlayGameGuiRequest gameRequest, [FromRoute] QuestionId questionId)
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



    [HttpGet("questions/{questionId:trueFalseQuestion}")]
    [ActionName(nameof(TrueFalseGameQuestionPageAsync))]
    public async Task<IActionResult> TrueFalseGameQuestionPageAsync(PlayGameGuiRequest gameRequest, [FromRoute] QuestionId questionId)
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


    [HttpGet("questions/{questionId:multipleChoiceQuestion}")]
    [ActionName(nameof(MultipleChoiceGameQuestionPageAsync))]
    public async Task<IActionResult> MultipleChoiceGameQuestionPageAsync(PlayGameGuiRequest gameRequest, [FromRoute] QuestionId questionId)
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
