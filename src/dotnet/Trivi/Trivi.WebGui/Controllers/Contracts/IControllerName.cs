using System.Runtime.CompilerServices;

namespace Trivi.WebGui.Controllers.Contracts;

public interface IControllerName
{
    private const string ControllerSuffix = "Controller";

    public static abstract string ControllerRedirectName { get; }

    public static string RemoveSuffix(string controllerName)
    {
        string result = controllerName;

        if (controllerName.EndsWith(ControllerSuffix))
        {
            result = controllerName[..^ControllerSuffix.Length];
        }

        return result;
    }
}


public static class ControllerNameExtensions
{
    public static string RemoveMe(this IControllerName controller, string controllerName)
    {
        return IControllerName.RemoveSuffix(controllerName);
    }
}
