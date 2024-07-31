import { IController, IControllerAsync } from "../../../../domain/contracts/icontroller";
import { NavigateToEvent } from "../../../../domain/events/events";
import { GamePlayUrlParms } from "../../../../domain/models/game-models";
import { PlayerGameHub } from "../../../../hubs/game/game-hub";



export class GameLobbyPageController implements IControllerAsync
{
    private _urlArgs: GamePlayUrlParms;
    private _gameHub: PlayerGameHub;

    constructor(args: GamePlayUrlParms)
    {
        this._urlArgs = args;
        this._gameHub = new PlayerGameHub(this._urlArgs);
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
            window.location.href = message.data?.data?.destination!;
        });
    }

}