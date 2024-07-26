

export enum ApiErrorCode
{

    //#region - Auth -

    AuthInvalidEmailOrPassword = 200,
    AuthEmailTaken = 201,
    AuthPasswordsNotMatch = 202,
    AuthPasswordCriteriaNotMet = 203,

    //#endregion



    //#region - Auth -

    AnswersInvalidAnswerIdFormat = 300,

    //#endregion



    //#region - Auth -

    GamesInvalidQuestionTimeLimit = 400,

    //#endregion



    //#region - Auth -

    JoinGameNicknameAlreadyTaken = 500,
    JoinGameNotFound = 501,
    JoinGameAlreadyFinished = 502,
    JoinGameInvalidNicknameLength = 503,

    //#endregion

}

