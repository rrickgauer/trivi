using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Domain.ViewModels.Api;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Contracts;

namespace Trivi.Lib.VMServices.Implementations;

public class GetQuestionsApiVMParms
{
    public required Guid CollectionId { get; set; }
}


[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class GetQuestionsApiVMService(IQuestionService questionService) : IAsyncVMService<GetQuestionsApiVMParms, GetQuestionsApiVM>
{
    private readonly IQuestionService _questionService = questionService;

    public async Task<ServiceDataResponse<GetQuestionsApiVM>> GetViewModelAsync(GetQuestionsApiVMParms parms)
    {
        try
        {
            var questions = await GetQuestionsAsync(parms);

            GetQuestionsApiVM viewModel = new()
            {
                Questions = questions,
            };

            return viewModel;
        }
        catch(ServiceException ex)
        {
            return new(ex.Response);
        }
    }


    private async Task<List<ViewQuestion>> GetQuestionsAsync(GetQuestionsApiVMParms parms)
    {
        var getquestions = await _questionService.GetQuestionsInCollectionAsync(parms.CollectionId);

        getquestions.ThrowIfError();

        return getquestions.Data ?? new();
    }

}
