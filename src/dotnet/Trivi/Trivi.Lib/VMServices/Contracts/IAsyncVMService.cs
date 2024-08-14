using Trivi.Lib.Domain.Responses;

namespace Trivi.Lib.VMServices.Contracts;

public interface IAsyncVMService<in TParms, TViewModel>
{
    public Task<ServiceResponse<TViewModel>> GetViewModelAsync(TParms parms);
}

