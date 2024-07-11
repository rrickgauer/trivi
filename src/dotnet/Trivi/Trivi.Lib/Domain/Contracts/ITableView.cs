﻿
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.Contracts;

public interface ITableView<TView, TModel>
    where TModel : new()
    where TView : ITableView<TView, TModel>
{
    public static abstract explicit operator TModel(TView other);

    private static IEnumerable<PropertyAttribute<CopyToPropertyAttribute<TModel>>> ViewProperties => PropertyAttribute<CopyToPropertyAttribute<TModel>>.GetAllPropertiesInClass<TView>();

    public TModel CastToModel()
    {
        TModel result = new();

        foreach (var property in ViewProperties)
        {
            var value = property.GetPropertyValueRaw(this);
            var modelPropertyName = property.Attribute.Name;
            result.GetType()?.GetProperty(modelPropertyName)?.SetValue(result, value);
        }

        return result;
    }

    public TModel CastToModelTry()
    {
        TModel result = new();

        foreach (var property in ViewProperties)
        {
            try
            {
                var value = property.GetPropertyValueRaw(this);
                var modelPropertyName = property.Attribute.Name;
                result.GetType()?.GetProperty(modelPropertyName)?.SetValue(result, value);
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                throw;
            }
        }

        return result;
    }
}

public static class TableViewExtensions
{
    public static TModel CastToModel<TView, TModel>(this ITableView<TView, TModel> tableView)
        where TModel : new()
        where TView : ITableView<TView, TModel>
    {
        return tableView.CastToModel();
    }


    public static TModel CastToModelTry<TView, TModel>(this ITableView<TView, TModel> tableView)
    where TModel : new()
    where TView : ITableView<TView, TModel>
    {
        return tableView.CastToModelTry();
    }



}
