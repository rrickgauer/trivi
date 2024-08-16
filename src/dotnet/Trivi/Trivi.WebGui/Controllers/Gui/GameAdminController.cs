using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Implementations;
using Trivi.WebGui.Controllers.Contracts;
using Trivi.WebGui.Filters;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("games/admin/{gameId:gameId}")]
[ServiceFilter<LoginFirstRedirectFilter>]
[ServiceFilter<ModifyGameFilter>]
public class GameAdminController(RequestItems requestItems, IResponseService responseService, AdminQuestionVMService adminQuestionVMService) : GuiController, IControllerName
{
    /// <inheritdoc/>
    public static string ControllerRedirectName => IControllerName.RemoveSuffix<GameAdminController>();

    private readonly RequestItems _requestItems = requestItems;
    private readonly IResponseService _responseService = responseService;
    private readonly AdminQuestionVMService _adminQuestionVMService = adminQuestionVMService;

    /// <summary>
    /// /games/admin/:gameId
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    [HttpGet]
    [ActionName(nameof(GameAdminPage))]
    public ActionResult GameAdminPage([FromRoute] string gameId)
    {
        var game = _requestItems.Game;

        if (game.Status == GameStatus.Open)
        {
            return RedirectToAction(nameof(GameAdminLobbyPage), new { gameId });
        }

        else if (game.Status == GameStatus.Running && game.ActiveQuestionId is QuestionId activeQuestionId)
        {
            return RedirectToAction(nameof(GameAdminQuestionPageAsync), new
            {
                gameId,
                questionId = activeQuestionId,
            });
        }

        return Ok("game page");
    }

    /// <summary>
    /// /games/admin/:gameId/lobby
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    [HttpGet("lobby")]
    [ActionName(nameof(GameAdminLobbyPage))]
    public ActionResult GameAdminLobbyPage([FromRoute] string gameId)
    {
        return View(GuiPages.AdminLobby, new AdminLobbyViewModel()
        {
            Game = _requestItems.Game,
        });
    }

    /// <summary>
    /// /games/admin/:gameId/questions/:questionId
    /// </summary>
    /// <param name="gameId"></param>
    /// <param name="questionId"></param>
    /// <returns></returns>
    [HttpGet("questions/{questionId:questionId}")]
    [ActionName(nameof(GameAdminQuestionPageAsync))]
    public async Task<ActionResult> GameAdminQuestionPageAsync([FromRoute] string gameId, [FromRoute] QuestionId questionId)
    {
        var getViewModel = await _adminQuestionVMService.GetViewModelAsync(new()
        {
            GameId = gameId,
            QuestionId = questionId
        });

        if (!getViewModel.Successful)
        {
            return BadRequest(getViewModel);
        }

        return View(GuiPages.AdminQuestion, getViewModel.Data);
    }
}
