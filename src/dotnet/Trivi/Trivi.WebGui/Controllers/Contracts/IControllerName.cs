using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Trivi.WebGui.Controllers.Contracts;

public interface IControllerName
{
    private const string ControllerSuffix = "Controller";

    public static abstract string ControllerRedirectName { get; }


    public static string RemoveSuffix<TController>() where TController : ControllerBase
    {
        var controllerName = typeof(TController).Name;
        return RemoveSuffix(controllerName);
    }

    private static string RemoveSuffix(string controllerName)
    {
        string result = controllerName;

        if (controllerName.EndsWith(ControllerSuffix))
        {
            result = controllerName[..^ControllerSuffix.Length];
        }

        return result;
    }
}

