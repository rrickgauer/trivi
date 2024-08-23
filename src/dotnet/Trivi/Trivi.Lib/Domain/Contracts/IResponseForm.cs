using Trivi.Lib.Domain.Models;
using Trivi.Lib.Domain.Other;

namespace Trivi.Lib.Domain.Contracts;

public interface IResponseForm<TResponse> where TResponse : Response
{
    public TResponse ToResponse(QuestionId questionId);
}




