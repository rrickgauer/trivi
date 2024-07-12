using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IUserService
{
    public Task<ServiceDataResponse<List<ViewUser>>> GetUsersAsync();
    public Task<ServiceDataResponse<ViewUser>> GetUserAsync(LoginForm credentials);
    public Task<ServiceDataResponse<ViewUser>> GetUserAsync(string email);
    public Task<ServiceDataResponse<ViewUser>> CreateUserAsync(User user);
}
