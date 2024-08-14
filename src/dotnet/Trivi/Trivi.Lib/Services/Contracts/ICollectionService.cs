using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface ICollectionService
{
    public Task<ServiceResponse<List<ViewCollection>>> GetUserCollectionsAsync(Guid userId);
    public Task<ServiceResponse<ViewCollection>> GetCollectionAsync(Guid collectionId);
    public Task<ServiceResponse<ViewCollection>> CreateCollectionAsync(Collection newCollection);
    public Task<ServiceResponse<ViewCollection>> UpdateCollectionAsync(Collection newCollection);
    public Task<ServiceResponse> DeleteCollectionAsync(Guid collectionId);

}
