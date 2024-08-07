import { IController } from "../../../../domain/contracts/icontroller";
import { GameQuestionSubmittedData, GameQuestionSubmittedEvent } from "../../../../domain/events/events";
import { GameQuestionUrlParms } from "../../../../domain/models/game-models";
import { UrlUtility } from "../../../../utility/url-utility";



export abstract class GameQuestionPageController implements IController
{
    protected readonly _gameId: string;
    protected readonly _playerId: string;
    protected readonly _questionId: string;

    constructor(data: GameQuestionUrlParms)
    {
        this._gameId = data.gameId;
        this._playerId = data.playerId;
        this._questionId = data.questionId;
    }


    public control()
    {
        this.addListenersBase();
    }

    private addListenersBase()
    {
        GameQuestionSubmittedEvent.addListener((message) =>
        {
            this.onGameQuestionSubmittedEvent(message.data!);
        });
    }


    private onGameQuestionSubmittedEvent(message: GameQuestionSubmittedData)
    {
        const newPath = `/games/${message.response.gameId}`;
        window.location.href = UrlUtility.replacePath(newPath).toString();

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