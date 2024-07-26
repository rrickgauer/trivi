import { HttpMethods } from "../domain/constants/api-constants";
import { ApplicationTypes } from "../domain/constants/application-types";
import { PlayerApiPostRequest } from "../domain/models/player-models";
import { ApiEndpoints } from "./api-base";


export class ApiPlayers
{
    private readonly _url: string;

    constructor()
    {
        this._url = ApiEndpoints.Players;
    }

    public async post(postRequest: PlayerApiPostRequest)
    {
        const url = this._url;

        return await fetch(url, {
            body: JSON.stringify(postRequest),
            headers: ApplicationTypes.GetJsonHeaders(),
            method: HttpMethods.POST,
        });
    }
}