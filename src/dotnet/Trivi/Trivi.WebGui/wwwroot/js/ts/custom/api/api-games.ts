import { HttpMethods } from "../domain/constants/api-constants";
import { ApplicationTypes } from "../domain/constants/application-types";
import { GameApiPostRequest } from "../domain/models/game-models";
import { ApiEndpoints } from "./api-base";



export class ApiGames
{
    private readonly _url: string;

    constructor()
    {
        this._url = ApiEndpoints.Games;
    }

    public async post(requestBody: GameApiPostRequest)
    {
        const url = this._url;

        return await fetch(url, {
            body: JSON.stringify(requestBody),
            headers: ApplicationTypes.GetJsonHeaders(),
            method: HttpMethods.POST,
        });
    }
}