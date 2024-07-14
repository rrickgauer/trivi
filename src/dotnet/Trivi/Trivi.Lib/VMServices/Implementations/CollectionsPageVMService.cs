using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Domain.ViewModels.Gui;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Contracts;
using Trivi.Lib.VMServices.Parms;

namespace Trivi.Lib.VMServices.Implementations;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class CollectionsPageVMService(ICollectionService collectionService) : IAsyncVMService<CollectionsPageVMParms, CollectionsPageVM>
{
    private readonly ICollectionService _collectionService = collectionService;

    public async Task<ServiceDataResponse<CollectionsPageVM>> GetViewModelAsync(CollectionsPageVMParms parms)
    {
        try
        {
            var collections = await GetCollectionsAsync(parms);

            CollectionsPageVM result = new()
            {
                Collections = collections,
            };

            return result;

        }
        catch(ServiceResponseException ex)
        {
            return new(ex.Errors);
        }
    }

    private async Task<List<ViewCollection>> GetCollectionsAsync(CollectionsPageVMParms parms)
    {
        var getCollections = await _collectionService.GetUserCollectionsAsync(parms.ClientId);

        getCollections.ThrowIfError();

        return getCollections.Data ?? new();
    }
}
