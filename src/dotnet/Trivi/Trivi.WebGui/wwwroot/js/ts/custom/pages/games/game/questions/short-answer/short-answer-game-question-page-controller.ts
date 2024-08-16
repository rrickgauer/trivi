import { GameQuestionUrlParms } from "../../../../../domain/models/game-models";
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

    public async control()
    {
        await super.control();

        this.addListeners();
        this._form.control();
    }

    protected addListeners()
    {

    }
}