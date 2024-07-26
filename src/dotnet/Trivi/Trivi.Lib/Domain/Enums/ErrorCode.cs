
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

    #region - Answers -

    [ErrorCodeGroup(ErrorCodeGroup.Answers)]
    AnswersInvalidAnswerIdFormat = 300,

    #endregion

    #region - Games -

    [ErrorCodeGroup(ErrorCodeGroup.Games)]
    GamesInvalidQuestionTimeLimit = 400,

    [ErrorCodeGroup(ErrorCodeGroup.Games)]
    GamesStartNonOpenGame = 401,

    #endregion


    #region - Join Game -

    [ErrorCodeGroup(ErrorCodeGroup.JoinGame)]
    JoinGameNicknameAlreadyTaken = 500,
    
    [ErrorCodeGroup(ErrorCodeGroup.JoinGame)]
    JoinGameNotFound = 501,
    
    [ErrorCodeGroup(ErrorCodeGroup.JoinGame)]
    JoinGameAlreadyFinished = 502,

    [ErrorCodeGroup(ErrorCodeGroup.JoinGame)]
    JoinGameInvalidNicknameLength = 503,

    #endregion

}
