

export enum ApiErrorCode
{

    //#region - Auth -

    AuthInvalidEmailOrPassword = 200,
    AuthEmailTaken = 201,
    AuthPasswordsNotMatch = 202,
    AuthPasswordCriteriaNotMet = 203,

    //#endregion

    //#region - Answers -

    AnswersInvalidAnswerIdFormat = 300,

    //#endregion



    //#region - Games -

    GamesInvalidQuestionTimeLimit = 400,
    GamesStartNonOpenGame = 401,
    GamesCloseQuestion = 402,

    //#endregion



    //#region - Join game -

    JoinGameNicknameAlreadyTaken = 500,
    JoinGameNotFound = 501,
    JoinGameAlreadyFinished = 502,
    JoinGameInvalidNicknameLength = 503,

    //#endregion


    //#region - Responses -

    ResponsesInvalidMultipleChoiceAnswerId = 600,

    //#endregion

}

