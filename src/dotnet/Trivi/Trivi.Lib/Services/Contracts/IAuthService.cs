using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Services.Contracts;

public interface IAuthService
{
    public Task<ServiceResponse<ViewUser>> LoginUserAsync(LoginForm credentials);
    public Task<ServiceResponse> SignupUserAsync(SignupForm credentials);
    public ServiceResponse<bool> IsClientLoggedIn();
    public ServiceResponse Logout();
}
