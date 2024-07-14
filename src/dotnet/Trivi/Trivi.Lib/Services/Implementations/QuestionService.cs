using System.Runtime.InteropServices;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;


[AutoInject<IQuestionService>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class QuestionService(IQuestionRepository questionRepo, ITableMapperService tableMapperService) : IQuestionService
{
    private readonly IQuestionRepository _questionRepo = questionRepo;
    private readonly ITableMapperService _tableMapperService = tableMapperService;

    #region - Get multiple -

    public async Task<ServiceDataResponse<List<ViewQuestion>>> GetQuestionsInCollectionAsync(Guid collectionId)
    {
        try
        {
            var table = await _questionRepo.SelectCollectionQuestionsAsync(collectionId);
            return _tableMapperService.ToModels<ViewQuestion>(table);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    #endregion

    #region - Select single -

    public async Task<ServiceDataResponse<ViewQuestion>> GetQuestionAsync(QuestionId questionId)
    {

        try
        {
            ViewQuestion? question = questionId.QuestionType switch
            {
                QuestionType.MultipleChoice => await GetSpecificQuestionAsync(questionId, GetMultipleChoiceAsync),
                QuestionType.TrueFalse      => await GetSpecificQuestionAsync(questionId, GetTrueFalseAsync),
                QuestionType.ShortAnswer    => await GetSpecificQuestionAsync(questionId, GetShortAnswerAsync),
                _                           => throw new NotImplementedException(),
            };

            return new(question);
        }
        catch (ServiceException ex)
        {
            return new(ex.Errors);
        }

    }


    private async Task<TQuestion?> GetSpecificQuestionAsync<TQuestion>(QuestionId questionId, Func<QuestionId, Task<ServiceDataResponse<TQuestion>>> getFunction) where TQuestion : ViewQuestion
    {
        var getQuestion = await getFunction(questionId);

        getQuestion.ThrowIfError();
        
        return getQuestion.Data;
    }



    public async Task<ServiceDataResponse<ViewShortAnswer>> GetShortAnswerAsync(QuestionId questionId)
    {
        try
        {
            var row = await _questionRepo.SelectShortAnswerAsync(questionId);
            var result = row != null ? _tableMapperService.ToModel<ViewShortAnswer>(row) : null;

            return new(result);
        }
        catch(RepositoryException ex)
        {
            return ex;
        }
    }

    
    public async Task<ServiceDataResponse<ViewMultipleChoice>> GetMultipleChoiceAsync(QuestionId questionId)
    {
        try
        {
            var row = await _questionRepo.SelectMultipleChoiceAsync(questionId);
            var result = row != null ? _tableMapperService.ToModel<ViewMultipleChoice>(row) : null;

            return new(result);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }

    }
    
    public async Task<ServiceDataResponse<ViewTrueFalse>> GetTrueFalseAsync(QuestionId questionId)
    {
        try
        {
            var row = await _questionRepo.SelectTrueFalseAsync(questionId);
            var result = row != null ? _tableMapperService.ToModel<ViewTrueFalse>(row) : null;

            return new(result);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }


    #endregion



    #region - Save -

    
    
    public async Task<ServiceDataResponse<ViewShortAnswer>> SaveShortAnswerAsync(ShortAnswer question)
    {
        return await SaveQuestionSteps(question, _questionRepo.UpsertShortAnswerAsync, GetShortAnswerAsync);
    }
    
    
    public async Task<ServiceDataResponse<ViewTrueFalse>> SaveTrueFalseAsync(TrueFalse question)
    {
        return await SaveQuestionSteps(question, _questionRepo.UpsertTrueFalseAsync, GetTrueFalseAsync);
    }
    
    
    public async Task<ServiceDataResponse<ViewMultipleChoice>> SaveMultipleChoiceAsync(MultipleChoice question)
    {
        return await SaveQuestionSteps(question, _questionRepo.UpsertMultipleChoiceAsync, GetMultipleChoiceAsync);
    }


    private delegate Task<int> SaveFunctionCallback<TModel>(TModel question);
    private delegate Task<ServiceDataResponse<TView>> GetFunctionCallback<TView>(QuestionId questionId);

    private async Task<ServiceDataResponse<TView>> SaveQuestionSteps<TModel, TView>(TModel question, SaveFunctionCallback<TModel> saveFunction, GetFunctionCallback<TView> getFunction) where TModel : Question where TView : ViewQuestion
    {
        try
        {
            await SaveQuestionBaseAsync(question);
            await saveFunction(question);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }

        return await getFunction(question.Id!);
    }




    private async Task<int> SaveQuestionBaseAsync(Question question)
    {
        return await _questionRepo.UpsertQuestionAsync(question);
    }


    #endregion
}
