import { ApiPlayers } from "../api/api-players";
import { PlayerApiPostRequest, PlayerApiResponse } from "../domain/models/player-models";
import { ServiceUtility } from "../utility/service-utility";



export class PlayerService
{
    public async joinGame(playerData: PlayerApiPostRequest)
    {
        const api = new ApiPlayers();

        const response = await api.post(playerData);

        return await ServiceUtility.toServiceResponse<PlayerApiResponse>(response);
    }
}