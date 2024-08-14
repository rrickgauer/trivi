using Trivi.Lib.Domain.Responses;

namespace Trivi.Lib.VMServices.Contracts;

public interface IVMService<in TParms, TViewModel>
{
    public ServiceResponse<TViewModel> GetViewModel(TParms parms);
}

