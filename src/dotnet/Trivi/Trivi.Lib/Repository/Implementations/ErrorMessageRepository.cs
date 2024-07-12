using MySql.Data.MySqlClient;
using System.Data;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Repository.Commands;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Repository.Other;

namespace Trivi.Lib.Repository.Implementations;

[AutoInject<IErrorMessageRepository>(AutoInjectionType.Singleton, InjectionProject.Always)]
public class ErrorMessageRepository(DatabaseConnection dbConnection) : IErrorMessageRepository
{
    private readonly DatabaseConnection _dbConnection = dbConnection;

    public async Task<DataTable> SelectErrorMessagesAsync()
    {
        MySqlCommand command = new(ErrorMessageRepositoryCommands.SelectAll);

        return await _dbConnection.FetchAllAsync(command);
    }
}
