using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Contracts;

namespace Trivi.Lib.VMServices.Implementations;

public class CollectionQuestionsPageVMParms
{
    public required Guid CollectionId { get; set; }
}


[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class CollectionQuestionsPageVMService(ICollectionService collectionService) : IAsyncVMService<CollectionQuestionsPageVMParms, CollectionPageLayoutModel<CollectionQuestionsPageVM>>
{
    private readonly ICollectionService _collectionService = collectionService;

    public async Task<ServiceDataResponse<CollectionPageLayoutModel<CollectionQuestionsPageVM>>> GetViewModelAsync(CollectionQuestionsPageVMParms parms)
    {
        try
        {
            var collection = await GetCollectionAsync(parms);

            CollectionQuestionsPageVM pageModel = new();

            CollectionPageLayoutModel<CollectionQuestionsPageVM> result = new(pageModel)
            {
                ActivePage = ActiveCollectionPage.Questions,
                Collection = collection,
                PageTitle = "Questions",
            };

            return result;

        }
        catch(ServiceException ex)
        {
            return new(ex.Response);
        }
    }



    private async Task<ViewCollection> GetCollectionAsync(CollectionQuestionsPageVMParms parms)
    {
        var getCollection = await _collectionService.GetCollectionAsync(parms.CollectionId);

        getCollection.ThrowIfError();

        return NotFoundHttpResponseException.ThrowIfNot<ViewCollection>(getCollection.Data);
    }
}
