using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IUserService
{
    public Task<ServiceDataResponse<List<ViewUser>>> GetUsersAsync();

    public Task<ServiceDataResponse<ViewUser>> GetUserAsync(LoginForm credentials);
}
