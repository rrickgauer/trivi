import { GameQuestionUrlParms } from "../../../../../domain/models/game-models";
import { GameQuestionPageController } from "../game-question-page-controller";
import { MultipleChoiceResponseForm } from "./multiple-choice-response-form";



export class MultipleChoiceGameQuestionPageController extends GameQuestionPageController
{
    private _form: MultipleChoiceResponseForm;

    constructor(urlParms: GameQuestionUrlParms)
    {
        super(urlParms);

        this._form = new MultipleChoiceResponseForm(urlParms);
    }

    public async control()
    {
        await super.control();

        this._form.control();
    }
}