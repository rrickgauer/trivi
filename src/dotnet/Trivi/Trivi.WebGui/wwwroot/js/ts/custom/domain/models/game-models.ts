import { DateTimeString, Guid } from "../types/aliases";




export type GameApiResponse = {
    id: string;
    collectionId: Guid;
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





