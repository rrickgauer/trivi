using Microsoft.AspNetCore.Mvc;

namespace Trivi.WebGui.ViewComponents;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

public abstract class ViewComp<T> : ViewComponent
{
    public abstract string RazorFileName { get; }

    protected string ViewFile => $"~/Views/Shared/Components/{RazorFileName}.cshtml";

    public async Task<IViewComponentResult> InvokeAsync(T model)
    {
        return View(ViewFile, model);
    }

}
