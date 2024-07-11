using System.Data;

namespace Trivi.Lib.Repository.Contracts;

public interface IUserRepository
{
    public Task<DataTable> SelectUsersAsync();

    public Task<DataRow?> SelectUserAsync(string email, string password);
}
