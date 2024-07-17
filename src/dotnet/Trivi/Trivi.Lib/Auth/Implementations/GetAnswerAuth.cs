using Trivi.Lib.Auth.Contracts;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Auth.Implementations;

public class GetAnswerAuthParms
{
    public required Guid ClientId { get; set; }
    public required string AnswerId { get; set; }
    public required QuestionId QuestionId { get; set; }
}

[AutoInject(AutoInjectionType.Scoped, InjectionProject.Always)]
public class GetAnswerAuth(IAnswerService answerService, RequestItems requestItems, IQuestionService questionService) : IAsyncPermissionsAuth<GetAnswerAuthParms>
{
    private readonly IAnswerService _answerService = answerService;
    private readonly IQuestionService _questionService = questionService;
    private readonly RequestItems _requestItems = requestItems;

    public async Task<ServiceResponse> HasPermissionAsync(GetAnswerAuthParms data)
    {
        try
        {

            var question = await GetQuestionAsync(data);

            if (question.UserId != data.ClientId)
            {
                throw new ForbiddenHttpResponseException();
            }


            var answer = await GetAnswerAsync(data);

            if (answer.UserId != data.ClientId)
            {
                throw new ForbiddenHttpResponseException();
            }

            if (answer.QuestionId != question.Id)
            {
                throw new NotFoundHttpResponseException();
            }

            _requestItems.Answer = answer;
            _requestItems.Question = question;

            return new();
        }
        catch (ServiceException ex)
        {
            return new(ex.Response);
        }
    }

    private async Task<ViewQuestion> GetQuestionAsync(GetAnswerAuthParms data)
    {
        var getQuestion = await _questionService.GetQuestionAsync(data.QuestionId);
        
        getQuestion.ThrowIfError();

        return NotFoundHttpResponseException.ThrowIfNot<ViewQuestion>(getQuestion.Data);

    }

    private async Task<ViewAnswer> GetAnswerAsync(GetAnswerAuthParms data)
    {
        var getAnswer = await _answerService.GetAnswerAsync(data.AnswerId);

        getAnswer.ThrowIfError();

        return NotFoundHttpResponseException.ThrowIfNot<ViewAnswer>(getAnswer.Data);
    }
}
