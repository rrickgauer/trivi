using MySql.Data.MySqlClient;
using System.Data;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Repository.Commands;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Repository.Other;

namespace Trivi.Lib.Repository.Implementations;

[AutoInject<ICollectionRepository>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class CollectionRepository(DatabaseConnection connection) : ICollectionRepository
{
    private readonly DatabaseConnection _connection = connection;

    public async Task<DataRow?> SelectCollectionAsync(Guid collectionId)
    {
        MySqlCommand command = new(CollectionRepositoryCommands.SelectById);
        command.Parameters.AddWithValue("@collection_id", collectionId);

        return await _connection.FetchAsync(command);
    }

    public async Task<DataTable> SelectUserCollectionsAsync(Guid userId)
    {
        MySqlCommand command = new(CollectionRepositoryCommands.SelectAllUserCollections);

        command.Parameters.AddWithValue("@user_id", userId);

        return await _connection.FetchAllAsync(command);
    }

    public async Task<int> InsertCollectionAsync(Collection collection)
    {
        return await UpsertCollectionAsync(collection);
    }

    public async Task<int> UpdateCollectionAsync(Collection collection)
    {
        return await UpsertCollectionAsync(collection);
    }


    private async Task<int> UpsertCollectionAsync(Collection collection)
    {
        MySqlCommand command = new(CollectionRepositoryCommands.Save);

        command.Parameters.AddWithValue("@id", collection.Id);
        command.Parameters.AddWithValue("@name", collection.Name);
        command.Parameters.AddWithValue("@user_id", collection.UserId);
        command.Parameters.AddWithValue("@created_on", collection.CreatedOn);

        return await _connection.ModifyAsync(command);
    }

    public async Task<int> DeleteCollectionAsync(Guid collectionId)
    {
        MySqlCommand command = new(CollectionRepositoryCommands.Delete);

        command.Parameters.AddWithValue("@collection_id", collectionId);

        return await _connection.ModifyAsync(command);
    }


}
