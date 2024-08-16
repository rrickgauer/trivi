using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IUserService
{
    public Task<ServiceResponse<List<ViewUser>>> GetUsersAsync();
    public Task<ServiceResponse<ViewUser>> GetUserAsync(LoginForm credentials);
    public Task<ServiceResponse<ViewUser>> GetUserAsync(string email);
    public Task<ServiceResponse<ViewUser>> CreateUserAsync(User user);
}
