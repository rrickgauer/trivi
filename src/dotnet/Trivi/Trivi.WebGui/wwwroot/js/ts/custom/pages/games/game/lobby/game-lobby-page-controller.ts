import { IController, IControllerAsync } from "../../../../domain/contracts/icontroller";
import { NavigateToEvent } from "../../../../domain/events/events";
import { GamePlayUrlParms } from "../../../../domain/models/game-models";
import { PlayerGameLobbyHub } from "../../../../hubs/game-lobby/game-lobby-hub";
import { UrlUtility } from "../../../../utility/url-utility";



export class GameLobbyPageController implements IControllerAsync
{
    private _urlArgs: GamePlayUrlParms;
    private _gameHub: PlayerGameLobbyHub;

    constructor(args: GamePlayUrlParms)
    {
        this._urlArgs = args;
        this._gameHub = new PlayerGameLobbyHub(this._urlArgs);
    }


    public async control()
    {
        this.addListeners();
        await this._gameHub.control();
        
    }

    private addListeners = () =>
    {
        NavigateToEvent.addListener((message) =>
        {
            const url = UrlUtility.replacePath(message.data?.data?.destination!);
            url.searchParams.set('player', this._urlArgs.playerId);

            window.location.href = url.toString();
        });
    }

}