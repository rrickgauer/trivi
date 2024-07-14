using Trivi.Lib.Domain.Responses;

namespace Trivi.Lib.VMServices.Contracts;

public interface IVMService<in TParms, TViewModel>
{
    public ServiceDataResponse<TViewModel> GetViewModel(TParms parms);
}

