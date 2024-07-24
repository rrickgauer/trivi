using Trivi.Lib.Auth.Contracts;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Auth.Implementations;

public class PostGameAuthParms
{
    public required NewGameForm NewGameForm { get; set; }
    public required Guid ClientId { get; set; }
}

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class PostGameAuth(ICollectionService collectionService) : IAsyncPermissionsAuth<PostGameAuthParms>
{
    private readonly ICollectionService _collectionService = collectionService;

    public async Task<ServiceResponse> HasPermissionAsync(PostGameAuthParms data)
    {
        try
        {
            var collection = await GetCollectionAsync(data);

            if (collection.UserId != data.ClientId)
            {
                throw new ForbiddenHttpResponseException();
            }

            return new();
        }
        catch(ServiceException e)
        {
            return e.Response;
        }
    }

    private async Task<ViewCollection> GetCollectionAsync(PostGameAuthParms data)
    {
        var getCollection = await _collectionService.GetCollectionAsync(data.NewGameForm.CollectionId);
        getCollection.ThrowIfError();
        return getCollection.GetData();
    }
}
