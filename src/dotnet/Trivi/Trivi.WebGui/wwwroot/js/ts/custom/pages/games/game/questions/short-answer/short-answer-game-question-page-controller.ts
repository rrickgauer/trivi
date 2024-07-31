import { GameQuestionSubmittedData, GameQuestionSubmittedEvent } from "../../../../../domain/events/events";
import { GameQuestionUrlParms } from "../../../../../domain/models/game-models";
import { UrlUtility } from "../../../../../utility/url-utility";
import { GameQuestionPageController } from "../game-question-page-controller";
import { ShortAnswerResponseForm } from "./short-answer-response-form";



export class ShortAnswerGameQuestionPageController extends GameQuestionPageController
{
    protected readonly _form: ShortAnswerResponseForm;

    constructor(data: GameQuestionUrlParms)
    {
        super(data);

        this._form = new ShortAnswerResponseForm(data);
    }

    public control()
    {
        this.addListeners();
        this._form.control();
    }

    private addListeners()
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
}