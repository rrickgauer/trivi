using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<ICollectionService>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class CollectionService(ICollectionRepository repo, ITableMapperService tableMapperService) : ICollectionService
{
    private readonly ICollectionRepository _repo = repo;
    private readonly ITableMapperService _tableMapperService = tableMapperService;

    public async Task<ServiceDataResponse<List<ViewCollection>>> GetUserCollectionsAsync(Guid userId)
    {
        try
        {
            var table = await _repo.SelectUserCollectionsAsync(userId);
            return _tableMapperService.ToModels<ViewCollection>(table);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceDataResponse<ViewCollection>> GetCollectionAsync(Guid collectionId)
    {
        try
        {
            var row = await _repo.SelectCollectionAsync(collectionId);

            if (row != null)
            {
                return _tableMapperService.ToModel<ViewCollection>(row);
            }

            return new();
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceDataResponse<ViewCollection>> CreateCollectionAsync(Collection newCollection)
    {
        try
        {
            var insertResult = await _repo.InsertCollectionAsync(newCollection);
            return await GetCollectionAsync(newCollection.Id!.Value);
        }
        catch(RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceDataResponse<ViewCollection>> UpdateCollectionAsync(Collection newCollection)
    {
        try
        {
            var insertResult = await _repo.UpdateCollectionAsync(newCollection);
            return await GetCollectionAsync(newCollection.Id!.Value);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceResponse> DeleteCollectionAsync(Guid collectionId)
    {
        try
        {
            var insertResult = await _repo.DeleteCollectionAsync(collectionId);

            return new();
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }
}
