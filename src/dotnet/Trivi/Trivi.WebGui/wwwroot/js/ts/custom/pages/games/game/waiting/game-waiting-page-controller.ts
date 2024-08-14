import { IControllerAsync } from "../../../../domain/contracts/icontroller";
import { NavigateToPageEvent } from "../../../../domain/events/events";
import { GamePlayUrlParms } from "../../../../domain/models/game-models";
import { GameQuestionHub } from "../../../../hubs/game-question/game-question-hub";
import { UrlUtility } from "../../../../utility/url-utility";



export class GameWaitingPageController implements IControllerAsync
{
    private _urlArgs: GamePlayUrlParms;
    private _gameHub: GameQuestionHub;

    constructor(args: GamePlayUrlParms)
    {
        this._urlArgs = args;
        this._gameHub = new GameQuestionHub(this._urlArgs.gameId);
    }

    public async control()
    {
        this.addListeners();

        await this._gameHub.control();

        await this._gameHub.playerJoinPage({
            gameId: this._urlArgs.gameId,
            playerId: this._urlArgs.playerId,
        });
    }

    private addListeners()
    {
        NavigateToPageEvent.addListener((message) =>
        {
            if (message.data?.destination)
            {
                window.location.href = UrlUtility.replacePath(message.data.destination).toString();
            }
        });
    }
}