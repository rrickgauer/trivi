using Trivi.Lib.Auth.Contracts;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Auth.Implementations;

public class SaveQuestionAuthParms
{
    public required Guid ClientId { get; set; }
    public required QuestionForm QuestionForm { get; set; }
    public required QuestionId QuestionId { get; set; }
}


[AutoInject(AutoInjectionType.Scoped, InjectionProject.Always)]
public class SaveQuestionAuth(IQuestionService questionService, ICollectionService collectionService) : IAsyncPermissionsAuth<SaveQuestionAuthParms>
{
    private readonly IQuestionService _questionService = questionService;
    private readonly ICollectionService _collectionService = collectionService;

    public async Task<ServiceResponse> HasPermissionAsync(SaveQuestionAuthParms data)
    {

        try
        {
            // make sure collection exists
            var collection = await GetCollectionAsync(data);

            // make sure client owns the specified collection
            if (collection.UserId != data.ClientId)
            {
                throw new ForbiddenHttpResponseException();
            }
            
            var question = await GetQuestionAsync(data);

            // if this is a new question, we don't need to check anything else
            if (question == null)
            {
                return new();
            }

            // make sure client owns the existing question
            if (question.UserId != data.ClientId)
            {
                throw new ForbiddenHttpResponseException();
            }

            return new();
        }
        catch (ServiceException ex)
        {
            return new(ex.Response);
        }
    }


    private async Task<ViewCollection> GetCollectionAsync(SaveQuestionAuthParms data)
    {
        var getCollection = await _collectionService.GetCollectionAsync(data.QuestionForm.CollectionId);

        getCollection.ThrowIfError();

        return NotFoundHttpResponseException.ThrowIfNot<ViewCollection>(getCollection.Data);
    }

    private async Task<ViewQuestion?> GetQuestionAsync(SaveQuestionAuthParms data)
    {
        var getQuestion = await _questionService.GetQuestionAsync(data.QuestionId);

        getQuestion.ThrowIfError();

        return getQuestion.Data;
    }


}
