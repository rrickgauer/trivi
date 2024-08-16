using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.Lib.VMServices.Implementations;
using Trivi.WebGui.Controllers.Contracts;
using Trivi.WebGui.Filters;

namespace Trivi.WebGui.Controllers.Gui;

[Controller]
[Route("app/collections")]
[ServiceFilter<LoginFirstRedirectFilter>]
public class CollectionsController(CollectionsPageVMService collectionsPageVM, CollectionSettingsPageVMService collectionSettingsPageVM, CollectionQuestionsPageVMService questionsVMService, IGameService gameService) : GuiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix<CollectionsController>();

    private readonly CollectionsPageVMService _collectionsPageVM = collectionsPageVM;
    private readonly CollectionSettingsPageVMService _collectionSettingsPageVM = collectionSettingsPageVM;
    private readonly CollectionQuestionsPageVMService _questionsVMService = questionsVMService;

    private readonly IGameService _gameService = gameService;

    /// <summary>
    /// /app/collections
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName(nameof(CollectionsPageAsync))]
    public async Task<ActionResult> CollectionsPageAsync()
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

    /// <summary>
    /// /app/collections/:collectionId
    /// /app/collections/:collectionId/questions
    /// </summary>
    /// <param name="collectionId"></param>
    /// <returns></returns>
    [HttpGet("{collectionId}")]
    [HttpGet("{collectionId}/questions")]
    [ActionName(nameof(CollectionQuestionsPageAsync))]
    [ServiceFilter<GetCollectionFilter>]
    public async Task<ActionResult> CollectionQuestionsPageAsync([FromRoute] Guid collectionId)
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

    /// <summary>
    /// /app/collections/:collectionId/settings
    /// </summary>
    /// <param name="collectionId"></param>
    /// <returns></returns>
    [HttpGet("{collectionId}/settings")]
    [ActionName(nameof(CollectionPageAsync))]
    [ServiceFilter<GetCollectionFilter>]
    public async Task<ActionResult> CollectionPageAsync([FromRoute] Guid collectionId)
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


    [HttpGet("{collectionId}/setup")]
    [ActionName(nameof(SetupPageAsync))]
    [ServiceFilter<GetCollectionFilter>]
    public ActionResult SetupPageAsync([FromRoute] Guid collectionId)
    {
        return View(GuiPages.CollectionSetup);
    }



}
