using System.Runtime.InteropServices;
using Trivi.Lib.Auth.Contracts;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.RequestArgs;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Auth.Implementations;

public class PutAnswerAuthParms
{
    public required Guid ClientId { get; set; }
    public required PutAnswerRequest AnswerRequest { get; set; }
}


[AutoInject(AutoInjectionType.Scoped, InjectionProject.Always)]
public class PutAnswerAuth(IQuestionService questionService, IAnswerService answerService, RequestItems requestItems) : IAsyncPermissionsAuth<PutAnswerAuthParms>
{
    private readonly IQuestionService _questionService = questionService;
    private readonly IAnswerService _answerService = answerService;
    private readonly RequestItems _requestItems = requestItems;

    public async Task<ServiceResponse> HasPermissionAsync(PutAnswerAuthParms data)
    {
        try
        {
            var question = await GetQuestionAsync(data);

            if (data.ClientId != question.UserId)
            {
                throw new ForbiddenHttpResponseException();
            }

            _requestItems.Question = question;

            var answer = await GetAnswerAsync(data);

            // new answer so we can return here
            if (answer == null)
            {
                return new();
            }

            if (answer.UserId != data.ClientId)
            {
                throw new ForbiddenHttpResponseException();
            }

            if (answer.QuestionId != question.Id)
            {
                throw new NotFoundHttpResponseException();
            }

            _requestItems.Answer = answer;

            return new();
        }
        catch(ServiceException ex)
        {
            return new(ex.Response);
        }
    }

    private async Task<ViewQuestion> GetQuestionAsync(PutAnswerAuthParms data)
    {
        var getQuestion = await _questionService.GetQuestionAsync(data.AnswerRequest.QuestionId);

        return getQuestion.GetData();
    }


    private async Task<ViewAnswer?> GetAnswerAsync(PutAnswerAuthParms data)
    {
        var getAnswer = await _answerService.GetAnswerAsync(data.AnswerRequest.AnswerId);

        getAnswer.ThrowIfError();

        return getAnswer.Data;
    }

    
}
