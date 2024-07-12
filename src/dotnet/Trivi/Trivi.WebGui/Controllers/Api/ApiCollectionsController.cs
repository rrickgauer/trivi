using Microsoft.AspNetCore.Mvc;
using Trivi.Lib.Domain.Forms;
using Trivi.Lib.Domain.Models;
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
    public static string ControllerRedirectName => IControllerName.RemoveSuffix(nameof(ApiCollectionsController));

    private readonly ICollectionService _collectionService = collectionService;

    [HttpPost]
    [ActionName(nameof(PostCollectionAsync))]
    public async Task<IActionResult> PostCollectionAsync([FromBody] CollectionForm collectionData)
    {
        var newCollection = Collection.FromForm(collectionData, ClientId);
        var saveResult = await _collectionService.CreateCollectionAsync(newCollection);

        if (!saveResult.Successful)
        {
            return BadRequest(saveResult);
        }

        var uri = saveResult.Data?.UriGui;

        return Created(uri, saveResult);
    }

    [HttpGet]
    [ActionName(nameof(GetCollectionsAsync))]
    public async Task<IActionResult> GetCollectionsAsync()
    {
        var getCollections = await _collectionService.GetUserCollectionsAsync(ClientId);

        if (!getCollections.Successful)
        {
            return BadRequest(getCollections);
        }    

        return Ok(getCollections);

    }


    [HttpPut("{collectionId}")]
    [ActionName(nameof(PutCollectionAsync))]
    [ServiceFilter<GetCollectionFilter>]
    public async Task<IActionResult> PutCollectionAsync([FromRoute] Guid collectionId, [FromBody] CollectionForm collectionForm)
    {
        Collection collection = new()
        {
            Id = collectionId,
            Name = collectionForm.Name,
            UserId = ClientId,
        };


        var updateResult = await _collectionService.UpdateCollectionAsync(collection);

        if (!updateResult.Successful)
        { 
            return BadRequest(updateResult); 
        }

        return Ok(updateResult);
    }



    [HttpDelete("{collectionId}")]
    [ActionName(nameof(DeleteCollectionAsync))]
    [ServiceFilter<GetCollectionFilter>]
    public async Task<IActionResult> DeleteCollectionAsync([FromRoute] Guid collectionId)
    {
        var updateResult = await _collectionService.DeleteCollectionAsync(collectionId);

        if (!updateResult.Successful)
        {
            return BadRequest(updateResult);
        }

        return NoContent();
    }

}
