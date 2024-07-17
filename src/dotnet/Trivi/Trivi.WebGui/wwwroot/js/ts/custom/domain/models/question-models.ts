import { QuestionType } from "../enums/question-type";
import { DateTimeString, Guid, QuestionId } from "../types/aliases";



export type QuestionApiResponse = {
    questionId: QuestionId;
    questionCollectionId: Guid;
    questionPrompt: string;
    questionCreatedOn: DateTimeString;
    questionType: QuestionType;
}


export type ShortAnswerApiResponse = QuestionApiResponse & {
    questionCorrectAnswer: string;
}

export type TrueFalseAnswerApiResponse = QuestionApiResponse & {
    questionCorrectAnswer: string;
}

export type MultipleChoiceApiResponse = QuestionApiResponse & {
    
}



export type GetQuestionsApiResponse = {
    questions: QuestionApiResponse[];
}



export type PutQuestionApiRequest = {
    collectionId: Guid;
    prompt: string;
}

export type PutMultipleChoiceApiRequest = PutQuestionApiRequest;

export type PutShortAnswerApiRequest = PutQuestionApiRequest & {
    correctAnswer: string;
}

export type PutTrueFalseApiRequest = PutQuestionApiRequest & {
    correctAnswer: boolean;
}

