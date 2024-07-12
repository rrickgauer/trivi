using Trivi.Lib.Domain.Models;

namespace Trivi.Lib.Domain.Responses;

public class ApiResponse<T>
{
    public List<ErrorMessage> Errors { get; set; } = new();
    public T? Data { get; set; }
}
