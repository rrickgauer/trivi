using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface ICollectionService
{
    public Task<ServiceDataResponse<List<ViewCollection>>> GetUserCollectionsAsync(Guid userId);
    public Task<ServiceDataResponse<ViewCollection>> GetCollectionAsync(Guid collectionId);
    public Task<ServiceDataResponse<ViewCollection>> CreateCollectionAsync(Collection newCollection);
    public Task<ServiceDataResponse<ViewCollection>> UpdateCollectionAsync(Collection newCollection);
    public Task<ServiceResponse> DeleteCollectionAsync(Guid collectionId);

}
