import { IController } from "../../../../domain/contracts/icontroller";
import { GameQuestionUrlParms } from "../../../../domain/models/game-models";
import { UrlUtility } from "../../../../utility/url-utility";



export abstract class GameQuestionPageController implements IController
{
    public abstract control(): void;

    protected readonly _gameId: string;
    protected readonly _playerId: string;
    protected readonly _questionId: string;

    constructor(data: GameQuestionUrlParms)
    {
        this._gameId = data.gameId;
        this._playerId = data.playerId;
        this._questionId = data.questionId;
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