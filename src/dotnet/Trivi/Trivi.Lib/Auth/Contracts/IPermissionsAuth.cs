using Trivi.Lib.Domain.Responses;

namespace Trivi.Lib.Auth.Contracts;

public interface IPermissionsAuth<in T>
{
    public ServiceResponse HasPermission(T data);
}
