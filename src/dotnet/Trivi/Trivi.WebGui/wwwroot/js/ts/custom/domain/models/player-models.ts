import { DateTimeString, Guid } from "../types/aliases";


export type PlayerApiResponse = {
	id: Guid;
	gameId: string;
	nickname: string;
	createdOn: DateTimeString;
	uriApi: string;
}


export type PlayerApiPostRequest = {
	gameId: string;
	nickname: string;
}


export type PlayerQuestionResponseApiResponse = PlayerApiResponse & {
	hasResponse: boolean;
}