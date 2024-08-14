import { IController, IControllerAsync } from "../../../../domain/contracts/icontroller";
import { GameQuestionSubmittedData, GameQuestionSubmittedEvent, NavigateToPageEvent } from "../../../../domain/events/events";
import { GameQuestionUrlParms } from "../../../../domain/models/game-models";
import { GameQuestionHub } from "../../../../hubs/game-question/game-question-hub";
import { PageLoadingUtility } from "../../../../utility/page-loading-utility";
import { UrlUtility } from "../../../../utility/url-utility";



export abstract class GameQuestionPageController implements IControllerAsync
{
    protected readonly _gameId: string;
    protected readonly _playerId: string;
    protected readonly _questionId: string;
    protected readonly _gameHub: GameQuestionHub;

    constructor(data: GameQuestionUrlParms)
    {
        PageLoadingUtility.hideLoader();

        this._gameId = data.gameId;
        this._playerId = data.playerId;
        this._questionId = data.questionId;

        this._gameHub = new GameQuestionHub(this._gameId);
    }


    public async control()
    {
        this.addListenersBase();

        await this._gameHub.control();

        await this._gameHub.playerJoinPage({
            gameId: this._gameId,
            playerId: this._playerId,
            //questionId: this._questionId,
        });
    }

    private addListenersBase()
    {
        GameQuestionSubmittedEvent.addListener((message) =>
        {
            this.onGameQuestionSubmittedEvent(message.data!);
        });
            
        NavigateToPageEvent.addListener((message) =>
        {
            window.location.href = UrlUtility.replacePath(message.data!.destination).toString();
        });
    }


    private onGameQuestionSubmittedEvent(message: GameQuestionSubmittedData)
    {
        PageLoadingUtility.showLoader();
    }


    public static getGameQuestionUrlParms(): GameQuestionUrlParms
    {
        const result: GameQuestionUrlParms = {
            gameId: UrlUtility.getCurrentPathValue(1)!,
            playerId: UrlUtility.getQueryParmValue('player')!,
            questionId: UrlUtility.getCurrentPathValue(3)!,
        }

        return result;
    }


}