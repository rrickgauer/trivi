using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.RequestArgs;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IAnswerService>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class AnswerService(IAnswerRepository repo, ITableMapperService mapperService) : IAnswerService
{
    private readonly IAnswerRepository _repo = repo;
    private readonly ITableMapperService _mapperService = mapperService;

    public async Task<ServiceDataResponse<List<ViewAnswer>>> GetAnswersAsync(QuestionId questionId)
    {
        try
        {
            var table = await _repo.SelectAnswersAsync(questionId);

            return _mapperService.ToModels<ViewAnswer>(table);
        }
        catch(RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceDataResponse<ViewAnswer>> GetAnswerAsync(string answerId)
    {
        try
        {
            ServiceDataResponse<ViewAnswer> result = new();
            
            var row = await _repo.SelectAnswerAsync(answerId);

            if (row != null)
            {
                result.Data = _mapperService.ToModel<ViewAnswer>(row);
            }

            return result;
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceDataResponse<ViewAnswer>> SaveAnswerAsync(Answer answer)
    {
        try
        {
            await _repo.UpsertAnswerAsync(answer);
            return await GetAnswerAsync(answer.Id!);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceDataResponse<List<ViewAnswer>>> ReplaceAnswersAsync(PutAnswersRequest answersRequest)
    {
        var answers = answersRequest.ToModels();
        
        if (!answers.Successful)
        {
            return new(answers.Errors);
        }

        try
        {
            var replaced = await _repo.ReplaceQuestionAnswersAsync(answersRequest.QuestionId, answers.Data!);
            return await GetAnswersAsync(answersRequest.QuestionId);
        }
        catch(RepositoryException ex)
        {
            return ex;
        }
    }


    public async Task<ServiceResponse> DeleteAnswerAsync(string answerId)
    {
        try
        {
            await _repo.DeleteAnswerAsync(answerId);
            return new();
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

}