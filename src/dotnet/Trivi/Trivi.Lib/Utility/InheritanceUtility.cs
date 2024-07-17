﻿
using System.Data;
using System.Reflection;
using Trivi.Lib.Mapping.Tables;

namespace Trivi.Lib.Utility;

public class InheritanceUtility
{

    public static void CopyParentProperties<TParent, TChild>(TParent parent, TChild child) where TChild : TParent
    {
        var parentProps = typeof(TParent).GetProperties(BindingFlags.Instance | BindingFlags.Public);

        foreach (var parentProperty in parentProps)
        {
            if (typeof(TChild).GetProperty(parentProperty.Name) is not PropertyInfo childProperty)
            {
                continue;
            }

            if (childProperty.GetSetMethod() == null)
            {
                continue;
            }

            var value = parentProperty.GetValue(parent);
            childProperty.SetValue(child, value);
        }
    }


    public static TChild GetParentProperties<TParent, TChild, TParentMapper>(DataRow row)
        where TChild : TParent, new()
        where TParentMapper : TableMapper<TParent>, new()
    {
        // get the mapped parent values
        TParentMapper parentMapper = new();
        TParent parent = parentMapper.ToModel(row);

        // create a new post instance
        TChild model = new();

        // copy over the parent properties into the child instance
        CopyParentProperties(parent, model);

        return model;
    }



}
