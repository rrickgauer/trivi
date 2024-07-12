using Microsoft.AspNetCore.Http;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.TableViews;

namespace Trivi.Lib.Domain.Other;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)] 
public class RequestItems
{
    private readonly IDictionary<object, object?> _items;

    public RequestItems(IHttpContextAccessor contextAccessor)
    {
        ArgumentNullException.ThrowIfNull(contextAccessor.HttpContext, nameof(contextAccessor));
        _items = contextAccessor.HttpContext.Items;
    }


    public ViewCollection Collection
    {
        get => GetItem<ViewCollection>(RequestItemKey.Collection);
        set => SetItem(RequestItemKey.Collection, value);
    }



    private T GetItem<T>(RequestItemKey key) where T : class
    {
        if (!_items.TryGetValue(key, out var result))
        {
            throw new RequestItemNotSetException(key);
        }

        return result as T ?? throw new RequestItemNotSetException(key);
    }

    private void SetItem(RequestItemKey key, object? value)
    {
        if (value == null)
        {
            _items.Remove(key);
        }
        else
        {
            if (_items.ContainsKey(key))
            {
                _items[key] = value;
            }
            else
            {
                _items.Add(key, value);
            }
        }
    }

}
