using MySql.Data.MySqlClient;
using System.Data;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Repository.Commands;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Repository.Other;

namespace Trivi.Lib.Repository.Implementations;


[AutoInject<IUserRepository>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class UserRepository(DatabaseConnection connection) : IUserRepository
{
    private readonly DatabaseConnection _connection = connection;

    public async Task<DataTable> SelectUsersAsync()
    {
        MySqlCommand command = new(UserRepositoryCommands.SelectAll);

        return await _connection.FetchAllAsync(command);
    }

    public async Task<DataRow?> SelectUserAsync(string email, string password)
    {
        MySqlCommand command = new(UserRepositoryCommands.SelectByEmailPassword);

        command.Parameters.AddWithValue("@email", email);
        command.Parameters.AddWithValue("@password", password);

        return await _connection.FetchAsync(command);
    }

    public async Task<DataRow?> SelectUserAsync(string email)
    {
        MySqlCommand command = new(UserRepositoryCommands.SelectByEmail);

        command.Parameters.AddWithValue("@email", email);

        return await _connection.FetchAsync(command);
    }

    public async Task<int> InsertUserAsync(User user)
    {
        MySqlCommand command = new(UserRepositoryCommands.Insert);

        command.Parameters.AddWithValue("@id", user.Id);
        command.Parameters.AddWithValue("@email", user.Email);
        command.Parameters.AddWithValue("@password", user.Password);
        command.Parameters.AddWithValue("@created_on", user.CreatedOn);

        return await _connection.ModifyAsync(command);
    }
}