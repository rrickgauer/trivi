import { HubConnection, HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";
import { IControllerAsync } from "../../domain/contracts/icontroller";
import { QuestionId } from "../../domain/types/aliases";
import { AdminJoinParms, AdminSendAllPlayersMessageParms, AdminSendPlayerMessageParms, AdminUpdatePlayerQuestionResponsesParms, DisplayToastParms, NavigateToPageParms, PlayerConnectParms } from "./models";
import { AdminUpdatePlayerQuestionResponsesEvent, DisplayToastEvent, NavigateToPageEvent } from "../../domain/events/events";




export enum GameQuestionHubEndpoints
{
    AdminConnectAsync = "AdminConnectAsync",
    PlayerConnectAsync = "PlayerConnectAsync",
    AdminSendPlayerMessageAsync = "AdminSendPlayerMessageAsync",
    AdminSendAllPlayersMessageAsync = "AdminSendAllPlayersMessageAsync",
}


export enum GameQuestionHubEvents
{
    AdminUpdatePlayerQuestionResponses = "AdminUpdatePlayerQuestionResponses",
    NavigateToPage = "NavigateToPage",
    DisplayToast = "DisplayToast",
}


export class GameQuestionHub implements IControllerAsync
{
    protected static readonly HUB_URL = '/hubs/game-question';
    private _gameId: string;
    private _connection: HubConnection;

    private _currentQuestionId: string | null = null;

    private _connectionStarted = false;

    constructor(gameId: string)
    {
        this._gameId = gameId;

        this._connection = new HubConnectionBuilder()
            .withUrl(GameQuestionHub.HUB_URL)
            .build();

        this.addListeners();
    }

    public async control()
    {
        await this._connection.start();

        this._connectionStarted = true;
    }

    public async adminJoinQuestionPage(questionId: QuestionId)
    {
        await this.ensureConnection();

        const data: AdminJoinParms = {
            gameId: this._gameId,
            questionId: questionId,
        }

        await this._connection.invoke(GameQuestionHubEndpoints.AdminConnectAsync, data);
    }

    public async playerJoinPage(data: PlayerConnectParms)
    {
        await this.ensureConnection();

        await this._connection.invoke(GameQuestionHubEndpoints.PlayerConnectAsync, data);
    }

    public async adminSendPlayerMessage(data: AdminSendPlayerMessageParms)
    {
        await this.ensureConnection();
        await this._connection.invoke(GameQuestionHubEndpoints.AdminSendPlayerMessageAsync, data);
    }

    public async adminSendAllPlayersMessage(data: AdminSendAllPlayersMessageParms)
    {
        await this.ensureConnection();
        await this._connection.invoke(GameQuestionHubEndpoints.AdminSendAllPlayersMessageAsync, data);
    }


    private addListeners()
    {
        this._connection.on(GameQuestionHubEvents.AdminUpdatePlayerQuestionResponses, (data: AdminUpdatePlayerQuestionResponsesParms) =>
        {
            AdminUpdatePlayerQuestionResponsesEvent.invoke(this, data);
        });

        this._connection.on(GameQuestionHubEvents.NavigateToPage, (data: NavigateToPageParms) =>
        {
            NavigateToPageEvent.invoke(this, data);
        });

        this._connection.on(GameQuestionHubEvents.DisplayToast, (data: DisplayToastParms) =>
        {
            DisplayToastEvent.invoke(this, data);
        });
    }

    private async ensureConnection()
    {
        if (this._connection.state != HubConnectionState.Connected)
        {
            await this.control();
        }
    }

}