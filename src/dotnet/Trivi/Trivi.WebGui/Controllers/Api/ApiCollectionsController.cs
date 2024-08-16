using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Domain.TableViews;
using Trivi.Lib.Filters;
using Trivi.Lib.Services.Contracts;
using Trivi.WebGui.Controllers.Contracts;
using Trivi.WebGui.Filters;

namespace Trivi.WebGui.Controllers.Api;

[ApiController]
[Route("api/collections")]
[ServiceFilter<InternalApiAuthFilter>]
public class ApiCollectionsController(ICollectionService collectionService) : InternalApiController, IControllerName
{
    public static string ControllerRedirectName => IControllerName.RemoveSuffix<ApiCollectionsController>();

    private readonly ICollectionService _collectionService = collectionService;

    /// <summary>
    /// POST: /api/collections
    /// </summary>
    /// <param name="collectionData"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName(nameof(PostCollectionAsync))]
    public async Task<ActionResult<ServiceResponse<ViewCollection>>> PostCollectionAsync([FromBody] CollectionForm collectionData)
    {
        var newCollection = Collection.FromForm(collectionData, ClientId);
        
        var saveResult = await _collectionService.CreateCollectionAsync(newCollection);
        
        return saveResult.ToActionCreated(saveResult.Data?.UriGui);
    }

    /// <summary>
    /// GET: /api/collections
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName(nameof(GetCollectionsAsync))]
    public async Task<ActionResult<ServiceResponse<List<ViewCollection>>>> GetCollectionsAsync()
    {
        var getCollections = await _collectionService.GetUserCollectionsAsync(ClientId);

        return getCollections.ToAction();
    }

    /// <summary>
    /// PUT: /api/collections/:collectionId
    /// </summary>
    /// <param name="collectionId"></param>
    /// <param name="collectionForm"></param>
    /// <returns></returns>
    [HttpPut("{collectionId}")]
    [ActionName(nameof(PutCollectionAsync))]
    [ServiceFilter<GetCollectionFilter>]
    public async Task<ActionResult<ServiceResponse<ViewCollection>>> PutCollectionAsync([FromRoute] Guid collectionId, [FromBody] CollectionForm collectionForm)
    {
        Collection collection = new()
        {
            Id = collectionId,
            Name = collectionForm.Name,
            UserId = ClientId,
        };

        var updateResult = await _collectionService.UpdateCollectionAsync(collection);

        return updateResult.ToAction();
    }

    /// <summary>
    /// DELETE: /api/collections/:collectionId
    /// </summary>
    /// <param name="collectionId"></param>
    /// <returns></returns>
    [HttpDelete("{collectionId}")]
    [ActionName(nameof(DeleteCollectionAsync))]
    [ServiceFilter<GetCollectionFilter>]
    public async Task<ActionResult<ServiceResponse>> DeleteCollectionAsync([FromRoute] Guid collectionId)
    {
        var updateResult = await _collectionService.DeleteCollectionAsync(collectionId);

        return updateResult.ToAction();
    }

}
