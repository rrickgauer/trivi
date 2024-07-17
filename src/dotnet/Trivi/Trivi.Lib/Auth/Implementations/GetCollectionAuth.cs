using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;
using Trivi.Lib.Auth.Contracts;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Auth.Implementations;

public class GetCollectionAuthParms
{
    public required Guid ClientId { get; set; }
    public required Guid CollectionId { get; set; }
}


[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class GetCollectionAuth(RequestItems requestItems, ICollectionService collectionService) : IAsyncPermissionsAuth<GetCollectionAuthParms>
{
    private readonly RequestItems _requestItems = requestItems;
    private readonly ICollectionService _collectionService = collectionService;

    public async Task<ServiceResponse> HasPermissionAsync(GetCollectionAuthParms data)
    {
        try
        {
            var collection = await GetCollectionAsync(data);

            if (collection.UserId != data.ClientId)
            {
                throw new ForbiddenHttpResponseException();
            }

            _requestItems.Collection = collection;

            return new();
        }
        catch(ServiceException ex)
        {
            return ex.Response;
        }
    }


    private async Task<ViewCollection> GetCollectionAsync(GetCollectionAuthParms data)
    {
        var getCollection = await _collectionService.GetCollectionAsync(data.CollectionId);

        getCollection.ThrowIfError();

        return NotFoundHttpResponseException.ThrowIfNot<ViewCollection>(getCollection.Data);
    }

}
