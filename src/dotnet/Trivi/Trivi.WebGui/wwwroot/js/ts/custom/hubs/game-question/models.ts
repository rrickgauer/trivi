import { PlayerQuestionResponseApiResponse } from "../../domain/models/player-models";
import { Guid, QuestionId } from "../../domain/types/aliases";



export type AdminJoinParms = {
    gameId: string;
    questionId: string;
}


export type AdminUpdatePlayerQuestionResponsesParms = {
    responses: PlayerQuestionResponseApiResponse[];
}



export type PlayerConnectParms = {
    gameId: string;
    questionId: QuestionId;
    playerId: Guid;
}


export type NavigateToPageParms = {
    destination: string;
}