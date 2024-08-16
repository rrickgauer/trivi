using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Contracts;

namespace Trivi.Lib.VMServices.Implementations;

public class CollectionSettingsPageVMParms
{
    public required Guid CollectionId { get; set; }
}


[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class CollectionSettingsPageVMService(ICollectionService collectionService) : IAsyncVMService<CollectionSettingsPageVMParms, CollectionPageLayoutModel<CollectionSettingsPageVM>>
{
    private readonly ICollectionService _collectionService = collectionService;

    public async Task<ServiceResponse<CollectionPageLayoutModel<CollectionSettingsPageVM>>> GetViewModelAsync(CollectionSettingsPageVMParms parms)
    {
        try
        {
            var collection = await GetCollectionAsync(parms);

            CollectionSettingsPageVM viewModel = new()
            {
                
            };

            CollectionPageLayoutModel<CollectionSettingsPageVM> layout = new(viewModel)
            {
                ActivePage = ActiveCollectionPage.Settings,
                Collection = collection,
                PageTitle = "General Settings",
            };

            return layout;
        }
        catch(ServiceException ex)
        {
            return new(ex.Errors);
        }
    }

    private async Task<ViewCollection> GetCollectionAsync(CollectionSettingsPageVMParms parms)
    {
        var getCollection = await _collectionService.GetCollectionAsync(parms.CollectionId);

        getCollection.ThrowIfError();

        return NotFoundHttpResponseException.ThrowIfNot<ViewCollection>(getCollection.Data);
    }
}
