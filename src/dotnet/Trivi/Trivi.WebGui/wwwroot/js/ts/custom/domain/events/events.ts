import { GameApiResponse } from "../models/game-models";
import { PlayerApiResponse } from "../models/player-models";
import { QuestionApiResponse } from "../models/question-models";
import { QuestionId } from "../types/aliases";
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