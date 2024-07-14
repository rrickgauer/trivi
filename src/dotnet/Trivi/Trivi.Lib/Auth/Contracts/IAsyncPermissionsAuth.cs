using Trivi.Lib.Domain.Responses;

namespace Trivi.Lib.Auth.Contracts;

public interface IAsyncPermissionsAuth<in T>
{
    public Task<ServiceResponse> HasPermissionAsync(T data);
}