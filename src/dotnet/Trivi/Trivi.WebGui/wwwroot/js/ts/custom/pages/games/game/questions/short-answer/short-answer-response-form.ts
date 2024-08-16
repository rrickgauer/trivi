import { NativeEvents } from "../../../../../domain/constants/native-events";
import { IController } from "../../../../../domain/contracts/icontroller";
import { GameQuestionSubmittedEvent } from "../../../../../domain/events/events";
import { Selector } from "../../../../../domain/helpers/element-selector/selector";
import { InputFeedbackText } from "../../../../../domain/helpers/input-feedback";
import { SpinnerButton } from "../../../../../domain/helpers/spinner-button";
import { GameQuestionUrlParms } from "../../../../../domain/models/game-models";
import { QuestionResponseService } from "../../../../../services/question-response-service";
import { AlertUtility } from "../../../../../utility/alert-utility";
import { ErrorUtility } from "../../../../../utility/error-utility";


const elements = {
    answerInputId: "#form-game-answer-input-sa",
    containerClass: ".form-game-answer-container",
    formClass: ".form-game-answer",
    alertsContainerClass: ".alerts-container",
}

export class ShortAnswerResponseForm implements IController
{
    private readonly _urlParms: GameQuestionUrlParms;
    private readonly _selector: Selector;
    private readonly _form: HTMLFormElement;
    private readonly _alertsContainer: HTMLDivElement;
    private readonly _answerInput: InputFeedbackText;
    private readonly _btnSubmit: SpinnerButton;
    private readonly _fieldset: HTMLFieldSetElement;
    private _responsesService: QuestionResponseService;


    private get _answerValue(): string
    {
        return this._answerInput.inputElement.value;
    }


    constructor(urlParms: GameQuestionUrlParms)
    {
        this._urlParms = urlParms;

        this._selector = Selector.fromString(elements.containerClass);

        this._form = this._selector.querySelector<HTMLFormElement>(elements.formClass);
        this._alertsContainer = this._selector.querySelector<HTMLDivElement>(elements.alertsContainerClass);
        this._answerInput = new InputFeedbackText(this._selector.querySelector<HTMLInputElement>(elements.answerInputId));
        this._btnSubmit = SpinnerButton.inParent(this._selector.element, '.btn-submit');
        this._fieldset = this._selector.querySelector<HTMLFieldSetElement>('fieldset');

        this._responsesService = new QuestionResponseService();
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


        this._answerInput.inputElement.addEventListener(NativeEvents.KeyUp, (e) =>
        {
            this.handleAnswerInputChange(); 
        });

        this._answerInput.inputElement.addEventListener(NativeEvents.Change, (e) =>
        {
            this.handleAnswerInputChange();
        });

    }


    private handleAnswerInputChange()
    {
        this._btnSubmit.button.disabled = this._answerValue.length < 1;
    }


    private async onFormSubmit()
    {
        try
        {
            this.showWaiting(true);


            const result = await this._responsesService.createShortAnswerResponse(this._urlParms.questionId, {
                answer: this._answerValue,
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
            this.onCreateShortAnswerResponseException(error);
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


    private onCreateShortAnswerResponseException(error: Error)
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

