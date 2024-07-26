import { ApiGames } from "../api/api-games";
import { GameStatus } from "../domain/enums/game-status";
import { GameApiResponse, GameApiPostRequest, GameApiPatchRequest } from "../domain/models/game-models";
import { ServiceUtility } from "../utility/service-utility";


export class GameService
{
    public async createGame(data: GameApiPostRequest)
    {
        const api = new ApiGames();

        const response = await api.post(data);

        return await ServiceUtility.toServiceResponse<GameApiResponse>(response);
    }

    public async startGame(gameId: string)
    {
        const api = new ApiGames();

        const response = await api.postStart(gameId);

        return await ServiceUtility.toServiceResponse<GameApiResponse>(response);
    }
}