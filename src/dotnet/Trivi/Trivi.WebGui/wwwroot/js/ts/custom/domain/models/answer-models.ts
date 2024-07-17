import { DateTimeString, QuestionId } from "../types/aliases";



export type AnswerApiResponse = {
    id: string;
    questionId: QuestionId;
    answer: string | null;
    isCorrect: boolean;
    createdOn: DateTimeString;
}


export type SaveAnswerApiRequestBody = {
    answer: string;
    isCorrect: boolean;
}
