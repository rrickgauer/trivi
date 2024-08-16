using Microsoft.AspNetCore.Http;
using Trivi.Lib.Domain.Attributes;

namespace Trivi.Lib.Domain.Other;

[AutoInject(AutoInjectionType.Scoped, InjectionProject.WebGui)]
public class ResultFilterCache
{
    private readonly IDictionary<object, object?> _items;

    public ResultFilterCache(IHttpContextAccessor contextAccessor)
    {
        ArgumentNullException.ThrowIfNull(contextAccessor.HttpContext, nameof(contextAccessor));
        _items = contextAccessor.HttpContext.Items;
    }

    public string? GameId
    {
        get => GetItemNull<string?>(ResultFilterCacheKey.GameId);
        set => SetItemNull(ResultFilterCacheKey.GameId, value);
    }


    private T? GetItemNull<T>(ResultFilterCacheKey key)
    {
        if (!_items.TryGetValue(key.GetKeyText(), out var result))
        {
            return default;
        }

        if (result == null)
        {
            return default;
        }

        return (T)result;
    }

    private void SetItemNull(ResultFilterCacheKey key, object? value)
    {
        var keyText = key.GetKeyText();

        if (value == null)
        {
            _items.Remove(keyText);
            return;
        }


        if (_items.ContainsKey(keyText))
        {
            _items[keyText] = value;
        }
        else
        {
            _items.Add(keyText, value);
        }

    }
}
