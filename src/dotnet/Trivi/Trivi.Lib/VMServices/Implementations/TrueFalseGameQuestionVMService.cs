using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Contracts;

namespace Trivi.Lib.VMServices.Implementations;


[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class TrueFalseGameQuestionVMService(IGameService gameService, IQuestionService questionService) : IAsyncVMService<GameQuestionVMServiceParms, GameQuestionLayoutModel<TrueFalseGameQuestionVM>>
{
    private readonly IGameService _gameService = gameService;
    private readonly IQuestionService _questionService = questionService;

    public async Task<ServiceResponse<GameQuestionLayoutModel<TrueFalseGameQuestionVM>>> GetViewModelAsync(GameQuestionVMServiceParms parms)
    {
        try
        {
            var question = await GetQuestionAsync(parms);
            var game = await GetGameAsync(parms);

            TrueFalseGameQuestionVM viewModel = new()
            {
                Question = question,
            };

            return new GameQuestionLayoutModel<TrueFalseGameQuestionVM>(viewModel)
            {
                Game = game,
                PageTitle = "True false page title",
                Points = question.Points,
                Prompt = question.Prompt!,
            };

        }
        catch(ServiceException ex)
        {
            return new(ex.Errors);
        }
    }


    private async Task<ViewTrueFalse> GetQuestionAsync(GameQuestionVMServiceParms parms)
    {
        var getQuestion = await _questionService.GetTrueFalseAsync(parms.QuestionId);

        return getQuestion.GetData();
    }

    private async Task<ViewGame> GetGameAsync(GameQuestionVMServiceParms parms)
    {
        var getGame = await _gameService.GetGameAsync(parms.GameId);

        return getGame.GetData();
    }
}
