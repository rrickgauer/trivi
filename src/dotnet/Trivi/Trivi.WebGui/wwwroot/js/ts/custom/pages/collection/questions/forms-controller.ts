import { BootstrapUtilityClasses } from "../../../domain/constants/bootstrap-constants";
import { IController } from "../../../domain/contracts/icontroller";
import { QuestionType } from "../../../domain/enums/question-type";
import { DeleteQuestionButtonClickedData, DeleteQuestionButtonClickedEvent } from "../../../domain/events/events";
import { Selector } from "../../../domain/helpers/element-selector/selector";
import { NotImplementedException } from "../../../domain/models/exceptions";
import { MultipleChoiceApiResponse, QuestionApiResponse, ShortAnswerApiResponse, TrueFalseAnswerApiResponse } from "../../../domain/models/question-models";
import { Guid, QuestionId } from "../../../domain/types/aliases";
import { QuestionsService } from "../../../services/questions-service";
import { AlertUtility } from "../../../utility/alert-utility";
import { ErrorUtility } from "../../../utility/error-utility";
import { MessageBoxUtility } from "../../../utility/message-box-utility";
import { NanoIdUtility } from "../../../utility/nanoid-utility";
import { PageUtility } from "../../../utility/page-utility";
import { UrlUtility } from "../../../utility/url-utility";
import { FormMultipleChoice } from "./form-multiple-choice";
import { FormShortAnswer } from "./form-short-answer";
import { FormTrueFalse } from "./form-true-false";


const elements = {
    containerClass: ".question-editors",
    spinnerContainerClass: ".question-editors-spinner",
    formsContainerClass: ".question-editors-forms",
}

export class QuestionFormsController implements IController
{
    private readonly _collectionId: string;
    private readonly _questionsService: QuestionsService;
    private readonly _selector: Selector;
    private readonly _container: HTMLDivElement;
    private readonly _spinner: HTMLDivElement;
    private readonly _formsContainer: HTMLDivElement;
    private readonly _formShortAnswer: FormShortAnswer;
    private readonly _formTrueFalse: FormTrueFalse;
    private readonly _formMultipleChoice: FormMultipleChoice;

    constructor(collectionId: Guid)
    {
        this._collectionId = collectionId;
        this._questionsService = new QuestionsService();

        this._formShortAnswer = new FormShortAnswer(this._collectionId);
        this._formTrueFalse = new FormTrueFalse(this._collectionId);
        this._formMultipleChoice = new FormMultipleChoice(this._collectionId);

        this._selector = Selector.fromString(elements.containerClass);
        this._container = this._selector.element as HTMLDivElement;
        this._spinner = this._selector.querySelector<HTMLDivElement>(elements.spinnerContainerClass);
        this._formsContainer = this._selector.querySelector<HTMLDivElement>(elements.formsContainerClass);
    }

    public control()
    {
        this._formShortAnswer.control();
        this._formTrueFalse.control();
        this._formMultipleChoice.control();

        this.addListeners();
    }


    public async editQuestion(questionId: QuestionId)
    {
        this.showSpinner();

        const question = await this.fetchQuestion(questionId);

        if (question)
        {
            this.showForms(question);
        }
    }

    public hideCurrentQuestion()
    {
        this._formsContainer.classList.add(BootstrapUtilityClasses.DisplayNone);
    }


    private showSpinner()
    {
        this._spinner.classList.remove(BootstrapUtilityClasses.DisplayNone);
        this._formsContainer.classList.add(BootstrapUtilityClasses.DisplayNone);
    }

    private showForms(question: QuestionApiResponse)
    {
        // hide all the forms
        this._formMultipleChoice.isVisible = false;
        this._formShortAnswer.isVisible = false;
        this._formTrueFalse.isVisible = false;

        // show the appropriate one
        switch (question.questionType)
        {
            case QuestionType.MultipleChoice:
                this._formMultipleChoice.showQuestion(question as MultipleChoiceApiResponse);
                this._formMultipleChoice.isVisible = true;
                break;

            case QuestionType.ShortAnswer:
                this._formShortAnswer.showQuestion(question as ShortAnswerApiResponse);
                this._formShortAnswer.isVisible = true;
                break;

            case QuestionType.TrueFalse:
                this._formTrueFalse.showQuestion(question as TrueFalseAnswerApiResponse);
                this._formTrueFalse.isVisible = true;
                break;

            default:
                throw new NotImplementedException();
        }

        // hide the spinner
        this._spinner.classList.add(BootstrapUtilityClasses.DisplayNone);

        // show the forms container
        this._formsContainer.classList.remove(BootstrapUtilityClasses.DisplayNone);
    }

    private async fetchQuestion(questionId: QuestionId): Promise<QuestionApiResponse | null>
    {
        try
        {
            const response = await this._questionsService.getQuestion(questionId);

            if (!response.successful)
            {
                MessageBoxUtility.showErrorList(response.response.errors);
                return null;
            }

            return response.response.data;
        }
        catch (error)
        {

            ErrorUtility.onException(error, {
                onApiNotFoundException: (e) => MessageBoxUtility.showError({ message: 'Question not found.' }),
                onApiForbiddenException: (e) => MessageBoxUtility.showError({ message: "You don't have permission to view this question." }),
                onOther: (e) => MessageBoxUtility.showError({ message: 'Unexpected error. Try refreshing the page.' }),
            });

            return null;
        }

    }


    private addListeners = () =>
    {
        
    }


}