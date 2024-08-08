using Trivi.Lib.Auth.Contracts;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Auth.Implementations;


public class CloseGameQuestionAuthParms
{
    public required Guid ClientId { get; set; }
    public required QuestionId QuestionId { get; set; }
    public required string GameId { get; set; }
}

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class CloseGameQuestionAuth(IGameQuestionService gameQuestionService) : IAsyncPermissionsAuth<CloseGameQuestionAuthParms>
{
    private readonly IGameQuestionService _gameQuestionService = gameQuestionService;

    public async Task<ServiceResponse> HasPermissionAsync(CloseGameQuestionAuthParms data)
    {
        try
        {
            var question = await GetQuestionAsync(data);

            if (question.UserId != data.ClientId)
            {
                throw new ForbiddenHttpResponseException();
            }

            if (question.QuestionStatus == GameQuestionStatus.Closed)
            {
                return new(ErrorCode.GamesCloseQuestion);
            }

            return new();
        }
        catch(ServiceException ex)
        {
            return new(ex.Errors);
        }
    }

    private async Task<ViewGameQuestion> GetQuestionAsync(CloseGameQuestionAuthParms data)
    {
        var getQuestion = await _gameQuestionService.GetGameQuestionAsync(new()
        {
            GameId = data.GameId,
            QuestionId = data.QuestionId,
        });

        return getQuestion.GetData();
    }
}
