import { BootstrapUtilityClasses } from "../../../domain/constants/bootstrap-constants";
import { NativeEvents } from "../../../domain/constants/native-events";
import { IController } from "../../../domain/contracts/icontroller";
import { DeleteQuestionButtonClickedEvent, QuestionUpdatedEvent } from "../../../domain/events/events";
import { Selector } from "../../../domain/helpers/element-selector/selector";
import { InputFeedbackText } from "../../../domain/helpers/input-feedback";
import { Ranger } from "../../../domain/helpers/ranger/ranger";
import { SpinnerButton } from "../../../domain/helpers/spinner-button";
import { ErrorMessage, ServiceResponse } from "../../../domain/models/api-response";
import { PutQuestionApiRequest, QuestionApiResponse } from "../../../domain/models/question-models";
import { Guid } from "../../../domain/types/aliases";
import { QuestionsService } from "../../../services/questions-service";
import { AlertUtility } from "../../../utility/alert-utility";
import { ErrorUtility } from "../../../utility/error-utility";


const elements = {
    alertsContainerClass: ".alerts-container",
    formClass: ".form-question",
    promptInputId: "#form-question-mc-input-prompt",
    promptInputClass: '.form-question-input-prompt',
    deleteBtnClass: '.btn-delete',
}


export type QuestionFormConstructor = {
    collectionId: Guid;
    editorContainerSelector: string;
}


export abstract class QuestionForm<TQuestion extends QuestionApiResponse> implements IController
{

    protected abstract _currentQuestion: TQuestion;
    public abstract showQuestion(question: TQuestion): void;
    protected abstract sendPutRequest(): Promise<ServiceResponse<TQuestion> | null>;


    protected readonly _collectionId: string;
    protected readonly _selector: Selector;
    protected readonly _form: HTMLFormElement;
    protected readonly _alertsContainer: HTMLDivElement;
    protected readonly _container: HTMLDivElement;
    protected readonly _prompt: InputFeedbackText;
    protected readonly _btnSubmit: SpinnerButton;
    protected readonly _fieldSet: HTMLFieldSetElement;
    protected readonly _btnDelete: SpinnerButton;
    protected readonly _ranger: Ranger;
    protected readonly _questionService: QuestionsService;


    public get isVisible(): boolean
    {
        return this._container.classList.contains(BootstrapUtilityClasses.DisplayNone);
    }

    public set isVisible(value: boolean)
    {
        if (value)
        {
            this._container.classList.remove(BootstrapUtilityClasses.DisplayNone);
        }
        else
        {
            this._container.classList.add(BootstrapUtilityClasses.DisplayNone);
        }
    }

    protected get _promptValue(): string
    {
        return this._prompt.inputElement.value;
    }

    protected set _promptValue(value: string)
    {
        this._prompt.inputElement.value = value;
    }

    constructor(args: QuestionFormConstructor)
    {
        this._collectionId = args.collectionId;

        this._selector = Selector.fromString(args.editorContainerSelector);

        this._container = this._selector.element as HTMLDivElement;
        this._alertsContainer = this._selector.querySelector<HTMLDivElement>(elements.alertsContainerClass);
        this._form = this._selector.querySelector<HTMLFormElement>(elements.formClass);
        this._prompt = new InputFeedbackText(this._selector.querySelector(elements.promptInputClass));
        this._btnSubmit = new SpinnerButton(this._selector.querySelector<HTMLButtonElement>('.btn-submit'));
        this._fieldSet = this._selector.querySelector<HTMLFieldSetElement>('fieldset');
        this._btnDelete = new SpinnerButton(this._selector.querySelector<HTMLButtonElement>(elements.deleteBtnClass));


        this._ranger = Ranger.initFromContainer(this._container);

        this._questionService = new QuestionsService();

    }

    public control()
    {
        this.addListenersBase();
    }

    protected addListenersBase = () =>
    {
        this._form.addEventListener(NativeEvents.Submit, async (e) =>
        {
            e.preventDefault();
            await this.onFormSubmit();
        });

        this._prompt.inputElement.addEventListener(NativeEvents.KeyUp, (e) =>
        {
            this.onPromptInputKeyUp();
        });

        this._btnDelete.button.addEventListener(NativeEvents.Click, async (e) =>
        {
            const deleted = await this.deleteQuestion();

            if (deleted)
            {
                DeleteQuestionButtonClickedEvent.invoke(this, {
                    questionId: this._currentQuestion.id,
                });
            }
        });
    }


    protected onPromptInputKeyUp()
    {
        this._btnSubmit.button.disabled = this._promptValue?.length === 0 ?? true;
    }


    protected getPromptValueLength()
    {
        return this._promptValue?.length ?? 0;
    }



    protected async onFormSubmit()
    {
        this.disableFormInputs();

        const updatedQuestion = await this.saveQuestion();

        this.enableFormInputs();

        if (!updatedQuestion)
        {
            return;
        }

        this.showSuccessAlert('Question saved successfully.');
        this.sendQuestionUpdatedEvent(updatedQuestion);
    }

    protected async saveQuestion() : Promise<TQuestion | null>
    {
        try
        {
            const response = await this.sendPutRequest();

            if (!response)
            {
                return null;
            }

            if (!response.successful)
            {
                this.showErrorListAlert(response.response.errors);
                return null;
            }

            return response.response.data;

        }
        catch (error)
        {
            this.handleServiceException(error);
            return null;
        }
    }


    protected enableFormInputs()
    {
        this._btnSubmit.reset();
        this._fieldSet.disabled = false;
    }

    protected disableFormInputs()
    {
        this._btnSubmit.spin();
        this._fieldSet.disabled = true;
    }

    protected showSuccessAlert(message: string)
    {
        AlertUtility.showSuccess({
            container: this._alertsContainer,
            message: message,
        });
    }

    protected showErrorAlert(message: string)
    {
        AlertUtility.showDanger({
            container: this._alertsContainer,
            message: message,
        });
    }

    protected showErrorListAlert(errors: ErrorMessage[])
    {
        AlertUtility.showErrors({
            container: this._alertsContainer,
            errors: errors,
        });
    }

    protected handleServiceException(error: Error)
    {
        ErrorUtility.onException(error, {
            onApiForbiddenException: (e) => this.showErrorAlert('You are not authorized to edit this question.'),
            onApiNotFoundException: (e) => this.showErrorAlert('This question was not found. Please try refreshing the page.'),
            onOther: (e) => this.showErrorAlert('Unexpected error. Please try again later.'),
        });

        console.error(error);
    }


    protected getBasicPutRequestData<TRequest extends PutQuestionApiRequest>(): TRequest
    {
        const result: PutQuestionApiRequest = {
            collectionId: this._collectionId,
            prompt: this._promptValue,
            points: this._ranger.value,
        }

        return result as TRequest;
    }


    protected sendQuestionUpdatedEvent(updatedQuestion: TQuestion)
    {
        QuestionUpdatedEvent.invoke(this, {
            question: updatedQuestion,
        });
    }


    protected clearAlertsContainer()
    {
        this._alertsContainer.innerHTML = '';
    }



    protected async deleteQuestion()
    {
        try
        {
            this._btnDelete.spin();

            const response = await this._questionService.deleteQuestion(this._currentQuestion.id);

            this._btnDelete.reset();

            if (!response.successful)
            {
                this.showErrorListAlert(response.response.errors);
                return false;
            }

            return true;
        }
        catch (error)
        {
            this.handleServiceException(error);
            this._btnDelete.reset();
            return false;
        }
    }
}


export const QuestionFormElements = elements;