/***

This filter redirects players to the waiting page if they have already answered the question.

*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.Utility;
using Trivi.WebGui.Controllers.Gui;

namespace Trivi.WebGui.Filters;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class ViewPlayerGameQuestionPageFilter(IResponseService responseService) : IAsyncActionFilter
{
    private readonly IResponseService _responseService = responseService;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var getResponse = await _responseService.GetResponseAsync(new()
        {
            PlayerId = context.GetPlayGameRequestForm().PlayerId,
            QuestionId = context.GetQuestionIdFromUrl(),
        });

        if (!getResponse.Successful)
        {
            context.ReturnBadServiceResponse(getResponse);
            return;
        }

        // if the service response data is null that means the player has not answered the question yet
        // so, continue on normally
        if (getResponse.Data is null)
        {
            await next();
            return;
        }

        // the player has already answered the question, so redirect them to the waiting page
        string actionName = nameof(GameController.GetWaitingPage);
        string controllerName = GameController.ControllerRedirectName;

        var routeValues = new
        {
            player = context.GetPlayGameRequestForm().PlayerId,
            gameId = context.GetPlayGameRequestForm().GameId,
        };

        context.Result = new RedirectToActionResult(actionName, controllerName, routeValues);
        return;
    }
}
