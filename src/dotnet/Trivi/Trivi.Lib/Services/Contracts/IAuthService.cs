using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IAuthService
{
    public Task<ServiceDataResponse<ViewUser>> LoginUserAsync(LoginForm credentials);
    public Task<ServiceResponse> SignupUserAsync(SignupForm credentials);
    public ServiceDataResponse<bool> IsClientLoggedIn();
    public ServiceResponse Logout();
}
