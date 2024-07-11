using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;


namespace Trivi.Lib.Repository.Other;

public static class MySqlCommandExtensions
{
    public static async Task<DataTable> GetDataTableAsync(this MySqlCommand command)
    {
        DataTable dataTable = new();

        DbDataReader reader = await command.ExecuteReaderAsync();
        dataTable.Load(reader);

        return dataTable;
    }

}
