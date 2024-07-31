import { QuestionType } from "../enums/question-type";
import { DateTimeString, Guid, QuestionId } from "../types/aliases"



export type ResponseApiResponse = {
    answer: string;
    questionId: QuestionId;
    playerId: Guid;
    createdOn: DateTimeString;
    questionType: QuestionType;
    questionPrompt: string;
    questionPoints: number;
    playerNickname: string;
    gameUrl: string;
    gameId: string;
}



export type ResponseApiPostRequestBase = {
    playerId: Guid;
}

export type ResponseShortAnswerApiPostRequest = ResponseApiPostRequestBase & {
    answer: string;
}

export type ResponseTrueFalseApiPostRequest = ResponseApiPostRequestBase & {
    answer: boolean;
}

export type ResponseApiPostRequestTypes = ResponseShortAnswerApiPostRequest | ResponseTrueFalseApiPostRequest;




