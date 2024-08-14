using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Contracts;

namespace Trivi.Lib.VMServices.Implementations;


public class GameQuestionVMServiceParms
{
    public required QuestionId QuestionId { get; set; }
    public required string GameId { get; set; }
}


[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class ShortAnswerGameQuestionVMService(IGameService gameService, IQuestionService questionService) : IAsyncVMService<GameQuestionVMServiceParms, GameQuestionLayoutModel<ShortAnswerGameQuestionVM>>
{
    private readonly IGameService _gameService = gameService;
    private readonly IQuestionService _questionService = questionService;

    public async Task<ServiceResponse<GameQuestionLayoutModel<ShortAnswerGameQuestionVM>>> GetViewModelAsync(GameQuestionVMServiceParms parms)
    {
        try
        {
            var question = await GetQuestionAsync(parms);
            var game = await GetGameAsync(parms);

            ShortAnswerGameQuestionVM viewModel = new()
            {
                Question = question,
            };

            return new GameQuestionLayoutModel<ShortAnswerGameQuestionVM>(viewModel)
            {
                Game = game,
                PageTitle = "Short answer",
            };
        }
        catch(ServiceException ex)
        {
            return new(ex.Errors);
        }
    }


    private async Task<ViewShortAnswer> GetQuestionAsync(GameQuestionVMServiceParms parms)
    {
        var getQuestion = await _questionService.GetShortAnswerAsync(parms.QuestionId);

        return getQuestion.GetData();
    }

    private async Task<ViewGame> GetGameAsync(GameQuestionVMServiceParms parms)
    {
        var getGame = await _gameService.GetGameAsync(parms.GameId);
                
        return getGame.GetData();
    }
}
