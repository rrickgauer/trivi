using System.Net;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Repository.Contracts;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Services.Implementations;


[AutoInject<IUserService>(AutoInjectionType.Scoped, InjectionProject.Always)]
public class UserService(IUserRepository repo, ITableMapperService tableMapperService) : IUserService
{
    private readonly IUserRepository _repo = repo;
    private readonly ITableMapperService _tableMapperService = tableMapperService;

    public async Task<ServiceResponse<List<ViewUser>>> GetUsersAsync()
    {

        try
        {
            var table = await _repo.SelectUsersAsync();
            return _tableMapperService.ToModels<ViewUser>(table);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceResponse<ViewUser>> GetUserAsync(LoginForm credentials)
    {
        try
        {
            var row = await _repo.SelectUserAsync(credentials.Email, credentials.Password);

            if (row == null)
            {
                return new();
            }

            return _tableMapperService.ToModel<ViewUser>(row);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceResponse<ViewUser>> GetUserAsync(string email)
    {
        try
        {
            var row = await _repo.SelectUserAsync(email);

            if (row == null)
            {
                return new();
            }

            return _tableMapperService.ToModel<ViewUser>(row);
        }
        catch (RepositoryException ex)
        {
            return ex;
        }
    }

    public async Task<ServiceResponse<ViewUser>> CreateUserAsync(User user)
    {
        try
        {
            await _repo.InsertUserAsync(user);
            return await GetUserAsync(user.Email!);
        }
        catch(RepositoryException ex)
        {
            return ex;
        }
    }
}
