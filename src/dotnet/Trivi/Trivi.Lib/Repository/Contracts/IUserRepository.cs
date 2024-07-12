using System.Data;
using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Repository.Contracts;

public interface IUserRepository
{
    public Task<DataTable> SelectUsersAsync();

    public Task<DataRow?> SelectUserAsync(string email, string password);
    public Task<DataRow?> SelectUserAsync(string email);

    public Task<int> InsertUserAsync(User user);
}
