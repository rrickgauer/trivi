import { IControllerAsync } from "../../domain/contracts/icontroller";
import { ApiResponse, ServiceResponse } from "../../domain/models/api-response";
import { GamePlayUrlParms } from "../../domain/models/game-models";
import { PlayerApiResponse } from "../../domain/models/player-models";
import { Guid } from "../../domain/types/aliases";

import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { AdminLobbyUpdatedData, NavigateToData } from "./models";
import { AdminLobbyUpdatedEvent, NavigateToEvent } from "../../domain/events/events";


export enum HubEndpoints 
{
    ClientJoinSessionRequestAsync = "PlayerJoinGameLobbyAsync",
    AdminJoinGameLobbyAsync = "AdminJoinGameLobbyAsync",
}

export enum HubEvents
{
    AdminLobbyUpdated = "AdminLobbyUpdated",
    NavigateTo = "NavigateTo",
}


export abstract class GameHubBase implements IControllerAsync
{
    protected static readonly HUB_URL = '/hubs/game';

    protected _gameId: string;
    protected _connection: HubConnection;


    constructor(gameId: string)
    {
        this._gameId = gameId;

        this._connection = new HubConnectionBuilder()
            .withUrl(GameHubBase.HUB_URL)
            //.configureLogging(LogLevel.Debug)
            //.withStatefulReconnect()
            .build();
    }

    public async control()
    {
        this.initListeners();

        await this._connection.start();
        await this.sendJoinRequest();
    }


    protected abstract sendJoinRequest(): Promise<void>;

    protected initListeners()
    {
        this._connection.on(HubEvents.NavigateTo, (data: ApiResponse<NavigateToData>) =>
        {
            NavigateToEvent.invoke(this, data);
        });
    }

}




export class PlayerGameHub extends GameHubBase
{
    private readonly _playerId: string;

    constructor(parms: GamePlayUrlParms)
    {
        super(parms.gameId);
        this._playerId = parms.playerId;
    }

    protected async sendJoinRequest(): Promise<void> 
    {
        await this._connection.invoke(HubEndpoints.ClientJoinSessionRequestAsync, {
            gameId: this._gameId,
            playerId: this._playerId,
        });
    }

    protected initListeners()
    {
        super.initListeners();
    }
}


export class AdminGameHub extends GameHubBase
{
    constructor(parms: {gameId: string})
    {
        super(parms.gameId);
    }

    protected async sendJoinRequest(): Promise<void> 
    {
        await this._connection.invoke(HubEndpoints.AdminJoinGameLobbyAsync, {
            gameId: this._gameId,
        });
    }

    protected initListeners = () =>
    {
        super.initListeners();

        this._connection.on(HubEvents.AdminLobbyUpdated, (e: ApiResponse<AdminLobbyUpdatedData>) =>
        {
            AdminLobbyUpdatedEvent.invoke(this, e.data!);
        });
    }
}