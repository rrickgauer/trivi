import { PlayerApiResponse } from "../../domain/models/player-models"



export type AdminLobbyUpdatedData = {
    players: PlayerApiResponse[];
}



export type NavigateToData = {
    destination: string;
}