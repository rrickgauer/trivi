using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;
using Trivi.Lib.Domain.Responses;

namespace Trivi.Lib.Services.Contracts;

public interface IGradingService
{
    public Task<ServiceResponse<ResponseGrade>> GradeResponseAsync(ResponseTrueFalse response);
    public Task<ServiceResponse<ResponseGrade>> GradeResponseAsync(ResponseMultipleChoice response);
    public Task<ServiceResponse<ResponseGrade>> GradeResponseAsync(ResponseShortAnswer response);

}






