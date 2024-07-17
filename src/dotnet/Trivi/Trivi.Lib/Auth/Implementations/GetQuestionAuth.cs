using Trivi.Lib.Auth.Contracts;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Auth.Implementations;

public class GetQuestionAuthParms
{
    public required Guid ClientId { get; set; }
    public required QuestionId QuestionId { get; set; }
}

[AutoInject(AutoInjectionType.Scoped, InjectionProject.Always)]
public class GetQuestionAuth(IQuestionService questionService, RequestItems requestItems) : IAsyncPermissionsAuth<GetQuestionAuthParms>
{
    private readonly IQuestionService _questionService = questionService;
    private readonly RequestItems _requestItems = requestItems;

    public async Task<ServiceResponse> HasPermissionAsync(GetQuestionAuthParms data)
    {
        try
        {
            var question = await GetQuestionAsync(data);

            if (question.QuestionUserId != data.ClientId)
            {
                throw new ForbiddenHttpResponseException();
            }

            _requestItems.Question = question;
            return new();

        }
        catch(ServiceException ex)
        {
            return new(ex.Errors);
        }
    }

    private async Task<ViewQuestion> GetQuestionAsync(GetQuestionAuthParms data)
    {
        var getQuestion = await _questionService.GetQuestionAsync(data.QuestionId);

        getQuestion.ThrowIfError();

        return NotFoundHttpResponseException.ThrowIfNot<ViewQuestion>(getQuestion.Data);
    }
}
