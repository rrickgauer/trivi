import { NativeEvents } from "../../../../../domain/constants/native-events";
import { IController } from "../../../../../domain/contracts/icontroller";
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

    radioInputClass: '.form-check-input',
}


export class MultipleChoiceResponseForm implements IController
{
    private _urlParms: GameQuestionUrlParms;
    private _selector: Selector;
    private _form: HTMLFormElement;
    private _alertsContainer: HTMLDivElement;
    private _btnSubmit: SpinnerButton;
    private _fieldset: HTMLFieldSetElement;
    private _responsesService: QuestionResponseService;
    private _radioGroup: RadioGroup<string>;


    private get _selectedRadioValue(): string | null
    {
        return this._radioGroup.selectedValue;
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
        this._radioGroup = new RadioGroup<string>(this._selector.querySelector('.radio-group'));
    }


    public control()
    {
        this.addListeners();
    }

    private addListeners()
    {
        this._form.addEventListener(NativeEvents.Submit, async (e) =>
        {
            e.preventDefault();
            await this.onFormSubmit();
        });


        this._selector.element.querySelectorAll<HTMLInputElement>(elements.radioInputClass).forEach((inputElement) =>
        {
            inputElement.addEventListener(NativeEvents.Click, (e) =>
            {
                this._btnSubmit.button.disabled = false;
            });
        });
    }


    private async onFormSubmit()
    {
        if (!this._selectedRadioValue)
        {
            this.showErrorAlert('Please select an answer.');
            return;
        }

        try
        {
            this.showWaiting(true);

            const result = await this._responsesService.createMultipleChoiceResponse(this._urlParms.questionId, {
                answer: this._selectedRadioValue,
                playerId: this._urlParms.playerId,
            });

            this.showWaiting(false);


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
            this.showWaiting(false);
            this.onCreateServiceException(error);
        }
    }

    private showWaiting(isDisabled: boolean)
    {
        if (isDisabled)
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


    private onCreateServiceException(error: Error)
    {
        ErrorUtility.onException(error, {
            onApiForbiddenException: (e) => this.showErrorAlert('You are not allowed to answer this question.'),
            onApiNotFoundException: (e) => this.showErrorAlert('Question not found.'),
            onOther: (e) =>
            {
                this.showErrorAlert('Unexpected error. Please try refreshing the page.');
                console.error(error);
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