import { NativeEvents } from "../../../../../domain/constants/native-events";
import { IController } from "../../../../../domain/contracts/icontroller";
import { TrueFalseText } from "../../../../../domain/enums/true-false";
import { GameQuestionSubmittedEvent } from "../../../../../domain/events/events";
import { Selector } from "../../../../../domain/helpers/element-selector/selector";
import { RadioGroup } from "../../../../../domain/helpers/radio-group/radio-group";
import { SpinnerButton } from "../../../../../domain/helpers/spinner-button";
import { GameQuestionUrlParms } from "../../../../../domain/models/game-models";
import { QuestionResponseService } from "../../../../../services/question-response-service";
import { AlertUtility } from "../../../../../utility/alert-utility";
import { ErrorUtility } from "../../../../../utility/error-utility";

const elements = {
    containerClass: ".form-game-answer-container",
    formClass: ".form-game-answer",
    alertsContainerClass: ".alerts-container",

    answerRadioInputName: "form-question-tf-input-answer",
    answerRadioInputTrueId: "#form-question-tf-input-answer-true",
    answerRadioInputFalseId: "#form-question-tf-input-answer-false",
}

export class TrueFalseResponseForm implements IController
{
    private _urlParms: GameQuestionUrlParms;
    private _selector: Selector;
    private _form: HTMLFormElement;
    private _alertsContainer: HTMLDivElement;
    
    private _fieldset: HTMLFieldSetElement;
    private _btnSubmit: SpinnerButton;
    private _responsesService: QuestionResponseService;
    private _radioGroup: RadioGroup<TrueFalseText>;


    private get _radioValue(): boolean
    {
        return this._radioGroup.selectedValue === TrueFalseText.True;
    }


    constructor(urlParms: GameQuestionUrlParms)
    {
        this._urlParms = urlParms;

        this._selector = Selector.fromString(elements.containerClass);

        this._form = this._selector.querySelector<HTMLFormElement>(elements.formClass);
        this._alertsContainer = this._selector.querySelector<HTMLDivElement>(elements.alertsContainerClass);
        this._btnSubmit = SpinnerButton.inParent(this._selector.element, '.btn-submit');
        this._fieldset = this._selector.querySelector<HTMLFieldSetElement>('fieldset');
        this._responsesService = new QuestionResponseService();

        this._radioGroup = new RadioGroup<TrueFalseText>(this._selector.querySelector(elements.answerRadioInputFalseId));
    }

    public control()
    {
        this.addListeners();
    }

    private addListeners()
    {
        // enable submit button after a radio has been selected
        this._selector.element.querySelectorAll<HTMLInputElement>(`input[name="${elements.answerRadioInputName}"]`).forEach((radioElement) =>
        {
            radioElement.addEventListener(NativeEvents.Click, (e) =>
            {
                this._btnSubmit.button.disabled = false; 
            });
        });


        this._form.addEventListener(NativeEvents.Submit, async (e) =>
        {
            e.preventDefault();
            await this.onFormSubmit();
        });

    }

    private async onFormSubmit()
    {
        try
        {
            this.setFormToLoading(true);


            const result = await this._responsesService.createTrueFalseResponse(this._urlParms.questionId, {
                answer: this._radioValue,
                playerId: this._urlParms.playerId,
            });

            this.setFormToLoading(false);

            if (!result.successful)
            {
                AlertUtility.showErrors({
                    container: this._alertsContainer,
                    errors: result.response.errors,
                });

                return;
            }

            GameQuestionSubmittedEvent.invoke(this, {
                response: result.response.data!,
            });

        }
        catch (error)
        {
            this.setFormToLoading(false);
            this.onCreateTrueFalseResponseException(error);
        }
    }


    private setFormToLoading(isLoading: boolean)
    {
        if (isLoading)
        {
            this._btnSubmit.spin();
            this._fieldset.disabled = true;
        }
        else
        {
            this._btnSubmit.reset();
            this._fieldset.disabled = false;
        }
    }

    private onCreateTrueFalseResponseException(error: Error)
    {
        ErrorUtility.onException(error, {
            onApiForbiddenException: (e) => this.showErrorAlert('You do not have permission to respond to this question.'),
            onApiNotFoundException: (e) => this.showErrorAlert('We could not find the current question.'),
            onOther: (e) =>
            {
                console.error(e);
                this.showErrorAlert('Unexepected error. Please try refreshing the page.');
            },
        });
    }


    private showErrorAlert(message: string)
    {
        AlertUtility.showDanger({
            container: this._alertsContainer,
            message: message,
        });
    }



}