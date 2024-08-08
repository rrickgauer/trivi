import { GameQuestionUrlParms } from "../../../../../domain/models/game-models";
import { GameQuestionPageController } from "../game-question-page-controller";
import { TrueFalseResponseForm } from "./true-false-response-form";



export class TrueFalseGameQuestionPageController extends GameQuestionPageController
{
    private _trueFalseResponseForm: TrueFalseResponseForm;

    constructor(urlParms: GameQuestionUrlParms)
    {
        super(urlParms);

        this._trueFalseResponseForm = new TrueFalseResponseForm(urlParms);
    }

    public async control()
    {
        await super.control();
        this.addListeners();

        this._trueFalseResponseForm.control();
    }

    private addListeners()
    {

    }

}