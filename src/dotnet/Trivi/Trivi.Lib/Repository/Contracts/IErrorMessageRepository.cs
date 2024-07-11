using System.Data;

namespace Trivi.Lib.Repository.Contracts;

public interface IErrorMessageRepository
{
    public Task<DataTable> SelectErrorMessagesAsync();
}
