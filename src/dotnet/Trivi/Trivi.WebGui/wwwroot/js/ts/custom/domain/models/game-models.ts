import { GameStatus } from "../enums/game-status";
import { DateTimeString, Guid, QuestionId } from "../types/aliases";




export type GameApiResponse = {
    id: string;
    collectionId: Guid;
    status: GameStatus;
    randomizeQuestions: boolean;
    questionTimeLimit: number | null;
    createdOn: DateTimeString;
    startedOn: DateTimeString | null;
    userId: Guid;
}



export type GameApiPostRequest = {
    collectionId: Guid;
    randomizeQuestions: boolean;
    questionTimeLimit: number | null;
}

export type GameApiPatchRequest = {
    status: GameStatus;
}

export type GamePlayUrlParms = {
    gameId: string;
    playerId: Guid;
}

export type GameQuestionUrlParms = GamePlayUrlParms & {
    questionId: QuestionId;
}





