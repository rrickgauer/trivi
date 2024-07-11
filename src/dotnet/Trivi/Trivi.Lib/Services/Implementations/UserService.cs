using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Forms;
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

    public async Task<ServiceDataResponse<List<ViewUser>>> GetUsersAsync()
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

    public async Task<ServiceDataResponse<ViewUser>> GetUserAsync(LoginForm credentials)
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
}
