using Microsoft.AspNetCore.Http;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IAuthService>(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class AuthService(IUserService userService, IHttpContextAccessor contextAccessor) : IAuthService
{
    private readonly IUserService _userService = userService;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    private HttpContext _context => _contextAccessor.HttpContext!;

    public async Task<ServiceDataResponse<ViewUser>> LoginUserAsync(LoginForm credentials)
    {

        try
        {

            var user = await GetUserAsync(credentials);

            SessionManager manager = new(_context.Session);
            manager.StoreClientId(user.UserId);

            return user;
        }
        catch(ServiceResponseException ex)
        {
            return new(ex.Response);
        }
    }

    private async Task<ViewUser> GetUserAsync(LoginForm credentials)
    {
        var getUser = await _userService.GetUserAsync(credentials);

        getUser.ThrowIfError();

        if (getUser.Data is not ViewUser user)
        {
            throw new ServiceResponseException(ErrorCode.AuthInvalidEmailOrPassword);
        }

        return user;
    }
}
