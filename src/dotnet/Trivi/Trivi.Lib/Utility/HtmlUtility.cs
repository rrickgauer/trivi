using Microsoft.AspNetCore.Html;

namespace Trivi.Lib.Utility;

public static class HtmlUtility
{
    public static HtmlString ToHtml(object? data)
    {
        return new HtmlString($"{data}");
    }
}
