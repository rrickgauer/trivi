using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Contracts;

namespace Trivi.Lib.VMServices.Implementations;


public class AdminQuestionVMServiceParms
{
    public required string GameId { get; set; }
    public required QuestionId QuestionId { get; set; }
}


[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class AdminQuestionVMService(IResponseService responseService, IGameService gameService, IGameQuestionService gameQuestionService) : IAsyncVMService<AdminQuestionVMServiceParms, AdminQuestionViewModel>
{
    private readonly IResponseService _responseService = responseService;
    private readonly IGameService _gameService = gameService;
    private readonly IGameQuestionService _gameQuestionService = gameQuestionService;

    public async Task<ServiceDataResponse<AdminQuestionViewModel>> GetViewModelAsync(AdminQuestionVMServiceParms parms)
    {
        try
        {
            var game = await GetGameAsync(parms);
            var responses = await GetPlayerQuestionResponsesAsync(parms);
            var gameQuestion = await GetGameQuestionAsync(parms);

            return new AdminQuestionViewModel()
            {
                Game = game,
                PlayerQuestionResponses = responses,
                GameQuestion = gameQuestion,
            };
        }
        catch (ServiceException ex)
        {
            return new(ex.Errors);
        }
    }


    private async Task<ViewGame> GetGameAsync(AdminQuestionVMServiceParms parms)
    {
        var getGame = await _gameService.GetGameAsync(parms.GameId);
        return getGame.GetData();
    }

    private async Task<ViewGameQuestion> GetGameQuestionAsync(AdminQuestionVMServiceParms parms)
    {
        var getQuestion = await _gameQuestionService.GetGameQuestionAsync(new()
        {
            GameId = parms.GameId,
            QuestionId = parms.QuestionId,
        });

        return getQuestion.GetData();
    }

    private async Task<List<ViewPlayerQuestionResponse>> GetPlayerQuestionResponsesAsync(AdminQuestionVMServiceParms parms)
    {
        var getResponses = await _responseService.GetPlayerQuestionResponsesAsync(parms.GameId, parms.QuestionId);

        return getResponses.GetData();
    }
}
