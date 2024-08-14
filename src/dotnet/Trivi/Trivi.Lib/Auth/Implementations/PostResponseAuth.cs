using Trivi.Lib.Auth.Contracts;
using Trivi.Lib.Domain.Attributes;
using Trivi.Lib.Domain.Errors;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;
using Trivi.Lib.Services.Contracts;

namespace Trivi.Lib.Auth.Implementations;

public class PostResponseAuthParms
{
    public required QuestionId QuestionId { get; set; }
    public required Guid PlayerId { get; set; }
}

[AutoInject(AutoInjectionType.Scoped, InjectionProject.Always)] 
public class PostResponseAuth(IResponseService responseService, RequestItems requestItems) : IAsyncPermissionsAuth<PostResponseAuthParms>
{
    private readonly IResponseService _responseService = responseService;
    private readonly RequestItems _requestItems = requestItems;

    public async Task<ServiceResponse> HasPermissionAsync(PostResponseAuthParms data)
    {
        var getResponse = await _responseService.GetResponseAsync(new()
        {
            PlayerId = data.PlayerId,
            QuestionId = data.QuestionId,
        });

        
        if (!getResponse.Successful)
        {
            return new(getResponse.Errors);
        }

        if (getResponse.Data != null)
        {
            throw new ForbiddenHttpResponseException();
        }


        return new();
    }




}
