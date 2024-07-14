using System.Data;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Repository.Contracts;

public interface ICollectionRepository
{
    public Task<DataTable> SelectUserCollectionsAsync(Guid userId);
    public Task<DataRow?> SelectCollectionAsync(Guid collectionId);
    public Task<int> InsertCollectionAsync(Collection collection);
    public Task<int> UpdateCollectionAsync(Collection collection);
    public Task<int> DeleteCollectionAsync(Guid collectionId);
}
