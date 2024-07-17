import { NativeEvents } from "../../../domain/constants/native-events";
import { AnswerApiResponse } from "../../../domain/models/answer-models";
import { ServiceResponse } from "../../../domain/models/api-response";
import { MultipleChoiceApiResponse, PutMultipleChoiceApiRequest, QuestionApiResponse } from "../../../domain/models/question-models";
import { Guid } from "../../../domain/types/aliases";
import { AnswersService } from "../../../services/answers-service";
import { AnswerTemplate } from "../../../templates/answer-template";
import { ErrorUtility } from "../../../utility/error-utility";
import { NanoIdUtility } from "../../../utility/nanoid-utility";
import { AnswerListItem, AnswerListItemElements } from "./answer-list-item";
import { QuestionForm } from "./form-question";

const elements = {
    editorContainerClass: ".question-editor-mc",
    answersContainerClass: ".form-question-mc-answers",
    answersListClass: ".answers-list",
    newAnswerBtnClass: ".btn-new-answer",
}

export class FormMultipleChoice extends QuestionForm<MultipleChoiceApiResponse>
{
    protected _currentQuestion: MultipleChoiceApiResponse;

    private readonly _answersContainer: HTMLDivElement;
    private _answerTemplateEngine: AnswerTemplate;
    private _answersList: HTMLUListElement;
    private _btnNewAnswer: HTMLAnchorElement;

    constructor(collectionId: Guid)
    {
        super({
            collectionId: collectionId,
            editorContainerSelector: elements.editorContainerClass,
        });


        this._answersContainer = this._selector.querySelector<HTMLDivElement>(elements.answersContainerClass);

        this._answersList = this._selector.querySelector<HTMLUListElement>(elements.answersListClass);
        this._btnNewAnswer = this._selector.querySelector<HTMLAnchorElement>(elements.newAnswerBtnClass);

        this._answerTemplateEngine = new AnswerTemplate();
    }

    public showQuestion(question: MultipleChoiceApiResponse)
    {
        this.clearAlertsContainer();
        this._currentQuestion = question;

        this._promptValue = question.prompt;

        const answersHtml = this._answerTemplateEngine.toHtmls(question.answers);
        this._answersList.innerHTML = answersHtml;
    }

    public control()
    {
        super.control();

        this._btnNewAnswer.addEventListener(NativeEvents.Click, async (e) =>
        {
            e.preventDefault();
            await this.createNewAnswer();
        });


        this._answersContainer.addEventListener(NativeEvents.Change, async (e) =>
        {
            const target = e.target as Element;

            const listItem = AnswerListItem.fromChild(target);

            if (listItem)
            {
                await this.updateListItem(listItem);
            }
        });

        this._answersContainer.addEventListener(NativeEvents.Click, async (e) =>
        {
            const target = e.target as Element;

            const btn = target.closest<HTMLButtonElement>(AnswerListItemElements.btnDeleteAnswerClass);

            if (btn)
            {
                const listItem = new AnswerListItem(btn);
                await this.deleteListItem(listItem);
            }
            

        });

    }


    protected async sendPutRequest(): Promise<ServiceResponse<MultipleChoiceApiResponse> | null>
    {
        const requestData = this.getPutRequestData();

        if (!requestData)
        {
            return null;
        }

        return await this._questionService.saveMultipleChoice(this._currentQuestion.id, requestData);
    }

    private getPutRequestData(): PutMultipleChoiceApiRequest | null
    {
        const requestData = this.getBasicPutRequestData<PutMultipleChoiceApiRequest>();

        return requestData;
    }


    private async updateListItem(listItem: AnswerListItem)
    {
        try
        {
            const data = listItem.toSaveAnswerApiRequestBody();

            const answerService = new AnswersService(this._currentQuestion.id);
            const response = await answerService.saveAnswer(listItem.answerId, data);

            if (!response.successful)
            {
                this.showErrorListAlert(response.response.errors);
                return;
            }
        }
        catch (error)
        {
            this.onAnswerServiceException(error);
        }
    }

    private async deleteListItem(listItem: AnswerListItem)
    {
        try
        {
            listItem.hide();

            const answerService = new AnswersService(this._currentQuestion.id);
            const response = await answerService.deleteAnswer(listItem.answerId);

            if (!response.successful)
            {
                this.showErrorListAlert(response.response.errors);
                listItem.show();
                return;
            }

            listItem.remove();

        }
        catch (error)
        {
            this.onAnswerServiceException(error);
            listItem.show();
        }
    }


    private onAnswerServiceException(error: Error)
    {
        ErrorUtility.onException(error, {
            onApiForbiddenException: (e) => this.showErrorAlert('You are not allowed to edit this answer.'),
            onApiNotFoundException: (e) => this.showErrorAlert('Answer not found.'),
            onApiValidationException: (e) => this.showErrorAlert('Validation error.'),
            onOther: (e) =>
            {
                console.error(e);
                this.showErrorAlert('Unexpected error. Please try again later.');
            },
        });
    }


    private async createNewAnswer()
    {
        const newAnswer = this.addNewAnswerElement();

        const listItem = AnswerListItem.fromId(newAnswer.id);

        if (listItem)
        {
            await this.updateListItem(listItem);
        }
    }


    private addNewAnswerElement(): AnswerApiResponse
    {
        const newAnswer: AnswerApiResponse = {
            answer: "",
            createdOn: "",
            id: NanoIdUtility.newAnswerId(),
            isCorrect: false,
            questionId: this._currentQuestion.id,
        };


        const html = this._answerTemplateEngine.toHtml(newAnswer);
        this._answersList.insertAdjacentHTML("beforeend", html);

        return newAnswer;
    }
}