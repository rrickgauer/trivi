import { ApiGames } from "../api/api-games";
import { GameApiResponse, GameApiPostRequest } from "../domain/models/game-models";
import { ServiceUtility } from "../utility/service-utility";


export class GameService
{
    public async createGame(data: GameApiPostRequest)
    {
        const api = new ApiGames();

        const response = await api.post(data);

        return await ServiceUtility.toServiceResponse<GameApiResponse>(response);

    }
}