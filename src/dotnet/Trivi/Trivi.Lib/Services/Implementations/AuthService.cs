using Microsoft.AspNetCore.Http;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Configurations;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;

[AutoInject<IAuthService>(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class AuthService(IUserService userService, IHttpContextAccessor contextAccessor, IConfigs configs) : IAuthService
{
    private readonly IUserService _userService = userService;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly IConfigs _configs = configs;

    private HttpContext _context => _contextAccessor.HttpContext!;
    private SessionManager _sessionManager => new(_context.Session);

    

    public async Task<ServiceDataResponse<ViewUser>> LoginUserAsync(LoginForm credentials)
    {
        try
        {
            var user = await GetUserAsync(credentials);
            _sessionManager.StoreClientId(user.UserId);

            return user;
        }
        catch (ServiceResponseException ex)
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

    public async Task<ServiceResponse> SignupUserAsync(SignupForm credentials)
    {
        var validateResult = await ValidateSignupAsync(credentials);

        if (!validateResult.Successful)
        {
            return validateResult;
        }

        // create a new user record in database
        var createUser = await CreateUserAsync(credentials);

        if (!createUser.Successful)
        {
            return createUser;
        }

        // store the user's id in the session
        _sessionManager.StoreClientId(createUser.Data?.UserId);

        return new();
    }

    private async Task<ServiceResponse> ValidateSignupAsync(SignupForm credentials)
    {
        ServiceResponse result = new();

        // make sure passwords match
        if (credentials.PasswordConfirm != credentials.Password)
        {
            result.AddError(ErrorCode.AuthPasswordsNotMatch);
        }

        // ensure password meets criteria
        if (credentials.Password.Length < _configs.MinimumUserPasswordLength)
        {
            result.AddError(ErrorCode.AuthPasswordCriteriaNotMet);
        }

        // ensure the email is not already taken
        var getUser = await _userService.GetUserAsync(credentials.Email);

        if (!getUser.Successful)
        {
            result.Errors.AddRange(getUser.Errors);
            return result;
        }

        if (getUser.Data is not null)
        {
            result.Errors.Add(ErrorCode.AuthEmailTaken);
        }

        return result;
    }

    private async Task<ServiceDataResponse<ViewUser>> CreateUserAsync(SignupForm credentials)
    {
        var newUserModel = User.FromSignup(credentials);

        var createUser = await _userService.CreateUserAsync(newUserModel);

        return createUser;
    }

    public ServiceDataResponse<bool> IsClientLoggedIn()
    {
        if (_sessionManager.ClientId is not Guid clientId)
        {
            return false;
        }

        return true;
    }

    public ServiceResponse Logout()
    {
        _sessionManager.ClearClientId();
        return new();
    }
}
