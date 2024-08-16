using Microsoft.AspNetCore.Http;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Models;
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

    public ViewQuestion Question
    {
        get => GetItem<ViewQuestion>(RequestItemKey.Question);
        set => SetItem(RequestItemKey.Question, value);
    }

    public ViewAnswer Answer
    {
        get => GetItem<ViewAnswer>(RequestItemKey.Answer);
        set => SetItem(RequestItemKey.Answer, value);
    }

    public ViewGame Game
    {
        get => GetItem<ViewGame>(RequestItemKey.Game);
        set => SetItem(RequestItemKey.Game, value);
    }

    public ViewPlayer Player
    {
        get => GetItem<ViewPlayer>(RequestItemKey.Player);
        set => SetItem(RequestItemKey.Player, value);
    }

    public ViewResponse ResponseResult
    {
        get => GetItem<ViewResponse>(RequestItemKey.ResponseResult);
        set => SetItem(RequestItemKey.ResponseResult, value);
    }


    private T GetItem<T>(RequestItemKey key) where T : class
    {
        if (!_items.TryGetValue(key.GetKeyText(), out var result))
        {
            throw new RequestItemNotSetException(key);
        }

        return result as T ?? throw new RequestItemNotSetException(key);
    }

    private void SetItem(RequestItemKey key, object? value)
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
