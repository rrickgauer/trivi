
using Trivi.Lib.Domain.Attributes;

namespace Trivi.Lib.Domain.Enums;

public enum ErrorCode : ulong
{
    #region - Auth -
    [ErrorCodeGroup(ErrorCodeGroup.Authorization)]
    AuthInvalidEmailOrPassword = 200,

    [ErrorCodeGroup(ErrorCodeGroup.Authorization)]
    AuthEmailTaken = 201,

    [ErrorCodeGroup(ErrorCodeGroup.Authorization)]
    AuthPasswordsNotMatch = 202,

    [ErrorCodeGroup(ErrorCodeGroup.Authorization)]
    AuthPasswordCriteriaNotMet = 203,

    #endregion
}
