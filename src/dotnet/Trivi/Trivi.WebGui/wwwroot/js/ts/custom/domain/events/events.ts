import { AdminLobbyUpdatedData, NavigateToData } from "../../hubs/game-lobby/models";
import { AdminUpdatePlayerQuestionResponsesParms, DisplayToastParms, NavigateToPageParms } from "../../hubs/game-question/models";
import { ApiResponse } from "../models/api-response";
import { GameApiResponse } from "../models/game-models";
import { PlayerApiResponse } from "../models/player-models";
import { QuestionApiResponse } from "../models/question-models";
import { ResponseBaseApiResponse } from "../models/question-response-models";
import { Guid, QuestionId } from "../types/aliases";
import { CustomEmptyMessage, CustomMessage } from "./custom-events";


export const SuccessfulLoginEvent = new CustomEmptyMessage();
export const SuccessfulSignupEvent = new CustomEmptyMessage();


export const OpenNewCollectionModal = new CustomEmptyMessage();





export type EditCollectionFormSubmittedData = {
    name: string;
}

export const EditCollectionFormSubmittedEvent = new CustomMessage<EditCollectionFormSubmittedData>();




export type OpenQuestionData = {
    questionId: QuestionId;
}

export const OpenQuestionEvent = new CustomMessage<OpenQuestionData>();


export type QuestionUpdatedData = {
    question: QuestionApiResponse;
}

export const QuestionUpdatedEvent = new CustomMessage<QuestionUpdatedData>();




export type DeleteQuestionButtonClickedData = {
    questionId: QuestionId;
}

export const DeleteQuestionButtonClickedEvent = new CustomMessage<DeleteQuestionButtonClickedData>();




export type GameCreatedData = {
    game: GameApiResponse;
}

export const GameCreatedEvent = new CustomMessage<GameCreatedData>();


export type PlayerJoinedGameData = {
    player: PlayerApiResponse;
}

export const PlayerJoinedGameEvent = new CustomMessage<PlayerJoinedGameData>();




export const AdminLobbyUpdatedEvent = new CustomMessage<AdminLobbyUpdatedData>();
export const NavigateToEvent = new CustomMessage<ApiResponse<NavigateToData>>();




export type GameQuestionSubmittedData = {
    response: ResponseBaseApiResponse;
}

export const GameQuestionSubmittedEvent = new CustomMessage<GameQuestionSubmittedData>();



export const AdminUpdatePlayerQuestionResponsesEvent = new CustomMessage<AdminUpdatePlayerQuestionResponsesParms>();

export const NavigateToPageEvent = new CustomMessage<NavigateToPageParms>();

export const DisplayToastEvent = new CustomMessage<DisplayToastParms>();



export type OpenModalPlayerQuestionSettingsData = {
    playerId: Guid;
}

export const OpenModalPlayerQuestionSettingsEvent = new CustomMessage<OpenModalPlayerQuestionSettingsData>();



export type SendPlayerMessageData = {
    playerId: Guid;
    message: string;
}

export const SendPlayerToastEvent = new CustomMessage<SendPlayerMessageData>();



export const OpenSendAllPlayersMessageModalEvent = new CustomEmptyMessage();

export type SendAllPlayersMessageData = {
    message: string;
}

export const SendAllPlayersMessageEvent = new CustomMessage<SendAllPlayersMessageData>();
