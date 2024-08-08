using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Contracts;

namespace Trivi.Lib.VMServices.Implementations;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class MulitpleChoiceGameQuestionVMService(IGameService gameService, IQuestionService questionService) : IAsyncVMService<GameQuestionVMServiceParms, GameQuestionLayoutModel<MultipleChoiceGameQuestionVM>>
{
    private readonly IGameService _gameService = gameService;
    private readonly IQuestionService _questionService = questionService;

    public async Task<ServiceDataResponse<GameQuestionLayoutModel<MultipleChoiceGameQuestionVM>>> GetViewModelAsync(GameQuestionVMServiceParms parms)
    {
        try
        {
            var question = await GetQuestionAsync(parms);
            var game = await GetGameAsync(parms);

            MultipleChoiceGameQuestionVM viewModel = new()
            {
                Question = question,
            };

            return new GameQuestionLayoutModel<MultipleChoiceGameQuestionVM>(viewModel)
            {
                Game = game,
                PageTitle = "Multiple Choice",
            };
        }
        catch(ServiceException ex)
        {
            return new(ex.Errors);
        }
    }

    private async Task<ViewMultipleChoice> GetQuestionAsync(GameQuestionVMServiceParms parms)
    {
        var getQuestion = await _questionService.GetMultipleChoiceAsync(parms.QuestionId);

        return getQuestion.GetData();
    }

    private async Task<ViewGame> GetGameAsync(GameQuestionVMServiceParms parms)
    {
        var getGame = await _gameService.GetGameAsync(parms.GameId);

        return getGame.GetData();
    }
}