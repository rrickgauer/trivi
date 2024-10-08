import { GameQuestionStatus } from "../enums/game-question-status";
import { GameStatus } from "../enums/game-status";
import { QuestionType } from "../enums/question-type";
import { DateTimeString, Guid, QuestionId } from "../types/aliases";
import { AnswerApiResponse } from "./answer-models";



export type QuestionApiResponse = {
    id: QuestionId;
    collectionId: Guid;
    prompt: string;
    points: number;
    createdOn: DateTimeString;
    questionType: QuestionType;
}


export type ShortAnswerApiResponse = QuestionApiResponse & {
    correctAnswer: string;
}

export type TrueFalseAnswerApiResponse = QuestionApiResponse & {
    correctAnswer: boolean;
}

export type MultipleChoiceApiResponse = QuestionApiResponse & {
    answers: AnswerApiResponse[];
}



export type GetQuestionsApiResponse = {
    questions: QuestionApiResponse[];
}



export type PutQuestionApiRequest = {
    collectionId: Guid;
    prompt: string;
    points: number;
}

export type PutMultipleChoiceApiRequest = PutQuestionApiRequest;

export type PutShortAnswerApiRequest = PutQuestionApiRequest & {
    correctAnswer: string;
}

export type PutTrueFalseApiRequest = PutQuestionApiRequest & {
    correctAnswer: boolean;
}








export type GameQuestionApiResponse = QuestionApiResponse & {
    gameId: string;
    questionStatus: GameQuestionStatus;
    gameStatus: GameStatus;
}
