namespace Trivi.Lib.Domain.Errors;

public class RequestItemNotSetException(RequestItemKey key) : Exception($"Request item not set: {Enum.GetName(key)}")
{
    public RequestItemKey Key { get; } = key;
}
