using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Filters;
using Trivi.Lib.VMServices.Implementations;
using Trivi.WebGui.Controllers.Contracts;
using Trivi.WebGui.Filters;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("app/collections")]
[ServiceFilter<LoginFirstRedirectFilter>]
public class CollectionsController(CollectionsPageVMService collectionsPageVM, CollectionSettingsPageVMService collectionSettingsPageVM, CollectionQuestionsPageVMService questionsVMService) : GuiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix(nameof(CollectionsController));

    private readonly CollectionsPageVMService _collectionsPageVM = collectionsPageVM;
    private readonly CollectionSettingsPageVMService _collectionSettingsPageVM = collectionSettingsPageVM;
    private readonly CollectionQuestionsPageVMService _questionsVMService = questionsVMService;


    [HttpGet]
    [ActionName(nameof(CollectionsPageAsync))]
    public async Task<IActionResult> CollectionsPageAsync()
    {
        var getViewModel = await _collectionsPageVM.GetViewModelAsync(new()
        {
            ClientId = ClientId,
        });

        if (!getViewModel.Successful)
        {
            return BadRequest(getViewModel);
        }

        return View(GuiPages.Collections, getViewModel.Data);
    }

    [HttpGet("{collectionId}")]
    [HttpGet("{collectionId}/settings")]
    [ActionName(nameof(CollectionPageAsync))]
    [ServiceFilter<GetCollectionFilter>]
    public async Task<IActionResult> CollectionPageAsync([FromRoute] Guid collectionId)
    {
        var getVM = await _collectionSettingsPageVM.GetViewModelAsync(new()
        {
            CollectionId = collectionId,
        });


        if (!getVM.Successful)
        {
            return BadRequest(getVM);
        }

        return View(GuiPages.CollectionSettings, getVM.Data);
    }

    [HttpGet("{collectionId}/questions")]
    [ActionName(nameof(CollectionQuestionsPageAsync))]
    [ServiceFilter<GetCollectionFilter>]
    public async Task<IActionResult> CollectionQuestionsPageAsync([FromRoute] Guid collectionId)
    {
        var getVM = await _questionsVMService.GetViewModelAsync(new()
        {
            CollectionId = collectionId,
        });

        if (!getVM.Successful)
        {
            return BadRequest(getVM);
        }

        return View(GuiPages.CollectionQuestions, getVM.Data);
    }


}
